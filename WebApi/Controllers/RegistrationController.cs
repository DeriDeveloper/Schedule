using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApi.Entities;
using WebApi.Services;
using static WebApi.Controllers.AuthorizationController;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegistrationController : ControllerBase
    {
        private readonly DBContext _context;

        public RegistrationController(DBContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<ActionResult<User>> Registration(RequestRegistration model)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.Login == model.Login);

            if (user is not null)
            {
                return Conflict();
            }

            var userRole = await _context.UserRoles.FirstOrDefaultAsync(x=>x.Name.ToLower() == "студент");

            var hashPassword = HashService.Hash(model.Password);

            user = new User()
            {
                Login = model.Login,
                HashPassword = hashPassword,
                Email = model.Email,
                Name = model.Name,
                UserRoleId = userRole.Id,
            };

            _context.Users.Add(user);
            _context.SaveChangesAsync();


            return user;
        }

        public class RequestRegistration
        {
            public string Login { get; set; }
            public string Password { get; set; }
            public string Email { get; set; }
            public string Name { get; set; }
        }
    }
}
