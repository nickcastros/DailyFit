using System.ComponentModel.DataAnnotations;

namespace DailyFit.Models;

public class Usuario
{
    [Key]
    [Required]
    public int Id { get; set; }
    [Required(ErrorMessage = "O campo nome é obrigatório")]
    public string Nome { get; set; }
    [Required(ErrorMessage = "O campo email é obrigatório")]
    public string Email { get; set; }
    [Required(ErrorMessage = "O campo senha é obrigatório")]
    public string Senha { get; set; }
}