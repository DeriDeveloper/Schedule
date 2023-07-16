using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
        public async Task<IActionResult> OnPost(string accessToken, string? name, int? collegeId, int? groupId)
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
            var userRole = (Types.Enums.UserRole)user.UserRoleId;
            
            if (userRole == Types.Enums.UserRole.Student)
            {
                if(collegeId is not null && groupId is not null)
                {
                    var studentDetail = _context.StudentDetails.FirstOrDefault(x=>x.UserId == user.Id);

                    if (studentDetail is null)
                    {
                        studentDetail = new StudentDetail()
                        {
                            UserId = user.Id,
                            GroupId = (int)groupId,
                        };
                        _context.StudentDetails.Add(studentDetail);
                        _context.SaveChanges();
                    }
                    else
                    {
                        studentDetail.GroupId = (int)groupId;

                        _context.SaveChanges();
                    }
                }
                else
                {
                    return BadRequest(new {message= "collegeId или groupId не указан!" });
                }
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
