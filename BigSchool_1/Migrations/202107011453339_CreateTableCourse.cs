namespace BigSchool_1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreateTableCourse : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Courses", "IdLecturer", "dbo.AspNetUsers");
            DropIndex("dbo.Courses", new[] { "IdLecturer" });
            AlterColumn("dbo.Courses", "Place", c => c.String(nullable: false, maxLength: 255));
            AlterColumn("dbo.Courses", "IdLecturer", c => c.String(nullable: false, maxLength: 128));
            AlterColumn("dbo.Categories", "Name", c => c.String(nullable: false, maxLength: 255));
            CreateIndex("dbo.Courses", "IdLecturer");
            AddForeignKey("dbo.Courses", "IdLecturer", "dbo.AspNetUsers", "Id", cascadeDelete: true);


        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Courses", "IdLecturer", "dbo.AspNetUsers");
            DropIndex("dbo.Courses", new[] { "IdLecturer" });
            AlterColumn("dbo.Categories", "Name", c => c.String());
            AlterColumn("dbo.Courses", "IdLecturer", c => c.String(maxLength: 128));
            AlterColumn("dbo.Courses", "Place", c => c.String());
            CreateIndex("dbo.Courses", "IdLecturer");
            AddForeignKey("dbo.Courses", "IdLecturer", "dbo.AspNetUsers", "Id");
        }
    }
}
