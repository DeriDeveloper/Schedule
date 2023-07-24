using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApi.Entities;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TeacherController : ControllerBase
    {
        private DBContext _context;
        public TeacherController(DBContext context)
        {
            _context = context;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var  teacherTemp= _context.Users
                .Where(x => x.Id == id)
                .Include(x => x.UserRole)
                .Include(x => x.AvatarProfileFileMetadatum)
                .Include(x => x.College)
                .Include(x => x.TeacherDetails)
                .Select(x=> new{
                    x.Id,
                    x.Name,
                    x.UserRoleId,
                    x.UserRole,
                    x.AvatarProfileFileMetadatumId,
                    x.AvatarProfileFileMetadatum,
                    x.CollegeId, 
                    x.College,
                    x.TeacherDetails,
                })
                .FirstOrDefault();

            if(teacherTemp is null)
            {
                return NotFound();
            }

            var teacher = new Models.Teacher()
            {
                Id = teacherTemp.Id,
                AvatarProfileFileMetadatumId = teacherTemp.AvatarProfileFileMetadatumId,
                AvatarProfileFileMetadatum = teacherTemp.AvatarProfileFileMetadatum,
                CollegeId = teacherTemp.CollegeId,
                College = teacherTemp.College,
                Name = teacherTemp.Name,
                UserRoleId = teacherTemp.UserRoleId,
                UserRole = teacherTemp.UserRole,
            };

            return Ok(teacher);
        }
    }
}
