using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using buffalo_3._1.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace buffalo_3._1.Components
{
    public class HeadFilterViewComponent : ViewComponent
    {
        private buffaloContext _context { get; set; }

        public HeadFilterViewComponent (buffaloContext temp)
        {
            _context = temp;
        }

        public IViewComponentResult Invoke()
        {
            ViewBag.SelectedHead = HttpContext.Session.GetString("HeadFilter");

            var data = _context.Master
                .Select(x => x.Headdirection)
                .Where(x => x != null && x != "" && x != "N LL")
                .Distinct()
                .OrderBy(x => x);

            return View(data);
        }
    }
}
