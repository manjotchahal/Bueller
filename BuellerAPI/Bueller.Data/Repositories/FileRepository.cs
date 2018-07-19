using AutoMapper;
using Bueller.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bueller.Data.Repositories
{
    public class FileRepository : Crud<File>
    {
        private readonly IDbContext _context;
        public FileRepository(IDbContext context) : base(context)
        {
            _context = context;
        }

        public new Library.Models.File GetById(object id) { return Mapper.Map<Library.Models.File>(base.GetById(id)); }
        public void Insert(Library.Models.File entity) { base.Insert(Mapper.Map<File>(entity)); }
        public void Update(Library.Models.File entity) { base.Update(Mapper.Map<File>(entity)); }
        public void Delete(Library.Models.File entity) { base.Delete(Mapper.Map<File>(entity)); }
        public IEnumerable<Library.Models.File> GetAll() { return Mapper.Map<IEnumerable<Library.Models.File>>(Table.ToList()); }

        public IEnumerable<Library.Models.File> GetFilesByStudentId(int id)
        {
            return Mapper.Map<IEnumerable<Library.Models.File>>(Table.Where(x => x.StudentId == id).ToList());
        }
        public IEnumerable<Library.Models.File> GetFilesByName(string name)
        {
            return Mapper.Map<IEnumerable<Library.Models.File>>(Table.Where(x => x.Name == name).ToList());
        }

        public IEnumerable<Library.Models.File> GetFilesByClassId(int classId)
        {
            return Mapper.Map<IEnumerable<Library.Models.File>>(Table.Where(x => x.Assignment.ClassId == classId).ToList());
        }

        public IEnumerable<Library.Models.File> GetByAsnIdAndStudentId(int studentId, int assignmentId)
        {
            return Mapper.Map<IEnumerable<Library.Models.File>>(Table.Where(x => x.StudentId == studentId && x.AssignmentId == assignmentId).ToList());
        }

        public IEnumerable<Library.Models.File> GetByAssignmentId(int id)
        {
            return Mapper.Map<IEnumerable<Library.Models.File>>(Table.Where(x => x.AssignmentId == id).ToList());
        }
    }
}
