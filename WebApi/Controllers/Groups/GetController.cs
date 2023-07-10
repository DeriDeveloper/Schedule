using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApi.Entities;
using WebApi.Models;

namespace WebApi.Controllers.Groups
{
    [Route("api/groups/[controller]")]
    [ApiController]
    public class GetController : ControllerBase
    {
        private DBContext _context;

        public GetController(DBContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> OnGet(int collegeId, int year)
        {
            var groups =  _context.Groups.Where(x=>x.CollegeId == collegeId && x.Year == year).ToList();

            var responseGroups = new List<ResponseGroup>();

            foreach (var group in groups)
            {
                responseGroups.Add(new ResponseGroup()
                {
                    Id = group.Id,
                    Name = group.Name,
                    Year = group.Year,
                    CollegeId = group.CollegeId,
                });
            }

            return Ok(responseGroups);
        }
    }
}
