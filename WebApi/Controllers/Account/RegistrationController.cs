using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApi.Entities;
using WebApi.Models;
using WebApi.Services;

namespace WebApi.Controllers.Account
{
    [Route("api/account/[controller]")]
    [ApiController]
    public class RegistrationController : ControllerBase
    {
        private DBContext _context;
        public RegistrationController(DBContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<IActionResult> Post(RequestRegistration model)
        {
            if (string.IsNullOrEmpty(model.Login))
            {
                return BadRequest("Login не указан!");
            }

            if (string.IsNullOrEmpty(model.Password))
            {
                return BadRequest("Password не указан!");
            }

            if (string.IsNullOrEmpty(model.Name))
            {
                return BadRequest("Name не указан!");
            }

            if (string.IsNullOrEmpty(model.Email))
            {
                return BadRequest("Email не указан!");
            }

            if (model.UserRoleId  <= 0)
            {
                return BadRequest("UserRoleId не указан!");
            }

            var userRoleTemp = _context.UserRoles.FirstOrDefault(x=>x.Id == model.UserRoleId);

            if(userRoleTemp is null)
            {
                return NotFound("UserRoleId не найден!");
            }

            var responseRegistration = new ResponseRegistration();


            var user = _context.Users.FirstOrDefault(x=>x.Login == model.Login);

            if(user is not null)
            {
                responseRegistration.IsSuccess = false;
                responseRegistration.ErrorMessages = new List<ErrorMessage>();
                responseRegistration.ErrorMessages.Add(new ErrorMessage()
                {
                     Message = "Пользователь с таким логином уже существует!"
                });

                return Conflict(responseRegistration);
            }

            var hashPassword = HashService.Hash(model.Password);

            user = new User()
            {
                Email = model.Email,
                Login = model.Login,
                HashPassword = hashPassword,
                Name = model.Name,
                UserRoleId = model.UserRoleId,
            };

            _context.Users.Add(user);
            _context.SaveChanges();

            


            responseRegistration.IsSuccess = true;

            return Ok(responseRegistration);
        }
     }
}
