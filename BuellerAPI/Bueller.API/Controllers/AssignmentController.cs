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
    [RoutePrefix("api/Assignment")]
    public class AssignmentController : ApiController
    {
        // GET: api/Assignments
        private readonly UnitOfWork unit = new UnitOfWork();
        private readonly AssignmentRepository assignmentRepo;

        public AssignmentController()
        {
            assignmentRepo = unit.AssignmentRepository();
        }

        [HttpGet]
        [Route("GetAll")]
        public IHttpActionResult GetAssignments()
        {
            var assignments = assignmentRepo.GetAll();
            if (!assignments.Any())
            {
                return Content(HttpStatusCode.NotFound, "List is empty");
            }
            return Ok(assignments);
        }

        [HttpGet]
        [Route("GetById/{id}")]
        public IHttpActionResult GetById(int id)
        {
            var assignment = assignmentRepo.GetById(id);
            if (assignment == null)
            {
                return Content(HttpStatusCode.NotFound, "Item does not exist");
            }
            return Ok(assignment);
        }
        [HttpGet]
        [Route("GetByTeacherId/{id}")]
        public IHttpActionResult GetByTeacherId(int id)
        {
            var assignment = assignmentRepo.GetAssignmentsByTeacherId(id);
            if (assignment == null)
            {
                return Content(HttpStatusCode.NotFound, "Item does not exist");
            }
            return Ok(assignment);
        }

        [HttpPost]
        [Route("Add", Name = "AddAssignment")]
        public IHttpActionResult Post(Assignment assignment)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            assignmentRepo.Insert(assignment);

            return CreatedAtRoute("AddAssignment", new { id = assignment.AssignmentId }, assignment);
        }

        [HttpPut]
        [Route("AddAt/{id}")]
        public IHttpActionResult Put(int id, Assignment assignment)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != assignment.AssignmentId)
            {
                return BadRequest();
            }

            try
            {
                assignmentRepo.Update(assignment);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AssignmentExists(id))
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
        [Route("Delete/{id}")]
        public IHttpActionResult Delete(int id)
        {
            var assignment = assignmentRepo.GetById(id);
            if (assignment == null)
            {
                return Content(HttpStatusCode.NotFound, "Item does not exist");
            }
            assignmentRepo.Delete(id);

            return Ok();
        }

        [HttpGet]
        [Route("GetByClassId/{id}")]
        public IHttpActionResult GetAssignmentsByClassId(int id)
        {
            var assignments = assignmentRepo.GetAssignmentsByClassId(id);
            if (!assignments.Any())
            {
                return Content(HttpStatusCode.NotFound, "List is empty");
            }
            return Ok(assignments);
        }

        [HttpGet]
        [Route("GetWithFiles")]
        public IHttpActionResult GetAssignmentsWithFiles()
        {
            var assignments = assignmentRepo.GetAssignmentsWithFiles();
            if (!assignments.Any())
            {
                return Content(HttpStatusCode.NotFound, "List is empty");
            }
            return Ok(assignments);
        }

        [HttpGet]
        [Route("GetByDueDate/{duedate}")]
        public IHttpActionResult GetAssignmentsByDueDate(DateTime duedate)
        {
            var assignments = assignmentRepo.GetAssignmentsByDueDate(duedate);
            if (!assignments.Any())
            {
                return Content(HttpStatusCode.NotFound, "List is empty");
            }
            return Ok(assignments);
        }

        private bool AssignmentExists(int id)
        {
            return assignmentRepo.AssignmentExists(id);
        }
    }
}
