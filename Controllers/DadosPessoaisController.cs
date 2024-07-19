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
    private IMapper _mapper;
    private UsuarioService _usuarioService;

    private readonly UserManager<Usuario> _userManager;

    public DadosPessoaisController(UsuarioContext context, IMapper mapper, UsuarioService usuarioService, UserManager<Usuario> userManager)
    {
        _context = context;
        _mapper = mapper;
        _usuarioService = usuarioService;
        _userManager = userManager;
    }

    [HttpPost]
    [Authorize]
    public async Task<IActionResult> AdicionaDadosPessoaisAsync([FromBody] CreateDadosPessoaisDto dadosPessoaisDto)
    {
        DadosPessoais dadosPessoais = _mapper.Map<DadosPessoais>(dadosPessoaisDto);

        string username = User.FindFirstValue("username");
        if (username == null)
        {
            return NotFound();
        }

        var user = _context.Users.FirstOrDefault(user => user.NormalizedUserName == username.ToUpper());

        if (user == null)
        {
            return NotFound();

        }
        dadosPessoais.usuarioId = user.Id;
        dadosPessoais.Usuario = user;

        _context.DadosPessoais.Add(dadosPessoais);
        await _context.SaveChangesAsync();

        user.DadosPessoaisId = dadosPessoais.Id;
        await _userManager.UpdateAsync(user);

        var usuarioDto = _mapper.Map<ReadUsuarioDto>(user);
        return CreatedAtAction(nameof(RecuperaDadosPessoais), RecuperaDadosPessoais());
        //return CreatedAtAction(nameof(RecuperaDadosPessoais), new { id = dadosPessoais.id });

    }

    [HttpGet("todos")]
    [Authorize]
    public IEnumerable<ReadDadosPessoaisDto> RecuperaDadosPessoaisDeTodos()
    {
        return _mapper.Map<List<ReadDadosPessoaisDto>>(_context.DadosPessoais.ToList());
    }

    [HttpGet]
    [Authorize]
    public IActionResult RecuperaDadosPessoais()
    {
        string username = User.FindFirstValue("username");
        if (username == null)
        {
            return NotFound();
        }

        var user = _context.Users.FirstOrDefault(user => user.NormalizedUserName == username.ToUpper());
        if (user == null)
        {
            return NotFound();
        }

        ReadDadosPessoaisDto dadosPessoais = _mapper.Map<ReadDadosPessoaisDto>(user.DadosPessoais);
        if (dadosPessoais != null)
        {
            return Ok(dadosPessoais);
        }
        return NotFound();
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
    public IActionResult AtualizaParcialmenteDadosPessoais(JsonPatchDocument<UpdateDadosPessoaisDto> patch)
    {
        if (patch == null)
        {
            return BadRequest("O documento de patch não pode ser nulo.");
        }

        string username = User.FindFirstValue("username");
        if (username == null)
        {
            return NotFound();
        }

        var user = _context.Users.FirstOrDefault(user => user.NormalizedUserName == username.ToUpper());
        if (user == null)
        {
            return NotFound();
        }

        var dadosPessoais = _context.DadosPessoais.FirstOrDefault(dp => dp.Id == user.DadosPessoaisId);
        if (dadosPessoais == null)
        {
            return NotFound();
        }
        var dadosPessoaisParaAtualizar = _mapper.Map<UpdateDadosPessoaisDto>(dadosPessoais);

        patch.ApplyTo(dadosPessoaisParaAtualizar, ModelState);
        if (!TryValidateModel(dadosPessoaisParaAtualizar))
        {
            return ValidationProblem(ModelState);
        }
        _mapper.Map(dadosPessoaisParaAtualizar, dadosPessoais);
        _context.SaveChanges();

        return NoContent();
    }

    [HttpDelete]
    [Authorize]
    public IActionResult DeletaDadosPessoais()
    {
        string username = User.FindFirstValue("username");
        if (username == null)
        {
            return NotFound();
        }

        var user = _context.Users.FirstOrDefault(user => user.NormalizedUserName == username.ToUpper());
        if (user == null)
        {
            return NotFound();
        }
        var dadosPessoais = _context.DadosPessoais.FirstOrDefault(dadosPessoais => dadosPessoais.Id == user.DadosPessoaisId);
        if (dadosPessoais == null)
        {
            return NotFound();
        }
        _context.DadosPessoais.Remove(dadosPessoais);
        _context.SaveChanges();
        return NoContent();
    }

}