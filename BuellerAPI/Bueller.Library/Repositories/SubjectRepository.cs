using Bueller.Data;
using Bueller.Library.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bueller.Library.Repositories
{
    public class SubjectRepository : Crud<Subject>
    {
        private readonly IDbContext _context;
        public SubjectRepository(IDbContext context) : base(context)
        {
            _context = context;
        }

        public Subject GetByName(string name)
        {
            return this.Table.Where(x => x.Name == name).FirstOrDefault();
        }

        public IEnumerable<Subject> GetSubjectsByDepartment(string department)
        {
            return this.Table.Where(x => x.Department.Equals(department)).ToList();
        }
    }
}
