using buffalo_3._1.Models;
using buffalo_3._1.Models.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace buffalo_3._1.Controllers
{
    public class HomeController : Controller
    {
        private buffaloContext _context { get; set; }

        public HomeController(buffaloContext temp)
        {
            _context = temp;
        }

        public IActionResult Index()
        {         
            return View();
        }

        public IActionResult ClearFilters()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Burials");
        }

        public IActionResult Burials(string AdultFilter, string WrappingFilter, string HairFilter,
            string SamplesFilter, string AgeFilter, string HeadFilter,
            string TFunctionFilter, string TColorFilter, string SFunctionFilter,
            int pageNum = 1)
        {


            int pageSize = 12;

            if (AdultFilter != null && AdultFilter != "")
            {
                HttpContext.Session.SetString("AdultFilter", AdultFilter);
            }
            if (AgeFilter != null && AgeFilter != "")
            {
                HttpContext.Session.SetString("AgeFilter", AgeFilter);
            }
            if (HairFilter != null && HairFilter != "")
            {
                HttpContext.Session.SetString("HairFilter", HairFilter);
            }
            if (HeadFilter != null && HeadFilter != "")
            {
                HttpContext.Session.SetString("HeadFilter", HeadFilter);
            }
            if (SamplesFilter != null && SamplesFilter != "")
            {
                HttpContext.Session.SetString("SamplesFilter", SamplesFilter);
            }
            if (WrappingFilter != null && WrappingFilter != "")
            {
                HttpContext.Session.SetString("WrappingFilter", WrappingFilter);
            }
            if (TFunctionFilter != null && TFunctionFilter != "")
            {
                HttpContext.Session.SetString("TFunctionFilter", TFunctionFilter);
            }
            if (TColorFilter != null && TColorFilter != "")
            {
                HttpContext.Session.SetString("TColorFilter", TColorFilter);
            }
            if (SFunctionFilter != null && SFunctionFilter != "")
            {
                HttpContext.Session.SetString("SFunctionFilter", SFunctionFilter);
            }



            AdultFilter = HttpContext.Session.GetString("AdultFilter");
            WrappingFilter = HttpContext.Session.GetString("WrappingFilter");
            AgeFilter = HttpContext.Session.GetString("AgeFilter");
            SamplesFilter = HttpContext.Session.GetString("SamplesFilter");
            HairFilter = HttpContext.Session.GetString("HairFilter");
            HeadFilter = HttpContext.Session.GetString("HeadFilter");
            TFunctionFilter = HttpContext.Session.GetString("TFunctionFilter");
            TColorFilter = HttpContext.Session.GetString("TColorFilter");
            SFunctionFilter = HttpContext.Session.GetString("SFunctionFilter");

            var Burials = _context.Master
                .Where(p => p.Textilefunction == TFunctionFilter || TFunctionFilter == null)
                .Where(p => p.Color == TColorFilter || TColorFilter == null)
                .Where(p => p.Structurefunction == SFunctionFilter || SFunctionFilter == null)
                .Where(p => p.Adultsubadult == AdultFilter || AdultFilter == null)
                .Where(p => p.Wrapping == WrappingFilter || WrappingFilter == null)
                .Where(p => p.Ageatdeath == AgeFilter || AgeFilter == null)
                .Where(p => p.Samplescollected == SamplesFilter || SamplesFilter == null)
                .Where(p => p.Haircolor == HairFilter || HairFilter == null)
                .Where(p => p.Headdirection == HeadFilter || HeadFilter == null);

            //anyplace that says burialType needs to be replaced by something else...
            var x = new BurialsViewModel
            {
                Burials = Burials.OrderBy(p => p.Masterid)
                .Skip((pageNum - 1) * pageSize)
                .Take(pageSize),

                PageInfo = new PageInfo
                {
                    CurrentPage = pageNum,
                    BurialsPerPage = pageSize,
                    TotalNumBurials = Burials.Count()//AdultFilter == null ? _context.Burialmain.Count()
                    //    : _context.Burialmain.Where(x => x.Adultsubadult == AdultFilter && x.Wrapping == WrappingFilter).Count()
                    //TotalNumBurials = _context.Burialmain.Count()
                }
            };
            return View(x);
        }

        public IActionResult Burial_Details(int burialId)
        {
            //var burial = _context.Burialmain
            //    .Where(x => x.Burialid == burialId)
            //    .FirstOrDefault();

            var burial = _context.Master
                .Where(x => x.Masterid == burialId)
                .FirstOrDefault();

            return View(burial);
        }

        public IActionResult Supervised()
        {
            //var presCat = _context.Burialmain
            //    .Select(x => x.Preservation)
            //    .Distinct()
            //    .ToArray();

            //ViewBag.preservationCats = presCat;

            return View();
        }

        public IActionResult Unsupervised()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [HttpGet]
        public IActionResult AddRecord()
        {
            return View(new Master());
        }

        [HttpPost]
        public IActionResult AddRecord(Master jedi)
        {
            if (ModelState.IsValid)
            {
                _context.Add(jedi);
                _context.SaveChanges();

            }           
            return RedirectToAction("Burials");    
        }

        [HttpGet]
        public IActionResult Edit(int burialId)
        {
            var record = _context.Master.Single(x => x.Masterid == burialId);

            return View("AddRecord", record);
        }

        [HttpPost]
        public IActionResult Edit(Master maestro)
        {
            _context.Update(maestro);
            _context.SaveChanges();

            return RedirectToAction("Burials");
        }
        
        [HttpGet]
        public IActionResult DeleteRecord(int burialId)
        {
            var record = _context.Master.Single(x => x.Masterid == burialId);

            return View(record);
        }

        [HttpPost]
        public IActionResult DeleteRecord(Master shifu)
        {
            _context.Master.Remove(shifu);
            _context.SaveChanges();

            HttpContext.Session.Clear();

            return RedirectToAction("Burials");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
