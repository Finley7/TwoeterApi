using System;
using TwoeterApi.Model.Entity;

namespace TwoeterApi.Model.Repository.Interfaces
{
    public interface IPostRepository : IRepository
    {
        public List<Post> GetFeed(Guid userId);
    }
}

