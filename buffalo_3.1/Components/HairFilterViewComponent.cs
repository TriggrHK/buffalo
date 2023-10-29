using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using buffalo_3._1.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace buffalo_3._1.Components
{
    public class HairFilterViewComponent : ViewComponent
    {
        private buffaloContext _context { get; set; }

        public HairFilterViewComponent (buffaloContext temp)
        {
            _context = temp;
        }

        public IViewComponentResult Invoke()
        {
            ViewBag.SelectedHair = HttpContext.Session.GetString("HairFilter");
            
            var data = _context.Master
                .Select(x => x.Haircolor)
                .Where(x => x != null && x != "")
                .Distinct()
                .OrderBy(x => x);

            return View(data);
        }
    }
}
