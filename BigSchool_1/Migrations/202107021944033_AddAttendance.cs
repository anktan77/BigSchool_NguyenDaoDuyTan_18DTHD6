namespace BigSchool_1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddAttendance : DbMigration
    {
        public override void Up()
        {
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
            CreateTable(
                "dbo.Attendances",
                c => new
                    {
                        CourseId = c.Int(nullable: false),
                        AttdendeeId = c.String(nullable: false, maxLength: 128),
                        Attendee_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => new { t.CourseId, t.AttdendeeId });
            
            DropForeignKey("dbo.Attendance_1", "CourseId", "dbo.Courses");
            DropForeignKey("dbo.Attendance_1", "AttdendeeId", "dbo.AspNetUsers");
            DropIndex("dbo.Attendance_1", new[] { "AttdendeeId" });
            DropIndex("dbo.Attendance_1", new[] { "CourseId" });
            DropTable("dbo.Attendance_1");
            CreateIndex("dbo.Attendances", "Attendee_Id");
            CreateIndex("dbo.Attendances", "CourseId");
            AddForeignKey("dbo.Attendances", "CourseId", "dbo.Courses", "Id");
            AddForeignKey("dbo.Attendances", "Attendee_Id", "dbo.AspNetUsers", "Id");
        }
    }
}
