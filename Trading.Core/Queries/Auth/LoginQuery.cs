using MediatR;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Trading.Core.Models.Request;
using Trading.Core.Models.Response;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace Trading.Core.Queries.Auth
{
    public class LoginQuery : AppRequest
    {
        public LoginQuery(string user, string password)
        {
            User = user;
            Password = password;
        }

        public string User { get; set; }
        public string Password { get; set; }
    }

    internal class LoginQueryHandler : IRequestHandler<LoginQuery, Response>
    {
        private readonly IConfiguration _configuration;

        public LoginQueryHandler(IConfiguration configuration)
        {
            _configuration = configuration ?? throw new ArgumentException(nameof(configuration));
        }

        public async Task<Response> Handle(LoginQuery request, CancellationToken cancellationToken)
        {
            if (request.User.ToLower() == "admin" && request.Password.ToLower() == "admin")
            {
                var claims = new[]
            {
                new Claim(ClaimTypes.Name, request.User),
            };

                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JwtSecurityKey"]));
                var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
                var expiry = DateTime.Now.AddMinutes(Convert.ToInt32(_configuration["JwtExpiryInMinutes"]));

                var token = new JwtSecurityToken(
                     _configuration["JwtIssuer"],
                     _configuration["JwtAudience"],
                     claims,
                     expires: expiry,
                     signingCredentials: creds
                 );

                return new Response.Successed<string>(new JwtSecurityTokenHandler().WriteToken(token));
            }
            else return new Response.Failed("ValidationError", "Username and password are invalid");
        }
    }
}
