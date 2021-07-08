using BigSchool_1.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace BigSchool_1.Controllers
{
    [Authorize]
    public class AttendancesController : ApiController
    {
        private ApplicationDbContext _dbContext;
        public AttendancesController()
        {
            _dbContext = new ApplicationDbContext();
        }
        [HttpPost]
        public IHttpActionResult Attend(Course attendanceDto)
        {
            var userId = User.Identity.GetUserId();
            if (_dbContext.attendances.Any(a=>a.AttdendeeId == userId && a.CourseId == attendanceDto.Id))
            {
                return BadRequest("The Attendance already exist");
            }
            var attendance = new Attendance_1
            {
                CourseId = attendanceDto.Id,
                AttdendeeId = userId,
                
            };
            _dbContext.attendances.Add(attendance);
            _dbContext.SaveChanges();
            return Ok();
        }
    }
}
