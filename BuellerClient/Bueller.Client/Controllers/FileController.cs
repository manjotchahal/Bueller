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
    public class FileController : ServiceController
    {

        //public async Task<ActionResult> Index()
        //{
        //    HttpRequestMessage apiRequest = CreateRequestToService(HttpMethod.Get, "api/File/GetAll");
        //    HttpResponseMessage apiResponse;

        //    try
        //    {
        //        apiResponse = await HttpClient.SendAsync(apiRequest);
        //    }
        //    catch
        //    {
        //        return View("Error");
        //    }

        //    List<File> files = new List<File>();
        //    if (apiResponse.IsSuccessStatusCode)
        //    {
        //        files = await apiResponse.Content.ReadAsAsync<List<File>>();
        //    }



        //    return View(files);
        //}

        //public async Task<ActionResult> GetByIdClass(int classId)
        //{
        //    if (classId == 0)
        //    {
        //        return View("Error");
        //    }

        //    HttpRequestMessage apiRequest = CreateRequestToService(HttpMethod.Get, $"api/File/GetByClassId/{classId}");
        //    HttpResponseMessage apiResponse;

        //    try
        //    {
        //        apiResponse = await HttpClient.SendAsync(apiRequest);
        //    }
        //    catch
        //    {
        //        return View("Error");
        //    }

        //    List<File> files = new List<File>();
        //    if (apiResponse.IsSuccessStatusCode)
        //    {
        //        files = await apiResponse.Content.ReadAsAsync<List<File>>();
        //    }


        //    return View(files);
        //}

        public async Task<ActionResult> GetByIdAssignment(int id)
        {
            if (id == 0)
            {
                return View("Error");
            }

            HttpRequestMessage apiRequest = CreateRequestToService(HttpMethod.Get, $"api/File/GetByAssignmentId/{id}");
            HttpResponseMessage apiResponse;

            try
            {
                apiResponse = await HttpClient.SendAsync(apiRequest);
            }
            catch
            {
                return View("Error");
            }

            List<File> files = new List<File>();
            if (apiResponse.IsSuccessStatusCode)
            {
                files = await apiResponse.Content.ReadAsAsync<List<File>>();
            }

            TempData.Keep("ClassId");
            ViewBag.ClassId = Convert.ToInt32(TempData.Peek("ClassId"));
            TempData.Keep("ClassId");


            return View(files);
        }

        public async Task<ActionResult> GetByIdStudent(int studentId, int assignmentId)
        {
            if (studentId == 0 || assignmentId == 0)
            {
                return View("Error");
            }

            HttpRequestMessage apiRequest = CreateRequestToService(HttpMethod.Get, $"api/File/GetByAsnIdAndStudentId/{studentId}/{assignmentId}");
            HttpResponseMessage apiResponse;

            try
            {
                apiResponse = await HttpClient.SendAsync(apiRequest);
            }
            catch
            {
                return View("Error");
            }

            List<File> files = new List<File>();
            if (apiResponse.IsSuccessStatusCode)
            {
                files = await apiResponse.Content.ReadAsAsync<List<File>>();
            }

            ViewBag.AssignmentId = assignmentId;

            TempData.Keep("ClassId");
            ViewBag.ClassId = Convert.ToInt32(TempData.Peek("ClassId"));
            TempData.Keep("ClassId");

            return View(files);
        }


        public ActionResult AddFile(int AssignmentId, int StudentId)
        {
            if (Request.Cookies["Role"].Value != "student")
            {
                return View("Error");
            }

            if (StudentId != Convert.ToInt32(Request.Cookies["Id"].Value))
            {
                return View("Error");
            }

            File file = new File();
            file.AssignmentId = AssignmentId;
            file.StudentId = StudentId;
            return View(file);
        }

        [HttpPost]
        public async Task<ActionResult> AddFile(File file)
        {
            if (Request.Cookies["Role"].Value != "student")
            {
                return View("Error");
            }

            if (file.StudentId != Convert.ToInt32(Request.Cookies["Id"].Value))
            {
                return View("Error");
            }

            if (!ModelState.IsValid)
            {
                return View("Error");
            }

            HttpRequestMessage apiRequest = CreateRequestToService(HttpMethod.Post, $"api/File/Add");
            apiRequest.Content = new ObjectContent<File>(file, new JsonMediaTypeFormatter());

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

            return RedirectToAction("GetByIdStudent", new { studentId = file.StudentId, assignmentId = file.AssignmentId });
        }

        public async Task<ActionResult> Edit(int id)
        {
            if (id == 0)
            {
                return View("Error");
            }

            if (Request.Cookies["Role"].Value != "student")
            {
                return View("Error");
            }

            HttpRequestMessage apiRequest = CreateRequestToService(HttpMethod.Get, $"api/File/GetById/{id}");
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

            File file = await apiResponse.Content.ReadAsAsync<File>();
            TempData["File"] = file;

            if (file.StudentId != Convert.ToInt32(Request.Cookies["Id"].Value))
            {
                return View("Error");
            }

            return View(file);
        }


        [HttpPost]
        public async Task<ActionResult> Edit(int id, File file)
        {
            if (!ModelState.IsValid)
            {
                return View("Error");
            }

            if (Request.Cookies["Role"].Value != "student")
            {
                return View("Error");
            }

            if (file.StudentId != Convert.ToInt32(Request.Cookies["Id"].Value))
            {
                return View("Error");
            }

            if (file.NotModified(TempData.Peek("File"))) { return RedirectToAction("GetByIdStudent", new { studentId = file.StudentId, assignmentId = file.AssignmentId }); }

            HttpRequestMessage apiRequest = CreateRequestToService(HttpMethod.Put, $"api/File/AddAt/{id}");
            apiRequest.Content = new ObjectContent<File>(file, new JsonMediaTypeFormatter());

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

            return RedirectToAction("GetByIdStudent", new { studentId = file.StudentId, assignmentId = file.AssignmentId });

        }

        [HttpGet]
        public async Task<ActionResult> Delete(int id)
        {
            if (id == 0)
            {
                return View("Error");
            }

            if (Request.Cookies["Role"].Value != "student")
            {
                return View("Error");
            }

            HttpRequestMessage apiRequest = CreateRequestToService(HttpMethod.Get, $"api/File/GetById/{id}");
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

            var file = await apiResponse.Content.ReadAsAsync<File>();

            if (file.StudentId != Convert.ToInt32(Request.Cookies["Id"].Value))
            {
                return View("Error");
            }

            if (file.Score != null)
            {
                return View("Error");
            }

            TempData["AssignmentId"] = file.AssignmentId;

            return View(file);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<ActionResult> ConfirmDelete(int id)
        {
            HttpRequestMessage apiRequest = CreateRequestToService(HttpMethod.Delete, $"api/File/Delete/{id}");
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

            return RedirectToAction("GetByIdStudent", new { studentId = Convert.ToInt32(Request.Cookies["Id"].Value), assignmentId = Convert.ToInt32(TempData.Peek("AssignmentId")) });
        }

        //public ActionResult Create(int fileId)
        //{
        //    if (Request.Cookies.Get("Role").Value != "teacher")
        //    {
        //        return View("Error");
        //    }

        //    Grade grade = new Grade();
        //    //grade.FileId = fileId;


        //    return View(grade);
        //}

        //public ActionResult Index()
        //{



        //    return View();
        //}


        //[HttpPost]
        //public async Task<ActionResult> Create(Grade newGrade)
        //{
        //    if (Request.Cookies.Get("Role").Value != "teacher")
        //    {
        //        return View("Error");
        //    }


        //    if (!ModelState.IsValid)
        //    {
        //        return View("Error");
        //    }


        //    HttpRequestMessage apiRequest = CreateRequestToService(HttpMethod.Post, $"api/Grade/Add");
        //    apiRequest.Content = new ObjectContent<Grade>(newGrade, new JsonMediaTypeFormatter());

        //    HttpResponseMessage apiResponse;


        //    try
        //    {
        //        apiResponse = await HttpClient.SendAsync(apiRequest);
        //    }
        //    catch
        //    {
        //        return View("Error");
        //    }

        //    if (!apiResponse.IsSuccessStatusCode)
        //    {
        //        return View("Error");
        //    }



        //    return RedirectToAction("MyClasses", "Class");
        //}



        public async Task<ActionResult> Grade(int id)
        {
            if (id == 0)
            {
                return View("Error");
            }

            if (Request.Cookies.Get("Role").Value != "teacher")
            {
                return View("Error");
            }

            HttpRequestMessage apiRequest = CreateRequestToService(HttpMethod.Get, $"api/File/GetById/{id}");
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

            File file = await apiResponse.Content.ReadAsAsync<File>();
            TempData["File"] = file;

            //if (file.StudentId != Convert.ToInt32(Request.Cookies["Id"].Value))
            //{
            //    return View("Error");
            //}

            return View(file);
        }


        [HttpPost]
        public async Task<ActionResult> Grade(int id, File file)
        {
            if (!ModelState.IsValid)
            {
                return View("Error");
            }

            if (Request.Cookies["Role"].Value != "teacher")
            {
                return View("Error");
            }

            //add second equals
            if (file.GradeNotModified(TempData.Peek("File"))) { return RedirectToAction("GetByIdAssignment", new { id = file.AssignmentId }); }

            HttpRequestMessage apiRequest = CreateRequestToService(HttpMethod.Put, $"api/File/AddAt/{id}");
            apiRequest.Content = new ObjectContent<File>(file, new JsonMediaTypeFormatter());

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

            return RedirectToAction("GetByIdAssignment", new { id = file.AssignmentId });

        }



        //[HttpGet]
        //public async Task<ActionResult> Details(int id)
        //{
        //    HttpRequestMessage apiRequest = CreateRequestToService(HttpMethod.Get, $"api/File/GetById/{id}");
        //    HttpResponseMessage apiResponse;

        //    try
        //    {
        //        apiResponse = await HttpClient.SendAsync(apiRequest);
        //    }
        //    catch
        //    {
        //        return View("Error");
        //    }

        //    File file = new File();
        //    if (apiResponse.IsSuccessStatusCode)
        //    {
        //        file = await apiResponse.Content.ReadAsAsync<File>();
        //    }

        //    return View(file);
        //}
    }
}