using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using UserService.Database;
using UserService.Logic.Models;


namespace UserService.Logic
{
    public class UserLogic : IUserLogic
    {
        private readonly IUserDomain domain;

        public UserLogic(IUserDomain domain)
        {
            this.domain = domain;
        }

        public string Auth(User user)
        {
            var dbuser = domain.GetUsers(user.Email, user.Password);
            if (dbuser == null)
            {
                return null;
            }

            var claims = new List<Claim>
                {
                    new Claim(ClaimsIdentity.DefaultNameClaimType, dbuser.Email),
                    new Claim(ClaimsIdentity.DefaultRoleClaimType, "Author")
                };
            ClaimsIdentity claimsIdentity =
            new ClaimsIdentity(claims, "Token", ClaimsIdentity.DefaultNameClaimType,
                ClaimsIdentity.DefaultRoleClaimType);

            var token = new JwtSecurityToken(
                    issuer: AuthOptions.ISSUER,
                    audience: AuthOptions.AUDIENCE,
                    claims: claimsIdentity.Claims,
                    expires: DateTime.UtcNow.Add(TimeSpan.FromMinutes(1440)),
                    signingCredentials: new SigningCredentials(AuthOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256));

            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(token);

            return encodedJwt;
        }

        public bool Register(User user)
        {
            return domain.Registration(new Database.Models.User
            {
                Id = user.Id,
                Surname = user.Surname,
                Name = user.Name,
                Patronymic = user.Patronymic,
                Password = user.Password,
                Email = user.Email,
                UserType = user.UserType,
                Phone = user.Phone,
                Adress = user.Adress,
                Snils = user.Snils,
                Birthday = user.Birthday
            });
        }

        private JwtSecurityToken CreateToken(List<Claim> claims)
        {
            return new JwtSecurityToken(
                    issuer: AuthOptions.ISSUER,
                    audience: AuthOptions.AUDIENCE,
                    claims: claims,
                    expires: DateTime.UtcNow.Add(TimeSpan.FromMinutes(1440)),
                    signingCredentials: new SigningCredentials(AuthOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256));
        }
    }
}
