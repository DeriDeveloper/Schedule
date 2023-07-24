using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Linq;
using WebApi.Entities;

namespace WebApi.Controllers.Accounts
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        private DBContext _context;

        public AccountsController(DBContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Get(string accessToken, int take, int skip, int orderBy, string textSeacrh = "")
        {
            if (take <= 0)
            {
                return BadRequest("take <= 0");
            }

            if (skip < 0)
            {
                return BadRequest("skip <= 0");
            }


            if (take > 100)
            {
                return BadRequest("take > 100");
            }

            if (accessToken is null)
            {
                return BadRequest(new { message = "Не корректный accessToken!" });
            }

            var userAccessToken = _context.UserAccessTokens.FirstOrDefault(x => x.Token == accessToken);

            if (userAccessToken is null)
            {
                return BadRequest(new { message = "accessToken не валидный!" });
            }

            var userMain = _context.Users.Where(x => x.Id == userAccessToken.UserId && (x.UserRoleId == (int)Types.Enums.UserRole.Administrator|| x.UserRoleId == (int)Types.Enums.UserRole.Developer)).FirstOrDefault();

            if (userMain is null)
            {
                return Forbid();
            }


            var userOrderBy = (Types.Enums.UserOrderBy)orderBy;

            var usersSelectQueryable = _context.Users.AsQueryable();


            if ((Types.Enums.UserRole)userMain.UserRoleId == Types.Enums.UserRole.Administrator)
            {
                usersSelectQueryable = usersSelectQueryable.Where(x=> x.IsDeleted == false && x.CollegeId == userMain.CollegeId && x.UserRoleId != (int)Types.Enums.UserRole.Administrator);
            }

            switch (userOrderBy)
            {
                case Types.Enums.UserOrderBy.Id:
                    {
                        usersSelectQueryable = usersSelectQueryable.OrderBy(x=>x.Id); 
                        break;
                    }
                case Types.Enums.UserOrderBy.IdDesc:
                    {
                        usersSelectQueryable = usersSelectQueryable.OrderByDescending(x => x.Id);
                        break;
                    }
                case Types.Enums.UserOrderBy.Email:
                    {
                        usersSelectQueryable = usersSelectQueryable.OrderBy(x => x.Email);
                        break;
                    }
                case Types.Enums.UserOrderBy.EmailDesc:
                    {
                        usersSelectQueryable = usersSelectQueryable.OrderByDescending(x => x.Email);
                        break;
                    }
                case Types.Enums.UserOrderBy.UserRole:
                    {
                        usersSelectQueryable = usersSelectQueryable.OrderBy(x => x.UserRoleId);
                        break;
                    }
                case Types.Enums.UserOrderBy.UserRoleDesc:
                    {
                        usersSelectQueryable = usersSelectQueryable.OrderByDescending(x => x.UserRoleId);
                        break;
                    }
                case Types.Enums.UserOrderBy.Login:
                    {
                        usersSelectQueryable = usersSelectQueryable.OrderBy(x => x.Login);
                        break;
                    }
                case Types.Enums.UserOrderBy.LoginDesc:
                    {
                        usersSelectQueryable = usersSelectQueryable.OrderByDescending(x => x.Login);
                        break;
                    }
                case Types.Enums.UserOrderBy.Name:
                    {
                        usersSelectQueryable = usersSelectQueryable.OrderBy(x => x.Name);
                        break;
                    }
                case Types.Enums.UserOrderBy.NameDesc:
                    {
                        usersSelectQueryable = usersSelectQueryable.OrderByDescending(x => x.Name);
                        break;
                    }
                case Types.Enums.UserOrderBy.None:
                default:
                    {

                        break;
                    }
            }


            var usersSelect = usersSelectQueryable
                .Where(x => (x.Name.Contains(textSeacrh) || x.Login.Contains(textSeacrh) || x.Email.Contains(textSeacrh)) && x.UserRoleId != (int)Types.Enums.UserRole.Developer)
                .Skip(skip)
                .Take(take)
                .Include(x=>x.UserRole)
                .Include(x=>x.College)
                .Select(x => new
                {
                    Id = x.Id,
                    Login = x.Login,
                    Name = x.Name,
                    UserRoleName = x.UserRole.Name,
                    UserRoleId = x.UserRole.Id,
                    CollegeId = x.College.Id,
                    CollegeName = x.College.Name,
                })
                .ToList();


            var users = new List<User>();

            foreach(var userSelect in usersSelect)
            {
                var user = new User()
                {
                     Id  = userSelect.Id,
                     Login = userSelect.Login,
                      Name  = userSelect.Name,
                };

                user.UserRole = new()
                {
                    Id = userSelect.UserRoleId,
                    Name = userSelect.UserRoleName,
                };

                user.College = new()
                {
                    Id = userSelect.CollegeId,
                    Name = userSelect.CollegeName,
                };

                users.Add(user);
            }



            return Ok(users);
        }
    }
}
