using System.ComponentModel.DataAnnotations;

namespace DailyFit.Data.Dtos;

public class UpdateUsuarioDto
{
    [Required(ErrorMessage = "O campo usuario é obrigatório")]
    public string Username { get; set; }

    [Required(ErrorMessage = "O campo senha é obrigatório")]
    [DataType(DataType.Password)]
    public string Password { get; set; }
    
    [Required]
    [DataType(DataType.Password)]
    [Compare("Password", ErrorMessage = "As senhas não conferem")]
    public string RePassword { get; set; }
}
