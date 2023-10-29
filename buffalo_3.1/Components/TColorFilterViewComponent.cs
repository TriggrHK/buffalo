using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using buffalo_3._1.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace buffalo_3._1.Components
{
    public class TColorFilterViewComponent : ViewComponent
    {
        private buffaloContext _context { get; set; }

        public TColorFilterViewComponent (buffaloContext temp)
        {
            _context = temp;
        }

        public IViewComponentResult Invoke()
        {
            ViewBag.SelectedTColor = HttpContext.Session.GetString("TColorFilter");

            var data = _context.Master
                .Select(x => x.Color)
                .Where(x => x != null && x != "")
                .Distinct()
                .OrderBy(x => x);

            return View(data);
        }
    }
}
