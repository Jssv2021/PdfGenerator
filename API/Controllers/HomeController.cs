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

        public async Task<IActionResult> Index()
        {
            try
            {
                string url = "https://ai-customer-onboarding-dev.azurewebsites.net/api/CustomerPolicyCoverage/5";
                HttpClient client = new HttpClient();

                string response = await client.GetStringAsync(url);

                var data = JsonConvert.DeserializeObject<Dictionary<string, string>>(response);

                string html = ViewsToStringOutputHelper.RenderRazorViewToString(this, "Pdf", data);
                byte[] result;

                using (var memoryStream = new MemoryStream())
                {
                    PdfWriter pdfWriter = new PdfWriter(memoryStream);
                    ConverterProperties converterProperties = new ConverterProperties();
                    PdfDocument pdfDocument = new PdfDocument(pdfWriter);
                    Document document = HtmlConverter.ConvertToDocument(html, pdfDocument, converterProperties);
                    document.SetMargins(0, 0, 0, 0);
                    document.Close();

                    result = memoryStream.ToArray();
                }
                //FileStream pdfDest = System.IO.File.Open("output.pdf", FileMode.OpenOrCreate);
                //PdfWriter pdfWriter = new PdfWriter(pdfDest);
                //ConverterProperties converterProperties = new ConverterProperties();
                //PdfDocument pdfDocument = new PdfDocument(pdfWriter);

                //Document document = HtmlConverter.ConvertToDocument(html, pdfDocument, converterProperties);
                //document.
                return File(result, "application/pdf", "Policy.pdf");
            }
            catch (Exception ex)
            {
                return View(ex);
                throw;
            }

        }

        public async Task<IActionResult> Pdf()
        {
            string url = "https://ai-customer-onboarding-dev.azurewebsites.net/api/CustomerPolicyCoverage/5";
            HttpClient client = new HttpClient();

            string response = await client.GetStringAsync(url);
            var model = JsonConvert.DeserializeObject<CustomerPolicyCoverage>(response);
            return new ViewAsPdf("Pdf", model) { 
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
