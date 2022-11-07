using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using TwoeterApi.Exceptions;
using TwoeterApi.Model.Entity;
using TwoeterApi.Model.Repository.Interfaces;
using System.Linq.Dynamic.Core;

namespace TwoeterApi.Model.Repository
{
    public class UserRepository : Repository, IUserRepository
    {
        public User? CheckUsername(string username)
        {
            return Db.Users!.FirstOrDefault(x => x.Username == username);
        }

        public User? CheckEmail(string email)
        {
            return Db.Users!.FirstOrDefault(x => x.Email == email);
        }

        public User Login(string username, string password)
        {
            var user = Db.Users!.FirstOrDefault(x => x.Username == username);

            if (user == null)
            {
                throw new UserNotFoundException();
            }

            if (!BCrypt.Net.BCrypt.Verify(password, user.Password))
            {
                throw new UserNotFoundException();
            }

            return user;
        }
        
    }
}
