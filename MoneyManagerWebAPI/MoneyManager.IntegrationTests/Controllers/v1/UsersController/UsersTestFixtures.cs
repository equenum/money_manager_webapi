using MoneyManager.Api.Core.Domain.Entities.Authentication;
using MoneyManager.Api.Infrastructure.Data.EntityFramework;
using System;
using System.Collections.Generic;
using System.Text;

namespace MoneyManager.IntegrationTests.Controllers.v1.UsersController
{
    public class UsersTestFixtures : IDisposable
    {
        private readonly ApplicationDbContext _context;

        public int TestUserId { get; set; }
        public string TestUserUsername { get; set; }
        public string TestUserPassword { get; set; }
        public string TestUserRole { get; set; }
        public int TestUserToBeRemovedId { get; set; } = 0; 

        public UsersTestFixtures()
        {
            _context = new ApplicationDbContext();

            CreateTestUser();
        }

        public void Dispose()
        {
            DeleteTestUser(TestUserId);

            if (TestUserToBeRemovedId != 0)
            {
                DeleteTestUser(TestUserToBeRemovedId);
            }
        }

        private void CreateTestUser()
        {
            var user = new User()
            {
                Username = "UserTestUser",
                Password = "UserTestUser",
                Role = "ADMIN"
            };

            _context.Users.Add(user);
            _context.SaveChanges();

            TestUserId = user.Id;
            TestUserUsername = user.Username;
            TestUserPassword = user.Password;
            TestUserRole = user.Role;
        }

        private void DeleteTestUser(int id)
        {
            var user = _context.Users.Find(id);

            if (user != null)
            {
                _context.Users.Remove(user);
                _context.SaveChanges();
            }
        }
    }
}
