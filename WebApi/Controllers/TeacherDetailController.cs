using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApi.Entities;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TeacherDetailController : ControllerBase
    {
        private DBContext _context;
        public TeacherDetailController(DBContext context)
        {
            _context = context;
        }

        [HttpGet("{userId}")]
        public async Task<IActionResult> Get(int userId)
        {
            var teacherDetailTemp = _context.TeacherDetails
                .Where(x => x.UserId == userId)
                .Select(x => new {
                    x.Id,
                    x.CuratorOfGroupId,
                    x.CuratorOfGroup,
                })
                .FirstOrDefault();

            if (teacherDetailTemp is null)
            {
                return NotFound();
            }

            var teacherDetail = new Models.TeacherDetail()
            {
                Id = teacherDetailTemp.Id,
                CuratorOfGroupId = teacherDetailTemp.CuratorOfGroupId,
                CuratorOfGroup = teacherDetailTemp.CuratorOfGroup,
            };

            return Ok(teacherDetail);
        }
    }
}
