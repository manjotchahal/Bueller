using Bueller.Client.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Bueller.Client.Controllers
{
    public class HomeController : ServiceController
    {
        public async Task<ActionResult> Index()
        {
            HttpRequestMessage apiRequest = CreateRequestToService(HttpMethod.Get, "api/Account/GetLoginInfo");
            HttpResponseMessage apiResponse;
            string role = "";
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

            else
            {
                var contentString = await apiResponse.Content.ReadAsStringAsync();

                //string role = contentString.Substring(contentString.IndexOf(":") + 3, contentString.LastIndexOf("]"));
                role = contentString.Substring(contentString.IndexOf(":") + 2).TrimEnd('"');
                ViewBag.Message = "Logged in! Result: " + contentString + "\n" + role;



                var cookie = Request.Cookies.Get("userEmailCookie");
                if (cookie != null)
                {
                    string email = cookie.Value;
                    await AddCookie(email, role);
                }
            }

            if (role != "teacher" && role != "student")
            {
                return RedirectToAction("IndexHome");
            }
            return RedirectToAction("IndexCalendar");

        }

        public async Task<ActionResult> IndexHome()
        {
            HttpRequestMessage apiRequest = CreateRequestToService(HttpMethod.Get, $"api/Home/GetHome");
            HttpResponseMessage apiResponse;

            try
            {
                apiResponse = await HttpClient.SendAsync(apiRequest);
            }
            catch
            {
                return View("Error");
            }


            var result = await apiResponse.Content.ReadAsAsync<Subject>();


            return View("Index", result);
        }

        public async Task<ActionResult> IndexCalendar()
        {
            int id = Convert.ToInt32(Request.Cookies["Id"].Value);
            string role = Request.Cookies["Role"].Value;

            HttpRequestMessage apiRequest;
            if (role == "student")
            {
                apiRequest = CreateRequestToService(HttpMethod.Get, $"api/Class/GetByStudentId/{id}");
            }
            else
            {
                apiRequest = CreateRequestToService(HttpMethod.Get, $"api/Class/GetByTeacherId/{id}");
            }

            HttpResponseMessage apiResponse;

            try
            {
                apiResponse = await HttpClient.SendAsync(apiRequest);
            }
            catch
            {
                return View("Error");
            }

            List<Class> classes = new List<Class>();
            if (apiResponse.IsSuccessStatusCode)
            {
                classes = await apiResponse.Content.ReadAsAsync<List<Class>>();
            }

            return View(classes);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public async Task AddCookie(string email, string role)
        {

            //HttpRequestMessage apiRequest = CreateRequestToService(HttpMethod.Get, $"api/Employee/GetByEmail/{email}/");
            HttpRequestMessage apiRequest = CreateRequestToService(HttpMethod.Get, $"api/Account/GetAccount/{email}/{role}");

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

            HttpCookie Id = new HttpCookie("Id");
            if (role == "teacher" || role == "employee")
            {
                var employee = await apiResponse.Content.ReadAsAsync<Teacher>();

                Id.Value = employee.TeacherID.ToString();
            }
            else if (role == "student")
            {
                var employee = await apiResponse.Content.ReadAsAsync<Student>();

                Id.Value = employee.StudentId.ToString();
            }

            Response.Cookies.Add(Id);

            HttpCookie Role = new HttpCookie("Role");
            Role.Value = role;
            Response.Cookies.Add(Role);

        }
    }
}