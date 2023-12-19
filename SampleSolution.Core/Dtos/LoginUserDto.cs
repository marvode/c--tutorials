﻿using System.ComponentModel.DataAnnotations;

namespace SampleSolution.Core.Dtos;

public class LoginUserDto
{
    [Required] public string Email { get; set; }

    [Required] public string Password { get; set; }
}