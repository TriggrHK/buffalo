using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace buffalo_3._1.Models
{
    public partial class Master
    {
        public long? Id { get; set; }
        public string Text { get; set; }
        public string Adultsubadult { get; set; }
        public string Wrapping { get; set; }
        public string Westtohead { get; set; }
        public string Length { get; set; }
        public string Westtofeet { get; set; }
        public string Southtofeet { get; set; }
        public string Fieldbookexcavationyear { get; set; }
        public string Clusternumber { get; set; }
        public string Samplescollected { get; set; }
        public string Fieldbookpage { get; set; }
        public string Preservation { get; set; }
        public string Squarenorthsouth { get; set; }
        public string Northsouth { get; set; }
        public string Squareeastwest { get; set; }
        public string Eastwest { get; set; }
        public string Area { get; set; }
        public string Burialnumber { get; set; }
        public string Sex { get; set; }
        public string Depth { get; set; }
        public string Ageatdeath { get; set; }
        public string Headdirection { get; set; }
        public string Haircolor { get; set; }
        public string Structurefunction { get; set; }
        public string Description { get; set; }
        public string Color { get; set; }
        public string Textilefunction { get; set; }
        [Key]
        [Required]
        public int Masterid { get; set; }
    }
}
