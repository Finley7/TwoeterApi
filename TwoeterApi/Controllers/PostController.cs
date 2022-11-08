using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TwoeterApi.DTO;
using TwoeterApi.DTO.Post;
using TwoeterApi.Exceptions;
using TwoeterApi.Middleware;
using TwoeterApi.Model.Entity;
using TwoeterApi.Model.Repository;
using TwoeterApi.Model.Repository.Interfaces;

namespace TwoeterApi.Controllers
{
    [ApiController]
    public class PostController : ApiController
    {
        private readonly IUserRepository _userRepository;
        private readonly IPostRepository _postRepository;


        public PostController(IUserRepository userRepository, IPostRepository postRepository)
        {
            _postRepository = postRepository;
            _userRepository = userRepository;
        }

        [HttpGet("/api/posts/feed")]
        public ActionResult Get()
        {
            Guid userId = FetchUser();

            if (userId == Guid.Empty) return Unauthorized();
            
            return StatusCode(200, new Response<List<Post>>()
            {
                Code = 200,
                Message = "User feed",
                Data = _postRepository.GetFeed(userId)
            });
        }

        [HttpGet("/api/post/create")]
        public ActionResult Create(NewPostRequest newPostRequest)
        {
            Guid userId = FetchUser();

            if (userId == Guid.Empty) return Unauthorized();

            Post post = new Post();
            post.AuthorId = userId;
            post.Body = newPostRequest.Body;

            try
            {
                _postRepository.Create(post);
            } catch(DbUpdateException e)
            {
                return StatusCode(500, new ErrorResponse());
            }

            return Ok();
        }

        [HttpGet("/api/posts/{guid}")]
        public ActionResult View(Guid guid)
        {
            Guid userId = FetchUser();

            if (userId == Guid.Empty) return Unauthorized();

            Post? post = _postRepository.FindBy<Post>("Id", guid.ToString()).Include("Author").FirstOrDefault();

            if(post.Deleted != null && post.AuthorId != userId)
            {
                return NotFound();
            }

            return StatusCode(200, new Response<Post>()
            {
                Code = 200,
                Data = post
            });
        }

        [HttpGet("/api/post/delete/{guid}")]
        public ActionResult Delete(Guid guid)
        {
            Guid userId = FetchUser();

            if (userId == Guid.Empty) return Unauthorized();
            
            Post? post = _postRepository.FindBy<Post>("Id", guid.ToString()).FirstOrDefault();

            if (post.AuthorId != userId) return NotFound();

            post.Delete(userId);
            _postRepository.Update(post);

            return Ok();
        }
    }
}
