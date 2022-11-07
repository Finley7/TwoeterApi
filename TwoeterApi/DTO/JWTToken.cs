using TwoeterApi.Model.Entity;

namespace TwoeterApi.DTO;

public class JWTToken
{
    public UserJwtToken user { get; set; }
}

public class UserJwtToken
{
    public int id { get; set; }
}