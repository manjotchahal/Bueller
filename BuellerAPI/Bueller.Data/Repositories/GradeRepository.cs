using AutoMapper;
using Bueller.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bueller.Data.Repositories
{
    public class GradeRepository : Crud<Grade>
    {
        private readonly IDbContext _context;
        public GradeRepository(IDbContext context) : base(context)
        {
            _context = context;
        }

        public new Library.Models.Grade GetById(object id) { return Mapper.Map<Library.Models.Grade>(base.GetById(id)); }
        public void Insert(Library.Models.Grade entity) { base.Insert(Mapper.Map<Grade>(entity)); }
        public void Update(Library.Models.Grade entity) { base.Update(Mapper.Map<Grade>(entity)); }
        public IEnumerable<Library.Models.Grade> GetAll() { return Mapper.Map<IEnumerable<Library.Models.Grade>>(Table.ToList()); }

        public IEnumerable<Library.Models.Grade> GetFailingGrades()
        {
            return Mapper.Map<IEnumerable<Library.Models.Grade>>(Table.Where(x => x.LetterGrade.Equals("F")).ToList());
        }
    }
}
