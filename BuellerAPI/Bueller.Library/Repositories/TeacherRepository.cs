using Bueller.Data;
using Bueller.Library.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bueller.Library.Repositories
{
    public class TeacherRepository : Crud<Teacher>
    {
        private readonly IDbContext _context;
        public TeacherRepository(IDbContext context) : base(context)
        {
            _context = context;
        }

        public Teacher GetEmployeeByEmail(string email)
        {
            return this.Table.Where(x => x.Email == email).FirstOrDefault();
        }

        public IEnumerable<Teacher> GetEmployeesByNameAscending()
        {
            return this.Entities.OrderBy(x => x.FirstName).ToList();
        }
    }
}
