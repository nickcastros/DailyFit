using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;
namespace DailyFit.Authorization;

public class IdadeAuthorization : AuthorizationHandler<IdadeMinima>
{
    private readonly ILogger<IdadeAuthorization> _logger;

    public IdadeAuthorization(ILogger<IdadeAuthorization> logger)
    {
        _logger = logger;
    }


    protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, IdadeMinima requirement)
    {
        foreach (var claim in context.User.Claims)
        {
            _logger.LogInformation("Claim Type: {Type}, Claim Value: {Value}", claim.Type, claim.Value);
        }


        var dataNascimentoClaim = context.User.FindFirst(claim => claim.Type == ClaimTypes.DateOfBirth);

        if (dataNascimentoClaim is null)
        {
            _logger.LogWarning("Claim de data de nascimento não encontrado.");
            return Task.CompletedTask;
        }

        if (!DateTime.TryParse(dataNascimentoClaim.Value, out var dataNascimento))
        {
            _logger.LogWarning("Falha ao converter a data de nascimento: {DataNascimento}", dataNascimentoClaim.Value);
            return Task.CompletedTask;
        }

        var idade = DateTime.Today.Year - dataNascimento.Year;

        if (dataNascimento > DateTime.Today.AddYears(-idade)) idade--;

        _logger.LogInformation("Data de nascimento: {DataNascimento}, Idade calculada: {Idade}", dataNascimento, idade);


        // Verificar se a idade do usuário é suficiente para atender ao requisito
        if (idade >= requirement._idade)
        {
            _logger.LogInformation("Requisito de idade atendido. Idade mínima: {IdadeMinima}, Idade do usuário: {Idade}", requirement._idade, idade);
            context.Succeed(requirement);
        }
        else
        {
            _logger.LogWarning("Requisito de idade não atendido. Idade mínima: {IdadeMinima}, Idade do usuário: {Idade}", requirement._idade, idade);
        }
        return Task.CompletedTask;
    }
}