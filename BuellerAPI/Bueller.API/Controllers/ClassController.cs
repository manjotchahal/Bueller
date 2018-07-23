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
    [RoutePrefix("api/Class")]
    public class ClassController : ApiController
    {
        private readonly UnitOfWork unit = new UnitOfWork();
        private readonly ClassRepository classRepo;
        private readonly SubjectRepository subjectRepo;
        private CrossTable cross;

        public ClassController()
        {
            classRepo = unit.ClassRepository();
            subjectRepo = unit.SubjectRepository();
            cross = new CrossTable();
        }

        #region Class

        [HttpGet]
        [Route("GetAll")]
        public IHttpActionResult GetAllClasses()
        {
            var classes = classRepo.GetAll();
            if (!classes.Any())
            {
                return Content(HttpStatusCode.NotFound, "List is empty");
            }

            return Ok(classes);
        }


        [HttpGet]
        [Route("GetByTeacherId/{id}/")]
        public IHttpActionResult GetClassByTeacherId(int id)
        {
            var classes = classRepo.GetClassesByTeacherId(id);
            if (!classes.Any())
            {
                return Content(HttpStatusCode.NotFound, "List is empty");
            }

            return Ok(classes);
        }


        [HttpGet]
        [Route("GetById/{id}")]
        public IHttpActionResult Get(int id)
        {
            var result = classRepo.GetById(id);
            if (result == null)
            {
                return Content(HttpStatusCode.NotFound, "Item does not exist");
            }
            return Ok(result);
        }

        [HttpGet]
        [Route("GetByStudentId/{id}")]
        public IHttpActionResult StudentClasses(int id)
        {
            //studentRepo.AddToClass();
            //var classes = studentRepo.Table.Where(x => x.StudentId == id).SelectMany(x => x.Classes).ToList();
            //var classes = classRepo.Table.Where(x => x.Students.Any(a => a.StudentId == id)).ToList();
            var classes = cross.GetClassesByStudentId(id);
            if (!classes.Any())
            {
                return Content(HttpStatusCode.NotFound, "List is empty");
            }
            return Ok(classes);
        }

        [HttpGet]
        [Route("EnrollmentCount/{id}")]
        public IHttpActionResult EnrollmentCount(int id)
        {
            //studentRepo.AddToClass();
            //var classes = studentRepo.Table.Where(x => x.StudentId == id).SelectMany(x => x.Classes).ToList();
            //var count = classRepo.GetEnrollmentCount(id);
            if (ClassExists(id))
            {
                return Ok(classRepo.GetEnrollmentCount(id));
            }
            return Content(HttpStatusCode.NotFound, "Item does not exist");
        }

        [HttpGet]
        [Route("Enroll/{id}/{studentid}")]
        public IHttpActionResult EnrollStudent(int id, int studentid)
        {
            //studentRepo.AddToClass();
            //var classes = studentRepo.Table.Where(x => x.StudentId == id).SelectMany(x => x.Classes).ToList();
            //var classes = classRepo.Table.Where(x => x.Students.Any(a => a.StudentId == id)).ToList();
            //if (classes != null)
            //{
            //    return Ok(classes);
            //}
            //return NotFound();
            cross.EnrollStudent(id, studentid);
            return Ok();
        }


        [HttpPost]
        [Authorize(Roles = "teacher")]
        [Route("Add", Name = "AddClass")]
        public IHttpActionResult Post(Class classDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            classRepo.Insert(classDto);

            return CreatedAtRoute("AddClass", new { id = classDto.ClassId }, classDto);
        }

        [HttpPut]
        [Authorize(Roles = "teacher")]
        [Route("Edit/{id}")]
        public IHttpActionResult Edit(int id, Class classDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != classDto.ClassId)
            {
                return Content(HttpStatusCode.NotFound, "Item does not exist");
            }

            try
            {
                classRepo.Update(classDto);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ClassExists(id))
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
        [Authorize(Roles = "teacher")]
        [Route("Delete/{id}")]
        public IHttpActionResult Delete(int id)
        {
            var result = classRepo.GetById(id);
            if (result == null)
            {
                return Content(HttpStatusCode.NotFound, "Item does not exist");
            }

            classRepo.Delete(id);

            return Ok();
        }

        private bool ClassExists(int id)
        {
            return classRepo.ClassExists(id);
        }

        #endregion
        #region Subject

        [HttpGet]
        [Route("Subject/GetAll")]
        public IHttpActionResult GetAllSubjects()
        {
            var subjects = subjectRepo.GetAll();
            if (!subjects.Any())
            {
                return Content(HttpStatusCode.NotFound, "List is empty");
            }

            return Ok(subjects);
        }

        [HttpGet]
        [Route("Subject/GetAllNames")]
        public IHttpActionResult GetAllSubjectNames()
        {
            var subjects = subjectRepo.GetAllNames();
            if (!subjects.Any())
            {
                return Content(HttpStatusCode.NotFound, "List is empty");
            }

            return Ok(subjects);
        }

        [HttpGet]
        [Route("Subject/GetById/{id}")]
        public IHttpActionResult GetSubjectById(int id)
        {
            var subject = subjectRepo.GetById(id);
            if (subject == null)
            {
                return Content(HttpStatusCode.NotFound, "Item does not exist");
            }

            return Ok(subject);
        }

        [HttpGet]
        [Route("Subject/GetByName/{name}")]
        public IHttpActionResult GetSubjectByName(string name)
        {
            var subject = subjectRepo.GetByName(name);
            if (subject == null)
            {
                return Content(HttpStatusCode.NotFound, "Item does not exist");
            }

            return Ok(subject);
        }

        [HttpPost]
        [Route("Subject/Add", Name = "AddSubject")]
        public IHttpActionResult AddSubject(Subject subjectDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            subjectRepo.Insert(subjectDto);

            return CreatedAtRoute("AddSubject", new { id = subjectDto.SubjectId }, subjectDto);
        }

        [HttpPut]
        [Route("Subject/AddAt/{id}")]
        public IHttpActionResult AddSubjectAt(int id, Subject subjectDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != subjectDto.SubjectId)
            {
                return BadRequest();
            }

            try
            {
                subjectRepo.Update(subjectDto);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SubjectExists(id))
                {
                    return Content(HttpStatusCode.NotFound, "Item does not exist");
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.OK);
        }

        [HttpDelete]
        [Route("Subject/Delete/{id}")]
        public IHttpActionResult DeleteSubject(int id)
        {
            var subject = subjectRepo.GetById(id);
            if (subject == null)
            {
                return Content(HttpStatusCode.NotFound, "Item does not exist");
            }

            subjectRepo.Delete(id);

            return Ok();
        }

        private bool SubjectExists(int id)
        {
            return subjectRepo.SubjectExists(id);
        }



        #endregion
    }
}
