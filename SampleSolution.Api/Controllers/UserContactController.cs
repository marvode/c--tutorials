using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SampleSolution.Api.Contexts;

namespace SampleSolution.Api.Controllers;

[Authorize]
[ApiController]
[Route("api/users/{userId}/contacts")]
public class UserContactController: ControllerBase
{
    [HttpGet]
    public IActionResult GetAllUserContact(string userId)
    {
        return Ok();
    }
    
    [HttpGet("{id}")]
    public IActionResult GetUserContactById(string userId, string id)
    {
        return Ok();
    }
    
    [HttpPost]
    public IActionResult CreateContact(string userId, [FromBody] object contactDto)
    {
        return Ok();
    }
    
    [HttpPut("{id}")]
    public IActionResult UpdateSingleContact(string userId, string id, [FromBody] object updatedContactDto)
    {
        return Ok();
    }
    
    [HttpDelete("{id}")]
    public IActionResult DeleteContact(string userId, string id)
    {
        return Ok();
    }
}