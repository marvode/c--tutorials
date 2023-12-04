namespace SampleSolution.Api.Models;

public class Contact
{
    public string Id { get; set; }
    public string Name { get; set; }
    public string UserId { get; set; }
    public User User { get; set; }
}