using LaYumba.Functional;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PdfGenerator.Services;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.IO;
using System.Net.Http;
using Newtonsoft.Json;
using API.Models;
using Rotativa.AspNetCore;

namespace PdfGenerator.Controllers
{
    using static F;

    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger) => _logger = logger;

        [HttpGet]
        [Route("{customerId}")]
        public async Task<IActionResult> Pdf(int customerId)
        {
            HttpResponseMessage response = await DataService.GetModelContentAsync(customerId);
            if (!response.IsSuccessStatusCode) return NotFound($"There was no customer matching id {customerId}");
            CustomerPolicyCoverage model = JsonConvert.DeserializeObject<CustomerPolicyCoverage>(await response.Content.ReadAsStringAsync());
            return new ViewAsPdf("Pdf", model)
            {
                FileName = "Policy.pdf"
            };
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
