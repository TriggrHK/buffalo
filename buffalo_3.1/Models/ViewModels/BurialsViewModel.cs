using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace buffalo_3._1.Models.ViewModels
{
    public class BurialsViewModel
    {
        public IQueryable<Master> Burials { get; set; }
        public PageInfo PageInfo { get; set; }
    }
}
