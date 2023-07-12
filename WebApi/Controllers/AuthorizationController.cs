using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApi.Entities;
using WebApi.Models;
using WebApi.Services;

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
        public async Task<ActionResult<ResponseAuth>> GetUser(RequestAuth model)
        {
            if (_context.Users == null)
            {
                return NotFound();
            }

            if(model is null || string.IsNullOrEmpty(model.Login) || string.IsNullOrEmpty(model.Password))
            {
                return BadRequest();
            }

            var user = await _context.Users.FirstOrDefaultAsync(x=>x.Login == model.Login);

            if(user is null)
            {
                return NotFound();
            }

            if (HashService.VerifyHash(model.Password, user.HashPassword))
            {
                var token = JwtHelperService.GenerateToken(user.Login, 1);

                _context.UserAccessTokens.Add(
                    new UserAccessToken
                    {
                        Guid = Guid.NewGuid(),
                        UserId = user.Id,
                        Token = token,
                         DateCreated = DateTime.UtcNow
                    });
                await _context.SaveChangesAsync();


                return new ResponseAuth()
                {
                    Token = token
                };
            }
            else
            {
                return NotFound();
            }
        }


    }
}
