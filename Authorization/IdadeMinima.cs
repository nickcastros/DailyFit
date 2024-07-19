using Microsoft.AspNetCore.Authorization;

namespace DailyFit.Authorization;

public class IdadeMinima : IAuthorizationRequirement
{
    public int _idade { get; set; }

    public IdadeMinima(int idade)
    {
        _idade = idade;
    }
}