using Bueller.Data;
using Bueller.Library.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bueller.Library.Repositories
{
    public class FileRepository : Crud<File>
    {
        private readonly IDbContext _context;
        public FileRepository(IDbContext context) : base(context)
        {
            _context = context;
        }

        public IEnumerable<File> GetFilesByStudentId(int id)
        {
            return this.Table.Where(x => x.StudentId == id).ToList();
        }
        public IEnumerable<File> GetFilesByName(string name)
        {
            return this.Table.Where(x => x.Name == name).ToList();
        }

        public IEnumerable<File> GetFilesByClassId(int classId)
        {
            return this.Table.Where(x => x.Assignment.ClassId == classId).ToList();
        }

        public IEnumerable<File> GetByAsnIdAndStudentId(int studentId, int assignmentId)
        {
            var temp = this.Table.Where(x => x.StudentId == studentId);
            return temp.Where(y => y.AssignmentId == assignmentId).ToList();
        }

        public IEnumerable<File> GetByAssignmentId(int id)
        {
            return this.Table.Where(x => x.AssignmentId == id).ToList();
        }
    }
}
