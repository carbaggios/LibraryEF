﻿using WebApi.Interfaces;
using Entity.Models;
using Microsoft.IdentityModel.Tokens;
using Microsoft.VisualBasic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace WebApi.Services
{
    public class TokenService : ITokenService
    {
        private readonly SymmetricSecurityKey _key;

        public TokenService(IConfiguration configuration)
        {
            var strKey = configuration["TokenKey"];
            var binKey = Encoding.UTF8.GetBytes(strKey);
            _key = new SymmetricSecurityKey(binKey);
        }

        public string GetToken(Librarian librarian)
        {
            var signature = new SigningCredentials(_key, SecurityAlgorithms.HmacSha512Signature);

            var claims = new List<Claim>()
            {
                new Claim(Microsoft.IdentityModel.JsonWebTokens.JwtRegisteredClaimNames.NameId, librarian.Login),

                //new Claim(ClaimTypes.Role, account.Role.ToString())
            };

            var descr = new SecurityTokenDescriptor
            {
                SigningCredentials = signature,
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(1),
            };

            var handler = new JwtSecurityTokenHandler();
            var token = handler.CreateToken(descr);
            return handler.WriteToken(token);
        }
    }
}
