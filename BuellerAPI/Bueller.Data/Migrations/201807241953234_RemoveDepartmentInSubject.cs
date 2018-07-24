namespace Bueller.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemoveDepartmentInSubject : DbMigration
    {
        public override void Up()
        {
            DropColumn("Classes.Subjects", "Department");
        }
        
        public override void Down()
        {
            AddColumn("Classes.Subjects", "Department", c => c.String(nullable: false, maxLength: 100));
        }
    }
}
