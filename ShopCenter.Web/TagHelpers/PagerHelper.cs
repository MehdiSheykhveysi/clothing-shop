using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Shop.Domain.Entities;

namespace ShopCenter.Web.TagHelpers
{
    [HtmlTargetElement("nav", Attributes = "Page-data")]
    public class PagerHelper : TagHelper
    {
        public PagerHelper(IUrlHelperFactory urlHelperFactory)
        {
            _urlHelperFactory = urlHelperFactory;
        }
        public IUrlHelperFactory _urlHelperFactory { get; set; }
        [ViewContext]
        [HtmlAttributeNotBound]
        public ViewContext ViewContext { get; set; }
        public PageData PageData { get; set; }
        public string Action { get; set; }
        public string Controller { get; set; }
        public string PageCategory { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            IUrlHelper urlHelper = _urlHelperFactory.GetUrlHelper(ViewContext);
            TagBuilder ul = new TagBuilder("ul");
            ul.Attributes["class"] = "myHelperclass";
            for (int i = 1; i <= PageData.TotalPages(); i++)
            {
                TagBuilder li = new TagBuilder("li");
                TagBuilder a = new TagBuilder("a");
                if (string.IsNullOrEmpty(PageCategory))
                {
                    a.Attributes["href"] = urlHelper.Action(Action, Controller, new { PageNumber = i });
                }
                else
                {
                    a.Attributes["href"] = urlHelper.Action(Action, Controller, new { PageNumber = i, Category = PageCategory });
                }
                a.InnerHtml.Append(i.ToString());
                li.InnerHtml.AppendHtml(a);
                ul.InnerHtml.AppendHtml(li);
            }
            output.Content.AppendHtml(ul);
        }
    }
}