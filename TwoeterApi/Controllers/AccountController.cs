using System.Linq.Dynamic.Core;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TwoeterApi.DTO;
using TwoeterApi.Exceptions;
using TwoeterApi.Middleware;
using TwoeterApi.Model.Entity;
using TwoeterApi.Model.Repository.Interfaces;

namespace TwoeterApi.Controllers;

[ApiController]
public class AccountController : ApiController
{
   private readonly IUserRepository _userRepository;
   private readonly IUserFollowRepository _userFollowRepository;
   
   public AccountController(IUserRepository userRepository, IUserFollowRepository userFollowRepository)
   {
      _userRepository = userRepository;
      _userFollowRepository = userFollowRepository;
   }
   
   [HttpGet("/api/account")]
   public ActionResult Get()
   {
       Guid userId = FetchUser();

       if (userId == Guid.Empty) return Unauthorized();

        var user = _userRepository.FindBy<User>("Id", userId.ToString())
            .Include("Posts")
            .Include("DeletedPosts")
            .Include("Followers")
            .Include("Following")
            .Include("Following.Following")
            .FirstOrDefault();

        return StatusCode(200, new Response<User>()
            {
            Code = 200,
            Message = "User fetched",
            Data = user
        });
   }
    [HttpGet("/api/account/follow/{guid:guid}")]
   public ActionResult Follow(Guid guid)
   {
       // TODO: send notification to user for new follow
       Guid userId = FetchUser();
       if (userId == Guid.Empty) return Unauthorized();

       var followingUser = _userRepository.FindBy<User>("Id", guid.ToString()).FirstOrDefault();

       if (followingUser == null) return NotFound();
       
       var follow = new UserFollow();
       follow.FollowerId = userId;
       follow.FollowingId = followingUser.Id;

       try
       {
           _userFollowRepository.Create(follow);
       }
       catch (DbUpdateException e)
       {
           return StatusCode(500, new ErrorResponse()
           {
               Code = 500,
               Message = "An error has occured"
           });
       }

       return Ok();
   }
    
   [HttpGet("/api/account/unfollow/{guid:guid}")]
   public ActionResult Unfollow(Guid guid)
   {
       Guid userId = FetchUser();
       if (userId == Guid.Empty) return Unauthorized();

       var followObj = _userFollowRepository.FindBy<UserFollow>("FollowerId", userId.ToString()).Where($"FollowingId = \"{guid.ToString()}\"").FirstOrDefault();

       try
       {
           _userFollowRepository.Delete(followObj);
       }
       catch (DbUpdateException e)
       {
           return StatusCode(500, new ErrorResponse()
           {
               Code = 500,
               Message = "An error has occured"
           });
       }

       return Ok();
   }
}