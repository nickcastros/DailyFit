using DailyFit.Models;

namespace DailyFit.Data.Dtos;

public class ReadDadosPessoaisDto
{
    public string Nome { get; set; }
    public string Altura { get; set; }
    public int Peso { get; set; }
    public string CPF { get; set; }
    public string DataNascimento { get; set; }
    public string Email { get; set; }

}