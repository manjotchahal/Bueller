using Bueller.Data;
using Bueller.Library.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bueller.Library.Repositories
{
    public class StudentRepository : Crud<Student>
    {
        private readonly IDbContext _context;
        public StudentRepository(IDbContext context) : base(context)
        {
            _context = context;
        }
    }
}
