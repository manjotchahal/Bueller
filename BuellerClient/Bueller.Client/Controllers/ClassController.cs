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
    public class ClassController : ServiceController
    {
        //add instructor name to view for students? or withhold if teacher logged in...
        //add enrollment count too?
        //have create/edit/delete only show up for teachers
        //details view for description and link to textbook purchase? subject and class level?

        // GET: Classes
        [HttpGet]
        public async Task<ViewResult> Index()
        {

            HttpRequestMessage apiRequest = CreateRequestToService(HttpMethod.Get, "api/Class/GetAll");
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

            var id = Request.Cookies["Id"].Value;
            var role = Request.Cookies["Role"].Value;

            //create list of ids of classes student is enrolled in to use in all classes view to hide enroll link if already enrolled in class
            if (role == "student")
            {
                HttpRequestMessage apiRequest2 = CreateRequestToService(HttpMethod.Get, $"api/Class/GetByStudentId/{id}");
                HttpResponseMessage apiResponse2;

                try
                {
                    apiResponse2 = await HttpClient.SendAsync(apiRequest2);
                }
                catch
                {
                    return View("Error");
                }

                List<Class> classes2 = new List<Class>();
                if (apiResponse2.IsSuccessStatusCode)
                {
                    classes2 = await apiResponse2.Content.ReadAsAsync<List<Class>>();
                    List<int> ids = new List<int>();
                    foreach (var item in classes2)
                    {
                        ids.Add(item.ClassId);
                    }
                    ViewBag.Ids = ids;
                }
            }

            ViewBag.Role = Request.Cookies["Role"].Value;
            TempData["ToDetailsFrom"] = "all";
            return View(classes);
        }


        [HttpGet]
        public async Task<ViewResult> MyClasses()
        {

            var id = Request.Cookies["Id"].Value;
            var role = Request.Cookies["Role"].Value;

            HttpRequestMessage apiRequest;

            if (role == "student")
                apiRequest = CreateRequestToService(HttpMethod.Get, $"api/Class/GetByStudentId/{id}");
            else if (role == "teacher")
                apiRequest = CreateRequestToService(HttpMethod.Get, $"api/Class/GetByTeacherId/{id}/");
            else
                apiRequest = CreateRequestToService(HttpMethod.Get, $"api/Class/GetAll");

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


            ViewBag.Role = Request.Cookies["Role"].Value;
            TempData["ToDetailsFrom"] = "my";
            return View(classes);
        }

        // GET: Classes
        [HttpGet]
        public async Task<ViewResult> Details(int id)
        {
            HttpRequestMessage apiRequest = CreateRequestToService(HttpMethod.Get, $"api/Class/GetById/{id}");

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

            var classresponse = await apiResponse.Content.ReadAsAsync<Class>();

            HttpRequestMessage apiRequest2 = CreateRequestToService(HttpMethod.Get, $"api/Class/EnrollmentCount/{id}");
            HttpResponseMessage apiResponse2;

            try
            {
                apiResponse2 = await HttpClient.SendAsync(apiRequest2);
            }
            catch
            {
                return View("Error");
            }

            if (!apiResponse2.IsSuccessStatusCode)
            {
                return View("Error");
            }

            var count = await apiResponse2.Content.ReadAsAsync<int>();
            classresponse.EnrollmentCount = count;

            //TempData.Keep("ToDetailsFrom");
            ViewBag.Goto = Convert.ToString(TempData.Peek("ToDetailsFrom"));
            //TempData.Keep("ToDetailsFrom");

            return View(classresponse);
        }

        // GET: Classes
        [HttpGet]
        public async Task<ActionResult> EnrollConfirm(int id)
        {
            //if (Request.Cookies.Get("Role").Value != "student")
            //{
            //    return View("Error");
            //}

            HttpRequestMessage apiRequest = CreateRequestToService(HttpMethod.Get, $"api/Class/GetById/{id}");

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

            var classresponse = await apiResponse.Content.ReadAsAsync<Class>();

            HttpRequestMessage apiRequest2 = CreateRequestToService(HttpMethod.Get, $"api/Class/EnrollmentCount/{id}");
            HttpResponseMessage apiResponse2;

            try
            {
                apiResponse2 = await HttpClient.SendAsync(apiRequest2);
            }
            catch
            {
                return View("Error");
            }

            if (!apiResponse2.IsSuccessStatusCode)
            {
                return View("Error");
            }

            var count = await apiResponse2.Content.ReadAsAsync<int>();
            classresponse.EnrollmentCount = count;

            if (Request.Cookies.Get("Role").Value == "teacher" && classresponse.TeacherId != null)
            {
                return View("Error");
            }

            ViewBag.Id = id;
            return View(classresponse);
            //return RedirectToAction("Index");
        }

        // GET: Enroll
        [HttpGet]
        public async Task<ActionResult> Enroll(int id)
        {
            //if (Request.Cookies.Get("Role").Value != "student")
            //{
            //    return View("Error");
            //}

            //if (Request.Cookies.Get("Role").Value == "teacher" && classresponse.TeacherId != null)
            //{
            //    return View("Error");
            //}

            var personId = Convert.ToInt32(Request.Cookies.Get("Id").Value);

            HttpRequestMessage apiRequest;
            if (Request.Cookies.Get("Role").Value == "student")
            {
                apiRequest = CreateRequestToService(HttpMethod.Get, $"api/Class/Enroll/{id}/{personId}");
            }
            else
            {
                apiRequest = CreateRequestToService(HttpMethod.Get, $"api/Class/EnrollTeacher/{id}/{personId}");
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

            if (!apiResponse.IsSuccessStatusCode)
            {
                return View("Error");
            }

            var classresponse = await apiResponse.Content.ReadAsAsync<Class>();

            //return View(classresponse);
            return RedirectToAction("MyClasses");
        }

        // GET: Classes
        [HttpGet]
        public async Task<ActionResult> UnenrollConfirm(int id)
        {
            //if (Request.Cookies.Get("Role").Value != "student")
            //{
            //    return View("Error");
            //}

            HttpRequestMessage apiRequest = CreateRequestToService(HttpMethod.Get, $"api/Class/GetById/{id}");

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

            var classresponse = await apiResponse.Content.ReadAsAsync<Class>();

            HttpRequestMessage apiRequest2 = CreateRequestToService(HttpMethod.Get, $"api/Class/EnrollmentCount/{id}");
            HttpResponseMessage apiResponse2;

            try
            {
                apiResponse2 = await HttpClient.SendAsync(apiRequest2);
            }
            catch
            {
                return View("Error");
            }

            if (!apiResponse2.IsSuccessStatusCode)
            {
                return View("Error");
            }

            var count = await apiResponse2.Content.ReadAsAsync<int>();
            classresponse.EnrollmentCount = count;

            if (Request.Cookies.Get("Role").Value == "teacher" && classresponse.TeacherId != Convert.ToInt32(Request.Cookies.Get("Id").Value))
            {
                return View("Error");
            }

            ViewBag.Id = id;
            return View(classresponse);
            //return RedirectToAction("Index");
        }

        // GET: Enroll
        [HttpGet]
        public async Task<ActionResult> Unenroll(int id)
        {
            //if (Request.Cookies.Get("Role").Value == "teacher" && classresponse.TeacherId != null)
            //{
            //    return View("Error");
            //}

            var personId = Convert.ToInt32(Request.Cookies.Get("Id").Value);
            HttpRequestMessage apiRequest;

            if (Request.Cookies.Get("Role").Value == "student")
            {
                apiRequest = CreateRequestToService(HttpMethod.Get, $"api/Class/Unenroll/{id}/{personId}");
            }
            else
            {
                HttpRequestMessage apiRequest2 = CreateRequestToService(HttpMethod.Get, $"api/Class/GetById/{id}");
                HttpResponseMessage apiResponse2;

                try
                {
                    apiResponse2 = await HttpClient.SendAsync(apiRequest2);
                }
                catch
                {
                    return View("Error");
                }

                if (!apiResponse2.IsSuccessStatusCode)
                {
                    return View("Error");
                }

                var classresponse = await apiResponse2.Content.ReadAsAsync<Class>();

                if (Request.Cookies.Get("Role").Value == "teacher" && classresponse.TeacherId != Convert.ToInt32(Request.Cookies.Get("Id").Value))
                {
                    return View("Error");
                }

                apiRequest = CreateRequestToService(HttpMethod.Get, $"api/Class/UnenrollTeacher/{id}");
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

            if (!apiResponse.IsSuccessStatusCode)
            {
                return View("Error");
            }

            return RedirectToAction("MyClasses");
        }


        public async Task<ActionResult> Create()
        {
            //Assignment assignment = new Assignment();
            //assignment.ClassId = ClassId;

            if (Request.Cookies.Get("Role").Value != "teacher")
            {
                return View("Error");
            }

            HttpRequestMessage apiRequest = CreateRequestToService(HttpMethod.Get, "api/Class/Subject/GetAllNames");
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

            var subjects = await apiResponse.Content.ReadAsAsync<List<string>>();
            var subjects2 = subjects.OrderBy(q => q);
            var subjectselectlist = subjects2.Select(c => new SelectListItem { Text = c, Value = c }).ToList();

            ViewBag.Subjects = subjectselectlist;
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Create(Class newClass)
        {
            if (Request.Cookies.Get("Role").Value != "teacher")
            {
                return View("Error");
            }
            HttpRequestMessage apiRequest2 = CreateRequestToService(HttpMethod.Get, $"api/Class/Subject/GetByName/{newClass.SubjectName}");
            HttpResponseMessage apiResponse2;

            try
            {
                apiResponse2 = await HttpClient.SendAsync(apiRequest2);
            }
            catch
            {
                return View("Error");
            }

            if (!apiResponse2.IsSuccessStatusCode)
            {
                return View("Error");
            }

            var subject = await apiResponse2.Content.ReadAsAsync<Subject>();

            newClass.SubjectId = subject.SubjectId;
            newClass.TeacherId = Convert.ToInt32(Request.Cookies.Get("Id").Value);


            if (!ModelState.IsValid)
            {
                return View("Error");
            }


            HttpRequestMessage apiRequest = CreateRequestToService(HttpMethod.Post, $"api/Class/Add");
            apiRequest.Content = new ObjectContent<Class>(newClass, new JsonMediaTypeFormatter());

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

        public async Task<ActionResult> Edit(int id)
        {
            if (id == 0)
            {
                return View("Error");
            }

            if (Request.Cookies["Role"].Value != "teacher")
            {
                return View("Error");
            }

            HttpRequestMessage apiRequest = CreateRequestToService(HttpMethod.Get, $"api/Class/GetById/{id}");
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

            Class classresult = await apiResponse.Content.ReadAsAsync<Class>();

            if (classresult.TeacherId != Convert.ToInt32(Request.Cookies["Id"].Value))
            {
                return View("Error");
            }

            HttpRequestMessage apiRequest2 = CreateRequestToService(HttpMethod.Get, "api/Class/Subject/GetAllNames");
            HttpResponseMessage apiResponse2;

            try
            {
                apiResponse2 = await HttpClient.SendAsync(apiRequest2);
            }
            catch
            {
                return View("Error");
            }

            if (!apiResponse2.IsSuccessStatusCode)
            {
                return View("Error");
            }

            var subjects = await apiResponse2.Content.ReadAsAsync<List<string>>();
            var subjects2 = subjects.OrderBy(q => q);
            var subjectselectlist = subjects2.Select(c => new SelectListItem { Text = c, Value = c }).ToList();

            ViewBag.Subjects = subjectselectlist;
            classresult.SubjectName = classresult.Subject.Name;

            TempData["Class"] = classresult;

            return View(classresult);
        }

        [HttpPost]
        public async Task<ActionResult> Edit(int id, Class classres)
        {
            if (Request.Cookies.Get("Role").Value != "teacher")
            {
                return View("Error");
            }

            HttpRequestMessage apiRequest2 = CreateRequestToService(HttpMethod.Get, $"api/Class/Subject/GetByName/{classres.SubjectName}");
            HttpResponseMessage apiResponse2;

            try
            {
                apiResponse2 = await HttpClient.SendAsync(apiRequest2);
            }
            catch
            {
                return View("Error");
            }

            if (!apiResponse2.IsSuccessStatusCode)
            {
                return View("Error");
            }

            var subject = await apiResponse2.Content.ReadAsAsync<Subject>();

            int oldSubject = classres.SubjectId;
            classres.ClassId = id;
            classres.SubjectId = subject.SubjectId;
            classres.TeacherId = Convert.ToInt32(Request.Cookies.Get("Id").Value);


            if (!ModelState.IsValid)
            {
                return View("Error");
            }

            if (Request.Cookies["Role"].Value != "teacher")
            {
                return View("Error");
            }

            if (classres.Equals(TempData.Peek("Class"))) { return RedirectToAction("MyClasses", "Class"); }

            HttpRequestMessage apiRequest = CreateRequestToService(HttpMethod.Put, $"api/Class/Edit/{id}");
            apiRequest.Content = new ObjectContent<Class>(classres, new JsonMediaTypeFormatter());

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

            return RedirectToAction("MyClasses", "Class");

        }
    }
}