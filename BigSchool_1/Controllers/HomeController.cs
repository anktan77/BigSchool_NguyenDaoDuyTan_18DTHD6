using BigSchool_1.Models;
using BigSchool_1.ViewModels;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BigSchool_1.Controllers
{
    public class HomeController : Controller
    {
        private ApplicationDbContext _dbContext;
        public HomeController()
        {
            _dbContext = new ApplicationDbContext();
        }
        public ActionResult Index()
        {
            
            var upcomingCourses = _dbContext.courses.Include(c => c.ApplicationUser).Include(c => c.Category).Where(c => c.DateTime > DateTime.Now);

            var userID = User.Identity.GetUserId();
            //var viewModel = new CourseViewModel
            //{
            //    UpcomingCourse = upcomingCourses,
            //    ShowAction = User.Identity.IsAuthenticated
            //};
            foreach (Course i in upcomingCourses)
            {
                ApplicationUser user = System.Web.HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>().FindById(i.IdLecturer);
                if (userID != null)
                {
                    i.isLogin = true;
                    Attendance_1 find = _dbContext.attendances.FirstOrDefault(p => p.CourseId == i.Id && p.AttdendeeId == userID);
                    if (find == null)
                    {
                        i.isShowGoing = true;
                    }
                    Following findFollow = _dbContext.followings.FirstOrDefault(p => p.FollowerId == userID && p.FolloweeId == i.IdLecturer);
                    if (findFollow == null)
                    {
                        i.isShowFollow = true;
                    }
                }
            }
            return View(upcomingCourses);

        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}