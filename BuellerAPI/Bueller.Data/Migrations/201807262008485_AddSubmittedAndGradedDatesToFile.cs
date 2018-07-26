namespace Bueller.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddSubmittedAndGradedDatesToFile : DbMigration
    {
        public override void Up()
        {
            AddColumn("Assignments.Files", "Submitted", c => c.DateTime(nullable: false, precision: 7, storeType: "datetime2"));
            AddColumn("Assignments.Files", "Graded", c => c.DateTime(precision: 7, storeType: "datetime2"));
        }
        
        public override void Down()
        {
            DropColumn("Assignments.Files", "Graded");
            DropColumn("Assignments.Files", "Submitted");
        }
    }
}
