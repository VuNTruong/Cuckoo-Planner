#pragma checksum "/Users/vntruong/Documents/VS/Planner/Views/Role/_RoleAssignmentItem.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "5a7d097cc1d00f8fe10647d80625ce0100a8d420"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Role__RoleAssignmentItem), @"mvc.1.0.view", @"/Views/Role/_RoleAssignmentItem.cshtml")]
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"5a7d097cc1d00f8fe10647d80625ce0100a8d420", @"/Views/Role/_RoleAssignmentItem.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"3e8294dd696117b12d3e1960d8f3afcd351d8276", @"/Views/_ViewImports.cshtml")]
    public class Views_Role__RoleAssignmentItem : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<Planner.ViewModels.RoleAssignmentViewModel>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("\n<div class=\"work-item\"");
            BeginWriteAttribute("id", " id=\"", 73, "\"", 78, 0);
            EndWriteAttribute();
            WriteLiteral(">\n    <p class=\"title\"");
            BeginWriteAttribute("id", " id=\"", 101, "\"", 147, 2);
            WriteAttributeValue("", 106, "role-assignment-", 106, 16, true);
#nullable restore
#line 4 "/Users/vntruong/Documents/VS/Planner/Views/Role/_RoleAssignmentItem.cshtml"
WriteAttributeValue("", 122, Model.RoleDetailRoleName, 122, 25, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral("></p>\n    <p class=\"detail\">User: ");
#nullable restore
#line 5 "/Users/vntruong/Documents/VS/Planner/Views/Role/_RoleAssignmentItem.cshtml"
                       Write(Model.UserProfileFullName);

#line default
#line hidden
#nullable disable
            WriteLiteral("</p>\n    <p class=\"detail\">Role: ");
#nullable restore
#line 6 "/Users/vntruong/Documents/VS/Planner/Views/Role/_RoleAssignmentItem.cshtml"
                       Write(Model.RoleDetailRoleName);

#line default
#line hidden
#nullable disable
            WriteLiteral("</p>\n    <div class=\"options\">\n        <button class=\"button\"");
            BeginWriteAttribute("id", " id=\"", 327, "\"", 341, 1);
#nullable restore
#line 8 "/Users/vntruong/Documents/VS/Planner/Views/Role/_RoleAssignmentItem.cshtml"
WriteAttributeValue("", 332, Model.Id, 332, 9, false);

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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<Planner.ViewModels.RoleAssignmentViewModel> Html { get; private set; }
    }
}
#pragma warning restore 1591
