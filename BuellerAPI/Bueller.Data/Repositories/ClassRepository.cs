using AutoMapper;
using Bueller.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bueller.Data.Repositories
{
    public class ClassRepository : Crud<Class>
    {
        private readonly IDbContext _context;
        public ClassRepository(IDbContext context) : base(context)
        {
            _context = context;
        }

        public new Library.Models.Class GetById(object id) { return Mapper.Map<Library.Models.Class>(base.GetById(id)); }
        public void Insert(Library.Models.Class entity) { base.Insert(Mapper.Map<Class>(entity)); }
        public void Update(Library.Models.Class entity) { base.Update(Mapper.Map<Class>(entity)); }
        public IEnumerable<Library.Models.Class> GetAll() { return Mapper.Map<IEnumerable<Library.Models.Class>>(Table.ToList()); }

        public int ConvertClassNameIntoId(string className)
        {
            return Table.Where(x => x.Name.StartsWith(className)).Select(x => x.ClassId).FirstOrDefault();
        }

        public IEnumerable<Library.Models.Class> GetClassesByTeacherId(int id)
        {
            return Mapper.Map<IEnumerable<Library.Models.Class>>(Table.Where(x => x.TeacherId == id).ToList());
        }

        public IEnumerable<Library.Models.Class> GetClassesByTeacherEmail(string email)
        {
            return Mapper.Map<IEnumerable<Library.Models.Class>>(Table.Where(x => x.Teacher.Email == email).ToList());
        }

        public IEnumerable<Library.Models.Class> GetClassesWithStudents()
        {
            return Mapper.Map<IEnumerable<Library.Models.Class>>(Table.Where(x => x.Students.Any()).ToList());
        }

        public int GetEnrollmentCount(int id)
        {
            return Table.FirstOrDefault(x => x.ClassId == id).Students.Count();
            //return this.Entities.Find(id).Students.Count();
        }

        public IEnumerable<Library.Models.Class> GetClassesWithAssignments()
        {
            return Mapper.Map<IEnumerable<Library.Models.Class>>(Table.Where(x => x.Assignments.Any()).ToList());
        }

        public IEnumerable<Library.Models.Class> GetClassesByCredits(int credits)
        {
            return Mapper.Map<IEnumerable<Library.Models.Class>>(Table.Where(x => x.Credits == credits).ToList());
        }

        public IEnumerable<Library.Models.Class> GetClassesByRoomNumber(string room)
        {
            return Mapper.Map<IEnumerable<Library.Models.Class>>(Table.Where(x => x.RoomNumber.Equals(room)).ToList());
        }

        public bool HasClassOnThisDay(string day, int classId)
        {
            var cls = Table.FirstOrDefault(x => x.ClassId == classId);
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
    }
}
