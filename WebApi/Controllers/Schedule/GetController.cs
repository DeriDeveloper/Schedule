using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApi.Entities;
using WebApi.Models;

namespace WebApi.Controllers.Schedule
{
    [Route("api/schedule/[controller]")]
    [ApiController]
    public class GetController : ControllerBase
    {
        private DBContext _context;

        public GetController(DBContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Get(DateTime date, int? groupId = null, int? teacherId = null)
        {
            if ((groupId == null && teacherId == null) || (groupId == 0 && teacherId ==0))
                return BadRequest("groupId или teacherId не указан!");

            if (groupId != null && groupId > 0)
            {
                if (_context.Groups.Find(groupId) == null)
                {
                    return NotFound("groupId не найден!");
                };
            }

            if (teacherId != null && teacherId > 0)
            {
                if (_context.Users.Find(teacherId) == null)
                {
                    return NotFound("teacherId не найден!");
                };
            }




            var responseScheduleCells = new ResponseScheduleCells();

            responseScheduleCells.ScheduleCells = new List<Models.ScheduleCell>();

            var scheduleCells = _context.ScheduleCells.Where(x=>x.Date == date).ToList();

            foreach (var scheduleCellTemp in scheduleCells)
            {
                var scheduleCell = new Models.ScheduleCell()
                {
                    Id = scheduleCellTemp.Id,
                    Date = scheduleCellTemp.Date,
                    NumberPair = scheduleCellTemp.NumberPair,
                    TimeStart = scheduleCellTemp.TimeStart,
                    TimeEnd = scheduleCellTemp.TimeEnd,
                    TypeWeekId = scheduleCellTemp.TypeWeekId,
                    ScheduleCellTeachers = new List<Models.ScheduleCellTeacher>(),
                    ScheduleCellAudiences = new List<Models.ScheduleCellAudience>(),
                    ScheduleCellGroups = new List<Models.ScheduleCellGroup>()
                };

                var scheduleCellGroupsTemp = _context.ScheduleCellGroups.Where(x => x.ScheduleCellId == scheduleCell.Id).ToList();

                foreach (var scheduleCellGroupTemp in scheduleCellGroupsTemp)
                {
                    var scheduleCellGroup = new Models.ScheduleCellGroup()
                    {
                        Id = scheduleCellGroupTemp.GroupId,
                        Name = scheduleCellGroupTemp.Group.Name,
                    };

                    scheduleCell.ScheduleCellGroups.Add(scheduleCellGroup);
                }

                var scheduleCellAudiencesTemp = _context.ScheduleCellAudiences.Where(x => x.ScheduleCellId == scheduleCell.Id).ToList();

                foreach (var scheduleCellAudienceTemp in scheduleCellAudiencesTemp)
                {
                    var scheduleCellAudience = new Models.ScheduleCellAudience()
                    {
                        Id = scheduleCellAudienceTemp.AudienceId,
                        Name = scheduleCellAudienceTemp.Audience.Name
                    };

                    scheduleCell.ScheduleCellAudiences.Add(scheduleCellAudience);
                }

                

                var scheduleCellTeachersTemp = _context.ScheduleCellTeachers.Where(x => x.ScheduleCellId == scheduleCell.Id).ToList();

                foreach (var scheduleCellTeacherTemp in scheduleCellTeachersTemp)
                {
                    var scheduleCellTeacher = new Models.ScheduleCellTeacher()
                    {
                        Id = scheduleCellTeacherTemp.TeacherId,
                        Name = scheduleCellTeacherTemp.Teacher.Name,
                    };

                    scheduleCell.ScheduleCellTeachers.Add(scheduleCellTeacher);
                }

                if (groupId != null && scheduleCell.ScheduleCellGroups.Any(x => x.Id == groupId) == true)
                {
                    responseScheduleCells.ScheduleCells.Add(scheduleCell);
                }
                else if(teacherId != null && scheduleCell.ScheduleCellTeachers.Any(x => x.Id == teacherId) == true)
                {
                    responseScheduleCells.ScheduleCells.Add(scheduleCell);
                }
            }



            return Ok(responseScheduleCells.ScheduleCells);
        }
    }
}
