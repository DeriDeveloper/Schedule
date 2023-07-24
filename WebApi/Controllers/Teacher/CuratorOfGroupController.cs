using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApi.Entities;

namespace WebApi.Controllers.Teacher
{
    [Route("api/Teacher/[controller]")]
    [ApiController]
    public class CuratorOfGroupController : ControllerBase
    {
        private DBContext _context;
        public CuratorOfGroupController(DBContext context)
        {
            _context = context;
        }


        [HttpGet("{groupId}")]
        public async Task<IActionResult> Get(int groupId)
        {
            var teacher = _context.Users.Where(x => x.UserRoleId == (int)Types.Enums.UserRole.Teacher && x.TeacherDetails.Any(x => x.CuratorOfGroupId == groupId)).FirstOrDefault();

            if (teacher is null)
            {
                return NotFound();
            }

            return Ok(teacher); // заменить
        }
    }
}
