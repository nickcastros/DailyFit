using AutoMapper;
using DailyFit.Data;
using DailyFit.Data.Dtos;
using DailyFit.Models;
using DailyFit.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace DailyFit.Controllers;

[ApiController]
[Route("[controller]")]

public class UsuarioController : ControllerBase
{

    private UsuarioService _usuarioService;

    public UsuarioController(UsuarioService usuarioService)
    {
      _usuarioService = usuarioService;
    }

    [HttpPost("cadastro")]
    public async Task<IActionResult> AdicionaUsuario(CreateUsuarioDto usuarioDto)
    {
        await _usuarioService.Cadastra(usuarioDto);
        return Ok("Usuário Cadastrado");
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginUsuarioDto dto){
        var token = await _usuarioService.Login(dto);
        return Ok(token);
    }
/*
    [HttpGet]
    public IEnumerable<ReadUsuarioDto> RecuperaUsuario()
    {
        return _mapper.Map<List<ReadUsuarioDto>>(_context.Users.ToList());
    }

    [HttpGet("{id}")]
    public IActionResult RecuperaUsuarioPorId(string id)
    {
        var usuario = _context.Users.FirstOrDefault(usuario => usuario.Id == id);
        if (usuario != null)
        {
            ReadUsuarioDto usuarioDto = _mapper.Map<ReadUsuarioDto>(usuario);
            return Ok(usuarioDto);
        }
        return NotFound();
    }

    [HttpPut("{id}")]
    public IActionResult AtualizaUsuario(string id, [FromBody] UpdateUsuarioDto usuarioDto)
    {
        var usuario = _context.Users.FirstOrDefault(usuario => usuario.Id == id);
        if (usuario == null)
        {
            return NotFound();
        }
        _mapper.Map(usuarioDto, usuario);
        _context.SaveChanges();
        return NoContent();
    }

    [HttpPatch("{id}")]

    public IActionResult AtualizaParcialmenteUsuario(string id, JsonPatchDocument<UpdateUsuarioDto> patch)
    {
        var usuario = _context.Users.FirstOrDefault(usuario => usuario.Id == id);
        if (usuario == null)
        {
            return NotFound();
        }
        var usuarioParaAtualizar = _mapper.Map<UpdateUsuarioDto>(usuario);
        patch.ApplyTo(usuarioParaAtualizar, ModelState);
        if (!TryValidateModel(usuarioParaAtualizar))
        {
            return ValidationProblem(ModelState);
        }
        _mapper.Map(usuarioParaAtualizar, usuario);
        _context.SaveChanges();

        return NoContent();
    }

    [HttpDelete("{id}")]

    public IActionResult DeletaUsuario(string id)
    {
        var usuario = _context.Users.FirstOrDefault(usuario => usuario.Id == id);
        if (usuario == null)
        {
            return NotFound();
        }
        _context.Users.Remove(usuario);
        _context.SaveChanges();
        return NoContent();
    }
    */
}