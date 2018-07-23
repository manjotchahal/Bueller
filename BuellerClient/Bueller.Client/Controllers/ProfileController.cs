using Bueller.Client.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Bueller.Client.Controllers
{
    public class ProfileController : ServiceController
    {
        // GET: Profile
        public async Task<ActionResult> Index()
        {
            var role = Request.Cookies["Role"].Value;

            if (role == "teacher" || role == "employee")
            {
                //Employee emp = await apiResponse.Content.ReadAsAsync<Employee>();
                return RedirectToAction("Teacher");
            }
            if (role == "student")
            {
                //Student stu = await apiResponse.Content.ReadAsAsync<Student>();
                return RedirectToAction("Student");
            }

            return View("Error");
        }

        // GET: Profile/Details/5
        public async Task<ActionResult> Teacher()
        {
            var role = Request.Cookies["Role"].Value;

            if (role != "teacher" && role != "employee")
            {
                return View("Error");
            }

            var id = Request.Cookies["Id"].Value;

            HttpRequestMessage apiRequest = CreateRequestToService(HttpMethod.Get, $"api/Employee/GetById/{id}");
            HttpResponseMessage apiResponse;

            try
            {
                apiResponse = await HttpClient.SendAsync(apiRequest);
            }
            catch
            {
                return View("Error");
            }

            if (!apiResponse.IsSuccessStatusCode)
            {
                return View("Error");
            }

            Teacher emp = await apiResponse.Content.ReadAsAsync<Teacher>();

            if (emp == null)
                return View("Error");

            return View(emp);
        }

        // GET: Profile/Details/5
        public async Task<ActionResult> Student()
        {
            var role = Request.Cookies["Role"].Value;

            if (role != "student")
            {
                return View("Error");
            }

            var id = Request.Cookies["Id"].Value;

            HttpRequestMessage apiRequest = CreateRequestToService(HttpMethod.Get, $"api/Student/GetById/{id}");
            HttpResponseMessage apiResponse;

            try
            {
                apiResponse = await HttpClient.SendAsync(apiRequest);
            }
            catch
            {
                return View("Error");
            }

            if (!apiResponse.IsSuccessStatusCode)
            {
                return View("Error");
            }

            Student stu = await apiResponse.Content.ReadAsAsync<Student>();

            if (stu == null)
                return View("Error");

            return View(stu);
        }

        // GET: Profile/Edit/5
        public async Task<ActionResult> EditTeacher()
        {
            var role = Request.Cookies["Role"].Value;

            if (role != "teacher" && role != "employee")
            {
                return View("Error");
            }

            var id = Request.Cookies["Id"].Value;

            HttpRequestMessage apiRequest = CreateRequestToService(HttpMethod.Get, $"api/Employee/GetById/{id}");
            HttpResponseMessage apiResponse;

            try
            {
                apiResponse = await HttpClient.SendAsync(apiRequest);
            }
            catch
            {
                return View("Error");
            }

            if (!apiResponse.IsSuccessStatusCode)
            {
                return View("Error");
            }

            Teacher emp = await apiResponse.Content.ReadAsAsync<Teacher>();

            if (emp == null)
                return View("Error");

            return View(emp);
        }

        [HttpPost]
        public async Task<ActionResult> EditTeacher(Teacher employee)
        {
            var role = Request.Cookies["Role"].Value;

            if (role != "teacher" && role != "employee")
            {
                return View("Error");
            }

            //if (role == "teacher")
            //    employee.EmployeeType = role;

            var id = Request.Cookies["Id"].Value;
            var email = Request.Cookies["userEmailCookie"].Value;

            employee.Email = email;
            employee.TeacherID = Convert.ToInt32(id);

            //if (!ModelState.IsValid)
            //{
            //    foreach (var modelError in ModelState)
            //    {
            //        string propertyName = modelError.Key;
            //        if (modelError.Value.Errors.Count > 0)
            //        {
            //            Console.WriteLine(propertyName);
            //            Console.WriteLine(modelError.Value.Errors);
            //        }
            //    }
            //    return View("Error");
            //}

            HttpRequestMessage apiRequest = CreateRequestToService(HttpMethod.Put, $"api/Employee/AddAt/{id}");
            apiRequest.Content = new ObjectContent<Teacher>(employee, new JsonMediaTypeFormatter());
            HttpResponseMessage apiResponse;

            try
            {
                apiResponse = await HttpClient.SendAsync(apiRequest);
            }
            catch
            {
                return View("Error");
            }

            if (!apiResponse.IsSuccessStatusCode)
            {
                return View("Error");
            }

            return RedirectToAction("Index");
        }

        // GET: Profile/Edit/5
        public async Task<ActionResult> EditStudent()
        {
            var role = Request.Cookies["Role"].Value;

            if (role != "student")
            {
                return View("Error");
            }

            var id = Request.Cookies["Id"].Value;

            HttpRequestMessage apiRequest = CreateRequestToService(HttpMethod.Get, $"api/Student/GetById/{id}");
            HttpResponseMessage apiResponse;

            try
            {
                apiResponse = await HttpClient.SendAsync(apiRequest);
            }
            catch
            {
                return View("Error");
            }

            if (!apiResponse.IsSuccessStatusCode)
            {
                return View("Error");
            }

            Student stu = await apiResponse.Content.ReadAsAsync<Student>();

            if (stu == null)
                return View("Error");

            return View(stu);
        }

        [HttpPost]
        public async Task<ActionResult> EditStudent(Student student)
        {
            var role = Request.Cookies["Role"].Value;

            if (role != "student")
            {
                return View("Error");
            }

            var id = Request.Cookies["Id"].Value;
            var email = Request.Cookies["userEmailCookie"].Value;

            student.Email = email;
            student.StudentId = Convert.ToInt32(id);

            HttpRequestMessage apiRequest = CreateRequestToService(HttpMethod.Put, $"api/Student/AddAt/{id}");
            apiRequest.Content = new ObjectContent<Student>(student, new JsonMediaTypeFormatter());
            HttpResponseMessage apiResponse;

            try
            {
                apiResponse = await HttpClient.SendAsync(apiRequest);
            }
            catch
            {
                return View("Error");
            }

            if (!apiResponse.IsSuccessStatusCode)
            {
                return View("Error");
            }

            return RedirectToAction("Index");
        }

        // GET: Profile/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Profile/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}