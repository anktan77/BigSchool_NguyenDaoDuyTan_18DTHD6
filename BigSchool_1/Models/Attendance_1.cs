using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace BigSchool_1.Models
{
    public class Attendance_1
    {
       
        [Key]
        [Column(Order = 1)]
        public int CourseId { get; set; }
        public Course Course { get; set; }

        [Key]
        [Column(Order = 2)]
        [ForeignKey("ApplicationUser")]
        public string AttdendeeId { get; set; }
        public ApplicationUser ApplicationUser { get; set; }
    }
}