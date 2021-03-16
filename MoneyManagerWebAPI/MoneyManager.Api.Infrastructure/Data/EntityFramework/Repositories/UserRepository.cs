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
using System.Threading.Tasks;

namespace MoneyManager.Api.Infrastructure.Data.EntityFramework.Repositories
{
    public class UserRepository : IUserRepositoryAsync
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

        public async Task<User> AuthenticateAsync(string username, string password)
        {
            var user = await _dbSet.SingleOrDefaultAsync(u => u.Username == username && u.Password == password);

            if (user == null)
            {
                return null;
            }

            user.Token = GetToken(user);

            return user;
        }

        public async Task<User> FindAsync(string username)
        {
            return await _dbSet.SingleOrDefaultAsync(u => u.Username == username);
        }

        public async Task<User> RegisterAsync(User user)
        {
            _dbSet.Add(user);
            await _context.SaveChangesAsync();

            return user;
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
