using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace PdfGenerator.Helpers
{
    public class ViewsToStringOutputHelper
    {
        public static string RenderRazorViewToString(Controller controller, string viewName, object model)
        {
            controller.ViewData.ModelState.Clear();
            ViewDataDictionary viewDataDictionary = new ViewDataDictionary(controller.ViewData);
            IDictionary<string, string> dictionary = (IDictionary<string, string>)model;
            dictionary.ToList().ForEach(kv =>
            {
                viewDataDictionary.Add(kv.Key, kv.Value);
            });
            controller.ViewData = viewDataDictionary;

            using (var sw = new StringWriter())
            {
                ViewEngineResult viewResult = null;
                var engine = controller.HttpContext.RequestServices.GetService(typeof(ICompositeViewEngine)) as ICompositeViewEngine;

                viewResult = engine.FindView(controller.ControllerContext, viewName, false);

                var viewContext = new ViewContext(controller.ControllerContext, viewResult.View, controller.ViewData, controller.TempData, sw, new HtmlHelperOptions());
                viewResult.View.RenderAsync(viewContext);

                return sw.GetStringBuilder().ToString();
            }
        }
    }
}