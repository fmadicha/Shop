using System.Text;
using Microsoft.AspNetCore.Razor.TagHelpers;
namespace Shop.Data.Components.TagHelpers
{
    public class PaginationTagHelpers : TagHelper
    {/*
        public overide void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "nav";
            output.TagMode = TagMode.StartTagAndEndTag;
            output.Attributes.Add("aria-label", "Page navigation");
            output.Content.SetHtmlContent(AddPageContent());
        }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public int PageCount { get; set; }
        public int PageRange { get; set; }
        public string PageFirst { get; set; }
        public string PageLast { get; set; }
        public string PageTarget { get; set; }
        private string AddPageContent()
        {
            if (PageRange == 0)
            {
                PageRange == 1;
            }
        }
        */
    }
}