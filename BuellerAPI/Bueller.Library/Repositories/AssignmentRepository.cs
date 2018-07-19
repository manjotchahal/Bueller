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
    public class AssignmentRepo : Crud<Assignment>
    {
        private readonly IDbContext _context;
        public AssignmentRepo(IDbContext context) : base(context)
        {
            _context = context;
        }

        public IEnumerable<Assignment> GetAssignmentsByClassId(int id)
        {
            //return this.Entities.Where(x => x.ClassId == id).ToList();
            var assignments = Entities.Where(x => x.ClassId == id).ToList();
            return Mapper.Map<IEnumerable<Assignment>>(assignments);
        }
        public IEnumerable<Assignment> GetAssignmentsByTeacherId(int id)
        {
            var assignments = Entities.Where(x => x.Class.TeacherId == id).ToList();
            return Mapper.Map<IEnumerable<Assignment>>(assignments);
        }

        public IEnumerable<Assignment> GetAssignmentsWithFiles()
        {
            var assignments = Entities.Where(x => x.Files.Count > 0).ToList();
            return Mapper.Map<IEnumerable<Assignment>>(assignments);
        }

        public IEnumerable<Assignment> GetAssignmentsByDueDate(DateTime duedate)
        {
            var assignments = Entities.Where(x => x.DueDate == duedate).ToList();
            return Mapper.Map<IEnumerable<Assignment>>(assignments);
        }
    }
}
