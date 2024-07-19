using System.ComponentModel.DataAnnotations;

namespace DailyFit.Data.Dtos;

public class UpdateDadosPessoaisDto
{
    public int Id { get; set; }
    public string Nome { get; set; }
    public string Altura { get; set; }
    public int Peso { get; set; }
    public string CPF { get; set; }
    public string DataNascimento { get; set; }
    public string Email { get; set; }
}
