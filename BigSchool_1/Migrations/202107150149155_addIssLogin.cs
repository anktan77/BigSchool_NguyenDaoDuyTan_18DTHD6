namespace BigSchool_1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addIssLogin : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Courses", "isLogin", c => c.Boolean(nullable: false));
            AddColumn("dbo.Courses", "isShowGoing", c => c.Boolean(nullable: false));
            AddColumn("dbo.Courses", "isShowFollow", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Courses", "isShowFollow");
            DropColumn("dbo.Courses", "isShowGoing");
            DropColumn("dbo.Courses", "isLogin");
        }
    }
}
