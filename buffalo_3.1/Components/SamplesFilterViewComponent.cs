using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using buffalo_3._1.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace buffalo_3._1.Components
{
    public class SamplesFilterViewComponent : ViewComponent
    {
        private buffaloContext _context { get; set; }

        public SamplesFilterViewComponent (buffaloContext temp)
        {
            _context = temp;
        }

        public IViewComponentResult Invoke()
        {

            ViewBag.SelectedSamples = HttpContext.Session.GetString("SamplesFilter");

            var data = _context.Master
                .Select(x => x.Samplescollected)
                .Where(x => x != null && x != "")
                .Distinct()
                .OrderBy(x => x);

            return View(data);
        }
    }
}
