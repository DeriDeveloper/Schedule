using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NuGet.Common;
using System.Net.Http;
using WebApi.Entities;
using WebApi.Models;

namespace WebApi.Controllers.Account
{
    [Route("api/account/[controller]")]
    [ApiController]
    public class GetProfileInfoController : ControllerBase
    {
        private DBContext _context;

        public GetProfileInfoController(DBContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> OnGet(string accessToken)
        {
            if (accessToken is null)
            {
                return BadRequest(new { message = "Не корректный accessToken!" });
            }

            var userAccessToken = _context.UserAccessTokens.FirstOrDefault(x => x.Token == accessToken);

            if (userAccessToken is null)
            {
                return BadRequest(new { message = "accessToken не валидный!" });
            }

            var userId = userAccessToken.UserId;

            var user = _context.Users.Find(userId);

            if(user is null)
            {
                return NotFound();
            }


            var profileInfoResponse = new ProfileInfoResponse()
            {
                Name = user.Name
            };


            return Ok(profileInfoResponse);
        }
    }
}
