using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using LaYumba.Functional;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Rotativa.AspNetCore;
using System.Net.Http;
using API.Models;
using System.Threading.Tasks;

namespace PdfGenerator.Services
{
    using static F;
    public class DataService
    {
        public static Func<int, Task<HttpResponseMessage>> GetModelContentAsync = async (id) =>
        {
            string url = $"https://ai-customer-onboarding-dev.azurewebsites.net/api/CustomerPolicyCoverage/{id}";
            HttpClient client = new HttpClient();
            return await client.GetAsync(url);
        };
    }
}