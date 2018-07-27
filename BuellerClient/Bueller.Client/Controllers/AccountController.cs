using Bueller.Client.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Bueller.Client.Controllers
{
    public class AccountController : ServiceController
    {
        public ActionResult Register()
        {
            return View();
        }

        //ability to delete user accounts...
        //prevent register/additional login once logged in... important? and hide logout when not logged in?...
        [HttpPost]
        public ActionResult Register(Account account, string role)
        {
            if (!ModelState.IsValid)
            {
                return View("Error");
            }

            if (role == "student")
            {
                TempData["account"] = account;
                return RedirectToAction("RegisterStudentInfo", "Account");
            }
            else
            {
                TempData["account"] = account;
                return RedirectToAction("RegisterTeacherInfo", "Account"); //, new { email = account.Email/*, employeetype = role*/ });//or email
            }
        }

        public ActionResult RegisterStudentInfo(/*string email*/)
        {
            //AccountStudentViewModel model = (AccountStudentViewModel)TempData["student"];
            return View();
        }

        //not safe to pass role in url.. potential security problem if url modified after registering with role
        //solution: separate register employee and teacher
        //but also emails... use tempdata instead of passing data in URL???.... do later if time
        [Route("RegisterTeacherInfo")]
        public ActionResult RegisterTeacherInfo(/*string email*//*, string employeetype*/)
        {
            //AccountTeacherViewModel model = (AccountTeacherViewModel)TempData["teacher"];
            return View();
        }

        //prevent registering account only and backing out of creating corresponding model...
        // change register/login steps?
        //tie in account to person models
        [HttpPost]
        public async Task<ActionResult> RegisterStudentInfo(Student student)
        {
            string role = "student";
            Account account = (Account)TempData["account"];
            student.Email = account.Email;

            if (!ModelState.IsValid)
            {
                return View("Error");
            }

            HttpRequestMessage apiRequest = CreateRequestToService(HttpMethod.Post, $"api/Account/RegisterRole/{role}");
            apiRequest.Content = new ObjectContent<Account>(account, new JsonMediaTypeFormatter());

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

            PassCookiesToClient(apiResponse);

            HttpCookie userEmailCookie = new HttpCookie("userEmailCookie");
            userEmailCookie.Value = student.Email;

            Response.Cookies.Add(userEmailCookie);

            TempData["student"] = student;
            TempData["account"] = account;

            return RedirectToAction("RegisterStudent", "Account");
        }

        public async Task<ActionResult> RegisterStudent()
        {
            Student student = (Student)TempData["student"];

            HttpRequestMessage apiRequest2 = CreateRequestToService(HttpMethod.Post, $"api/Student/Add");
            apiRequest2.Content = new ObjectContent<Student>(student, new JsonMediaTypeFormatter());

            HttpResponseMessage apiResponse2;
            try
            {
                apiResponse2 = await HttpClient.SendAsync(apiRequest2);
            }
            catch
            {
                await DeleteAccount();
                return View("Error");
            }

            if (!apiResponse2.IsSuccessStatusCode)
            {
                await DeleteAccount();
                return View("Error");
            }

            PassCookiesToClient(apiResponse2);

            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public async Task<ActionResult> RegisterTeacherInfo(Teacher teacher)
        {
            //string role = (string)TempData.Peek("Role");
            //if (role == "teacher")
            //{
            //    employee.EmployeeType = "teacher";
            //}

            string role = "teacher";
            Account account = (Account)TempData["account"];
            teacher.Email = account.Email;

            if (!ModelState.IsValid)
            {
                return View("Error");
            }

            HttpRequestMessage apiRequest = CreateRequestToService(HttpMethod.Post, $"api/Account/RegisterRole/{role}");
            apiRequest.Content = new ObjectContent<Account>(account, new JsonMediaTypeFormatter());

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

            PassCookiesToClient(apiResponse);

            HttpCookie userEmailCookie = new HttpCookie("userEmailCookie");
            userEmailCookie.Value = teacher.Email;

            Response.Cookies.Add(userEmailCookie);

            TempData["teacher"] = teacher;
            TempData["account"] = account;

            return RedirectToAction("RegisterTeacher", "Account");
        }

        public async Task<ActionResult> RegisterTeacher()
        {
            Teacher teacher = (Teacher)TempData["teacher"];

            HttpRequestMessage apiRequest2 = CreateRequestToService(HttpMethod.Post, $"api/Teacher/Add");
            apiRequest2.Content = new ObjectContent<Teacher>(teacher, new JsonMediaTypeFormatter());

            HttpResponseMessage apiResponse2;
            try
            {
                apiResponse2 = await HttpClient.SendAsync(apiRequest2);
            }
            catch
            {
                await DeleteAccount();
                return View("Error");
            }

            if (!apiResponse2.IsSuccessStatusCode)
            {
                await DeleteAccount();
                return View("Error");
            }

            PassCookiesToClient(apiResponse2);

            return RedirectToAction("Index", "Home");
        }

        // GET: Account/Login
        public ActionResult Login()
        {
            return View();
        }

        // POST: Account/Login
        [HttpPost]
        public async Task<ActionResult> Login(Account account)
        {
            if (!ModelState.IsValid)
            {
                return View("Error");
            }

            HttpRequestMessage apiRequest = CreateRequestToService(HttpMethod.Post, "api/Account/Login");
            apiRequest.Content = new ObjectContent<Account>(account, new JsonMediaTypeFormatter());

            HttpResponseMessage apiResponse;
            try
            {
                apiResponse = await HttpClient.SendAsync(apiRequest);
            }
            catch (Exception ex)
            {
                Console.Write(ex.Message);
                return View("Error");
            }

            if (!apiResponse.IsSuccessStatusCode)
            {
                return View("Error");
            }

            PassCookiesToClient(apiResponse);


            HttpCookie userEmailCookie = new HttpCookie("userEmailCookie");
            userEmailCookie.Value = account.Email;

            Response.Cookies.Add(userEmailCookie);

            //await AddEmployeeCookie(account.Email);

            return RedirectToAction("Index", "Home");
        }

        // GET: Account/Logout
        public async Task<ActionResult> Logout()
        {
            if (!ModelState.IsValid)
            {
                return View("Error");
            }

            HttpRequestMessage apiRequest = CreateRequestToService(HttpMethod.Get, "api/Account/Logout");

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


            if (Request.Cookies["userEmailCookie"] != null)
            {
                var c = new HttpCookie("userEmailCookie");
                var c2 = new HttpCookie("Id");
                var c3 = new HttpCookie("Role");
                c3.Expires = DateTime.Now.AddDays(-1);
                c2.Expires = DateTime.Now.AddDays(-1);
                c.Expires = DateTime.Now.AddDays(-1);
                Response.Cookies.Add(c3);
                Response.Cookies.Add(c2);
                Response.Cookies.Add(c);
            }

            PassCookiesToClient(apiResponse);
            return RedirectToAction("Index", "Home");
        }

        public async Task<ActionResult> DeleteAccount()
        {
            //here delete created identity account if student/teacher creation problem
            Account account = (Account)TempData["account"];
            HttpRequestMessage apiRequest = CreateRequestToService(HttpMethod.Delete, $"api/Account/Delete");
            apiRequest.Content = new ObjectContent<Account>(account, new JsonMediaTypeFormatter());
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
                if (apiResponse.StatusCode != HttpStatusCode.Unauthorized)
                {
                    return View("Error");
                }
                ViewBag.Message = "Not logged in!";
            }

            if (Request.Cookies["userEmailCookie"] != null)
            {
                var c = new HttpCookie("userEmailCookie");
                var c2 = new HttpCookie("Id");
                var c3 = new HttpCookie("Role");
                c3.Expires = DateTime.Now.AddDays(-1);
                c2.Expires = DateTime.Now.AddDays(-1);
                c.Expires = DateTime.Now.AddDays(-1);
                Response.Cookies.Add(c3);
                Response.Cookies.Add(c2);
                Response.Cookies.Add(c);
            }

            PassCookiesToClient(apiResponse);
            return new EmptyResult();
            //return RedirectToAction("Index", "Home");
        }

        public async Task AddEmployeeCookie(string email)
        {

            //HttpRequestMessage apiRequest = CreateRequestToService(HttpMethod.Get, $"api/Employee/GetByEmail/{email}/");
            HttpRequestMessage apiRequest = CreateRequestToService(HttpMethod.Get, $"api/Account/GetAccount/{email}/teacher");

            HttpResponseMessage apiResponse;
            Assignment assignment = new Assignment();

            //try
            //{
            apiResponse = await HttpClient.SendAsync(apiRequest);
            //}
            //catch
            //{

            //}

            //if (!apiResponse.IsSuccessStatusCode)
            //{

            // }
            //PassCookiesToClient(apiResponse);

            var employee = await apiResponse.Content.ReadAsAsync<Teacher>();
            HttpCookie employeeIdCookie = new HttpCookie("EmployeeId");
            employeeIdCookie.Value = employee.TeacherID.ToString();

            Response.Cookies.Add(employeeIdCookie);


        }

        private bool PassCookiesToClient(HttpResponseMessage apiResponse)
        {
            if (apiResponse.Headers.TryGetValues("Set-Cookie", out IEnumerable<string> values))
            {
                foreach (string value in values)
                {
                    Response.Headers.Add("Set-Cookie", value);
                }
                return true;
            }
            return false;
        }
    }
}