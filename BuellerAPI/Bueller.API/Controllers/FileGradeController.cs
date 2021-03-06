﻿using Bueller.Data.Repositories;
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
    [RoutePrefix("api")]
    public class FileGradeController : ApiController
    {
        private readonly UnitOfWork unit = new UnitOfWork();
        private readonly FileRepository fileRepo;

        public FileGradeController()
        {
            fileRepo = unit.FileRepository();
        }
        #region File
        [HttpGet]
        [Route("File/GetAll")]
        public IHttpActionResult GetAllFiles()
        {
            var files = fileRepo.GetAll();
            if (!files.Any())
            {
                return Content(HttpStatusCode.NotFound, "List is empty");
            }
            return Ok(files);
        }

        [HttpGet]
        [Route("File/GetById/{id}")]
        public IHttpActionResult GetFileById(int id)
        {
            var file = fileRepo.GetById(id);
            if (file == null)
            {
                return Content(HttpStatusCode.NotFound, "Item does not exist");
            }
            return Ok(file);
        }

        [HttpPost]
        [Route("File/Add", Name = "AddFile")]
        public IHttpActionResult Post(File file)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            fileRepo.Insert(file);

            return CreatedAtRoute("AddFile", new { id = file.FileId }, file);
        }

        [HttpPut]
        [Route("File/AddAt/{id}")]
        public IHttpActionResult Put(int id, File file)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != file.FileId)
            {
                return BadRequest();
            }
            try
            {
                fileRepo.Update(file);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FileExists(id))
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
        [Route("File/Delete/{id}")]
        public IHttpActionResult Delete(int id)
        {
            var file = fileRepo.GetById(id);
            if (file == null)
            {
                return Content(HttpStatusCode.NotFound, "Item does not exist");
            }
            fileRepo.Delete(id);
            return Ok();
        }

        [HttpGet]
        [Route("File/GetByStudentId/{id}")]
        public IHttpActionResult GetFilesByStudentId(int id)
        {
            var files = fileRepo.GetFilesByStudentId(id).ToList();
            if (!files.Any())
            {
                return Content(HttpStatusCode.NotFound, "List is empty");
            }
            return Ok(files);
        }

        [HttpGet]
        [Route("File/GetByName/{name}")]
        public IHttpActionResult GetFilesByName(string name)
        {
            var files = fileRepo.GetFilesByName(name).ToList();
            if (!files.Any())
            {
                return Content(HttpStatusCode.NotFound, "List is empty");
            }
            return Ok(files);
        }

        [HttpGet]
        [Route("File/GetByClassId/{classId}")]
        public IHttpActionResult GetFilesByClassId(int classId)
        {
            var files = fileRepo.GetFilesByClassId(classId).ToList();
            if (!files.Any())
            {
                return Content(HttpStatusCode.NotFound, "List is empty");
            }
            return Ok(files);
        }

        [HttpGet]
        [Route("File/GetByAsnIdAndStudentId/{studentId}/{assignmentId}")]
        public IHttpActionResult GetByAsnIdAndStudentId(int studentId, int assignmentId)
        {
            var files = fileRepo.GetByAsnIdAndStudentId(studentId, assignmentId).ToList();
            if (!files.Any())
            {
                return Content(HttpStatusCode.NotFound, "List is empty");
            }

            return Ok(files);
        }

        [HttpGet]
        [Route("File/GetByAssignmentId/{id}")]
        public IHttpActionResult GetByAssignmentId(int id)
        {
            var files = fileRepo.GetByAssignmentId(id).ToList();
            if (!files.Any())
            {
                return Content(HttpStatusCode.NotFound, "List is empty");
            }

            return Ok(files);
        }

        private bool FileExists(int id)
        {
            return fileRepo.FileExists(id);
        }
        #endregion
        #region Grade
        //[HttpGet]
        //[Route("Grade/GetAll")]
        //public IHttpActionResult GetAllGrades()
        //{
        //    var grades = gradeRepo.GetAll();
        //    if (!grades.Any())
        //    {
        //        return Content(HttpStatusCode.NotFound, "List is empty");
        //    }
        //    return Ok(grades);
        //}

        //[HttpGet]
        //[Route("Grade/GetById/{id}")]
        //public IHttpActionResult GetGradeById(int id)
        //{
        //    var grade = gradeRepo.GetById(id);
        //    if (grade == null)
        //    {
        //        return Content(HttpStatusCode.NotFound, "Item does not exist");
        //    }
        //    return Ok(grade);
        //}

        //[HttpPost]
        //[Route("Grade/Add", Name = "AddGrade")]
        //public IHttpActionResult AddGrade(Grade grade)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    gradeRepo.Insert(grade);

        //    return CreatedAtRoute("AddGrade", new { id = grade.GradeId }, grade);
        //}

        //[HttpPut]
        //[Route("Grade/AddAt/{id}")]
        //public IHttpActionResult AddGradeAt(int id, Grade grade)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    if (id != grade.GradeId)
        //    {
        //        return BadRequest();
        //    }

        //    try
        //    {
        //        gradeRepo.Update(grade);
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!GradeExists(id))
        //        {
        //            return Content(HttpStatusCode.NotFound, "Item does not exist");
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }
        //    return StatusCode(HttpStatusCode.NoContent);
        //}

        //[HttpDelete]
        //[Route("Grade/Delete/{id}")]
        //public IHttpActionResult DeleteGrade(int id)
        //{
        //    var grade = gradeRepo.GetById(id);
        //    if (grade == null)
        //    {
        //        return Content(HttpStatusCode.NotFound, "Item does not exist");
        //    }
        //    gradeRepo.Delete(id);

        //    return Ok();
        //}

        //[HttpGet]
        //[Route("Grade/GetFailing")]
        //public IHttpActionResult GetFailingGrade()
        //{
        //    var grades = gradeRepo.GetFailingGrades().ToList();
        //    if (!grades.Any())
        //    {
        //        return Content(HttpStatusCode.NotFound, "List is empty");
        //    }
        //    return Ok(grades);
        //}



        //private bool GradeExists(int id)
        //{
        //    return gradeRepo.GradeExists(id);
        //}
        #endregion
    }
}
