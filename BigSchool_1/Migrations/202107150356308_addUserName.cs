namespace BigSchool_1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addUserName : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Courses", "UserName", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Courses", "UserName");
        }
    }
}
