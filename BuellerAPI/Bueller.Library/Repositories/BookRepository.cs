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
    public class BookRepository : Crud<Book>
    {
        private readonly IDbContext _context;
        public BookRepository(IDbContext context) : base(context)
        {
            _context = context;
        }

        //move to cross table
        //public IEnumerable<Book> GetBookbyClassId(int id)
        //{
        //    var temp = this.Entities.Where(x => x.ClassId == id);
        //    return Mapper.Map<IEnumerable<Book>>(temp);
        //}
    }
}
