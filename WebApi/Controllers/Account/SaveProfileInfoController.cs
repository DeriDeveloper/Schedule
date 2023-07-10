using Microsoft.AspNetCore.Mvc;
using WebApi.Entities;
using WebApi.Models;

namespace WebApi.Controllers.Account
{
    [Route("api/account/[controller]")]
    [ApiController]
    public class SaveProfileInfoController : ControllerBase
    {
        private DBContext _context;

        public SaveProfileInfoController(DBContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<IActionResult> OnPost(string accessToken, string? name)
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

            if (user is null)
            {
                return NotFound();
            }

            if (!string.IsNullOrEmpty(name))
            {
                user.Name = name;
            }

            await _context.SaveChangesAsync();


            return NoContent();
        }
    }
}
