using TwoeterApi.Model.Entity;

namespace TwoeterApi.Model.Repository.Interfaces;

public interface IUserRepository : IRepository
{
    public User? CheckUsername(string username);
    public User? CheckEmail(string email);
    public User Login(string email, string password);
}