#pragma checksum "/Users/vntruong/Documents/VS/Planner/Views/Role/_RoleOverviewItem.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "554671a6904641aeac42bb7009e8adcfeb91d2e9"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Role__RoleOverviewItem), @"mvc.1.0.view", @"/Views/Role/_RoleOverviewItem.cshtml")]
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"554671a6904641aeac42bb7009e8adcfeb91d2e9", @"/Views/Role/_RoleOverviewItem.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"3e8294dd696117b12d3e1960d8f3afcd351d8276", @"/Views/_ViewImports.cshtml")]
    public class Views_Role__RoleOverviewItem : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<Planner.ViewModels.RoleViewModel>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("\n<div class=\"work-item\"");
            BeginWriteAttribute("id", " id=\"", 64, "\"", 69, 0);
            EndWriteAttribute();
            WriteLiteral(">\n    <p class=\"title\"");
            BeginWriteAttribute("id", " id=\"", 92, "\"", 119, 2);
            WriteAttributeValue("", 97, "rolename-", 97, 9, true);
#nullable restore
#line 4 "/Users/vntruong/Documents/VS/Planner/Views/Role/_RoleOverviewItem.cshtml"
WriteAttributeValue("", 106, Model.RoleId, 106, 13, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(">");
#nullable restore
#line 4 "/Users/vntruong/Documents/VS/Planner/Views/Role/_RoleOverviewItem.cshtml"
                                            Write(Model.RoleName);

#line default
#line hidden
#nullable disable
            WriteLiteral("</p>\n    <div class=\"options\">\n        <button class=\"button\"");
            BeginWriteAttribute("id", " id=\"", 197, "\"", 215, 1);
#nullable restore
#line 6 "/Users/vntruong/Documents/VS/Planner/Views/Role/_RoleOverviewItem.cshtml"
WriteAttributeValue("", 202, Model.RoleId, 202, 13, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(">Remove</button>\n    </div>\n</div>");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<Planner.ViewModels.RoleViewModel> Html { get; private set; }
    }
}
#pragma warning restore 1591
