using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using buffalo_3._1.Models.ViewModels;

namespace buffalo_3._1.Infrastructure
{
    [HtmlTargetElement("div", Attributes = "page-help")]
    public class PaginationTagHelper : TagHelper
    {
        //Dynamically create the page links for us

        private IUrlHelperFactory uhf;

        public PaginationTagHelper(IUrlHelperFactory temp)
        {
            uhf = temp;
        }

        [ViewContext]
        [HtmlAttributeNotBound]
        public ViewContext vc { get; set; }
        
        //different than the view context
        public PageInfo PageHelp { get; set; }
        public string PageAction { get; set; }
        public string PageClass { get; set; }
        public bool PageClassesEnabled { get; set; }
        public string PageClassNormal { get; set; }
        public string PageClassSelected { get; set; }

        public override void Process (TagHelperContext thc, TagHelperOutput tho)
        {
            IUrlHelper uh = uhf.GetUrlHelper(vc);

            TagBuilder final = new TagBuilder("div");

            int startPage = 1;
            if (PageHelp.TotalPages > 5)
            {
                if (PageHelp.CurrentPage > PageHelp.TotalPages - 4)
                {
                    startPage = PageHelp.TotalPages - 4;
                }
                else if (PageHelp.CurrentPage > 3)
                {
                    startPage = PageHelp.CurrentPage - 2;
                }
            }
            int endPage = startPage + 5;
            if (PageHelp.TotalPages <= 5)
            {
                endPage = PageHelp.TotalPages + 1;
            }

            for (int i = startPage; i < endPage; i++)
            {
                TagBuilder tb = new TagBuilder("a");
                tb.Attributes["href"] = uh.Action(PageAction, new { pageNum = i });

                if (PageClassesEnabled)
                {
                    tb.AddCssClass(PageClass);
                    tb.AddCssClass(i == PageHelp.CurrentPage ? PageClassSelected : PageClassNormal);
                }
                
                tb.InnerHtml.Append(i.ToString());

                final.InnerHtml.AppendHtml(tb);

            }

            tho.Content.AppendHtml(final.InnerHtml);
        }
    }
}
