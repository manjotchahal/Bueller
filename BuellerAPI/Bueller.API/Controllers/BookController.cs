using Bueller.Data.Repositories;
using Bueller.Library.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
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
        [Route("GetById/{id}")]
        public IHttpActionResult Get(int id)
        {
            var result = bookRepo.GetById(id);
            if (result == null)
            {
                return Content(HttpStatusCode.NotFound, "Item does not exist");
            }
            return Ok(result);
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("Add", Name = "AddBook")]
        public IHttpActionResult Post(Book bookDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            bookRepo.Insert(bookDto);

            return CreatedAtRoute("AddClass", new { id = bookDto.BookId }, bookDto);
        }

        [HttpPut]
        [AllowAnonymous]
        [Route("Edit/{id}")]
        public IHttpActionResult Edit(int id, Book book)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != book.BookId)
            {
                return Content(HttpStatusCode.NotFound, "Item does not exist");
            }

            try
            {
                bookRepo.Update(book);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BookExists(id))
                {
                    return Content(HttpStatusCode.NotFound, "Item does not exist");
                }
                else
                {
                    throw;
                }
            }
            return StatusCode(HttpStatusCode.NoContent);
        }

        [HttpDelete]
        [AllowAnonymous]
        [Route("Delete/{id}")]
        public IHttpActionResult Delete(int id)
        {
            var result = bookRepo.GetById(id);
            if (result == null)
            {
                return Content(HttpStatusCode.NotFound, "Item does not exist");
            }

            bookRepo.Delete(id);

            return Ok();
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

        [HttpGet]
        [AllowAnonymous]
        [Route("Assign/{classid}/{bookid}")]
        public IHttpActionResult AssignBook(int classid, int bookid)
        {
            cross.AssignBook(classid, bookid);
            return Ok();
        }

        private bool BookExists(int id)
        {
            return bookRepo.BookExists(id);
        }
    }
}
