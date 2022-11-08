using Jose;
using TwoeterApi.Model.Entity;

namespace TwoeterApi.DTO;

public class JWTToken
{
    public UserJwtToken user { get; set; }
    public DateTime created { get; set; }
}

public class UserJwtToken
{
    public Guid id { get; set; }
    
    public string username { get; set; }
}