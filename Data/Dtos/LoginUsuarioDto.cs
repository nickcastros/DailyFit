using System.ComponentModel.DataAnnotations;

namespace DailyFit.Data.Dtos;

public class LoginUsuarioDto{
    [Required]
    public string Username { get; set; }
    [Required]
    public string Password { get; set; }
}