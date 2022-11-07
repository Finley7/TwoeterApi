using System.Text;
using Jose;
using Newtonsoft.Json;
using TwoeterApi.DTO;
using TwoeterApi.Exceptions;
using TwoeterApi.Model.Entity;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace TwoeterApi.Middleware;

public static class Authentication
{
    public static bool IsAuthenticated(HttpRequest request)
    {
        return request.Headers["Authorization"].ToString() != "";
    }

    public static int GetUser(HttpRequest request)
    {
        if (!IsAuthenticated(request))
        {
            throw new NotAuthenticatedException();
        }

        var jwtTokenEncoded = request.Headers["Authorization"].ToString();
        string jwtTokenDecoded;
        JWTToken token;
        
        try
        {
            jwtTokenDecoded = JWT.Decode(jwtTokenEncoded, Encoding.UTF8.GetBytes("mYq3t6v9y$B&E)H@McQfTjWnZr4u7x!z"));
            token = JsonSerializer.Deserialize<JWTToken>(jwtTokenDecoded);
        }
        catch (JsonException e)
        {
            throw new NotAuthenticatedException();
        }

        return token.user.id;

    }
}