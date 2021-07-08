namespace BigSchool_1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Add_LectureName : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Courses", "LectureName", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Courses", "LectureName");
        }
    }
}
