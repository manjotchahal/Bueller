using Bueller.Library.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bueller.Library.Repositories
{
    public class CrossTable
    {
        private readonly UnitOfWork unit = new UnitOfWork();
        private AssignmentRepository assignmentRepository;
        private BookRepository bookRepository;
        private ClassRepository classRepository;
        private FileRepository fileRepository;
        private GradeRepository gradeRepository;
        private StudentRepository studentRepository;
        private SubjectRepository subjectRepository;
        private TeacherRepository teacherRepository;

        public CrossTable()
        {
            assignmentRepository = unit.AssignmentRepository();
            bookRepository = unit.BookRepository();
            classRepository = unit.ClassRepository();
            fileRepository = unit.FileRepository();
            gradeRepository = unit.GradeRepository();
            studentRepository = unit.StudentRepository();
            subjectRepository = unit.SubjectRepository();
            teacherRepository = unit.TeacherRepository();
        }

        public void EnrollStudent(int classid, int studentid)
        {
            var student = studentRepository.GetById(studentid);
            var classresult = classRepository.GetById(classid);
            classresult.Students.Add(student);
            unit.SaveChanges();

            //var student1 = _context.Set<Student>().FirstOrDefault(s => s.FirstName == "bobby");
            //var class1 = _context.Set<Class>().Find(3);
            //class1.Students.Add(student1);

            //_context.SaveChanges();
        }

        public void AssignBook(int classid, int bookid)
        {
            var book = bookRepository.GetById(bookid);
            var classresult = classRepository.GetById(classid);
            classresult.Books.Add(book);
            unit.SaveChanges();
        }

        public IEnumerable<Grade> GetGradesByStudentId(int id)
        {
            var grades = gradeRepository.Table
                .Join(fileRepository.Table, x => x.FileId, y => y.FileId, (x, y) => new { Grade = x, File = y })
                .Where(xy => xy.File.StudentId == id);

            var result = new List<Grade>();

            foreach (var var in grades)
            {
                result.Add(var.Grade);
            }

            return result;
        }

        public IEnumerable<Student> GetStudentsByTeacherId(int id)
        {
            var classes = classRepository.Table.Where(x => x.TeacherId == id).ToList();
            return classes.SelectMany(x => x.Students).Distinct().ToList();
        }

        public IEnumerable<Teacher> GetTeachersByStudentId(int id)
        {
            var classes = GetClassesByStudentId(id);
            return classes.Select(x => x.Teacher).Where(x => x != null).Distinct().ToList();
        }

        public IEnumerable<Class> GetClassesByStudentId(int id)
        {
            return classRepository.Table.Where(x => x.Students.Any(y => y.StudentId == id)).ToList();
        }

        public IEnumerable<Student> GetStudentsByClassId(int id)
        {
            return studentRepository.Table.Where(x => x.Classes.Any(y => y.ClassId == id)).ToList();
        }

        public IEnumerable<Class> GetClassesByBookId(int id)
        {
            return classRepository.Table.Where(x => x.Books.Any(y => y.BookId == id)).ToList();
        }

        public IEnumerable<Book> GetBooksByClassId(int id)
        {
            return bookRepository.Table.Where(x => x.Classes.Any(y => y.ClassId == id)).ToList();
        }

        public Subject GetHome()
        {
            var result = new List<int> { studentRepository.Table.Count(), teacherRepository.Table.Count(), classRepository.Table.Count() };
            return new Subject()
            {
                Name = result[0] + "," + result[1] + "," + result[2]
            };
        }
    }
}
