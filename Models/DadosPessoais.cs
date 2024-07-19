using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DailyFit.Models;

public class DadosPessoais
{
    [Key]
    [Required]
    public int Id { get; set; }
    public string Nome { get; set; }
    public string Altura { get; set; }
    public int Peso { get; set; }
    public string CPF { get; set; }
    public DateTime DataNascimento { get; set; }
    public string Email { get; set; }
    
    [Required]
    [ForeignKey("AspNetUsers")]
    public string usuarioId { get; set; }
    public virtual Usuario Usuario { get; set; }

}
