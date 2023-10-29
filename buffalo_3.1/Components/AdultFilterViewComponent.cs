using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using buffalo_3._1.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace buffalo_3._1.Components
{
    public class AdultFilterViewComponent : ViewComponent
    {
        private buffaloContext _context { get; set; }

        public AdultFilterViewComponent (buffaloContext temp)
        {
            _context = temp;
        }

        public IViewComponentResult Invoke()
        {
            ViewBag.SelectedAdult = HttpContext.Session.GetString("AdultFilter");

            var data = _context.Master
                .Select(x => x.Adultsubadult)
                .Where(x => x != null && x != "" && x != "N LL")
                .Distinct()
                .OrderBy(x => x);

            return View(data);
        }
    }
}
