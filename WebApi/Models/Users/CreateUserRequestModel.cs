using System.ComponentModel.DataAnnotations;

namespace WebApi.Models.Users;

public class CreateUserRequestModel
{
    [Required] [MaxLength(128)] public string Name { get; set; }

    [Required] public string UserName { get; set; }

    [Required] public string Email { get; set; }

    [Required] public string Password { get; set; }

    [Required]
    [RegularExpression("Webmaster|Moderator|Editor")]
    public string Role { get; set; }

    [Url] public string? ProfilePictureUrl { get; set; }
}