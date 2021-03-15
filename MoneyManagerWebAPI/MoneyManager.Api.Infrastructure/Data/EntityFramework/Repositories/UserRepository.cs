using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using MoneyManager.Api.Core.Domain.Entities.Authentication;
using MoneyManager.Api.Core.Domain.Settings;
using MoneyManager.Api.Core.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace MoneyManager.Api.Infrastructure.Data.EntityFramework.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly DbSet<User> _dbSet;
        private readonly SecretSettings _secretSettings;

        public UserRepository(ApplicationDbContext context, IOptions<SecretSettings> secretSettings)
        {
            _context = context;
            _dbSet = _context.Users;
            _secretSettings = secretSettings.Value;
        }

        public User Authenticate(string username, string password)
        {
            var user = _dbSet.SingleOrDefault(u => u.Name == username && u.Password == password);

            if (user == null)
            {
                return null;
            }

            user.Token = GetToken(user);

            return user;
        }

        public User Find(string username)
        {
            return _dbSet.SingleOrDefault(u => u.Name == username);
        }

        public void Register(string username, string password, string role)
        {
            var user = new User 
            {
                Name = username,
                Password = password,
                Role = role
            };

            _dbSet.Add(user);
            _context.SaveChanges();
        }

        private string GetToken(User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_secretSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.Id.ToString()),
                    new Claim(ClaimTypes.Role, user.Role)
                }),
                Expires = DateTime.UtcNow.AddDays(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key),
                                                            SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }
    }
}
