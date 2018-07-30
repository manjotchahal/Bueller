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
    public class AssignmentController : ServiceController
    {
        // GET: Assignment
        [HttpGet]
        public async Task<ActionResult> Index(int id)
        {

            if (!ModelState.IsValid)
            {
                return View("Error");
            }

            HttpRequestMessage apiRequest = CreateRequestToService(HttpMethod.Get, $"api/Assignment/GetByClassId/{id}");

            HttpResponseMessage apiResponse;
            Assignment assignment = new Assignment();

            try
            {
                apiResponse = await HttpClient.SendAsync(apiRequest);
            }
            catch
            {
                return View("Error");
            }

            List<Assignment> assignments = new List<Assignment>();
            if (apiResponse.IsSuccessStatusCode)
            {
                assignments = await apiResponse.Content.ReadAsAsync<List<Assignment>>();
            }

            int count = 0;
            double grade = 0;
            bool hasGrades = false;
            foreach(var item in assignments)
            {
                foreach (var file in item.Files)
                {
                    if (file.Score != null)
                    {
                        grade += (double)file.Score;
                        count++;
                        hasGrades = true;
                    }
                }
            }
            ViewBag.HasGrades = hasGrades;
            ViewBag.AverageGrade = grade / count;

            ViewBag.classid = id;
            //ViewBag.ClassName = "";

            TempData["ClassId"] = id;
            return View(assignments);
        }




        public ViewResult Create()
        {
            if (Request.Cookies["AuthTestCookie"] == null)
            {
                TempData["Error"] = "Not logged in!";
                return View("Error");
            }

            if (Request.Cookies["Role"].Value != "teacher")
            {
                return View("Error");
            }

            Assignment assignment = new Assignment();

            TempData.Keep("ClassId");
            //ViewBag.RestaurantId = TempData.Peek("RestaurantId");
            assignment.ClassId = Convert.ToInt32(TempData.Peek("ClassId"));
            TempData.Keep("ClassId");


            return View(assignment);

        }
        [HttpPost]
        public async Task<ActionResult> Create(Assignment assignment)
        {
            if (Request.Cookies["Role"].Value != "teacher")
            {
                return View("Error");
            }

            if (!ModelState.IsValid)
            {
                return View("Error");
            }


            HttpRequestMessage apiRequest = CreateRequestToService(HttpMethod.Post, $"api/Assignment/Add");
            apiRequest.Content = new ObjectContent<Assignment>(assignment, new JsonMediaTypeFormatter());

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



            return RedirectToAction("Index", "Assignment", new { id = assignment.ClassId });
        }

        public async Task<ActionResult> Edit(int id)
        {
            if (Request.Cookies["AuthTestCookie"] == null)
            {
                TempData["Error"] = "Not logged in!";
                return View("Error");
            }

            if (id == 0)
            {
                return View("Error");
            }

            if (Request.Cookies["Role"].Value != "teacher")
            {
                return View("Error");
            }

            HttpRequestMessage apiRequest = CreateRequestToService(HttpMethod.Get, $"api/Assignment/GetById/{id}");
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

            Assignment assignment = await apiResponse.Content.ReadAsAsync<Assignment>();
            TempData["Assignment"] = assignment;
            return View(assignment);
        }


        [HttpPost]
        public async Task<ActionResult> Edit(int id, Assignment assignment)
        {
            if (!ModelState.IsValid)
            {
                return View("Error");
            }

            if (Request.Cookies["Role"].Value != "teacher")
            {
                return View("Error");
            }

            if (assignment.NotModified(TempData.Peek("Assignment"))) { return RedirectToAction("Index", new { id = assignment.ClassId }); }

            HttpRequestMessage apiRequest = CreateRequestToService(HttpMethod.Put, $"api/Assignment/AddAt/{id}");
            apiRequest.Content = new ObjectContent<Assignment>(assignment, new JsonMediaTypeFormatter());

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

            return RedirectToAction("Index", new { id = assignment.ClassId });

        }

        [HttpGet]
        public async Task<ActionResult> Delete(int id)
        {
            if (Request.Cookies["AuthTestCookie"] == null)
            {
                TempData["Error"] = "Not logged in!";
                return View("Error");
            }

            if (Request.Cookies["Role"].Value != "teacher")
            {
                return View("Error");
            }

            if (id == 0)
            {
                return View("Error");
            }

            HttpRequestMessage apiRequest = CreateRequestToService(HttpMethod.Get, $"api/Assignment/GetById/{id}");
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

            var assignment = await apiResponse.Content.ReadAsAsync<Assignment>();

            return View(assignment);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<ActionResult> ConfirmDelete(int id)
        {
            HttpRequestMessage apiRequest = CreateRequestToService(HttpMethod.Delete, $"api/Assignment/Delete/{id}");
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