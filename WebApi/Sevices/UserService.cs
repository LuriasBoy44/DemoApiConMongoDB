using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Request;
using WebApi.Response;
using WebApi.Common;
using Microsoft.Extensions.Options;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;

namespace WebApi.Sevices
{
    public class UserService : IUserService
    {
        private readonly AppSettings _appSettings;

        public UserService(IOptions<AppSettings> appSettings)
        {
            _appSettings = appSettings.Value;
        }
        public UserResponse Auth(AuthRequest model)
        {
            UserResponse userresponse = new UserResponse();
            Usuario user = new Usuario();

            // aqui deberia validar usurio contra BD
            user.Email = model.Email;
            user.Password = model.Password;
            user.Id = "001";
            //#############
            userresponse.Email = model.Email;
            userresponse.Token = GetJsonWebToken(user);
            return userresponse;
        }

        private string GetJsonWebToken(Usuario user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var llave = Encoding.ASCII.GetBytes(_appSettings.Secreto);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(
                    new Claim[]
                    {
                       new Claim(ClaimTypes.NameIdentifier,user.Id.ToString()),
                       new Claim(ClaimTypes.Email,user.Email.ToString())
                    }),
                Expires = DateTime.UtcNow.AddDays(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(llave), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
