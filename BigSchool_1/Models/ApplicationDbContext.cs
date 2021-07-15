using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace BigSchool_1.Models
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<Course> courses { get; set; }
        public DbSet<Category> categories { get; set; }
        public DbSet<Attendance_1> attendances { get; set; }
        public DbSet<Following> followings { get; set; }
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Attendance_1>()
                .HasRequired(a => a.Course)
                .WithMany()
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<ApplicationUser>()
               .HasMany(u => u.followers)
               .WithRequired(f=>f.Followee)
               .WillCascadeOnDelete(false);

            modelBuilder.Entity<ApplicationUser>()
               .HasMany(u => u.followees)
               .WithRequired(f => f.Follower)
               .WillCascadeOnDelete(false);

            base.OnModelCreating(modelBuilder);
            
        }
    }
}