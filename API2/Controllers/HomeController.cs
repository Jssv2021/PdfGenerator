using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PdfGenerator.Helpers;
using PdfGenerator.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Entities.Models;
using Syncfusion.HtmlConverter;
using Syncfusion.Pdf;
using System.IO;
using System.Net.Http;
using Newtonsoft.Json;

namespace PdfGenerator.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public async Task<IActionResult> Index()
        {
            HtmlToPdfConverter htmlConverter = new HtmlToPdfConverter();
            WebKitConverterSettings settings = new WebKitConverterSettings();

            string url = "https://ai-customer-onboarding-dev.azurewebsites.net/api/CustomerPolicyCoverage/5";
            HttpClient client = new HttpClient();

            string response = await client.GetStringAsync(url);

            var data = JsonConvert.DeserializeObject<CustomerPolicyCoverage>(response);

            string html = ViewsToStringOutputHelper.RenderRazorViewToString(this, "Pdf", data);
            htmlConverter = new HtmlToPdfConverter();

            string baseUrl = @"C:/Temp/HTMLFiles/";

            //Set WebKit path
            settings.WebKitPath = @"QtBinariesDotNetCore\";

            //Assign WebKit settings to HTML converter
            htmlConverter.ConverterSettings = settings;

            //Convert HTML string to PDF
            PdfDocument document = htmlConverter.Convert(html, baseUrl);

            //Save the document into stream.
            MemoryStream stream = new MemoryStream();

            document.Save(stream);

            stream.Position = 0;

            //Close the document.
            document.Close(true);

            //Defining the ContentType for pdf file.
            string contentType = "application/pdf";

            //Define the file name.
            string fileName = "Output.pdf";

            //Creates a FileContentResult object by using the file contents, content type, and file name.
            return File(stream, contentType, fileName);
        }

        public IActionResult Pdf()
        {
            return View();
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
