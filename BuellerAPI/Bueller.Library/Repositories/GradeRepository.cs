using Bueller.Data;
using Bueller.Library.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bueller.Library.Repositories
{
    public class GradeRepository : Crud<Grade>
    {
        private readonly IDbContext _context;
        public GradeRepository(IDbContext context) : base(context)
        {
            _context = context;
        }

        public IEnumerable<Grade> GetFailingGrades()
        {
            return this.Table.Where(x => x.LetterGrade.Equals("F"));
        }
    }
}
