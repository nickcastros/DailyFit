using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using DailyFit.Models;
using Microsoft.IdentityModel.Tokens;

namespace DailyFit.Services
{
    public class TokenService
    {
        public string GenerateToken(Usuario usuario)
        {
            Claim[] claims = new Claim[]{
                new Claim("username", usuario.UserName),
                new Claim("id", usuario.Id.ToString()),
            };

            var chave = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("chave-api-dailyfit-1234567890-ABCDEFGH"));

            var signingCredentials = new SigningCredentials(chave, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                expires: DateTime.Now.AddHours(8),
                claims: claims,
                signingCredentials: signingCredentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
            
        }


    }
}
