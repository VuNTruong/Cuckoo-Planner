#pragma checksum "/Users/vntruong/Documents/VS/Planner/Views/User/_UserManagerItem.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "5abd9d087d2f27a665d0b70410c5c98cc91729d2"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_User__UserManagerItem), @"mvc.1.0.view", @"/Views/User/_UserManagerItem.cshtml")]
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"5abd9d087d2f27a665d0b70410c5c98cc91729d2", @"/Views/User/_UserManagerItem.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"3e8294dd696117b12d3e1960d8f3afcd351d8276", @"/Views/_ViewImports.cshtml")]
    public class Views_User__UserManagerItem : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<Planner.ViewModels.UserProfileViewModel>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("\n<div class=\"work-item\"");
            BeginWriteAttribute("id", " id=\"", 71, "\"", 85, 1);
#nullable restore
#line 3 "/Users/vntruong/Documents/VS/Planner/Views/User/_UserManagerItem.cshtml"
WriteAttributeValue("", 76, Model.Id, 76, 9, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(">\n    <p class=\"title\"");
            BeginWriteAttribute("id", " id=\"", 108, "\"", 128, 2);
            WriteAttributeValue("", 113, "title-", 113, 6, true);
#nullable restore
#line 4 "/Users/vntruong/Documents/VS/Planner/Views/User/_UserManagerItem.cshtml"
WriteAttributeValue("", 119, Model.Id, 119, 9, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(">");
#nullable restore
#line 4 "/Users/vntruong/Documents/VS/Planner/Views/User/_UserManagerItem.cshtml"
                                     Write(Model.FullName);

#line default
#line hidden
#nullable disable
            WriteLiteral("</p>\n    <p class=\"detail\"");
            BeginWriteAttribute("id", " id=\"", 171, "\"", 192, 2);
            WriteAttributeValue("", 176, "detail-", 176, 7, true);
#nullable restore
#line 5 "/Users/vntruong/Documents/VS/Planner/Views/User/_UserManagerItem.cshtml"
WriteAttributeValue("", 183, Model.Id, 183, 9, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(">\n        Email: ");
#nullable restore
#line 6 "/Users/vntruong/Documents/VS/Planner/Views/User/_UserManagerItem.cshtml"
          Write(Model.UserEmail);

#line default
#line hidden
#nullable disable
            WriteLiteral("\n    </p>\n\n    <ul>\n");
#nullable restore
#line 10 "/Users/vntruong/Documents/VS/Planner/Views/User/_UserManagerItem.cshtml"
         foreach (var roleDetailUserProfile in Model.RoleDetailUserProfiles)
        {

#line default
#line hidden
#nullable disable
            WriteLiteral("            <li>");
#nullable restore
#line 12 "/Users/vntruong/Documents/VS/Planner/Views/User/_UserManagerItem.cshtml"
           Write(roleDetailUserProfile.RoleDetail.Role.Name);

#line default
#line hidden
#nullable disable
            WriteLiteral("</li>\n");
#nullable restore
#line 13 "/Users/vntruong/Documents/VS/Planner/Views/User/_UserManagerItem.cshtml"
        }

#line default
#line hidden
#nullable disable
            WriteLiteral("    </ul>\n\n    <div class=\"options\">\n        <button class=\"button\"");
            BeginWriteAttribute("id", " id=\"", 475, "\"", 489, 1);
#nullable restore
#line 17 "/Users/vntruong/Documents/VS/Planner/Views/User/_UserManagerItem.cshtml"
WriteAttributeValue("", 480, Model.Id, 480, 9, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(">Disable</button>\n        <button class=\"button\"");
            BeginWriteAttribute("id", " id=\"", 538, "\"", 552, 1);
#nullable restore
#line 18 "/Users/vntruong/Documents/VS/Planner/Views/User/_UserManagerItem.cshtml"
WriteAttributeValue("", 543, Model.Id, 543, 9, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(" onclick=\"sendPasswordResetTokenToUserWithId(event.target.id)\">Reset password</button>\n    </div>\n</div>");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<Planner.ViewModels.UserProfileViewModel> Html { get; private set; }
    }
}
#pragma warning restore 1591
