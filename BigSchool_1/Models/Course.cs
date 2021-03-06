using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace BigSchool_1.Models
{
    public class Course
    {
        [Key]
        public int Id { get; set; }

        public bool IsCanceled { get; set; }

        [Required]
        [StringLength(255)]
        public string Place { get; set; }
        public DateTime DateTime { get; set; }

        [ForeignKey("ApplicationUser")]
        [Required]
        public string IdLecturer { get; set; }
        public ApplicationUser ApplicationUser { get; set; }

        [ForeignKey("Category")]
        [Required]
        public int IdCategory { get; set; }
        public Category Category { get; set; }
        public string UserName { get; set; }
        public bool isLogin { get; set; }
        public bool isShowGoing { get; set; }
        public bool isShowFollow { get; set; }
    }
}