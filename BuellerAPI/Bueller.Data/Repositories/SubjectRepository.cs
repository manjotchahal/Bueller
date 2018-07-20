using AutoMapper;
using Bueller.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bueller.Data.Repositories
{
    public class SubjectRepository : Crud<Subject>
    {
        private readonly IDbContext _context;
        public SubjectRepository(IDbContext context) : base(context)
        {
            _context = context;
        }

        public new Library.Models.Subject GetById(object id) { return Mapper.Map<Library.Models.Subject>(base.GetById(id)); }
        public void Insert(Library.Models.Subject entity) { base.Insert(Mapper.Map<Subject>(entity)); }
        public void Update(Library.Models.Subject entity) { base.Update(Mapper.Map<Subject>(entity)); }
        public IEnumerable<Library.Models.Subject> GetAll() { return Mapper.Map<IEnumerable<Library.Models.Subject>>(Table.ToList()); }

        public Library.Models.Subject GetByName(string name)
        {
            return Mapper.Map<Library.Models.Subject>(Table.Where(x => x.Name == name).FirstOrDefault());
        }

        public IEnumerable<Library.Models.Subject> GetSubjectsByDepartment(string department)
        {
            return Mapper.Map<IEnumerable<Library.Models.Subject>>(Table.Where(x => x.Department.Equals(department)).ToList());
        }

        public IEnumerable<string> GetAllNames()
        {
            return Table.Select(t => t.Name).ToList();
        }

        public bool SubjectExists(int id)
        {
            return Table.Any(a => a.SubjectId == id);
        }
    }
}
