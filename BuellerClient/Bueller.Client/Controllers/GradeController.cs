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
    public class GradeController : ServiceController
    {
        // GET: Grade
        public ActionResult Create(int fileId)
        {
            if (Request.Cookies.Get("Role").Value != "teacher")
            {
                return View("Error");
            }

            Grade grade = new Grade();
            //grade.FileId = fileId;


            return View(grade);
        }

        public ActionResult Index()
        {



            return View();
        }


        [HttpPost]
        public async Task<ActionResult> Create(Grade newGrade)
        {
            if (Request.Cookies.Get("Role").Value != "teacher")
            {
                return View("Error");
            }


            if (!ModelState.IsValid)
            {
                return View("Error");
            }


            HttpRequestMessage apiRequest = CreateRequestToService(HttpMethod.Post, $"api/Grade/Add");
            apiRequest.Content = new ObjectContent<Grade>(newGrade, new JsonMediaTypeFormatter());

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