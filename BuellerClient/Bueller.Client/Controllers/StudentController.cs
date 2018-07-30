using Bueller.Client.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Bueller.Client.Controllers
{
    public class StudentController : ServiceController
    {
        // GET: Student
        [HttpGet]
        public async Task<ActionResult> Index(int ClassId)
        {
            if (Request.Cookies["AuthTestCookie"] == null)
            {
                TempData["Error"] = "Not logged in!";
                return View("Error");
            }

            if (ClassId == 0)
            {
                return View("Error");
            }

            HttpRequestMessage apiRequest =
                CreateRequestToService(HttpMethod.Get, $"api/Assignment/GetByClassId/{ClassId}");

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

        public async Task<ActionResult> GetTeachers()
        {
            if (Request.Cookies["AuthTestCookie"] == null)
            {
                TempData["Error"] = "Not logged in!";
                return View("Error");
            }

            int id = Convert.ToInt32(Request.Cookies["Id"].Value);

            HttpRequestMessage apiRequest = CreateRequestToService(HttpMethod.Get, $"api/Student/GetTeachersByStudentId/{id}");
            HttpResponseMessage apiResponse;

            try
            {
                apiResponse = await HttpClient.SendAsync(apiRequest);
            }
            catch
            {
                return View("Error");
            }

            List<Teacher> files = new List<Teacher>();
            if (apiResponse.IsSuccessStatusCode)
            {
                files = await apiResponse.Content.ReadAsAsync<List<Teacher>>();
            }


            return View(files);
        }

        // GET: Student/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Student/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Student/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Student/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Student/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Student/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Student/Delete/5
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