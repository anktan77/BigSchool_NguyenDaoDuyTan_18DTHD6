using BigSchool_1.DTOs;
using BigSchool_1.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace BigSchool_1.Controllers.Api
{
    public class CoursesController : ApiController
    {
        public ApplicationDbContext _dbContext { get; set; }
        public CoursesController()
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
        public IHttpActionResult Cancel(int id)
        {
            var userId = User.Identity.GetUserId();

            var courses = _dbContext.courses.Single(c => c.Id == id && c.IdLecturer == userId);

            if (courses.IsCanceled)
            {
                return NotFound();
            }

            courses.IsCanceled = true;
            _dbContext.SaveChanges();

            return Ok();
        }
    }
}
