﻿using Microsoft.AspNetCore.Identity;
using SampleSolution.Core.Abstractions;
using SampleSolution.Core.Dtos;
using SampleSolution.Domain.Constants;
using SampleSolution.Domain.Entities;

namespace SampleSolution.Core.Services;

public class AuthService : IAuthService
{
    private readonly IJwtService _jwtService;
    private readonly UserManager<User> _userManager;

    public AuthService(UserManager<User> userManager, IJwtService jwtService)
    {
        _userManager = userManager;
        _jwtService = jwtService;
    }

    public async Task<Result> Register(RegisterUserDto registerUserDto)
    {
        var user = User.Create(registerUserDto.Name, registerUserDto.Email);

        var result = await _userManager.CreateAsync(user, registerUserDto.Password);
        if (!result.Succeeded)
            return result.Errors.Select(error => new Error(error.Code, error.Description)).ToArray();

        result = await _userManager.AddToRoleAsync(user, RolesConstant.User);
        if (!result.Succeeded)
            return result.Errors.Select(error => new Error(error.Code, error.Description)).ToArray();

        return Result.Success();
    }

    public async Task<Result<LoginResponseDto>> Login(LoginUserDto loginUserDto)
    {
        var user = await _userManager.FindByEmailAsync(loginUserDto.Email);

        if (user is null) return new Error[] { new("Auth.Error", "email or password not correct") };

        var isValidUser = await _userManager.CheckPasswordAsync(user, loginUserDto.Password);

        if (!isValidUser) return new Error[] { new("Auth.Error", "email or password not correct") };

        var token = _jwtService.GenerateToken(user);

        return new LoginResponseDto(token);
    }
}