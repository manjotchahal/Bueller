using AutoMapper;
using Bueller.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bueller.Data.Repositories
{
    public class StudentRepository : Crud<Student>
    {
        private readonly IDbContext _context;
        public StudentRepository(IDbContext context) : base(context)
        {
            _context = context;
        }

        public new Library.Models.Student GetById(object id) { return Mapper.Map<Library.Models.Student>(base.GetById(id)); }
        public void Insert(Library.Models.Student entity) { base.Insert(Mapper.Map<Student>(entity)); }
        public void Update(Library.Models.Student entity) { base.Update(Mapper.Map<Student>(entity)); }
        public IEnumerable<Library.Models.Student> GetAll() { return Mapper.Map<IEnumerable<Library.Models.Student>>(Table.ToList()); }

        internal Student GetByIdDataObject (object id) { return base.GetById(id); }

        public Library.Models.Student GetStudentByEmail(string email)
        {
            return Mapper.Map<Library.Models.Student>(Table.Where(x => x.Email == email).FirstOrDefault());
        }

        public bool StudentExists(int id)
        {
            return Table.Any(a => a.StudentId == id);
        }
    }
}
