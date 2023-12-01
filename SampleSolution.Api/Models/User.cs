using Microsoft.AspNetCore.Identity;

namespace SampleSolution.Api.Models;

public class User: IdentityUser
{
    public string Name { get; set; }
}