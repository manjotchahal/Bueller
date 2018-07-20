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
    [RoutePrefix("api/Employee")]
    public class EmployeeController : ApiController
    {
        private readonly UnitOfWork unit = new UnitOfWork();
        private readonly TeacherRepository repo;
        //private readonly EmployeeAccountRepo accountRepo;
        private CrossTable cross;

        EmployeeController()
        {
            repo = unit.TeacherRepository();
            //accountRepo = unit.EmployeeAccountRepo();
            cross = new CrossTable();
        }

        #region Employees
        [HttpGet]
        [Route("GetAll")]
        public IHttpActionResult GetEmployees()
        {
            var employees = repo.GetAll();
            if (!employees.Any())
            {
                return Content(HttpStatusCode.NoContent, "List is empty");
            }
            return Ok(employees);
        }

        [HttpGet]
        [Route("GetById/{id}")]
        public IHttpActionResult GetEmployeeById(int id)
        {
            var employee = repo.GetById(id);
            if (employee == null)
            {
                return Content(HttpStatusCode.NotFound, "Item does not exist");
            }
            return Ok(employee);
        }

        [HttpGet]
        [Route("GetByEmail/{email}/")]
        public IHttpActionResult GetEmployeeByEmail(string email)
        {
            var employee = repo.GetTeacherByEmail(email);
            if (employee == null)
            {
                return Content(HttpStatusCode.NotFound, "Item does not exist");
            }
            return Ok(employee);
        }

        [HttpPost]
        [Route("Add", Name = "AddEmployee")]
        public IHttpActionResult AddEmployee(Teacher employee)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            repo.Insert(employee);

            return CreatedAtRoute("AddEmployee", new { id = employee.TeacherID }, employee);
        }

        [HttpPut]
        [Route("AddAt/{id}")]
        public IHttpActionResult UpdateEmployee(int id, Teacher employee)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != employee.TeacherID)
            {
                return BadRequest();
            }

            try
            {
                repo.Update(employee);
            }

            catch (DbUpdateConcurrencyException)
            {
                if (!EmployeeExists(id))
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
        public IHttpActionResult DeleteEmployee(int id)
        {
            var employee = repo.GetById(id);
            if (employee == null)
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
        public IHttpActionResult GetEmployeesByNameAscending()
        {
            var employees = repo.GetTeachersByNameAscending();
            if (!employees.Any())
            {
                return Content(HttpStatusCode.NoContent, "List is empty");
            }

            return Ok(employees);
        }

        [HttpGet]
        [Route("GetStudentsByTeacherId/{id}")]
        public IHttpActionResult GetStudentsByTeacherId(int id)
        {
            var students = cross.GetStudentsByTeacherId(id);
            if (!students.Any())
            {
                return Content(HttpStatusCode.NoContent, "List is empty");
            }

            return Ok(students);
        }

        private bool EmployeeExists(int id)
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
