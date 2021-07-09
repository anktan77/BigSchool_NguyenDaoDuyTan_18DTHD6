namespace BigSchool_1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                 "dbo.Courses",
                 c => new
                 {
                     Id = c.Int(nullable: false, identity: true),
                     Place = c.String(),
                     DateTime = c.DateTime(nullable: false),
                     IdLecturer = c.String(maxLength: 128),
                     IdCategory = c.Int(nullable: false),
                 })
                 .PrimaryKey(t => t.Id)
                 .ForeignKey("dbo.AspNetUsers", t => t.IdLecturer)
                 .ForeignKey("dbo.Categories", t => t.IdCategory, cascadeDelete: true)
                 .Index(t => t.IdLecturer)
                 .Index(t => t.IdCategory);

            CreateTable(
                "dbo.Categories",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    Name = c.String(),
                })
                .PrimaryKey(t => t.Id);
        }
        
        public override void Down()
        {
            
        }
    }
}
