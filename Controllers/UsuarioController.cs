using AutoMapper;
using DailyFit.Data;
using DailyFit.Data.Dtos;
using DailyFit.Models;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace DailyFit.Controllers;

[ApiController]
[Route("[controller]")]

public class UsuarioController : ControllerBase{
    private DailyFitContext _context;
    private IMapper _mapper;
    public UsuarioController(DailyFitContext context, IMapper mapper){
        _context = context;
        _mapper = mapper;
    }

    [HttpPost]

    public IActionResult AdicionaUsuario([FromBody] CreateUsuarioDto usuarioDto){
        Usuario usuario = _mapper.Map<Usuario>(usuarioDto);

        _context.Usuarios.Add(usuario);
        _context.SaveChanges();
        return CreatedAtAction(nameof(RecuperaUsuarioPorId), new {Id = usuario.Id}, usuario);
    }

    [HttpGet]
    public IEnumerable<ReadUsuarioDto> RecuperaUsuario(){
        return _mapper.Map<List<ReadUsuarioDto>>(_context.Usuarios.ToList());
    }

    [HttpGet("{id}")]
    public IActionResult RecuperaUsuarioPorId(int id){
        var usuario = _context.Usuarios.FirstOrDefault(usuario => usuario.Id == id);
        if (usuario != null){
            ReadUsuarioDto usuarioDto = _mapper.Map<ReadUsuarioDto>(usuario);
            return Ok(usuarioDto);
        }
        return NotFound();
    }

    [HttpPut("{id}")]
    public IActionResult AtualizaUsuario(int id, [FromBody] UpdateUsuarioDto usuarioDto){
        var usuario = _context.Usuarios.FirstOrDefault(usuario => usuario.Id == id);
        if (usuario == null){
            return NotFound();
        }
        _mapper.Map(usuarioDto, usuario);
        _context.SaveChanges();
        return NoContent();
    }

    [HttpPatch("{id}")]

    public IActionResult AtualizaParcialmenteUsuario(int id, JsonPatchDocument<UpdateUsuarioDto> patch){
        var usuario = _context.Usuarios.FirstOrDefault(usuario => usuario.Id == id);
        if (usuario == null){
            return NotFound();
        }
        var usuarioParaAtualizar = _mapper.Map<UpdateUsuarioDto>(usuario);
        patch.ApplyTo(usuarioParaAtualizar, ModelState);
        if(!TryValidateModel(usuarioParaAtualizar)){
            return ValidationProblem(ModelState);
        }
        _mapper.Map(usuarioParaAtualizar, usuario);
        _context.SaveChanges();

        return NoContent();
    }

    [HttpDelete("{id}")]

    public IActionResult DeletaUsuario(int id){
        var usuario = _context.Usuarios.FirstOrDefault(usuario => usuario.Id == id);
        if (usuario == null){
            return NotFound();
        }
        _context.Usuarios.Remove(usuario);
        _context.SaveChanges();
        return NoContent();
    }
}