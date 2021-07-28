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
    public class CoursesController : Controller
    {
        private readonly ApplicationDbContext _dbContext;
        public CoursesController()
        {
            _dbContext = new ApplicationDbContext();
        }
        // GET: Courses
        [Authorize]
        public ActionResult Create()
        {
            var viewModel = new CourseViewModel
            {
                categories = _dbContext.categories.ToList(),
                Heading = "Add Course"
                
            };

            return View(viewModel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CourseViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var course = new Course
                {
                    IdLecturer = User.Identity.GetUserId(),
                    DateTime = viewModel.GetDateTime(),
                    IdCategory = viewModel.Category,
                    Place = viewModel.Place
                };
                _dbContext.courses.Add(course);
                _dbContext.SaveChanges();
                return RedirectToAction("Index", "Home");
            }
            else
            {
                viewModel.categories = _dbContext.categories.ToList();
                return View("Create", viewModel);
            }       
        }

        [Authorize]
        public ActionResult Attending()
        {
           
            var userID = User.Identity.GetUserId();
            var course = _dbContext.attendances
                .Where(a => a.AttdendeeId == userID)
                .Select(a => a.Course)
                .Include(l => l.ApplicationUser)
                .Include(l => l.Category)
                .ToList();
           
            foreach (Course item in course)
            {
                ApplicationUser user = System.Web.HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>().FindById(item.IdLecturer);
                item.UserName = user.Name;

                if (userID != null)
                {
                    var find = _dbContext.attendances.Where(a => a.CourseId == item.Id && a.AttdendeeId == userID).FirstOrDefault();
                    if (find == null)
                    {
                        item.isShowGoing = true;
                    }
                    
                    var findFollow = _dbContext.followings.FirstOrDefault(p => p.FolloweeId == userID && p.FollowerId == item.IdLecturer);
                    if (findFollow == null)
                    {
                        item.isShowFollow = true;
                    }
                }
            }

            var viewModel = new CoursesViewModel
            {
                UpcomingCourse = course,
                ShowAction = User.Identity.IsAuthenticated
            };

            return View(course);
        }

        [Authorize]
        public ActionResult Mine()
        {
            var userID = User.Identity.GetUserId();
            var course = _dbContext.courses
                .Where(a => a.IdLecturer == userID && a.DateTime>DateTime.Now)
                .Include(l => l.ApplicationUser)
                .Include(l => l.Category)
                .ToList();
            return View(course);
        }

        [Authorize]
        public ActionResult Edit(int id)
        {
            var userID = User.Identity.GetUserId();
            var course = _dbContext.courses.Single(c => c.Id == id && c.IdLecturer == userID);
            var viewModel = new CourseViewModel
            {
                categories = _dbContext.categories.ToList(),
                Date = course.DateTime.ToString("dd/MM/yyyy"),
                Time = course.DateTime.ToString("HH:mm"),
                Category = course.IdCategory,
                Place = course.Place,
                Heading = "Edit Course",
                Id = course.Id
            };
            return View("Create", viewModel);
        }
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Update(CourseViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                viewModel.categories = _dbContext.categories.ToList();
                return View("Create", viewModel);
            }
            var userId = User.Identity.GetUserId();
            var course = _dbContext.courses.Single(c => c.Id == viewModel.Id && c.IdLecturer == userId);
            course.Place = viewModel.Place;
            course.DateTime = viewModel.GetDateTime();
            course.IdCategory = viewModel.Category;
            _dbContext.SaveChanges();
            return RedirectToAction("Index", "Home");
        }

        public JsonResult deleteAjax(int Id)
        {
            var userID = User.Identity.GetUserId();
            var empDetail = _dbContext.courses.Include(a => a.ApplicationUser).Include(k => k.Category)
                .FirstOrDefault(a => a.Id == Id && a.IdLecturer == userID);
            var attendance = _dbContext.attendances.FirstOrDefault(a => a.CourseId == Id && a.AttdendeeId == userID);
            _dbContext.attendances.Remove(attendance);
            _dbContext.courses.Remove(empDetail);
            _dbContext.SaveChanges();
            return Json(new { status = "Success" });
        }


        public ActionResult LectureIamGoing()
        {
            var userId = User.Identity.GetUserId();

            var listFollwee = _dbContext.followings.Where(p => p.FollowerId == userId).ToList();

            var listAttendances = _dbContext.attendances.Where(p => p.AttdendeeId == userId).ToList();

            var totalCourses = new List<Course>();
            foreach (var lecturer in listFollwee)
            {
                var courses = _dbContext.courses
                .Where(a => a.IdLecturer == lecturer.FolloweeId && a.DateTime >= DateTime.Now)
                .Include(l => l.ApplicationUser)
                .Include(c => c.Category)
                .ToList();

                foreach (Course item in courses)
                {
                    //ApplicationUser user = System.Web.HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>().FindById(item.IdLecturer);
                    //item.UserName = user.Name;

                    if (userId != null)
                    {
                        var find = _dbContext.attendances.Where(a => a.CourseId == item.Id && a.AttdendeeId == userId).FirstOrDefault();
                        if (find == null)
                        {
                            item.isShowGoing = true;
                        }

                        var findFollow = _dbContext.followings.FirstOrDefault(p => p.FolloweeId == userId && p.FollowerId == item.IdLecturer);
                        if (findFollow == null)
                        {
                            item.isShowFollow = true;
                        }
                    }
                    totalCourses.Add(item);
                }

            }

            var viewModel = new CoursesViewModel
            {
                UpcomingCourse = totalCourses,
                ShowAction = User.Identity.IsAuthenticated
            };

            return View(viewModel);
        }
    }
}