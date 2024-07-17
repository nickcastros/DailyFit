using System.ComponentModel.DataAnnotations;

namespace DailyFit.Data.Dtos;

public class UpdateUsuarioDto
{
    [Required(ErrorMessage = "O campo nome é obrigatório")]
    public string Nome { get; set; }
    [Required(ErrorMessage = "O campo email é obrigatório")]
    public string Email { get; set; }
    [Required(ErrorMessage = "O campo senha é obrigatório")]
    public string Senha { get; set; }
}
