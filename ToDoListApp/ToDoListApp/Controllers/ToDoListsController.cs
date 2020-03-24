using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using DataTables.AspNet.AspNetCore;
using DataTables.AspNet.Core;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ToDoListAppData.Model;
using ToDoListAppData.ViewModel;

namespace ToDoListApp.Controllers
{
    public class ToDoListsController : Controller
    {
        readonly HttpClient httpClient = new HttpClient();
        public ToDoListsController()
        {
            httpClient.BaseAddress = new Uri("https://localhost:44323/api/");
            //httpClient.DefaultRequestHeaders.Accept.Clear();
        }
        // GET: ToDoLists
        public ActionResult Index()
        {
            if (HttpContext.Session.GetString("Token") != null)
            {
                httpClient.DefaultRequestHeaders.Add("Authorization", HttpContext.Session.GetString("Token"));
                //ViewBag.ToDoLists = ToDoLists();
                return View();
            }
            else
            {
                return RedirectToAction("","Users");
            }
        }

        // GET : ToDoLists/Lists
        //public IList<ToDoListVM> ToDoLists()
        //{
        //    var iduser = HttpContext.Session.GetString("IdUser");
        //    IList<ToDoListVM> toDoLists = null;
        //    var responseTask = httpClient.GetAsync("ToDoLists/Users/"+iduser);
        //    responseTask.Wait();
        //    var result = responseTask.Result;
        //    if (result.IsSuccessStatusCode)
        //    {
        //        var readTask = result.Content.ReadAsAsync<IList<ToDoListVM>>();
        //        readTask.Wait();
        //        toDoLists = readTask.Result;
        //    }

        //    return toDoLists;
        //}

        public async Task<ToDoListVM> GetData(int status, string keyword, int size, int page)
        {
            httpClient.DefaultRequestHeaders.Add("Authorization", HttpContext.Session.GetString("Token"));
            var iduser = HttpContext.Session.GetString("IdUser");
            var url = "ToDoLists/Data?uid=" + iduser + "&status=" + status + "&keyword=" + keyword + "&page=" + page + "&size=" + size;
            HttpResponseMessage response = await httpClient.GetAsync(url);
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadAsAsync<ToDoListVM>();
            }
            return null;
        }

        [HttpGet("ToDoLists/Data/{status}")]
        public IActionResult Data(IDataTablesRequest request, int status)
        {
            var pageSize = request.Length;
            var pageNumber = request.Start / request.Length + 1;
            string keyword = string.Empty;
            if (request.Search.Value != null)
            {
                keyword = request.Search.Value;
            }
            var dataPage = GetData(status, keyword, pageSize, pageNumber).Result;
            var filteredData = dataPage.filterLength;
            var response = DataTablesResponse.Create(request, dataPage.length, filteredData, dataPage.data);
            return new DataTablesJsonResult(response, true);
        }

        // GET: ToDoLists/Details/5
        public JsonResult Get(int id, string userId)
        {
            var cek = httpClient.GetAsync("ToDoLists/" + id + "/" + userId).Result;
            var read = cek.Content.ReadAsAsync<ToDoListVM>().Result;
            return Json(new { data = read });
        }

        // POST: ToDoLists/Create
        public ActionResult Create(ToDoListVM toDoListVM)
        {
            try
            {
                var myContent = JsonConvert.SerializeObject(toDoListVM);
                var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
                var byteContent = new ByteArrayContent(buffer);
                byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                var affectedRow = httpClient.PostAsync("ToDoLists", byteContent).Result;
                return Json(new { data = affectedRow, affectedRow.StatusCode });
            }
            catch
            {
                return View();
            }
        }

        // POST: ToDoLists/Edit/5
        public ActionResult Edit(int id, ToDoListVM toDoListVM)
        {
            try
            {
                var myContent = JsonConvert.SerializeObject(toDoListVM);
                var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
                var ByteContent = new ByteArrayContent(buffer);
                ByteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                var update = httpClient.PutAsync("ToDoLists/" + id, ByteContent).Result;
                return Json(new { data = update, update.StatusCode });
            }
            catch
            {
                return View();
            }
        }

        // GET: ToDoLists/Delete/5
        public ActionResult Delete(int id, string userId)
        {
            var affectedRow = httpClient.DeleteAsync("ToDoLists/" + id + "/" + userId).Result;
            return Json(new { data = affectedRow });
        }

        // POST: ToDoLists/UpdateStatus/5
        public ActionResult UpdateStatus(int id, string userId, ToDoListVM toDoListVM)
        {
            try
            {
                var getdata = Get(id, userId);
                var myContent = JsonConvert.SerializeObject(toDoListVM);
                var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
                var ByteContent = new ByteArrayContent(buffer);
                ByteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                var update = httpClient.PutAsync("ToDoLists/Status/" + id + "/" + userId, ByteContent).Result;
                return Json(new { data = update, update.StatusCode });
            }
            catch
            {
                return View();
            }
        }
    }
}