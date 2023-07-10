using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApi.Entities;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TokenValidationController : ControllerBase
    {
        private readonly DBContext _context;

        public TokenValidationController(DBContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> OnGet(string token)
        {
            if (string.IsNullOrEmpty(token))
            {
                return BadRequest();
            }

            var userAccessToken = _context.UserAccessTokens.FirstOrDefault(x => x.Token == token);
            
            if(userAccessToken is not null)
            {
                return Ok();
            }
            else
            {
                return NotFound();
            }
        }
    }
}
