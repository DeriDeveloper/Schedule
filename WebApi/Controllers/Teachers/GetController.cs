using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApi.Entities;

namespace WebApi.Controllers.Teachers
{
    [Route("api/teachers/[controller]")]
    [ApiController]
    public class GetController : ControllerBase
    {
        private DBContext _context;

        public GetController(DBContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Get(int collegeId)
        {
            var users = _context.Users.Where(x => x.CollegeId == collegeId && x.UserRoleId == (int)Types.Enums.UserRole.Teacher).Select(x=> new { x.Id, x.Name }).ToList();

            var teachers = new List<Models.Teacher>();

            foreach (var user in users)
            {
                teachers.Add(new Models.Teacher()
                {
                    Id = user.Id,
                    Name = user.Name
                });
            }

            return Ok(teachers);
        }
    }
}
