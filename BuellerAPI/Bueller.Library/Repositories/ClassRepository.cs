using Bueller.Data;
using Bueller.Library.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bueller.Library.Repositories
{
    public class ClassRepository : Crud<Class>
    {
        private readonly IDbContext _context;
        public ClassRepository(IDbContext context) : base(context)
        {
            _context = context;
        }

        public bool HasClassOnThisDay(string day, int classId)
        {
            var cls = this.Table.FirstOrDefault(x => x.ClassId == classId);
            switch (day)
            {
                case "Monday":
                    if (cls.Mon == 1)
                        return true;
                    break;
                case "Tuesday":
                    if (cls.Tues == 1)
                        return true;
                    break;
                case "Wednesday":
                    if (cls.Wed == 1)
                        return true;
                    break;
                case "Thursday":
                    if (cls.Thurs == 1)
                        return true;
                    break;
                case "Friday":
                    if (cls.Fri == 1)
                        return true;
                    break;
                default:
                    return false;
            }
            return false;
        }

        public int ConvertClassNameIntoId(string className)
        {
            return this.Table.Where(x => x.Name.StartsWith(className)).Select(x => x.ClassId).FirstOrDefault();
        }

        public IEnumerable<Class> GetClassesByTeacherId(int id)
        {
            return this.Table.Where(x => x.TeacherId == id).ToList();
        }

        public IEnumerable<Class> GetClassesByTeacherEmail(string email)
        {
            return this.Table.Where(x => x.Teacher.Email == email).ToList();
        }

        public IEnumerable<Class> GetClassesWithStudents()
        {
            return this.Table.Where(x => x.Students.Any()).ToList();
        }

        public int GetEnrollmentCount(int id)
        {
            return this.Table.FirstOrDefault(x => x.ClassId == id).Students.Count();
            //return this.Entities.Find(id).Students.Count();
        }

        public IEnumerable<Class> GetClassesWithAssignments()
        {
            return this.Table.Where(x => x.Assignments.Any()).ToList();
        }

        public IEnumerable<Class> GetClassesByCredits(int credits)
        {
            return this.Table.Where(x => x.Credits == credits).ToList();
        }

        public IEnumerable<Class> GetClassesByRoomNumber(string room)
        {
            return this.Table.Where(x => x.RoomNumber.Equals(room)).ToList();
        }
    }
}
