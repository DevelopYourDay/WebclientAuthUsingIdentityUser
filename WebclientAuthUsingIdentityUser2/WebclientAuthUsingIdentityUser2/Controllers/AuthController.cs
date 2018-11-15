using Microsoft.AspNet.Identity.Owin;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using WebclientAuthUsingIdentityUser.Dto;
using WebclientAuthUsingIdentityUser.Helpers;
using WebclientAuthUsingIdentityUser.Models;
using System.Net.Http.Formatting;
using Newtonsoft.Json.Linq;
using WebclientAuthUsingIdentityUser2.Helps;

namespace WebclientAuthUsingIdentityUser2.Controllers
{
    public class AuthController : Controller
    {
 

        public AuthController()
        {
        }

        //
        // GET: /Auth/Login
        [AllowAnonymous]
        public ActionResult Login()
        {
            return View("Login","_Layout");
        }

        // GET: /Auth/Register
        [AllowAnonymous]
        public ActionResult Register()
        {
            return View("Register", "_Layout");
        }

 
        //
        // GET: /Auth/ForgetYourPassword
        [AllowAnonymous]
        public ActionResult ForgetYourPassword()
        {
            return View("ForgetYourPassword", "_Layout");
        }



        //
        // POST: /Auth/Login
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                StringBuilder strB = new StringBuilder(500);
                foreach (ModelState modelState in ModelState.Values)
                {
                    foreach (ModelError error in modelState.Errors)
                    {
                        strB.Append(error.ErrorMessage + ".");
                    }
                }
                Response.StatusCode = (int)System.Net.HttpStatusCode.BadRequest;
                return Content(strB.ToString());
            }

            try
            {
                var client = WebApiHttpClient.GetClient();


                string json = JsonConvert.SerializeObject(model);
                var httpContent = new StringContent(json, Encoding.UTF8, "application/json");
                var httpResponse = await client.PostAsync("api/auth/login", httpContent);

                if (httpResponse.IsSuccessStatusCode)
                {
                    TokenResponse tokenResponse = await httpResponse.Content.ReadAsAsync<TokenResponse>();
                    WebApiHttpClient.storeToken(tokenResponse);
                    // return Content(tokenResponse.AccessToken);
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    Response.StatusCode = (int)System.Net.HttpStatusCode.BadRequest;
                    MessageResponse json2 = await httpResponse.Content.ReadAsAsync<MessageResponse>();
                    return Content("" + json2.message);

                }

            }
            catch (Exception ee)
            {
                Response.StatusCode = (int)System.Net.HttpStatusCode.BadRequest;
                return Content("Sorry, an error occured." + ee.Message);
            }
        }

        // POST: /Auth/Create
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Register(RegisterViewModel model)
        {
            if (!ModelState.IsValid)
            {
                StringBuilder strB = new StringBuilder(500);
                foreach (ModelState modelState in ModelState.Values)
                {
                    foreach (ModelError error in modelState.Errors)
                    {
                        strB.Append(error.ErrorMessage + ".");
                    }
                }
                Response.StatusCode = (int)System.Net.HttpStatusCode.BadRequest;
                return Content(strB.ToString());
            }

            try
            {
                var client = WebApiHttpClient.GetClient();
                string json = JsonConvert.SerializeObject(model);
                var httpContent = new StringContent(json, Encoding.UTF8, "application/json");
                var httpResponse = await client.PostAsync("api/auth/create", httpContent);

                if (httpResponse.IsSuccessStatusCode)
                {
                    MessageResponse response = await httpResponse.Content.ReadAsAsync<MessageResponse>();
                    Response.StatusCode = (int)System.Net.HttpStatusCode.OK;
                    return Content(response.message);
                }
                else
                {
                    Response.StatusCode = (int)System.Net.HttpStatusCode.BadRequest;
                    MessageResponse json2 = await httpResponse.Content.ReadAsAsync<MessageResponse>();
                    return Content(json2.message);
                }

            }
            catch (Exception ee)
            {
                Response.StatusCode = (int)System.Net.HttpStatusCode.BadRequest;
                return Content("Sorry, an error occured." + ee.Message);
            }
        }

        // POST: /Auth/Create
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ForgetYourPassword(ForgotPasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                StringBuilder strB = new StringBuilder(500);
                foreach (ModelState modelState in ModelState.Values)
                {
                    foreach (ModelError error in modelState.Errors)
                    {
                        strB.Append(error.ErrorMessage + ".");
                    }
                }
                Response.StatusCode = (int)System.Net.HttpStatusCode.OK;
                return Content(strB.ToString());
            }

            try
            {
                var client = WebApiHttpClient.GetClient();
                string json = JsonConvert.SerializeObject(model);
                var httpContent = new StringContent(json, Encoding.UTF8, "application/json");
                var httpResponse = await client.PostAsync("api/auth/ForgetPassword", httpContent);

              
                    MessageResponse response = await httpResponse.Content.ReadAsAsync<MessageResponse>();
                    Response.StatusCode = (int)System.Net.HttpStatusCode.OK;
                    return Content(response.message);

            }
            catch (Exception)
            {
                Response.StatusCode = (int)System.Net.HttpStatusCode.OK;
                return Content("Sorry, an error occured.");
            }
        }
    }
}