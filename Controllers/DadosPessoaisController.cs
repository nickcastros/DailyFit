using System.Security.Claims;
using AutoMapper;
using DailyFit.Data;
using DailyFit.Data.Dtos;
using DailyFit.Models;
using DailyFit.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace DailyFit.Controllers;

[ApiController]
[Route("[controller]")]

public class DadosPessoaisController : ControllerBase
{
    private UsuarioContext _context;

    private DadosPessoaisService _dadosPessoaisService;

    public DadosPessoaisController(UsuarioContext context, DadosPessoaisService dadosPessoaisService)
    {
        _context = context;

        _dadosPessoaisService = dadosPessoaisService;
    }

    [HttpPost]
    [Authorize]
    public async Task<IActionResult> AdicionaDadosPessoaisAsync([FromBody] CreateDadosPessoaisDto dadosPessoaisDto)
    {
        string username = User.FindFirstValue("username");
        if (username == null)
        {
            return NotFound();
        }
        try
        {
            await _dadosPessoaisService.AdicionaDadosPessoais(dadosPessoaisDto, username);
            return Ok("Dados Pessoais Cadastrados!");
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }

    }

    [HttpGet("todos")]
    [Authorize]
    public IEnumerable<ReadDadosPessoaisDto> RecuperaDadosPessoaisDeTodos()
    {
        return _dadosPessoaisService.RecuperaDadosPessoaisDeTodos();
    }

    [HttpGet]
    [Authorize]
    public async Task<IActionResult> RecuperaDadosPessoaisAsync()
    {
        string username = User.FindFirstValue("username");
        if (username == null)
        {
            return NotFound();
        }
        try
        {
            return Ok(await _dadosPessoaisService.RecuperaDadosPessoais(username));
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
    /*
        [HttpPut("{id}")]
        public IActionResult AtualizaDadosPessoais(int id, [FromBody] UpdateDadosPessoaisDto dadosPessoaisDto)
        {
            var dadosPessoais = _context.DadosPessoais.FirstOrDefault(dadosPessoaisDto => dadosPessoaisDto.id == id);
            if (dadosPessoaisDto == null)
            {
                return NotFound();
            }
            _mapper.Map(dadosPessoaisDto, dadosPessoais);
            _context.SaveChanges();
            return NoContent();
        }
*/
    [HttpPatch]
    [Authorize]
    public async Task<IActionResult> AtualizaParcialmenteDadosPessoaisAsync(JsonPatchDocument<UpdateDadosPessoaisDto> patch)
    {
        if (patch == null)
        {
            return BadRequest("O documento de patch não pode ser nulo.");
        }
        string username = User.FindFirstValue("username");
        if (username == null)
        {
            return NotFound("Usuário não encontrado.");
        }

        UpdateDadosPessoaisDto dadosPessoaisParaAtualizar;

        try
        {
            dadosPessoaisParaAtualizar = _dadosPessoaisService.GetDadosPessoaisParaAtualizar(username);
        }
        catch (Exception ex)
        {
            return NotFound(ex.Message);
        }

        patch.ApplyTo(dadosPessoaisParaAtualizar, ModelState);

        if (!TryValidateModel(dadosPessoaisParaAtualizar))
        {
            return ValidationProblem(ModelState);
        }

        try
        {
            await _dadosPessoaisService.AtualizaParcialmenteDadosPessoais(dadosPessoaisParaAtualizar, patch);
            return NoContent();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpDelete]
    [Authorize]
    public async Task<IActionResult> DeletaDadosPessoaisAsync()
    {
        string username = User.FindFirstValue("username");
        if (username == null)
        {
            return NotFound();
        }
        try
        {
            await _dadosPessoaisService.DeletaDadosPessoais(username);
            return NoContent();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

}