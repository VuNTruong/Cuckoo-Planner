#pragma checksum "/Users/vntruong/Documents/VS/Planner/Views/User/UserManager.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "7b906be950f8234f23c0cfabae139c4f49ed4e42"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_User_UserManager), @"mvc.1.0.view", @"/Views/User/UserManager.cshtml")]
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
#nullable restore
#line 1 "/Users/vntruong/Documents/VS/Planner/Views/User/UserManager.cshtml"
using Planner.ViewModels;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"7b906be950f8234f23c0cfabae139c4f49ed4e42", @"/Views/User/UserManager.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"3e8294dd696117b12d3e1960d8f3afcd351d8276", @"/Views/_ViewImports.cshtml")]
    public class Views_User_UserManager : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<dynamic>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("\n");
#nullable restore
#line 3 "/Users/vntruong/Documents/VS/Planner/Views/User/UserManager.cshtml"
  
    ViewData["Title"] = "List of users";

    // Get list of user profiles from controller
    var users = ViewData["Users"] as List<UserProfileViewModel>;

#line default
#line hidden
#nullable disable
            WriteLiteral("\n<h2>");
#nullable restore
#line 10 "/Users/vntruong/Documents/VS/Planner/Views/User/UserManager.cshtml"
Write(ViewData["Header"]);

#line default
#line hidden
#nullable disable
            WriteLiteral("</h2>\n\n<div class=\"list-of-work-item\">\n");
#nullable restore
#line 13 "/Users/vntruong/Documents/VS/Planner/Views/User/UserManager.cshtml"
     foreach (var user in users)
    {
        

#line default
#line hidden
#nullable disable
#nullable restore
#line 15 "/Users/vntruong/Documents/VS/Planner/Views/User/UserManager.cshtml"
   Write(await Html.PartialAsync("_UserManagerItem", user));

#line default
#line hidden
#nullable disable
#nullable restore
#line 15 "/Users/vntruong/Documents/VS/Planner/Views/User/UserManager.cshtml"
                                                          ;
    }

#line default
#line hidden
#nullable disable
            WriteLiteral("</div>");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<dynamic> Html { get; private set; }
    }
}
#pragma warning restore 1591
