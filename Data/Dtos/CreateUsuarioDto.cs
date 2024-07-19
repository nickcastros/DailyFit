using System.ComponentModel.DataAnnotations;

namespace DailyFit.Data.Dtos;

public class CreateUsuarioDto
{
    [Required(ErrorMessage = "O campo Username é obrigatório")]
    public string Username { get; set; }
    [Required(ErrorMessage = "O campo Senha é obrigatório")]
    [DataType(DataType.Password)]
    public string Password { get; set; }
    [Required]
    [Compare("Password", ErrorMessage = "As senhas não conferem")]
    public string RePassword { get; set; }
}
