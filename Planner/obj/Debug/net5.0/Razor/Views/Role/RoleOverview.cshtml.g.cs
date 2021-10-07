#pragma checksum "/Users/vntruong/Documents/VS/Planner/Views/Role/RoleOverview.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "cc8d95ae32e6bc9124c7fb11d37ebe465d7c1900"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Role_RoleOverview), @"mvc.1.0.view", @"/Views/Role/RoleOverview.cshtml")]
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
#line 1 "/Users/vntruong/Documents/VS/Planner/Views/Role/RoleOverview.cshtml"
using Planner.ViewModels;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"cc8d95ae32e6bc9124c7fb11d37ebe465d7c1900", @"/Views/Role/RoleOverview.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"3e8294dd696117b12d3e1960d8f3afcd351d8276", @"/Views/_ViewImports.cshtml")]
    public class Views_Role_RoleOverview : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<dynamic>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("src", new global::Microsoft.AspNetCore.Html.HtmlString("~/js/role.js"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        #line hidden
        #pragma warning disable 0649
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperExecutionContext __tagHelperExecutionContext;
        #pragma warning restore 0649
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner __tagHelperRunner = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner();
        #pragma warning disable 0169
        private string __tagHelperStringValueBuffer;
        #pragma warning restore 0169
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager __backed__tagHelperScopeManager = null;
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager __tagHelperScopeManager
        {
            get
            {
                if (__backed__tagHelperScopeManager == null)
                {
                    __backed__tagHelperScopeManager = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager(StartTagHelperWritingScope, EndTagHelperWritingScope);
                }
                return __backed__tagHelperScopeManager;
            }
        }
        private global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#nullable restore
#line 2 "/Users/vntruong/Documents/VS/Planner/Views/Role/RoleOverview.cshtml"
  
    ViewData["Title"] = "List of roles";

    // Get list of roles from controller
    var roles = ViewData["Roles"] as List<RoleViewModel>;

#line default
#line hidden
#nullable disable
            WriteLiteral("\n<h2>");
#nullable restore
#line 9 "/Users/vntruong/Documents/VS/Planner/Views/Role/RoleOverview.cshtml"
Write(ViewData["Header"]);

#line default
#line hidden
#nullable disable
            WriteLiteral("</h2>\n\n<div class=\"add-more-work-area\">\n    <p class=\"text-desc\">Add more roles</p>\n    ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("script", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "cc8d95ae32e6bc9124c7fb11d37ebe465d7c19004108", async() => {
            }
            );
            __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_0);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral(@"
    <div class=""text-enter-area"">
        <textarea placeholder=""Name"" class=""text-field"" id=""new_role_name_text_field""></textarea>
        <textarea placeholder=""Description"" class=""text-field"" id=""new_role_description_text_field""></textarea>
    </div>
    <button class=""button"" onclick=""onAddRole()"">Add</button>
</div>

<div class=""list-of-work-item"">
");
#nullable restore
#line 22 "/Users/vntruong/Documents/VS/Planner/Views/Role/RoleOverview.cshtml"
     foreach (var role in roles)
    {
        

#line default
#line hidden
#nullable disable
#nullable restore
#line 24 "/Users/vntruong/Documents/VS/Planner/Views/Role/RoleOverview.cshtml"
   Write(await Html.PartialAsync("_RoleOverviewItem", role));

#line default
#line hidden
#nullable disable
#nullable restore
#line 24 "/Users/vntruong/Documents/VS/Planner/Views/Role/RoleOverview.cshtml"
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
