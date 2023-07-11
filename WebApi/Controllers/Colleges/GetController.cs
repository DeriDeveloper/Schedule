using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApi.Entities;

namespace WebApi.Controllers.Colleges
{
    [Route("api/colleges/[controller]")]
    [ApiController]
    public class GetController : ControllerBase
    {
        private DBContext _context;
        public GetController(DBContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> OnGet()
        {
            var colleges = _context.Colleges.ToList();

            return Ok(colleges);
        }
    }
}
