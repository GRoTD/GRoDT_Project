using System.ComponentModel.DataAnnotations;

namespace Slipp.Services.DTO;

public class CreateAppUserInput
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    [DataType(DataType.Password)] public string Password { get; set; }
    [EmailAddress] public string Email { get; set; }
}