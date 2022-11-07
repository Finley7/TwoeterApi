using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TwoeterApi.DTO;
using TwoeterApi.Exceptions;
using TwoeterApi.Middleware;
using TwoeterApi.Model.Entity;
using TwoeterApi.Model.Repository.Interfaces;

namespace TwoeterApi.Controllers;

[ApiController]
[Route("api/account")]
public class AccountController : ControllerBase
{
   private readonly IUserRepository _userRepository;
   
   public AccountController(IUserRepository userRepository)
   {
      _userRepository = userRepository;
   }
   
   [HttpGet("/")]
   public ActionResult Get()
   {
      int userId;
      try
      {
         userId = Authentication.GetUser(Request);
      }
      catch (NotAuthenticatedException)
      {
         return StatusCode(400, new ErrorResponse()
         {
            Code = 400,
            Message = "Access denied"
         });
      }

      var user = _userRepository.FindBy<User>("Id", userId.ToString()).Include("Posts").Include("DeletedPosts").FirstOrDefault();

      return StatusCode(200, new Response<User>()
      {
         Code = 200,
         Message = "User fetched",
         Data = user
      });
   }
}