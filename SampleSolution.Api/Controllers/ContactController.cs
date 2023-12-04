using Microsoft.AspNetCore.Mvc;

namespace SampleSolution.Api.Controllers;

[ApiController]
[Route("api/contacts")]
public class ContactController : ControllerBase
{
    [HttpGet]
    public IActionResult GetAllContact()
    {
        return Ok();
    }
    
    [HttpGet("{id}")]
    public IActionResult GetSingleContact(string id)
    {
        return Ok();
    }
    
    [HttpPost]
    public IActionResult CreateContact([FromBody] object contactDto)
    {
        return Ok();
    }
    
    [HttpPut("{id}")]
    public IActionResult UpdateSingleContact(string id, [FromBody] object updatedContactDto)
    {
        return Ok();
    }
    
    [HttpDelete("{id}")]
    public IActionResult DeleteContact(string id)
    {
        return Ok();
    }
}