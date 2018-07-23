using Bueller.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Bueller.API.Controllers
{
    [RoutePrefix("api/book")]
    public class BookController : ApiController
    {

        // GET: api/Assignments
        private readonly UnitOfWork unit = new UnitOfWork();
        private readonly BookRepository bookRepo;
        private CrossTable cross;

        public BookController()
        {
            bookRepo = unit.BookRepository();
            cross = new CrossTable();
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("GetAll")]
        public IHttpActionResult GetBooks()
        {
            var books = bookRepo.GetAll();
            if (!books.Any())
            {
                return Content(HttpStatusCode.NotFound, "List is empty");
            }
            return Ok(books);
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("GetBooksByClassId/{id}")]
        public IHttpActionResult GetBooksByClassId(int id)
        {
            var books = cross.GetBooksByClassId(id);
            if (!books.Any())
            {
                return Content(HttpStatusCode.NotFound, "List is empty");
            }
            return Ok(books);
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("GetClassesByBookId/{id}")]
        public IHttpActionResult GetClassesByBookId(int id)
        {
            var classes = cross.GetClassesByBookId(id);
            if (!classes.Any())
            {
                return Content(HttpStatusCode.NotFound, "List is empty");
            }
            return Ok(classes);
        }
    }
}
