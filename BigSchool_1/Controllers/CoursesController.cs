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

    }
}