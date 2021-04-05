using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PdfGenerator.Helpers;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.IO;
using System.Net.Http;
using Newtonsoft.Json;
using API.Models;
using iText.Html2pdf;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Kernel.Geom;
using iText.Layout.Element;
using Rotativa.AspNetCore;

namespace PdfGenerator.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        [Route("/Home/Index/{customerId}")]
        [HttpGet]
        public async Task<IActionResult> Index(int? customerId)
        {
            if (customerId != null)
            {
                string url = $"https://ai-customer-onboarding-dev.azurewebsites.net/api/CustomerPolicyCoverage/{customerId}";
                HttpClient client = new HttpClient();

                string response = await client.GetStringAsync(url);
                var model = JsonConvert.DeserializeObject<CustomerPolicyCoverage>(response);
                return new ViewAsPdf("Pdf", model)
                {
                    FileName = "Policy.pdf"
                };
            }
            else return View("Empty");
        }

        [HttpGet]
        [Route("{customerId}")]
        public async Task<IActionResult> Pdf(int? customerId)
        {
            
            if (customerId != null)
            {
                string url = $"https://ai-customer-onboarding-dev.azurewebsites.net/api/CustomerPolicyCoverage/{customerId}";
                HttpClient client = new HttpClient();

                string response = await client.GetStringAsync(url);
                var model = JsonConvert.DeserializeObject<CustomerPolicyCoverage>(response);
                return new ViewAsPdf("Pdf", model)
                {
                    FileName = "Policy.pdf"
                };
            }
            else return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }


    }
}
