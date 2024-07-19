using AutoMapper;
using DailyFit.Data;
using DailyFit.Data.Dtos;
using DailyFit.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace DailyFit.Services;

public class DadosPessoaisService
{
    private UsuarioContext _context;
    private IMapper _mapper;
    private UserManager<Usuario> _userManager;
    private SignInManager<Usuario> _signInManager;


    public DadosPessoaisService(UsuarioContext context, IMapper mapper, UserManager<Usuario> userManager, SignInManager<Usuario> signInManager)
    {
        _context = context;
        _mapper = mapper;
        _userManager = userManager;
        _signInManager = signInManager;
    }

    public async Task AdicionaDadosPessoais(CreateDadosPessoaisDto dadosPessoaisDto, string username)
    {

        var user = _context.Users.FirstOrDefault(user => user.NormalizedUserName == username.ToUpper());
        if (user == null)
        {
            throw new ApplicationException("Falha ao Cadastrar Usuário");
        }

        DadosPessoais dadosPessoais = _mapper.Map<DadosPessoais>(dadosPessoaisDto);
        dadosPessoais.usuarioId = user.Id;
        dadosPessoais.Usuario = user;

        _context.DadosPessoais.Add(dadosPessoais);
        _context.SaveChanges();

        user.DadosPessoaisId = dadosPessoais.Id;
        await _userManager.UpdateAsync(user);

    }

    public IEnumerable<ReadDadosPessoaisDto> RecuperaDadosPessoaisDeTodos()
    {
        return _mapper.Map<List<ReadDadosPessoaisDto>>(_context.DadosPessoais.ToList());
    }

    public async Task<ReadDadosPessoaisDto> RecuperaDadosPessoais(string username)
    {

        var user = _context.Users.FirstOrDefault(user => user.NormalizedUserName == username.ToUpper());
        if (user == null)
        {
            throw new ApplicationException("Falha ao procurar Usuário");
        }

        ReadDadosPessoaisDto dadosPessoais = _mapper.Map<ReadDadosPessoaisDto>(user.DadosPessoais);
        if (dadosPessoais == null)
        {
            throw new ApplicationException("Falha ao procurar Dados Pessoais");
        }
        return dadosPessoais;
    }

    public UpdateDadosPessoaisDto GetDadosPessoaisParaAtualizar(string username)
    {
        var user = _context.Users.FirstOrDefault(u => u.NormalizedUserName == username.ToUpper());
        if (user == null) throw new Exception("Usuário não encontrado");

        var dadosPessoais = _context.DadosPessoais.FirstOrDefault(dp => dp.Id == user.DadosPessoaisId);
        if (dadosPessoais == null) throw new Exception("Dados pessoais não encontrados");

        return _mapper.Map<UpdateDadosPessoaisDto>(dadosPessoais);
    }

    public async Task AtualizaParcialmenteDadosPessoais(UpdateDadosPessoaisDto dadosPessoaisParaAtualizar, JsonPatchDocument<UpdateDadosPessoaisDto> patch)
    {
        patch.ApplyTo(dadosPessoaisParaAtualizar);

        var dadosPessoais = _context.DadosPessoais.FirstOrDefault(dp => dp.Id == dadosPessoaisParaAtualizar.Id);
        if (dadosPessoais == null)
        {
            throw new ApplicationException("Dados pessoais não encontrados");
        }

        _mapper.Map(dadosPessoaisParaAtualizar, dadosPessoais);
        await _context.SaveChangesAsync();
    }

    public async Task DeletaDadosPessoais(string username)
    {

        var user = _context.Users.FirstOrDefault(user => user.NormalizedUserName == username.ToUpper());
        if (user == null)
        {
            throw new ApplicationException("Usuário não encontrado");
        }
        var dadosPessoais = _context.DadosPessoais.FirstOrDefault(dadosPessoais => dadosPessoais.Id == user.DadosPessoaisId);
        if (dadosPessoais == null)
        {
            throw new ApplicationException("Dados pessoais não encontrados");
        }
        _context.DadosPessoais.Remove(dadosPessoais);
        await _context.SaveChangesAsync();
    }

}