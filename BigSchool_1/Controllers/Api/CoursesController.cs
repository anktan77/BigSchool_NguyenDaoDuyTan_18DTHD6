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

        [HttpDelete]
        public IHttpActionResult Cancel(int id)
        {
            var userID = User.Identity.GetUserId();
            var course = _dbContext.courses.Single(c => c.Id == id && c.IdLecturer == userID);
            if (course == null)
            {
                return NotFound();
            }

            course.IsCanceled = true;
            _dbContext.SaveChanges();
            return Ok();
        }
    }
}
