namespace Bueller.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddGradeNavigationToFile : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("Assignments.Grades", "FileId", "Assignments.Files");
            DropIndex("Assignments.Grades", new[] { "FileId" });
            DropColumn("Assignments.Grades", "GradeId");
            RenameColumn(table: "Assignments.Grades", name: "FileId", newName: "GradeId");
            //DropPrimaryKey("Assignments.Grades");
            AlterColumn("Assignments.Grades", "GradeId", c => c.Int(nullable: false, identity: true));
            AddPrimaryKey("Assignments.Grades", "GradeId");
            CreateIndex("Assignments.Grades", "GradeId");
            AddForeignKey("Assignments.Grades", "GradeId", "Assignments.Files", "FileId");
        }
        
        public override void Down()
        {
            DropForeignKey("Assignments.Grades", "GradeId", "Assignments.Files");
            DropIndex("Assignments.Grades", new[] { "GradeId" });
            DropPrimaryKey("Assignments.Grades");
            AlterColumn("Assignments.Grades", "GradeId", c => c.Int(nullable: false));
            AddPrimaryKey("Assignments.Grades", "GradeId");
            RenameColumn(table: "Assignments.Grades", name: "GradeId", newName: "FileId");
            AddColumn("Assignments.Grades", "GradeId", c => c.Int(nullable: false, identity: true));
            CreateIndex("Assignments.Grades", "FileId");
            AddForeignKey("Assignments.Grades", "FileId", "Assignments.Files", "FileId", cascadeDelete: true);
        }
    }
}
