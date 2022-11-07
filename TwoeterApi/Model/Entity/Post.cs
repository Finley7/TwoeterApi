using System.ComponentModel.DataAnnotations.Schema;

namespace TwoeterApi.Model.Entity;

public class Post
{
    public int Id { get; set; }
    public string Body { get; set; }
    public DateTime Created { get; set; }
    public DateTime? Edited { get; set; }
    public DateTime? Deleted { get; set; }
    
    public int? DeletedById { get; set; }
    public int AuthorId { get; set; }
    
    public virtual User Author { get; set; }
    public virtual User? DeletedBy { get; set; }

    public Post()
    {
        Created = DateTime.Now;
    }

    public void Delete(User user)
    {
        Deleted = DateTime.Now;
        DeletedBy = user;
    }
}