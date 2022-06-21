using Denex.Application.Interfaces.Service;
using Denex.Persistance.Settings;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Denex.Persistance.Service
{
    public class JwtService :IJwtService
    {
        private readonly JwtSettings jwtSettings;
        public JwtService(IOptions<JwtSettings> jwtSettings)
        {
            this.jwtSettings = jwtSettings.Value;
        }
        public String CreateToken(string userId)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.Key));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var claims = new[] {
                new Claim(ClaimTypes.Name, Convert.ToString(userId)),
                };
            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddDays(jwtSettings.ValidateLifetime),
                signingCredentials: credentials
                );

            string tokenStr = new JwtSecurityTokenHandler().WriteToken(token);
            return tokenStr;
        }
    }
}
