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
    public class CovidController : Controller
    {
        readonly HttpClient httpClient = new HttpClient();

        public CovidController()
        {
            httpClient.BaseAddress = new Uri("https://corona.lmao.ninja/countries/");
            httpClient.DefaultRequestHeaders.Accept.Clear();
        }
        // GET: Covid
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult CovidIndonesia()
        {
            var responseTask = httpClient.GetAsync("indonesia");
            responseTask.Wait();
            var result = responseTask.Result;
            if (result.IsSuccessStatusCode)
            {
                AttributeVM attributeVMs = result.Content.ReadAsAsync<AttributeVM>().Result;
                List<CovidIdVM> chart = new List<CovidIdVM>();
                string[] Label = { "Kasus", "Meninggal", "Sembuh" };
                string[] Value = { attributeVMs.Cases, attributeVMs.Deaths, attributeVMs.Recovered };
                for(var i = 0; i < 3; i++)
                {
                    CovidIdVM covidIdVM = new CovidIdVM();
                    covidIdVM.label = Label[i];
                    covidIdVM.value = Value[i];
                    chart.Add(covidIdVM);
                }
                return Json(JsonConvert.SerializeObject(chart, Formatting.Indented));
            }
            return Json("Internal Server Error");
        }

        public JsonResult CovidAllCountries()
        {
            IEnumerable<AttributeVM> attributeVMs = null;
            var responseTask = httpClient.GetAsync("");
            responseTask.Wait();
            var result = responseTask.Result;
            if (result.IsSuccessStatusCode)
            {
                var readTask = result.Content.ReadAsAsync<IList<AttributeVM>>();
                readTask.Wait();
                attributeVMs = readTask.Result;
                attributeVMs = attributeVMs.Take(10).OrderByDescending(c => c.Cases);
                List<CovidVM> chart = new List<CovidVM>();
                foreach(var c in attributeVMs)
                {
                    CovidVM covidVM = new CovidVM();
                    covidVM.label = c.Country;
                    covidVM.value = c.Cases;
                    chart.Add(covidVM);
                }
                return Json(JsonConvert.SerializeObject(chart, Formatting.Indented));
            }
            return Json("Internal Server Error");
        }
    }
}