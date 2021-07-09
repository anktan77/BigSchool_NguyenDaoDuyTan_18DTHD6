using BigSchool_1.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Web;

namespace BigSchool_1.ViewModels
{
    public class CourseViewModel
    {
        public int Id { get; set; }
        [Required]
        public string Place { get; set; }

        [Required]
        [FutureDate]
        public string Date { get; set; }

        [Required]
        [ValidTime]
        public string Time { get; set; }

        [Required]
        public int Category { get; set; }
        public IEnumerable<Category> categories { get; set; }

        public string Heading { get; set; }
        public string Action {
            get { return (Id != 0) ? "Update" : "Create"; }
        }

        public DateTime GetDateTime()
        {
            string day = string.Format("{0} {1}", Date, Time);
            DateTime date = DateTime.ParseExact(day, "dd/MM/yyyy HH:mm", null);
            string format = date.ToString("yyyy/MM/dd HH:mm");
            DateTime mainTime = DateTime.Parse(format);
            return mainTime;
        }
    }
}
