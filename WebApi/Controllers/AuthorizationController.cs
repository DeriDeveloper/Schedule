using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApi.Entities;

namespace WebApi.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthorizationController : ControllerBase
    {
        private readonly DBContext _context;

        public AuthorizationController(DBContext context)
        {
            _context = context;
        }

        // GET: api/auth
        [HttpGet]
        public async Task<ActionResult<User>> GetUser(RequestAuth model)
        {
            if (_context.Users == null)
            {
                return NotFound();
            }

            if(model is null || string.IsNullOrEmpty(model.Login) || string.IsNullOrEmpty(model.Password))
            {
                return BadRequest();
            }

            var user = await _context.Users.FirstOrDefaultAsync(x=>x.Login == model.Login && x.HashPassword == model.Password);

            if(user is null)
            {
                return NotFound();
            }

            return user;
        }

        public class RequestAuth
        {
            public string Login { get; set; }
            public string Password { get; set; }
        }
    }
}
