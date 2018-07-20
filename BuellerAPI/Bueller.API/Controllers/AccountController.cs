using Bueller.API.Models;
using Bueller.Data;
using Bueller.Data.Repositories;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Web.Http;

namespace Bueller.API.Controllers
{
    [RoutePrefix("api/Account")]
    public class AccountController : ApiController
    {
        private readonly UnitOfWork unit = new UnitOfWork();
        private StudentRepository studentRepo;
        private TeacherRepository employeeRepo;

        public AccountController()
        {
            studentRepo = unit.StudentRepository();
            employeeRepo = unit.TeacherRepository();
        }

        //[HttpPost]
        //[Route("Register")]
        //[AllowAnonymous]
        //public IHttpActionResult Register(Account account)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    // actually register
        //    var userStore = new UserStore<IdentityUser>(new IdentityContext());
        //    var userManager = new UserManager<IdentityUser>(userStore);
        //    var user = new IdentityUser(account.Email);

        //    if (userManager.Users.Any(u => u.UserName == account.Email))
        //    {
        //        return BadRequest();
        //    }

        //    userManager.Create(user, account.Password);


        //    return Ok();
        //}

        [HttpPost]
        [Route("RegisterRole/{role}")]
        [AllowAnonymous]
        public IHttpActionResult RegisterWithRoles(Account account, string role)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!role.Equals("student") && !role.Equals("teacher") && !role.Equals("employee"))
            {
                return BadRequest(role);
            }

            // actually register
            var userStore = new UserStore<IdentityUser>(new IdentityContext());
            var userManager = new UserManager<IdentityUser>(userStore);
            var user = new IdentityUser(account.Email);

            if (userManager.Users.Any(u => u.UserName == account.Email))
            {
                return BadRequest();
            }

            userManager.Create(user, account.Password);

            // the only difference from Register 
            userManager.AddClaim(user.Id, new Claim(ClaimTypes.Role, role));

            //login
            var authManager = Request.GetOwinContext().Authentication;
            var claimsIdentity = userManager.CreateIdentity(user, WebApiConfig.AuthenticationType);

            authManager.SignIn(new AuthenticationProperties { IsPersistent = true }, claimsIdentity);

            return Ok();
        }

        [HttpPost]
        [Route("Login")]
        [AllowAnonymous]
        public IHttpActionResult LogIn(Account account)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // actually login
            var userStore = new UserStore<IdentityUser>(new IdentityContext());
            var userManager = new UserManager<IdentityUser>(userStore);
            var user = userManager.Users.FirstOrDefault(u => u.UserName == account.Email);


            if (user == null)
            {
                return BadRequest();
            }

            if (!userManager.CheckPassword(user, account.Password))
            {
                return Unauthorized();
            }

            var authManager = Request.GetOwinContext().Authentication;
            var claimsIdentity = userManager.CreateIdentity(user, WebApiConfig.AuthenticationType);

            authManager.SignIn(new AuthenticationProperties { IsPersistent = true }, claimsIdentity);

            return Ok();
        }

        [HttpGet]
        [Route("Logout")]
        public IHttpActionResult Logout()
        {
            Request.GetOwinContext().Authentication.SignOut(WebApiConfig.AuthenticationType);
            return Ok("Logged Out");
        }

        [HttpGet]
        [Route("GetLoginInfo")]
        public IHttpActionResult GetLoginInfo()
        {
            // making use of global authorize filter in webapiconfig / filterconfig

            // get the currently logged-in user
            var user = Request.GetOwinContext().Authentication.User;

            // get his username
            string username = user.Identity.Name;

            // get whether user has some role
            bool isAdmin = user.IsInRole("admin");

            // get all user's roles
            //List<string> roles = user.Claims.Where(x => x.Type == ClaimTypes.Role).Select(x => x.Value.ToString()).ToList();
            string role = user.Claims.Where(x => x.Type == ClaimTypes.Role).Select(x => x.Value.ToString()).FirstOrDefault();
            //return Ok($"Authenticated {username}, with roles: [{string.Join(", ", roles)}]!");
            return Ok($"Authenticated {username}/nRole: {role}");
        }

        //in uri have email and type of account to look for (student/employee)
        [HttpGet]
        [Route("GetAccount/{email}/{type}")]
        public IHttpActionResult GetAccountByEmail(string email, string type)
        {
            dynamic account;
            bool match = false;
            if (type.Equals("employee") || type.Equals("teacher"))
            {
                account = employeeRepo.GetTeacherByEmail(email);
                match = true;
                if (account != null)
                {
                    return Ok(account);
                }
            }

            if (type.Equals("student"))
            {
                account = studentRepo.GetStudentByEmail(email);
                match = true;
                if (account != null)
                {
                    return Ok(account);
                }
            }

            if (match)
            {
                return Content(HttpStatusCode.NotFound, "Email does not exist");
            }

            return BadRequest();
        }
    }
}
