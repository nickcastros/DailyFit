using System.ComponentModel.DataAnnotations;

namespace DailyFit.Data.Dtos;

public class CreateDadosPessoaisDto
{
    public string Nome { get; set; }
    public int Altura { get; set; }
    public int Peso { get; set; }
    public string CPF { get; set; }
    public string DataNascimento { get; set; }
    public string Email { get; set; }

}
