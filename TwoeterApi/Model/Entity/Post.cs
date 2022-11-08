using System.ComponentModel.DataAnnotations.Schema;

namespace TwoeterApi.Model.Entity;

public class Post
{
    public Guid Id { get; set; }
    public string Body { get; set; }
    public DateTime Created { get; set; }
    public DateTime? Edited { get; set; }
    public DateTime? Deleted { get; set; }
    
    public Guid? DeletedById { get; set; }
    public Guid AuthorId { get; set; }
    
    public virtual User Author { get; set; }
    public virtual User? DeletedBy { get; set; }

    public Post()
    {
        Id = Guid.NewGuid();
        Created = DateTime.Now;
    }

    public void Delete(Guid userId)
    {
        Deleted = DateTime.Now;
        DeletedById = userId;
    }
}