#pragma checksum "/Users/vntruong/Documents/Visual Studio/Planner/Planner/Views/Auth/ResetPassword.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "629760335459b5d4d25524005fbb67cf69fc7df7"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Auth_ResetPassword), @"mvc.1.0.view", @"/Views/Auth/ResetPassword.cshtml")]
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
#line 1 "/Users/vntruong/Documents/Visual Studio/Planner/Planner/Views/_ViewImports.cshtml"
using Planner;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "/Users/vntruong/Documents/Visual Studio/Planner/Planner/Views/_ViewImports.cshtml"
using Planner.Models;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"629760335459b5d4d25524005fbb67cf69fc7df7", @"/Views/Auth/ResetPassword.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"3e8294dd696117b12d3e1960d8f3afcd351d8276", @"/Views/_ViewImports.cshtml")]
    public class Views_Auth_ResetPassword : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<dynamic>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("src", new global::Microsoft.AspNetCore.Html.HtmlString("~/js/auth.js"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("class", new global::Microsoft.AspNetCore.Html.HtmlString("update-form"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
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
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.FormTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper;
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.RenderAtEndOfFormTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#nullable restore
#line 1 "/Users/vntruong/Documents/Visual Studio/Planner/Planner/Views/Auth/ResetPassword.cshtml"
  
    ViewData["Title"] = "Reset password ??";

    string userEmail = (string)ViewData["UserEmail"];

#line default
#line hidden
#nullable disable
            WriteLiteral("\n<h2>");
#nullable restore
#line 7 "/Users/vntruong/Documents/Visual Studio/Planner/Planner/Views/Auth/ResetPassword.cshtml"
Write(ViewData["Header"]);

#line default
#line hidden
#nullable disable
            WriteLiteral("</h2>\n<br>\n<br>\n<div class=\"login-form-area\">\n    ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("script", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "629760335459b5d4d25524005fbb67cf69fc7df74591", async() => {
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
            WriteLiteral("\n    ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("form", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "629760335459b5d4d25524005fbb67cf69fc7df75618", async() => {
                WriteLiteral("\n        <h2>\n            One more step\n        </h2>\n\n        <p>Enter your password reset token and enter your new password. Then, you should be all set. Resetting password for </p>\n        <p id=\"user_email_for_password_reset\">");
#nullable restore
#line 18 "/Users/vntruong/Documents/Visual Studio/Planner/Planner/Views/Auth/ResetPassword.cshtml"
                                         Write(userEmail);

#line default
#line hidden
#nullable disable
                WriteLiteral(@"</p>

        <br>
        <label for=""tokenFieldPasswordReset"">Token: </label><br>
        <input type=""text"" id=""token_password_reset_field"" name=""token_password_reset_field"" placeholder=""Token"" class=""field"">
        <br>

        <label for=""newPasswordPasswordReset"">New password: </label><br>
        <input type=""password"" id=""new_password_password_reset_field"" name=""new_password_password_reset_field"" placeholder=""New password"" class=""field""/>
        <br>

        <label for=""confirmPasswordPasswordReset"">Confirm new password</label><br>
        <input type=""password"" id=""confirm_new_password_password_reset_field"" name=""new_password_password_reset_field"" placeholder=""Confirm new password"" class=""field""");
                BeginWriteAttribute("onclick", " onclick=\"", 1203, "\"", 1269, 3);
                WriteAttributeValue("", 1213, "resetUserPasswordBasedOnTokenAndEmail(event,", 1213, 44, true);
#nullable restore
#line 30 "/Users/vntruong/Documents/Visual Studio/Planner/Planner/Views/Auth/ResetPassword.cshtml"
WriteAttributeValue(" ", 1257, userEmail, 1258, 10, false);

#line default
#line hidden
#nullable disable
                WriteAttributeValue("", 1268, ")", 1268, 1, true);
                EndWriteAttribute();
                WriteLiteral("/>\n        <br>\n\n        <button type=\"submit\" value=\"Submit\" class=\"button\" onclick=\"resetUserPasswordBasedOnTokenAndEmail(event)\">Change password</button>\n    ");
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.FormTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.RenderAtEndOfFormTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_1);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\n</div>");
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