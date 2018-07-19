using AutoMapper;
using Bueller.Data;
using Bueller.Library.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bueller.Library.Repositories
{
    public class AssignmentRepository : Crud<Assignment>
    {
        private readonly IDbContext _context;
        public AssignmentRepository(IDbContext context) : base(context)
        {
            _context = context;
        }

        public IEnumerable<Assignment> GetAssignmentsByClassId(int id)
        {
            //return this.Entities.Where(x => x.ClassId == id).ToList();
            return Table.Where(x => x.ClassId == id).ToList();
        }
        public IEnumerable<Assignment> GetAssignmentsByTeacherId(int id)
        {
            return Table.Where(x => x.Class.TeacherId == id).ToList();
        }

        public IEnumerable<Assignment> GetAssignmentsWithFiles()
        {
            return Table.Where(x => x.Files.Count > 0).ToList();
        }

        public IEnumerable<Assignment> GetAssignmentsByDueDate(DateTime duedate)
        {
            return Table.Where(x => x.DueDate == duedate).ToList();
        }
    }
}
