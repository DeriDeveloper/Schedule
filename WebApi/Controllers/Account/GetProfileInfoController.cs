using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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

            var user = await _context.Users.Where(x=>x.Id == userId).Include(x=>x.UserRole).FirstOrDefaultAsync();

            if (user is null)
            {
                return NotFound();
            }


            var userRoleStudent = await _context.UserRoles.FirstAsync(x => x.Name.ToLower().Trim() == "студент");

            if (userRoleStudent is not null)
            {
                if (user.UserRoleId == userRoleStudent.Id)
                {
                    if (user.StudentDetails is null)
                    {
                        user.StudentDetails = new List<StudentDetail>();
                    }

                    var studentDetail = _context.StudentDetails
                        .Where(x=>x.UserId == user.Id)
                        .Select(x => new
                        {
                            groupId = x.GroupId,
                            group = x.Group
                        })
                        .FirstOrDefault();

                    if (studentDetail is not null)
                    {
                        user.StudentDetails.Add(new StudentDetail()
                        {
                            GroupId = studentDetail.groupId,
                            Group = studentDetail.group,
                        });
                    }
                }
            }


            var profileInfoResponse = new ProfileInfoResponse()
            {
                Name = user.Name,
                UserRole = user.UserRole
            };

            if (user.AvatarProfileFileMetadatumId is not null)
            {
                user.AvatarProfileFileMetadatum = await _context.FileMetadata.FirstAsync(x => x.Id == user.AvatarProfileFileMetadatumId);

                if (user.AvatarProfileFileMetadatum is not null)
                {
                    profileInfoResponse.ProfileAvatarName = user.AvatarProfileFileMetadatum.Name;
                }
            }

            if(user.StudentDetails?.Count > 0)
            {
                profileInfoResponse.StudentDetail = user.StudentDetails.First();
            }


            return Ok(profileInfoResponse);
        }
    }
}
