using System.ComponentModel.DataAnnotations;

namespace Slipp.Services.DTO;

public class LoginInput
{
    [EmailAddress] [Required] public string Email { get; set; }

    [DataType(DataType.Password)]
    [Required]
    public string Password { get; set; }
}