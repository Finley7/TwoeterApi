using System.Linq.Dynamic.Core;
using Microsoft.EntityFrameworkCore;
using TwoeterApi.Model.Repository.Interfaces;

namespace TwoeterApi.Model.Repository
{
    public class Repository : IRepository
    {
        public static readonly TwoeterContext Db = new();
        public void Create<T>(T entity) where T : class
        {
            Db.Set<T>().Add(entity);
            Db.SaveChanges();
        }
        public void Update<T>(T entity) where T : class
        {
            Db.Set<T>().Update(entity);
            Db.SaveChanges();
        }
        public void Delete<T>(T entity) where T : class
        {
            Db.Set<T>().Remove(entity);
            Db.SaveChanges();
        }
        public List<T> FindAll<T>() where T : class
        {
            return Db.Set<T>().ToList();
        }
        public IQueryable<T> FindBy<T>(string key, string value, string op = "=") where T : class
        {
            return Db.Set<T>().Where($"{key} {op} \"{value}\"");
        }
    }
}
