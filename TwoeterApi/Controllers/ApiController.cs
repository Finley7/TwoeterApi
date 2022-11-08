using System;
using Microsoft.AspNetCore.Mvc;
using TwoeterApi.DTO;
using TwoeterApi.Exceptions;
using TwoeterApi.Middleware;

namespace TwoeterApi.Controllers
{
    public class ApiController : ControllerBase
    {

        [NonAction]
        protected Guid FetchUser()
        {
            try
            {
                return Authentication.GetUser(Request);
            }
            catch (NotAuthenticatedException e)
            {
                
            }

            return Guid.Empty;
        }

        [NonAction]
        protected ActionResult NotAuthenticated()
        {
            return StatusCode(403, new ErrorResponse()
            {
                Code = 403,
                Message = "Access denied"
            });
        }
    }
}

