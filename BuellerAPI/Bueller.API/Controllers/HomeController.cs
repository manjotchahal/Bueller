using Bueller.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace Bueller.API.Controllers
{
    [RoutePrefix("api/Home")]
    public class HomeController : ApiController
    {
        private readonly CrossTable cross = new CrossTable();

        [HttpGet]
        [Route("GetHome")]
        [AllowAnonymous]
        public IHttpActionResult GetHome()
        {
            var result = cross.GetHome();
            return Ok(result);
        }
    }
}
