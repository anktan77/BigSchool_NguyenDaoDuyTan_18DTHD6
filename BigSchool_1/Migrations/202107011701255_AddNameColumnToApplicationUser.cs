namespace BigSchool_1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddNameColumnToApplicationUser : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.AspNetUsers", "Name", c => c.String(nullable: false, maxLength: 255));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.AspNetUsers", "Name", c => c.String());
        }
    }
}
