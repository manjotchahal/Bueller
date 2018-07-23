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


        public AssignmentController()
        {
            ViewBag.Title = "AssignmentController";
        }



        // GET: Assignment
        [HttpGet]
        public async Task<ViewResult> Index(int id)
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

            ViewBag.classid = id;
            //ViewBag.ClassName = "";

            TempData["ClassId"] = id;
            return View(assignments);
        }




        public ViewResult Create()
        {
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

        [HttpGet]
        public async Task<ActionResult> Delete(int id)
        {
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

            var file = await apiResponse.Content.ReadAsAsync<Assignment>();

            //if (file.Class.TeacherId != Convert.ToInt32(Request.Cookies["Id"].Value))
            //{
            //    return View("Error");
            //}

            return View(file);
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