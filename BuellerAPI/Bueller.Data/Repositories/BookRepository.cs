using AutoMapper;
using Bueller.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bueller.Data.Repositories
{
    public class BookRepository : Crud<Book>
    {
        private readonly IDbContext _context;
        public BookRepository(IDbContext context) : base(context)
        {
            _context = context;
        }

        public new Library.Models.Book GetById(object id) { return Mapper.Map<Library.Models.Book>(base.GetById(id)); }
        public void Insert(Library.Models.Book entity) { base.Insert(Mapper.Map<Book>(entity)); }
        public void Update(Library.Models.Book entity) { base.Update(Mapper.Map<Book>(entity)); }
        public void Delete(Library.Models.Book entity) { base.Delete(Mapper.Map<Book>(entity)); }
        public IEnumerable<Library.Models.Book> GetAll() { return Mapper.Map<IEnumerable<Library.Models.Book>>(Table.ToList()); }

        //move to cross table
        //public IEnumerable<Book> GetBookbyClassId(int id)
        //{
        //    var temp = this.Entities.Where(x => x.ClassId == id);
        //    return Mapper.Map<IEnumerable<Book>>(temp);
        //}
    }
}
