using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using DataTables.AspNet.AspNetCore;
using DataTables.AspNet.Core;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using OfficeOpenXml;
using ToDoListApp.Reports;
using ToDoListAppData.Model;
using ToDoListAppData.ViewModel;

namespace ToDoListApp.Controllers
{
    [Authorize(Roles = "User")]
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
            if (HttpContext.Session.GetString("Token") != null)
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
            else
            {
                return RedirectToAction("","Users");
            }
        }

        [Route("ToDoLists/Excel/{userId}")]
        public async Task<IActionResult> Excel(string userId)
        {
            httpClient.DefaultRequestHeaders.Add("Authorization", HttpContext.Session.GetString("Token"));
            IEnumerable<ToDoListVM> todolists = GetDataToDoLists(userId);
            string Status = null;
            string CompletedDate = null;

            var columnheader = new string[]
            {
                "Id",
                "Name",
                "Date",
                "Completed Date",
                "Status"
            };

            byte[] result;

            using (var package = new ExcelPackage())
            {
                // add a new worksheet to the empty workbook
                var worksheet = package.Workbook.Worksheets.Add("Current ToDoList"); //Worksheet name
                using (var cells = worksheet.Cells[1, 1, 1, 5]) //(1,1) (1,5)
                {
                    cells.Style.Font.Bold = true;
                }
                //First add the headers
                for (var i = 0; i < columnheader.Count(); i++)
                {
                    worksheet.Cells[1, i + 1].Value = columnheader[i];
                    worksheet.Cells[1, i + 1].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
                    worksheet.Cells[1, i + 1].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Left;
                }
                //Add values
                var j = 2;
                var num = 0;
                foreach (var tdl in todolists)
                {
                    worksheet.Cells["A" + j].Value = num+1;
                    worksheet.Cells["A" + j].AutoFitColumns();
                    worksheet.Cells["A" + j].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
                    worksheet.Cells["A" + j].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Left;

                    worksheet.Cells["B" + j].Value = tdl.Name;
                    worksheet.Cells["B" + j].AutoFitColumns();
                    worksheet.Cells["B" + j].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
                    worksheet.Cells["B" + j].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Left;

                    worksheet.Cells["C" + j].Value = tdl.CreateDate.ToString("dd MMMM yyyy");
                    worksheet.Cells["C" + j].AutoFitColumns();
                    worksheet.Cells["C" + j].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
                    worksheet.Cells["C" + j].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Left;

                    if (tdl.CompletedDate.ToString() == null ||
                    tdl.CompletedDate.GetValueOrDefault().ToString("dd MMMM yyyy") == "01 January 0001")
                    {
                        CompletedDate = "On Progress";
                    }
                    else
                    {
                        CompletedDate = tdl.CompletedDate.GetValueOrDefault().ToString("dd MMMM yyyy");
                    }
                    worksheet.Cells["D" + j].Value = CompletedDate;
                    worksheet.Cells["D" + j].AutoFitColumns();
                    worksheet.Cells["D" + j].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
                    worksheet.Cells["D" + j].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Left;

                    if (tdl.Status == false)
                    {
                        Status = "Active";
                    }
                    else
                    {
                        Status = "Completed";
                    }
                    worksheet.Cells["E" + j].Value = Status;
                    worksheet.Cells["E" + j].AutoFitColumns();
                    worksheet.Cells["E" + j].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
                    worksheet.Cells["E" + j].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Left;
                    j++;
                    num++;
                }
                result = package.GetAsByteArray();
            }
            return File(result, "application/ms-excel", $"ToDoList Table.xlsx");
        }

        [Route("ToDoLists/Report/{userId}")]
        public ActionResult Report(string userId, ToDoListVM toDoListVM, UserVM userVM)
        {
            if (HttpContext.Session.GetString("Token") != null)
            {
                httpClient.DefaultRequestHeaders.Add("Authorization", HttpContext.Session.GetString("Token"));
                var name = HttpContext.Session.GetString("Name");
                var date = DateTimeOffset.Now.ToLocalTime().ToString("yyyy-MM-dd");
                ToDoListsReport toDoListsReport = new ToDoListsReport();
                byte[] qbytes = toDoListsReport.PrepareReport(GetDataToDoLists(userId), GetDataUser(userId));
                return File(qbytes, "application/pdf", "ToDoLists(" + name + ", " + date + ").pdf");
            }
            else
            {
                return RedirectToAction("", "Users");
            }
        }

        public List<ToDoListVM> GetDataToDoLists(string userId)
        {
            List<ToDoListVM> toDoLists = new List<ToDoListVM>();
            var responseTask = httpClient.GetAsync("ToDoLists/GetToDoLists/"+userId);
            responseTask.Wait();
            var result = responseTask.Result;
            if (result.IsSuccessStatusCode)
            {
                var readTask = result.Content.ReadAsAsync<List<ToDoListVM>>();
                readTask.Wait();
                toDoLists = readTask.Result;
            }
            return toDoLists;
        }

        public UserVM GetDataUser(string userId)
        {
            UserVM user = new UserVM();
            var cek = httpClient.GetAsync("ToDoLists/GetDataUser/" + userId).Result;
            user = cek.Content.ReadAsAsync<UserVM>().Result;
            return user;
        }

        // GET: ToDoLists/Details/5
        public JsonResult Get(int id, string userId)
        {
            httpClient.DefaultRequestHeaders.Add("Authorization", HttpContext.Session.GetString("Token"));
            var cek = httpClient.GetAsync("ToDoLists/" + id + "/" + userId).Result;
            var read = cek.Content.ReadAsAsync<ToDoListVM>().Result;
            return Json(new { data = read });
        }

        // POST: ToDoLists/Create
        public ActionResult Create(ToDoListVM toDoListVM)
        {
            if (HttpContext.Session.GetString("Token") != null)
            {
                try
                {
                    httpClient.DefaultRequestHeaders.Add("Authorization", HttpContext.Session.GetString("Token"));
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
            else
            {
                return RedirectToAction("", "Users");
            }
        }

        // POST: ToDoLists/Edit/5
        public ActionResult Edit(int id, ToDoListVM toDoListVM)
        {
            if (HttpContext.Session.GetString("Token") != null)
            {
                try
                {
                    httpClient.DefaultRequestHeaders.Add("Authorization", HttpContext.Session.GetString("Token"));
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
            else
            {
                return RedirectToAction("", "Users");
            }
        }

        // GET: ToDoLists/Delete/5
        public ActionResult Delete(int id, string userId)
        {
            if (HttpContext.Session.GetString("Token") != null)
            {
                httpClient.DefaultRequestHeaders.Add("Authorization", HttpContext.Session.GetString("Token"));
                var affectedRow = httpClient.DeleteAsync("ToDoLists/" + id + "/" + userId).Result;
                return Json(new { data = affectedRow });
            }
            else
            {
                return RedirectToAction("", "Users");
            }
        }

        // POST: ToDoLists/UpdateStatus/5
        public ActionResult UpdateStatus(int id, string userId, ToDoListVM toDoListVM)
        {
            if (HttpContext.Session.GetString("Token") != null)
            {
                try
                {
                    httpClient.DefaultRequestHeaders.Add("Authorization", HttpContext.Session.GetString("Token"));
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
            else
            {
                return RedirectToAction("", "Users");
            }
        }
    }
}