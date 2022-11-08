using System;
namespace TwoeterApi.Model.Entity
{
    public class UserFollow
    {
        public int Id { get; set; }
        public DateTime Created { get; set; }
        public Guid FollowerId { get; set; }
        public Guid FollowingId { get; set; }

        public virtual User Follower { get; set; }
        public virtual User Following { get; set; }

        public UserFollow()
        {
            Created = DateTime.Now;
        }
    }
}

