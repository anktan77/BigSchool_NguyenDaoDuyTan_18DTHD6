namespace BigSchool_1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UPdate_Course : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Courses", "LectureName");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Courses", "LectureName", c => c.String());
        }
    }
}
