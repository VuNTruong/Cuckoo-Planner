#pragma checksum "/Users/vntruong/Documents/VS/Planner/Views/WorkItem/_WorkItemOverviewItem.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "502cfec0b5a7d378b4fef790e94eedff4e17b37a"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_WorkItem__WorkItemOverviewItem), @"mvc.1.0.view", @"/Views/WorkItem/_WorkItemOverviewItem.cshtml")]
namespace AspNetCore
{
    #line hidden
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.AspNetCore.Mvc.ViewFeatures;
#nullable restore
#line 1 "/Users/vntruong/Documents/VS/Planner/Views/_ViewImports.cshtml"
using Planner;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "/Users/vntruong/Documents/VS/Planner/Views/_ViewImports.cshtml"
using Planner.Models;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"502cfec0b5a7d378b4fef790e94eedff4e17b37a", @"/Views/WorkItem/_WorkItemOverviewItem.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"3e8294dd696117b12d3e1960d8f3afcd351d8276", @"/Views/_ViewImports.cshtml")]
    public class Views_WorkItem__WorkItemOverviewItem : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<Planner.ViewModels.WorkItemViewModel>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("\n");
            WriteLiteral("\n<div class=\"work-item\"");
            BeginWriteAttribute("id", " id=\"", 126, "\"", 140, 1);
#nullable restore
#line 5 "/Users/vntruong/Documents/VS/Planner/Views/WorkItem/_WorkItemOverviewItem.cshtml"
WriteAttributeValue("", 131, Model.Id, 131, 9, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(">\n    <p class=\"title\"");
            BeginWriteAttribute("id", " id=\"", 163, "\"", 183, 2);
            WriteAttributeValue("", 168, "title-", 168, 6, true);
#nullable restore
#line 6 "/Users/vntruong/Documents/VS/Planner/Views/WorkItem/_WorkItemOverviewItem.cshtml"
WriteAttributeValue("", 174, Model.Id, 174, 9, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(">");
#nullable restore
#line 6 "/Users/vntruong/Documents/VS/Planner/Views/WorkItem/_WorkItemOverviewItem.cshtml"
                                     Write(Model.Title);

#line default
#line hidden
#nullable disable
            WriteLiteral("</p>\n    <p class=\"detail\"");
            BeginWriteAttribute("id", " id=\"", 223, "\"", 244, 2);
            WriteAttributeValue("", 228, "detail-", 228, 7, true);
#nullable restore
#line 7 "/Users/vntruong/Documents/VS/Planner/Views/WorkItem/_WorkItemOverviewItem.cshtml"
WriteAttributeValue("", 235, Model.Id, 235, 9, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(">");
#nullable restore
#line 7 "/Users/vntruong/Documents/VS/Planner/Views/WorkItem/_WorkItemOverviewItem.cshtml"
                                       Write(Model.Content);

#line default
#line hidden
#nullable disable
            WriteLiteral("</p>\n    <div class=\"options\">\n        <button class=\"button\"");
            BeginWriteAttribute("id", " id=\"", 321, "\"", 335, 1);
#nullable restore
#line 9 "/Users/vntruong/Documents/VS/Planner/Views/WorkItem/_WorkItemOverviewItem.cshtml"
WriteAttributeValue("", 326, Model.Id, 326, 9, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(" onclick=\"onDelete(event.target.id)\">Remove</button>\n        <button class=\"button\"");
            BeginWriteAttribute("id", " id=\"", 419, "\"", 433, 1);
#nullable restore
#line 10 "/Users/vntruong/Documents/VS/Planner/Views/WorkItem/_WorkItemOverviewItem.cshtml"
WriteAttributeValue("", 424, Model.Id, 424, 9, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(" onclick=\"openUpdateTaskMenu(event.target.id)\">Update</button>\n    </div>\n</div>");
        }
        #pragma warning restore 1998
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.ViewFeatures.IModelExpressionProvider ModelExpressionProvider { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IUrlHelper Url { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IViewComponentHelper Component { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IJsonHelper Json { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<Planner.ViewModels.WorkItemViewModel> Html { get; private set; }
    }
}
#pragma warning restore 1591
