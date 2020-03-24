using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ToDoListAppData.ViewModel;

namespace ToDoListApp.Controllers
{
    public class UsersController : Controller
    {
        readonly HttpClient httpClient = new HttpClient();
        public UsersController()
        {
            httpClient.BaseAddress = new Uri("https://localhost:44323/api/");
            httpClient.DefaultRequestHeaders.Accept.Clear();
        }

        // GET: Users
        public ActionResult Index()
        {
            var username = HttpContext.Session.GetString("Token");
            if (username == null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Index", "ToDoLists");
            }
        }

        [Route("Logout")]
        public ActionResult Logout()
        {
            var username = HttpContext.Session.GetString("Token");
            if (username == null)
            {
                return RedirectToAction("", "Users");
            }
            else
            {
                HttpContext.Session.Remove("IdUser");
                HttpContext.Session.Remove("Username");
                HttpContext.Session.Remove("Name");
                HttpContext.Session.Remove("Token");
                return RedirectToAction(nameof(Index));
            }
        }

        // POST: Login
        public ActionResult Login(UserVM userVM)
        {
            var myContent = JsonConvert.SerializeObject(userVM);
            var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            var affectedRow = httpClient.PostAsync("Users/Login", byteContent).Result;
            if (affectedRow.IsSuccessStatusCode)
            {
                var readTask = affectedRow.Content.ReadAsStringAsync().Result.Replace("\"", "").Split("...");
                var token = "Bearer " + readTask[0];
                var username = readTask[1];
                var iduser = readTask[2];
                var name = readTask[3];
                HttpContext.Session.SetString("IdUser", iduser);
                HttpContext.Session.SetString("Token", token);
                var cek = httpClient.GetAsync("Get/" + iduser).Result;
                var read = cek.Content.ReadAsStringAsync().Result;
                //userVM.Id = Convert.ToInt16(iduser);
                HttpContext.Session.SetString("Username", username);
                HttpContext.Session.SetString("Name", name);
                return Json(new { data = read, affectedRow.StatusCode });
            }
            return Json(new { affectedRow.StatusCode });
        }

        // GET: Users/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Users/Create
        public ActionResult Register()
        {
            return View();
        }

        // POST: Users/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Users/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Users/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Users/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Users/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}