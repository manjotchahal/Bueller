namespace Bueller.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SeparateGradeIntoFileAndAssignment : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("Assignments.Grades", "GradeId", "Assignments.Files");
            DropIndex("Assignments.Grades", new[] { "GradeId" });
            AddColumn("Assignments.Assignments", "EvaluationType", c => c.String(nullable: false, maxLength: 50));
            AddColumn("Assignments.Files", "Score", c => c.Double());
            AddColumn("Assignments.Files", "Comment", c => c.String(maxLength: 500));
            DropTable("Assignments.Grades");
        }
        
        public override void Down()
        {
            CreateTable(
                "Assignments.Grades",
                c => new
                    {
                        GradeId = c.Int(nullable: false, identity: true),
                        EvaluationType = c.String(nullable: false, maxLength: 50),
                        Score = c.Double(nullable: false),
                        LetterGrade = c.String(nullable: false, maxLength: 2),
                        Comment = c.String(maxLength: 500),
                        Created = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        Modified = c.DateTime(precision: 7, storeType: "datetime2"),
                    })
                .PrimaryKey(t => t.GradeId);
            
            DropColumn("Assignments.Files", "Comment");
            DropColumn("Assignments.Files", "Score");
            DropColumn("Assignments.Assignments", "EvaluationType");
            CreateIndex("Assignments.Grades", "GradeId");
            AddForeignKey("Assignments.Grades", "GradeId", "Assignments.Files", "FileId");
        }
    }
}
