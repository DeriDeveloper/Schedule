using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApi.Entities;

namespace WebApi.Controllers.UserRoles
{
    [Route("api/UserRoles/[controller]")]
    [ApiController]
    public class GetController : ControllerBase
    {
        private DBContext _context;

        public GetController(DBContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var userRoles = _context.UserRoles.ToList();

            if (userRoles is null)
                return NotFound();

            return Ok(userRoles);
        }
    }
}
