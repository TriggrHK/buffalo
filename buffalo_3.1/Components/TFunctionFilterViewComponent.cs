using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using buffalo_3._1.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace buffalo_3._1.Components
{
    public class TFunctionFilterViewComponent : ViewComponent
    {
        private buffaloContext _context { get; set; }

        public TFunctionFilterViewComponent (buffaloContext temp)
        {
            _context = temp;
        }

        public IViewComponentResult Invoke()
        {
            ViewBag.SelectedTFunction = HttpContext.Session.GetString("TFunctionFilter");

            var data = _context.Master
                .Select(x => x.Textilefunction)
                .Where(x => x != null && x != "")
                .Distinct()
                .OrderBy(x => x);

            return View(data);
        }
    }
}
