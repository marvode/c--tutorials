using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SampleSolution.Api.Contexts;
using SampleSolution.Api.Dtos;
using SampleSolution.Api.Models;
using SampleSolution.Api.Services;

namespace SampleSolution.Api.Controllers;

[ApiController]
[Route("auth")]
public class AuthController: ControllerBase
{
    private readonly UserManager<User> _userManager;
    private readonly AppDbContext _context;
    private readonly JwtService _jwtService;

    public AuthController(UserManager<User> userManager, AppDbContext context, JwtService jwtService)
    {
        _userManager = userManager;
        _context = context;
        _jwtService = jwtService;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterUserDto registerUserDto)
    {
        var user = new User
        {
            Name = registerUserDto.Name,
            Email = registerUserDto.Email,
            UserName = registerUserDto.Email,
        };

        var result = await _userManager.CreateAsync(user, registerUserDto.Password);
        if (!result.Succeeded)
        {
            return BadRequest(result.Errors);
        }
        
        return Ok();
    }
    
    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginUserDto loginUserDto)
    {
        var user = await _context.Users.FirstOrDefaultAsync(user => user.Email == loginUserDto.Email);

        if (user is null)
        {
            return BadRequest("email or password invalid");
        }

        // var isValidUser = user.PasswordHash == loginUserDto.Password;
        var isValidUser = await _userManager.CheckPasswordAsync(user, loginUserDto.Password);
        
        if (!isValidUser)
        {
            return BadRequest("email or password invalid");
        }

        var token = _jwtService.GenerateToken(user);
        
        return Ok(new
        {
            token = token
        });
    }
}