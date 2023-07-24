using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApi.Entities;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private DBContext _context;
        public UserController(DBContext context)
        {
            _context = context;
        }

        [HttpDelete("{userId}")]
        public async Task<IActionResult> Delete(string accessToken, int userId)
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

            var userAdmin = _context.Users.Find(userAccessToken.UserId);



            if(userAdmin  is null)
            {
                return NotFound(new { message = "Админ не найден!"});
            }

            if((Types.Enums.UserRole)userAdmin.UserRoleId != Types.Enums.UserRole.Administrator)
            {
                return Forbid();
            }


            var user = _context.Users.Where(x => x.Id == userId && x.CollegeId == userAdmin.CollegeId).FirstOrDefault();
        
            if(user is null)
            {
                return NotFound();
            }


            var userAccessTokens = _context.UserAccessTokens.Where(x => x.UserId == user.Id).ToList();

            _context.UserAccessTokens.RemoveRange(userAccessTokens);
            _context.SaveChanges();


            user.IsDeleted = true;

            _context.Users.Update(user);
            _context.SaveChanges();

            return NoContent();
        }
    }
}
