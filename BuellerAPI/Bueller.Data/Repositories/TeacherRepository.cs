using AutoMapper;
using Bueller.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bueller.Data.Repositories
{
    public class TeacherRepository : Crud<Teacher>
    {
        private readonly IDbContext _context;
        public TeacherRepository(IDbContext context) : base(context)
        {
            _context = context;
        }

        public new Library.Models.Teacher GetById(object id) { return Mapper.Map<Library.Models.Teacher>(base.GetById(id)); }
        public void Insert(Library.Models.Teacher entity) { base.Insert(Mapper.Map<Teacher>(entity)); }
        public void Update(Library.Models.Teacher entity) { base.Update(Mapper.Map<Teacher>(entity)); }
        public void Delete(Library.Models.Teacher entity) { base.Delete(Mapper.Map<Teacher>(entity)); }
        public IEnumerable<Library.Models.Teacher> GetAll() { return Mapper.Map<IEnumerable<Library.Models.Teacher>>(Table.ToList()); }

        public Library.Models.Teacher GetEmployeeByEmail(string email)
        {
            return Mapper.Map<Library.Models.Teacher>(Table.Where(x => x.Email == email).FirstOrDefault());
        }

        public IEnumerable<Library.Models.Teacher> GetEmployeesByNameAscending()
        {
            return Mapper.Map<IEnumerable<Library.Models.Teacher>>(Table.OrderBy(x => x.FirstName).ToList());
        }
    }
}
