using Newtonsoft.Json;
using TwoeterApi.Model.Repository;
using TwoeterApi.Model.Repository.Interfaces;

namespace TwoeterApi.Model.Entity;

public class User
{
    public Guid Id { get; set; }
    public string Username { get; set; }

    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string Email { get; set; }
    [JsonIgnore]
    public string Password { get; set; }
    public DateTime Created { get; set; }
    public DateTime? LastLogin { get; set; }
    
    public DateTime? TokenCreated { get; set; }
    
    public ICollection<Post>? Posts { get; set; }
    
    public ICollection<Post> DeletedPosts { get; set; }

    public ICollection<UserFollow> Followers { get; set; }

    public ICollection<UserFollow> Following { get; set; }

    public User()
    {
        Id = Guid.NewGuid();
        Created = DateTime.Now;
    }

}

