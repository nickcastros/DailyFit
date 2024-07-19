using Microsoft.AspNetCore.Identity;

namespace DailyFit.Models;

public class Usuario : IdentityUser
{
    public int? DadosPessoaisId { get; set; }
    public virtual DadosPessoais? DadosPessoais { get; set; }
    public Usuario() : base() {}
}