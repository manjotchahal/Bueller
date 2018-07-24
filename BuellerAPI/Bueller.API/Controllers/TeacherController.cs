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
    [RoutePrefix("api/Teacher")]
    public class TeacherController : ApiController
    {
        private readonly UnitOfWork unit = new UnitOfWork();
        private readonly TeacherRepository repo;
        //private readonly EmployeeAccountRepo accountRepo;
        private CrossTable cross;

        TeacherController()
        {
            repo = unit.TeacherRepository();
            //accountRepo = unit.EmployeeAccountRepo();
            cross = new CrossTable();
        }

        #region Teachers
        [HttpGet]
        [Route("GetAll")]
        public IHttpActionResult GetTeachers()
        {
            var teachers = repo.GetAll();
            if (!teachers.Any())
            {
                return Content(HttpStatusCode.NotFound, "List is empty");
            }
            return Ok(teachers);
        }

        [HttpGet]
        [Route("GetById/{id}")]
        public IHttpActionResult GetTeacherById(int id)
        {
            var teacher = repo.GetById(id);
            if (teacher == null)
            {
                return Content(HttpStatusCode.NotFound, "Item does not exist");
            }
            return Ok(teacher);
        }

        [HttpGet]
        [Route("GetByEmail/{email}/")]
        public IHttpActionResult GetTeacherByEmail(string email)
        {
            var teacher = repo.GetTeacherByEmail(email);
            if (teacher == null)
            {
                return Content(HttpStatusCode.NotFound, "Item does not exist");
            }
            return Ok(teacher);
        }

        [HttpPost]
        [Route("Add", Name = "AddTeacher")]
        public IHttpActionResult AddTeacher(Teacher teacher)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            repo.Insert(teacher);

            return CreatedAtRoute("AddTeacher", new { id = teacher.TeacherID }, teacher);
        }

        [HttpPut]
        [Route("AddAt/{id}")]
        public IHttpActionResult UpdateTeacher(int id, Teacher teacher)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != teacher.TeacherID)
            {
                return BadRequest();
            }

            try
            {
                repo.Update(teacher);
            }

            catch (DbUpdateConcurrencyException)
            {
                if (!TeacherExists(id))
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
        public IHttpActionResult DeleteTeacher(int id)
        {
            var teacher = repo.GetById(id);
            if (teacher == null)
            {
                return Content(HttpStatusCode.NotFound, "Item does not exist");
            }

            repo.Delete(id);

            return Ok();
        }

        //[HttpGet]
        //[Route("Type/{type}")]
        //public IHttpActionResult GetEmployeesByType(string type)
        //{
        //    var employees = repo.GetEmployeesByType(type);
        //    if (!employees.Any())
        //    {
        //        return Content(HttpStatusCode.NotFound, "List is empty");
        //    }

        //    return Ok(employees);
        //}

        [HttpGet]
        [Route("Name")]
        public IHttpActionResult GetTeachersByNameAscending()
        {
            var teachers = repo.GetTeachersByNameAscending();
            if (!teachers.Any())
            {
                return Content(HttpStatusCode.NotFound, "List is empty");
            }

            return Ok(teachers);
        }

        [HttpGet]
        [Route("GetStudentsByTeacherId/{id}")]
        public IHttpActionResult GetStudentsByTeacherId(int id)
        {
            var students = cross.GetStudentsByTeacherId(id);
            if (!students.Any())
            {
                return Content(HttpStatusCode.NotFound, "List is empty");
            }

            return Ok(students);
        }

        private bool TeacherExists(int id)
        {
            return repo.TeacherExists(id);
        }

        #endregion
        #region Employee Accounts
        //[HttpGet]
        //[Route("Account/GetAll")]
        //public IHttpActionResult GetEmployeeAccounts()
        //{
        //    var accounts = accountRepo.Table.ToList();
        //    if (!accounts.Any())
        //    {
        //        return Content(HttpStatusCode.NotFound, "List is empty");
        //    }
        //    return Ok(accounts);
        //}

        //[HttpPost]
        //[Route("Account/Add", Name = "AddEmplyeeAccount")]
        //public IHttpActionResult AddEmployeeAccount(int employeeID, EmployeeAccountDto employeeAccount)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    accountRepo.Insert(Mapper.Map<EmployeeAccount>(employeeAccount));

        //    return CreatedAtRoute("AddEmployeeAccount", new { id = employeeAccount.EmployeeAccountId }, employeeAccount);
        //}

        //[HttpDelete]
        //[Route("Account/Delete/{id}")]
        //public IHttpActionResult DeleteEmployeeAccount(int id)
        //{
        //    var employeeAccount = accountRepo.GetById(id);
        //    if (employeeAccount == null)
        //    {
        //        return Content(HttpStatusCode.NotFound, "Item does not exist");
        //    }

        //    accountRepo.Delete(employeeAccount);

        //    return Ok(employeeAccount);
        //}

        //[HttpGet]
        //[Route("Account/GetById/{id}")]
        //public IHttpActionResult GetEmployeeAccountById(int id)
        //{
        //    var employeeAccount = accountRepo.GetById(id);
        //    if (employeeAccount == null)
        //    {
        //        return Content(HttpStatusCode.NotFound, "Item does not exist");
        //    }
        //    return Ok(employeeAccount);
        //}

        //[HttpGet]
        //[Route("Account/GetByEmployeeId/{id}")]
        //public IHttpActionResult GetAccountByEmployeeId(int id)
        //{
        //    var employeeAccount = accountRepo.GetAccountByEmployeeId(id);
        //    if (employeeAccount == null)
        //    {
        //        return Content(HttpStatusCode.NotFound, "Item does not exist");
        //    }
        //    return Ok(employeeAccount);
        //}

        //[HttpGet]
        //[Route("Account/GetByPayPeriod/{period}")]
        //public IHttpActionResult GetAccountsByPayPeriod(string period)
        //{
        //    var employeeAccounts = accountRepo.GetAccountsByPayPeriod(period);
        //    if (!employeeAccounts.Any())
        //    {
        //        return Content(HttpStatusCode.NotFound, "List is empty");
        //    }
        //    return Ok(employeeAccounts);
        //}

        //[HttpPut]
        //[Route("Account/AddAt/{id}")]
        //public IHttpActionResult AddAccountAt(int id, EmployeeAccountDto employeeAccount)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }
        //    if (id != employeeAccount.EmployeeAccountId)
        //    {
        //        return BadRequest();
        //    }

        //    try
        //    {
        //        accountRepo.Update(Mapper.Map<EmployeeAccount>(employeeAccount));
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!EmployeeAccountExists(id))
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

        //private bool EmployeeAccountExists(int id)
        //{
        //    return accountRepo.Table.Count(e => e.EmployeeAccountId == id) > 0;
        //}
        #endregion
    }
}
