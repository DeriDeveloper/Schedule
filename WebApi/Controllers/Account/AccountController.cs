using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using WebApi.Entities;

namespace WebApi.Controllers.Account
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private DBContext _context;

        public AccountController(DBContext context)
        {
            _context = context;
        }

        [HttpGet("{userId}")]
        public async Task<IActionResult> Get(string accessToken, int userId)
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

            var userMain = _context.Users.FirstOrDefault(x => x.Id == userAccessToken.UserId);

            if (userMain is null)
            {
                return NotFound();
            }

            var userMainRole = (Types.Enums.UserRole)userMain.UserRoleId;

            if (userMainRole != Types.Enums.UserRole.Administrator && userMainRole != Types.Enums.UserRole.Developer)
            {
                return Forbid();
            }


            var user = _context.Users
                .Where(x => x.Id == userId != x.IsDeleted == true)
                .Select(x => new
                {
                    x.Id,
                    x.Name,
                    x.Login,
                    x.UserRoleId,
                    x.CollegeId,
                    x.Email,
                    x.AvatarProfileFileMetadatumId
                })
                .FirstOrDefault();

            if (user is null)
            {
                return NotFound();
            }

            return Ok(user);
        }
    }
}
