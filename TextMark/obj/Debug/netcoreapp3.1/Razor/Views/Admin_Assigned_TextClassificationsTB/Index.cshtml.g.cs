#pragma checksum "C:\Users\Amin\source\repos\TextMark\TextMark\Views\Admin_Assigned_TextClassificationsTB\Index.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "737fa9ee4f058d34dcda801e604c0dd94a5503e2"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Admin_Assigned_TextClassificationsTB_Index), @"mvc.1.0.view", @"/Views/Admin_Assigned_TextClassificationsTB/Index.cshtml")]
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
#line 1 "C:\Users\Amin\source\repos\TextMark\TextMark\Views\_ViewImports.cshtml"
using TextMark;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "C:\Users\Amin\source\repos\TextMark\TextMark\Views\_ViewImports.cshtml"
using TextMark.Models;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "C:\Users\Amin\source\repos\TextMark\TextMark\Views\Admin_Assigned_TextClassificationsTB\Index.cshtml"
using Microsoft.AspNetCore.Http;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"737fa9ee4f058d34dcda801e604c0dd94a5503e2", @"/Views/Admin_Assigned_TextClassificationsTB/Index.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"42febadfbd89ac97c00366c611a07bf2235dd9d3", @"/Views/_ViewImports.cshtml")]
    public class Views_Admin_Assigned_TextClassificationsTB_Index : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<IEnumerable<TextMark.Models.Assigned_TextClassifications_ToUsers_TB>>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("type", new global::Microsoft.AspNetCore.Html.HtmlString("submit"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("value", new global::Microsoft.AspNetCore.Html.HtmlString("Delete"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_2 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("class", new global::Microsoft.AspNetCore.Html.HtmlString("btn btn-danger"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_3 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "DeleteFilter", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_4 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-controller", "Admin_Assigned_TextClassificationsTB", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_5 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "Index", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_6 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("class", new global::Microsoft.AspNetCore.Html.HtmlString("btn btn-info"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_7 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "Details", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_8 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "Delete", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
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
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.FormTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper;
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.RenderAtEndOfFormTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper;
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.FormActionTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_FormActionTagHelper;
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("\r\n");
#nullable restore
#line 4 "C:\Users\Amin\source\repos\TextMark\TextMark\Views\Admin_Assigned_TextClassificationsTB\Index.cshtml"
  
    ViewData["Title"] = "Assigned Text Classifications to Users - Admin Page";
    Layout = "~/Views/Shared/_Layout_Admin.cshtml";

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n<h1>List of Assigned Text Classifications to Users</h1>\r\n\r\n");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("form", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "737fa9ee4f058d34dcda801e604c0dd94a5503e27246", async() => {
                WriteLiteral("\r\n    <div>\r\n        <div class=\"form-group flex-row\">\r\n            <label>Select a Username: </label>\r\n            ");
#nullable restore
#line 15 "C:\Users\Amin\source\repos\TextMark\TextMark\Views\Admin_Assigned_TextClassificationsTB\Index.cshtml"
       Write(Html.DropDownList("User_ID", new SelectList(ViewBag.Users, "User_ID", "Username"), "Select a User", new { @class = "form-control" }));

#line default
#line hidden
#nullable disable
                WriteLiteral("\r\n\r\n");
                WriteLiteral(@"        </div>
        <div class=""flex-row"">
            <input type=""submit"" value=""Show Text Classifications"" class=""btn btn-primary"" />
            <button type=""button"" class=""btn btn-danger"" data-toggle=""modal"" data-target=""#exampleModal"">DELETE</button>
        </div>

    </div>
    <div class=""modal fade"" id=""exampleModal"" tabindex=""-1"" role=""dialog"" aria-labelledby=""exampleModalLabel"" aria-hidden=""true"">
        <div class=""modal-dialog"" role=""document"">
            <div class=""modal-content"">
                <div class=""modal-header"">
                    <h5 class=""modal-title"" id=""exampleModalLabel"">DELETE ALERT !!</h5>
                    <button type=""button"" class=""close"" data-dismiss=""modal"" aria-label=""Close"">
                        <span aria-hidden=""true"">&times;</span>
                    </button>
                </div>
                <div class=""modal-body"">
                    Are you sure to DELETE?
                </div>
                <div class=""modal-footer"">");
                WriteLiteral("\n                    <button type=\"button\" class=\"btn btn-secondary\" data-dismiss=\"modal\">No, Cancel</button>\r\n                    ");
                __tagHelperExecutionContext = __tagHelperScopeManager.Begin("button", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "737fa9ee4f058d34dcda801e604c0dd94a5503e29289", async() => {
                    WriteLiteral("YES, DELETE");
                }
                );
                __Microsoft_AspNetCore_Mvc_TagHelpers_FormActionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.FormActionTagHelper>();
                __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_FormActionTagHelper);
                __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_0);
                __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_1);
                __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_2);
                __Microsoft_AspNetCore_Mvc_TagHelpers_FormActionTagHelper.Action = (string)__tagHelperAttribute_3.Value;
                __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_3);
                __Microsoft_AspNetCore_Mvc_TagHelpers_FormActionTagHelper.Controller = (string)__tagHelperAttribute_4.Value;
                __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_4);
                await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
                if (!__tagHelperExecutionContext.Output.IsContentModified)
                {
                    await __tagHelperExecutionContext.SetOutputContentAsync();
                }
                Write(__tagHelperExecutionContext.Output);
                __tagHelperExecutionContext = __tagHelperScopeManager.End();
                WriteLiteral("\r\n                </div>\r\n            </div>\r\n        </div>\r\n    </div>\r\n");
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.FormTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.RenderAtEndOfFormTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.Action = (string)__tagHelperAttribute_5.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_5);
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.Controller = (string)__tagHelperAttribute_4.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_4);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n");
#nullable restore
#line 46 "C:\Users\Amin\source\repos\TextMark\TextMark\Views\Admin_Assigned_TextClassificationsTB\Index.cshtml"
 if (Model.Count() > 0)
{

#line default
#line hidden
#nullable disable
            WriteLiteral("    <table class=\"table\">\r\n        <thead>\r\n            <tr>\r\n                <th>\r\n                    ");
#nullable restore
#line 52 "C:\Users\Amin\source\repos\TextMark\TextMark\Views\Admin_Assigned_TextClassificationsTB\Index.cshtml"
               Write(Html.DisplayNameFor(model => model.Projects_TB.Project_Name));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                </th>\r\n                <th>\r\n                    ");
#nullable restore
#line 55 "C:\Users\Amin\source\repos\TextMark\TextMark\Views\Admin_Assigned_TextClassificationsTB\Index.cshtml"
               Write(Html.DisplayNameFor(model => model.Users_TB.Username));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                </th>\r\n                <th>\r\n                    ");
#nullable restore
#line 58 "C:\Users\Amin\source\repos\TextMark\TextMark\Views\Admin_Assigned_TextClassificationsTB\Index.cshtml"
               Write(Html.DisplayNameFor(model => model.Annotations_TB.Annotation_Title));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                </th>\r\n                <th>\r\n                    ");
#nullable restore
#line 61 "C:\Users\Amin\source\repos\TextMark\TextMark\Views\Admin_Assigned_TextClassificationsTB\Index.cshtml"
               Write(Html.DisplayNameFor(model => model.Annotations_TB.Annotation_Text));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                </th>\r\n                <th>\r\n                    ");
#nullable restore
#line 64 "C:\Users\Amin\source\repos\TextMark\TextMark\Views\Admin_Assigned_TextClassificationsTB\Index.cshtml"
               Write(Html.DisplayNameFor(model => model.Count_Classifications));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                </th>\r\n                <th>\r\n                    ");
#nullable restore
#line 67 "C:\Users\Amin\source\repos\TextMark\TextMark\Views\Admin_Assigned_TextClassificationsTB\Index.cshtml"
               Write(Html.DisplayNameFor(model => model.Comments));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                </th>\r\n                <th>\r\n                    ");
#nullable restore
#line 70 "C:\Users\Amin\source\repos\TextMark\TextMark\Views\Admin_Assigned_TextClassificationsTB\Index.cshtml"
               Write(Html.DisplayNameFor(model => model.Not_Sure));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                </th>\r\n\r\n                <th></th>\r\n            </tr>\r\n        </thead>\r\n        <tbody>\r\n");
#nullable restore
#line 77 "C:\Users\Amin\source\repos\TextMark\TextMark\Views\Admin_Assigned_TextClassificationsTB\Index.cshtml"
             foreach (var item in Model)
            {

#line default
#line hidden
#nullable disable
            WriteLiteral("                <tr>\r\n                    <td>\r\n                        ");
#nullable restore
#line 81 "C:\Users\Amin\source\repos\TextMark\TextMark\Views\Admin_Assigned_TextClassificationsTB\Index.cshtml"
                   Write(Html.DisplayFor(modelItem => item.Projects_TB.Project_Name));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                    </td>\r\n                    <td>\r\n                        ");
#nullable restore
#line 84 "C:\Users\Amin\source\repos\TextMark\TextMark\Views\Admin_Assigned_TextClassificationsTB\Index.cshtml"
                   Write(Html.DisplayFor(modelItem => item.Users_TB.Username));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                    </td>\r\n");
#nullable restore
#line 86 "C:\Users\Amin\source\repos\TextMark\TextMark\Views\Admin_Assigned_TextClassificationsTB\Index.cshtml"
                     try
                    {

#line default
#line hidden
#nullable disable
            WriteLiteral("                        <td>\r\n                            ");
#nullable restore
#line 89 "C:\Users\Amin\source\repos\TextMark\TextMark\Views\Admin_Assigned_TextClassificationsTB\Index.cshtml"
                       Write(item.Annotations_TB.Annotation_Title.ToString().Substring(0, 15));

#line default
#line hidden
#nullable disable
            WriteLiteral("...\r\n                        </td>\r\n");
#nullable restore
#line 91 "C:\Users\Amin\source\repos\TextMark\TextMark\Views\Admin_Assigned_TextClassificationsTB\Index.cshtml"
                    }
                    catch
                    {
                        

#line default
#line hidden
#nullable disable
#nullable restore
#line 94 "C:\Users\Amin\source\repos\TextMark\TextMark\Views\Admin_Assigned_TextClassificationsTB\Index.cshtml"
                   Write(item.Annotations_TB.Annotation_Title);

#line default
#line hidden
#nullable disable
#nullable restore
#line 94 "C:\Users\Amin\source\repos\TextMark\TextMark\Views\Admin_Assigned_TextClassificationsTB\Index.cshtml"
                                                             
                    }

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n");
#nullable restore
#line 97 "C:\Users\Amin\source\repos\TextMark\TextMark\Views\Admin_Assigned_TextClassificationsTB\Index.cshtml"
                     try
                    {

#line default
#line hidden
#nullable disable
            WriteLiteral("                        <td>\r\n                            ");
#nullable restore
#line 100 "C:\Users\Amin\source\repos\TextMark\TextMark\Views\Admin_Assigned_TextClassificationsTB\Index.cshtml"
                       Write(item.Annotations_TB.Annotation_Text.ToString().Substring(0, 25));

#line default
#line hidden
#nullable disable
            WriteLiteral("...\r\n                        </td>\r\n");
#nullable restore
#line 102 "C:\Users\Amin\source\repos\TextMark\TextMark\Views\Admin_Assigned_TextClassificationsTB\Index.cshtml"
                    }
                    catch
                    {
                        

#line default
#line hidden
#nullable disable
#nullable restore
#line 105 "C:\Users\Amin\source\repos\TextMark\TextMark\Views\Admin_Assigned_TextClassificationsTB\Index.cshtml"
                   Write(item.Annotations_TB.Annotation_Text);

#line default
#line hidden
#nullable disable
#nullable restore
#line 105 "C:\Users\Amin\source\repos\TextMark\TextMark\Views\Admin_Assigned_TextClassificationsTB\Index.cshtml"
                                                            
                    }

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                    <td>\r\n                        ");
#nullable restore
#line 109 "C:\Users\Amin\source\repos\TextMark\TextMark\Views\Admin_Assigned_TextClassificationsTB\Index.cshtml"
                   Write(Html.DisplayFor(modelItem => item.Count_Classifications));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                    </td>\r\n                    <td>\r\n                        ");
#nullable restore
#line 112 "C:\Users\Amin\source\repos\TextMark\TextMark\Views\Admin_Assigned_TextClassificationsTB\Index.cshtml"
                   Write(Html.DisplayFor(modelItem => item.Comments));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                    </td>\r\n                    <td>\r\n");
#nullable restore
#line 115 "C:\Users\Amin\source\repos\TextMark\TextMark\Views\Admin_Assigned_TextClassificationsTB\Index.cshtml"
                         if (item.Not_Sure == true)
                        {

#line default
#line hidden
#nullable disable
            WriteLiteral("                            <label>Yes</label>\r\n");
#nullable restore
#line 118 "C:\Users\Amin\source\repos\TextMark\TextMark\Views\Admin_Assigned_TextClassificationsTB\Index.cshtml"
                        }
                        else
                        {

#line default
#line hidden
#nullable disable
            WriteLiteral("                            <label>No</label>\r\n");
#nullable restore
#line 122 "C:\Users\Amin\source\repos\TextMark\TextMark\Views\Admin_Assigned_TextClassificationsTB\Index.cshtml"
                        }

#line default
#line hidden
#nullable disable
            WriteLiteral("                    </td>\r\n                    <td>\r\n");
            WriteLiteral("                        ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "737fa9ee4f058d34dcda801e604c0dd94a5503e221335", async() => {
                WriteLiteral("Details");
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_6);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Action = (string)__tagHelperAttribute_7.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_7);
            if (__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues == null)
            {
                throw new InvalidOperationException(InvalidTagHelperIndexerAssignment("asp-route-projectID", "Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper", "RouteValues"));
            }
            BeginWriteTagHelperAttribute();
#nullable restore
#line 126 "C:\Users\Amin\source\repos\TextMark\TextMark\Views\Admin_Assigned_TextClassificationsTB\Index.cshtml"
                                                                              WriteLiteral(item.Project_ID);

#line default
#line hidden
#nullable disable
            __tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["projectID"] = __tagHelperStringValueBuffer;
            __tagHelperExecutionContext.AddTagHelperAttribute("asp-route-projectID", __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["projectID"], global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
            BeginWriteTagHelperAttribute();
#nullable restore
#line 126 "C:\Users\Amin\source\repos\TextMark\TextMark\Views\Admin_Assigned_TextClassificationsTB\Index.cshtml"
                                                                                                              WriteLiteral(item.Assigned_TextClassification_ID);

#line default
#line hidden
#nullable disable
            __tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["id"] = __tagHelperStringValueBuffer;
            __tagHelperExecutionContext.AddTagHelperAttribute("asp-route-id", __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["id"], global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n                        ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "737fa9ee4f058d34dcda801e604c0dd94a5503e224508", async() => {
                WriteLiteral("Delete");
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_2);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Action = (string)__tagHelperAttribute_8.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_8);
            if (__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues == null)
            {
                throw new InvalidOperationException(InvalidTagHelperIndexerAssignment("asp-route-id", "Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper", "RouteValues"));
            }
            BeginWriteTagHelperAttribute();
#nullable restore
#line 127 "C:\Users\Amin\source\repos\TextMark\TextMark\Views\Admin_Assigned_TextClassificationsTB\Index.cshtml"
                                                                        WriteLiteral(item.Assigned_TextClassification_ID);

#line default
#line hidden
#nullable disable
            __tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["id"] = __tagHelperStringValueBuffer;
            __tagHelperExecutionContext.AddTagHelperAttribute("asp-route-id", __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["id"], global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n                    </td>\r\n                </tr>\r\n");
#nullable restore
#line 130 "C:\Users\Amin\source\repos\TextMark\TextMark\Views\Admin_Assigned_TextClassificationsTB\Index.cshtml"
            }

#line default
#line hidden
#nullable disable
            WriteLiteral("        </tbody>\r\n    </table>\r\n");
#nullable restore
#line 133 "C:\Users\Amin\source\repos\TextMark\TextMark\Views\Admin_Assigned_TextClassificationsTB\Index.cshtml"
}

#line default
#line hidden
#nullable disable
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<IEnumerable<TextMark.Models.Assigned_TextClassifications_ToUsers_TB>> Html { get; private set; }
    }
}
#pragma warning restore 1591
