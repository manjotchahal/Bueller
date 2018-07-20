using AutoMapper;
using Bueller.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Bueller.Data.Repositories
{
    public class AssignmentRepository : Crud<Assignment>
    {
        private readonly IDbContext _context;
        public AssignmentRepository(IDbContext context) : base(context)
        {
            _context = context;
        }

        public new Library.Models.Assignment GetById(object id) { return Mapper.Map<Library.Models.Assignment>(base.GetById(id)); }
        public void Insert(Library.Models.Assignment entity) { base.Insert(Mapper.Map<Assignment>(entity)); }
        public void Update(Library.Models.Assignment entity) { base.Update(Mapper.Map<Assignment>(entity)); }
        //public void Delete(Library.Models.Assignment entity) { base.Delete(Mapper.Map<Assignment>(entity)); }
        public IEnumerable<Library.Models.Assignment> GetAll() { return Mapper.Map<IEnumerable<Library.Models.Assignment>>(Table.ToList()); }

        public IEnumerable<Library.Models.Assignment> GetAssignmentsByClassId(int id)
        {
            //return this.Entities.Where(x => x.ClassId == id).ToList();
            return Mapper.Map<IEnumerable<Library.Models.Assignment>>(Table.Where(x => x.ClassId == id).ToList());
        }
        public IEnumerable<Library.Models.Assignment> GetAssignmentsByTeacherId(int id)
        {
            return Mapper.Map<IEnumerable<Library.Models.Assignment>>(Table.Where(x => x.Class.TeacherId == id).ToList());
        }

        public IEnumerable<Library.Models.Assignment> GetAssignmentsWithFiles()
        {
            return Mapper.Map<IEnumerable<Library.Models.Assignment>>(Table.Where(x => x.Files.Count > 0).ToList());
        }

        public IEnumerable<Library.Models.Assignment> GetAssignmentsByDueDate(DateTime duedate)
        {
            return Mapper.Map<IEnumerable<Library.Models.Assignment>>(Table.Where(x => x.DueDate == duedate).ToList());
        }
    }
}
