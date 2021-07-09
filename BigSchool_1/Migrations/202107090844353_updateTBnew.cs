namespace BigSchool_1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updateTBnew : DbMigration
    {
        public override void Up()
        {
            Sql("INSERT INTO CATEGORIES (NAME) VALUES ('Development')");
            Sql("INSERT INTO CATEGORIES (NAME) VALUES ('Business')");
            Sql("INSERT INTO CATEGORIES (NAME) VALUES ('Marketing')");

            DropForeignKey("dbo.Attendances", "Attendee_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.Attendances", "CourseId", "dbo.Courses");
            DropIndex("dbo.Attendances", new[] { "CourseId" });
            DropIndex("dbo.Attendances", new[] { "Attendee_Id" });
            CreateTable(
                "dbo.Attendance_1",
                c => new
                {
                    CourseId = c.Int(nullable: false),
                    AttdendeeId = c.String(nullable: false, maxLength: 128),
                })
                .PrimaryKey(t => new { t.CourseId, t.AttdendeeId })
                .ForeignKey("dbo.AspNetUsers", t => t.AttdendeeId, cascadeDelete: true)
                .ForeignKey("dbo.Courses", t => t.CourseId)
                .Index(t => t.CourseId)
                .Index(t => t.AttdendeeId);
        }
        
        public override void Down()
        {
        }
    }
}
