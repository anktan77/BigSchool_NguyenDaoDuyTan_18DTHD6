using BigSchool_1.DTOs;
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
        public IHttpActionResult Attend(AttendanceDto attendanceDto)
        {
            var userId = User.Identity.GetUserId();
            if (_dbContext.attendances.Any(a => a.AttdendeeId == userId && a.CourseId == attendanceDto.CourseID))
            {
                return BadRequest("The Attendance already exists!");
            }

            var attendance = new Attendance_1
            {
                CourseId = attendanceDto.CourseID,
                AttdendeeId = userId
            };

            _dbContext.attendances.Add(attendance);
            _dbContext.SaveChanges();

            return Ok();
        }

        [HttpDelete]
        public IHttpActionResult DeleteAttendance(int id)
        {
            var userId = User.Identity.GetUserId();

            var attendance = _dbContext.attendances.SingleOrDefault(a => a.AttdendeeId == userId && a.CourseId == id);
            if (attendance == null)
            {
                return NotFound();
            }

            _dbContext.attendances.Remove(attendance);
            _dbContext.SaveChanges();

            return Ok(attendance);
        }
    }
}
