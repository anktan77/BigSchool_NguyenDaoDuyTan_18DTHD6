using BigSchool_1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BigSchool_1.ViewModels
{
    public class CoursesViewModel
    {
        public IEnumerable<Course> UpcomingCourse { get; set; }
        public bool ShowAction { get; set; }
    }
}