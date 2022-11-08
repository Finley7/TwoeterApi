using System;
using Microsoft.EntityFrameworkCore;
using TwoeterApi.Model.Entity;
using TwoeterApi.Model.Repository.Interfaces;

namespace TwoeterApi.Model.Repository
{
    public class PostRepository : Repository, IPostRepository
    {
        public List<Post> GetFeed(Guid userId)
        {
            var posts = from post in Db.Posts.Include("Author")
                join follow in Db.UserFollow on post.AuthorId equals follow.FollowingId
                where follow.FollowerId == userId
                where post.Deleted == null
                orderby post.Created
                select(post);

            return posts.ToList();
        }
    }
}

