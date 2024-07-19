using AutoMapper;
using DailyFit.Data.Dtos;
using DailyFit.Models;
using Microsoft.AspNetCore.Identity;

namespace DailyFit.Services;

public class UsuarioService
{
    private IMapper _mapper;
    private UserManager<Usuario> _userManager;
    private SignInManager<Usuario> _signInManager;
    private TokenService _tokenService;

    public UsuarioService(IMapper mapper, UserManager<Usuario> userManager, SignInManager<Usuario> signInManager, TokenService tokenService)
    {
        _mapper = mapper;
        _userManager = userManager;
        _signInManager = signInManager;
        _tokenService = tokenService;
    }

    public async Task Cadastra(CreateUsuarioDto usuarioDto)
    {
        Usuario usuario = _mapper.Map<Usuario>(usuarioDto);
        IdentityResult resultado = await _userManager.CreateAsync(usuario, usuarioDto.Password);
        if (!resultado.Succeeded)
        {
            throw new ApplicationException("Falha ao Cadastrar Usuário");
        }
    }

    public async Task<string> Login(LoginUsuarioDto dto)
    {
        var resultado = await _signInManager.PasswordSignInAsync(dto.Username, dto.Password, false, false);
        if (!resultado.Succeeded)
        {
            throw new ApplicationException("Falha ao Logar Usuário");
        }
        var usuario = _signInManager.UserManager.Users.FirstOrDefault(user => user.NormalizedUserName == dto.Username.ToUpper());
        var token = _tokenService.GenerateToken(usuario);
        return token;

    }
}