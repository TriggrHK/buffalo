using buffalo_3._1.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace buffalo_3._1.Components
{
    public class WrappingFilterViewComponent : ViewComponent
    {
        private buffaloContext _context { get; set; }

        public WrappingFilterViewComponent(buffaloContext temp)
        {
            _context = temp;
        }

        public IViewComponentResult Invoke()
        {
            ViewBag.SelectedWrapping = HttpContext.Session.GetString("WrappingFilter");

            var data = _context.Master
                .Select(x => x.Wrapping)
                .Where(x => x != null && x != "")
                .Distinct()
                .OrderBy(x => x);

            return View(data);
        }
    }
}
