namespace Bueller.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "Assignments.Assignments",
                c => new
                {
                    AssignmentId = c.Int(nullable: false, identity: true),
                    Name = c.String(nullable: false, maxLength: 100),
                    EvaluationType = c.String(nullable: false, maxLength: 50),
                    DueDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                    ClassId = c.Int(nullable: false),
                    Created = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                    Modified = c.DateTime(precision: 7, storeType: "datetime2"),
                })
                .PrimaryKey(t => t.AssignmentId)
                .ForeignKey("Classes.Classes", t => t.ClassId, cascadeDelete: true)
                .Index(t => t.ClassId);

            CreateTable(
                "Classes.Classes",
                c => new
                {
                    ClassId = c.Int(nullable: false, identity: true),
                    Name = c.String(nullable: false, maxLength: 100),
                    RoomNumber = c.String(nullable: false, maxLength: 20),
                    Section = c.String(nullable: false, maxLength: 10),
                    Credits = c.Int(nullable: false),
                    Description = c.String(nullable: false, maxLength: 500),
                    StartTime = c.Time(nullable: false, precision: 7),
                    EndTime = c.Time(nullable: false, precision: 7),
                    Mon = c.Int(nullable: false),
                    Tues = c.Int(nullable: false),
                    Wed = c.Int(nullable: false),
                    Thurs = c.Int(nullable: false),
                    Fri = c.Int(nullable: false),
                    TeacherId = c.Int(),
                    SubjectId = c.Int(nullable: false),
                    Created = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                    Modified = c.DateTime(precision: 7, storeType: "datetime2"),
                })
                .PrimaryKey(t => t.ClassId)
                .ForeignKey("Classes.Subjects", t => t.SubjectId, cascadeDelete: true)
                .ForeignKey("Persons.Teachers", t => t.TeacherId)
                .Index(t => t.TeacherId)
                .Index(t => t.SubjectId);

            CreateTable(
                "Classes.Books",
                c => new
                {
                    BookId = c.Int(nullable: false, identity: true),
                    Title = c.String(nullable: false, maxLength: 100),
                    Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                    Description = c.String(maxLength: 500),
                    Created = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                    Modified = c.DateTime(precision: 7, storeType: "datetime2"),
                })
                .PrimaryKey(t => t.BookId);

            CreateTable(
                "Persons.Students",
                c => new
                {
                    StudentId = c.Int(nullable: false, identity: true),
                    FirstName = c.String(nullable: false, maxLength: 100),
                    MiddleName = c.String(maxLength: 100),
                    LastName = c.String(nullable: false, maxLength: 100),
                    Street = c.String(nullable: false, maxLength: 200),
                    City = c.String(nullable: false, maxLength: 100),
                    State = c.String(nullable: false, maxLength: 100),
                    Country = c.String(nullable: false, maxLength: 100),
                    Zipcode = c.String(nullable: false),
                    Email = c.String(nullable: false),
                    PhoneNumber = c.String(nullable: false),
                    Grade = c.String(nullable: false, maxLength: 100),
                    Created = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                    Modified = c.DateTime(precision: 7, storeType: "datetime2"),
                })
                .PrimaryKey(t => t.StudentId);

            CreateTable(
                "Classes.Subjects",
                c => new
                {
                    SubjectId = c.Int(nullable: false, identity: true),
                    Name = c.String(nullable: false, maxLength: 100),
                    Created = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                    Modified = c.DateTime(precision: 7, storeType: "datetime2"),
                })
                .PrimaryKey(t => t.SubjectId);

            CreateTable(
                "Persons.Teachers",
                c => new
                {
                    TeacherID = c.Int(nullable: false, identity: true),
                    OfficeNumber = c.Int(),
                    FirstName = c.String(nullable: false, maxLength: 100),
                    MiddleName = c.String(maxLength: 100),
                    LastName = c.String(nullable: false, maxLength: 100),
                    Title = c.String(nullable: false, maxLength: 100),
                    Street = c.String(nullable: false, maxLength: 200),
                    City = c.String(nullable: false, maxLength: 100),
                    State = c.String(nullable: false, maxLength: 100),
                    Country = c.String(nullable: false, maxLength: 100),
                    Zipcode = c.String(nullable: false),
                    Email = c.String(nullable: false),
                    PersonalPhoneNumber = c.String(nullable: false),
                    OfficePhoneNumber = c.String(),
                    Created = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                    Modified = c.DateTime(precision: 7, storeType: "datetime2"),
                })
                .PrimaryKey(t => t.TeacherID);

            CreateTable(
                "Assignments.Files",
                c => new
                {
                    FileId = c.Int(nullable: false, identity: true),
                    Name = c.String(nullable: false, maxLength: 100),
                    Score = c.Double(),
                    Comment = c.String(maxLength: 500),
                    Submitted = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                    Graded = c.DateTime(precision: 7, storeType: "datetime2"),
                    AssignmentId = c.Int(nullable: false),
                    StudentId = c.Int(nullable: false),
                    Created = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                    Modified = c.DateTime(precision: 7, storeType: "datetime2"),
                })
                .PrimaryKey(t => t.FileId)
                .ForeignKey("Assignments.Assignments", t => t.AssignmentId, cascadeDelete: true)
                .ForeignKey("Persons.Students", t => t.StudentId, cascadeDelete: true)
                .Index(t => t.AssignmentId)
                .Index(t => t.StudentId);

            CreateTable(
                "dbo.BookClasses",
                c => new
                {
                    Book_BookId = c.Int(nullable: false),
                    Class_ClassId = c.Int(nullable: false),
                })
                .PrimaryKey(t => new { t.Book_BookId, t.Class_ClassId })
                .ForeignKey("Classes.Books", t => t.Book_BookId, cascadeDelete: true)
                .ForeignKey("Classes.Classes", t => t.Class_ClassId, cascadeDelete: true)
                .Index(t => t.Book_BookId)
                .Index(t => t.Class_ClassId);

            CreateTable(
                "dbo.StudentClasses",
                c => new
                {
                    Student_StudentId = c.Int(nullable: false),
                    Class_ClassId = c.Int(nullable: false),
                })
                .PrimaryKey(t => new { t.Student_StudentId, t.Class_ClassId })
                .ForeignKey("Persons.Students", t => t.Student_StudentId, cascadeDelete: true)
                .ForeignKey("Classes.Classes", t => t.Class_ClassId, cascadeDelete: true)
                .Index(t => t.Student_StudentId)
                .Index(t => t.Class_ClassId);

        }
        
        public override void Down()
        {
            DropForeignKey("Assignments.Files", "StudentId", "Persons.Students");
            DropForeignKey("Assignments.Files", "AssignmentId", "Assignments.Assignments");
            DropForeignKey("Classes.Classes", "TeacherId", "Persons.Teachers");
            DropForeignKey("Classes.Classes", "SubjectId", "Classes.Subjects");
            DropForeignKey("dbo.StudentClasses", "Class_ClassId", "Classes.Classes");
            DropForeignKey("dbo.StudentClasses", "Student_StudentId", "Persons.Students");
            DropForeignKey("dbo.BookClasses", "Class_ClassId", "Classes.Classes");
            DropForeignKey("dbo.BookClasses", "Book_BookId", "Classes.Books");
            DropForeignKey("Assignments.Assignments", "ClassId", "Classes.Classes");
            DropIndex("dbo.StudentClasses", new[] { "Class_ClassId" });
            DropIndex("dbo.StudentClasses", new[] { "Student_StudentId" });
            DropIndex("dbo.BookClasses", new[] { "Class_ClassId" });
            DropIndex("dbo.BookClasses", new[] { "Book_BookId" });
            DropIndex("Assignments.Files", new[] { "StudentId" });
            DropIndex("Assignments.Files", new[] { "AssignmentId" });
            DropIndex("Classes.Classes", new[] { "SubjectId" });
            DropIndex("Classes.Classes", new[] { "TeacherId" });
            DropIndex("Assignments.Assignments", new[] { "ClassId" });
            DropTable("dbo.StudentClasses");
            DropTable("dbo.BookClasses");
            DropTable("Assignments.Files");
            DropTable("Persons.Teachers");
            DropTable("Classes.Subjects");
            DropTable("Persons.Students");
            DropTable("Classes.Books");
            DropTable("Classes.Classes");
            DropTable("Assignments.Assignments");
        }
    }
}
