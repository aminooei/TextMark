#pragma checksum "C:\Users\Amin\source\repos\TextMark\TextMark\Views\Admin_Assigned_TextClassificationsTB\ExportDetails.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "c618b6a3d003f0f461ca205a5e264c7ac937fdf2"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Admin_Assigned_TextClassificationsTB_ExportDetails), @"mvc.1.0.view", @"/Views/Admin_Assigned_TextClassificationsTB/ExportDetails.cshtml")]
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
#line 3 "C:\Users\Amin\source\repos\TextMark\TextMark\Views\Admin_Assigned_TextClassificationsTB\ExportDetails.cshtml"
using Microsoft.AspNetCore.Http;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"c618b6a3d003f0f461ca205a5e264c7ac937fdf2", @"/Views/Admin_Assigned_TextClassificationsTB/ExportDetails.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"42febadfbd89ac97c00366c611a07bf2235dd9d3", @"/Views/_ViewImports.cshtml")]
    public class Views_Admin_Assigned_TextClassificationsTB_ExportDetails : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<TextMark.Models.Details_ClassifiedTexts_Tags>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("id", new global::Microsoft.AspNetCore.Html.HtmlString("btn_dl_json_classifications"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("class", new global::Microsoft.AspNetCore.Html.HtmlString("btn btn-warning btn-lg float-right "), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_2 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "Export", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_3 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-controller", "Admin_Assigned_TextClassificationsTB", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_4 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "Index", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
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
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("\r\n");
            WriteLiteral("\r\n");
#nullable restore
#line 5 "C:\Users\Amin\source\repos\TextMark\TextMark\Views\Admin_Assigned_TextClassificationsTB\ExportDetails.cshtml"
  
    ViewData["Title"] = "Export Details of Classifications";
    Layout = "~/Views/Shared/_Layout_Admin.cshtml";

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n");
#nullable restore
#line 10 "C:\Users\Amin\source\repos\TextMark\TextMark\Views\Admin_Assigned_TextClassificationsTB\ExportDetails.cshtml"
 if (Model.ClassifiedTexts_Tags.Count() > 0)
{

#line default
#line hidden
#nullable disable
            WriteLiteral("    <h2> All Classifications Details</h2>\r\n    <table class=\"table\" id=\"DataTableClassificationsTags\">\r\n        <thead>\r\n            <tr>\r\n                <th>\r\n                    ");
#nullable restore
#line 17 "C:\Users\Amin\source\repos\TextMark\TextMark\Views\Admin_Assigned_TextClassificationsTB\ExportDetails.cshtml"
               Write(Html.DisplayNameFor(model => model.ClassifiedTexts_Tags[0].Assigned_TextClassification_ID));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                </th>\r\n                <th>\r\n                    ");
#nullable restore
#line 20 "C:\Users\Amin\source\repos\TextMark\TextMark\Views\Admin_Assigned_TextClassificationsTB\ExportDetails.cshtml"
               Write(Html.DisplayNameFor(model => model.ClassifiedTexts_Tags[0].ClassificationLabels_TB.ClassificationLabel_Text));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                </th>\r\n                <th style=\"display:none;\">\r\n                    ");
#nullable restore
#line 23 "C:\Users\Amin\source\repos\TextMark\TextMark\Views\Admin_Assigned_TextClassificationsTB\ExportDetails.cshtml"
               Write(Html.DisplayNameFor(model => model.ClassifiedTexts_Tags[0].Assigned_TextClassifications_ToUsers_TB.Projects_TB.Project_Name));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                </th>\r\n                <th style=\"display:none;\">\r\n                    ");
#nullable restore
#line 26 "C:\Users\Amin\source\repos\TextMark\TextMark\Views\Admin_Assigned_TextClassificationsTB\ExportDetails.cshtml"
               Write(Html.DisplayNameFor(model => model.ClassifiedTexts_Tags[0].Assigned_TextClassifications_ToUsers_TB.Users_TB.Username));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                </th>\r\n\r\n                <th>\r\n                    ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "c618b6a3d003f0f461ca205a5e264c7ac937fdf27775", async() => {
                WriteLiteral("\r\n                        <span class=\"glyphicon glyphicon-export\" aria-hidden=\"true\"></span> Export\r\n                    ");
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_0);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_1);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Action = (string)__tagHelperAttribute_2.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_2);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Controller = (string)__tagHelperAttribute_3.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_3);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n                </th>\r\n            </tr>\r\n        </thead>\r\n        <tbody>\r\n");
#nullable restore
#line 37 "C:\Users\Amin\source\repos\TextMark\TextMark\Views\Admin_Assigned_TextClassificationsTB\ExportDetails.cshtml"
             foreach (var item in Model.ClassifiedTexts_Tags)
            {

#line default
#line hidden
#nullable disable
            WriteLiteral("                <tr>                  \r\n                    <td>\r\n                        ");
#nullable restore
#line 41 "C:\Users\Amin\source\repos\TextMark\TextMark\Views\Admin_Assigned_TextClassificationsTB\ExportDetails.cshtml"
                   Write(Html.DisplayFor(modelItem => item.Assigned_TextClassification_ID));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                    </td>\r\n                    <td>\r\n                        ");
#nullable restore
#line 44 "C:\Users\Amin\source\repos\TextMark\TextMark\Views\Admin_Assigned_TextClassificationsTB\ExportDetails.cshtml"
                   Write(Html.DisplayFor(modelItem => item.ClassificationLabels_TB.ClassificationLabel_Text));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                    </td>\r\n                    <td style=\"display:none;\">\r\n                        ");
#nullable restore
#line 47 "C:\Users\Amin\source\repos\TextMark\TextMark\Views\Admin_Assigned_TextClassificationsTB\ExportDetails.cshtml"
                   Write(Html.DisplayFor(modelItem => item.Assigned_TextClassifications_ToUsers_TB.Projects_TB.Project_Name));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                    </td>\r\n                    <td style=\"display:none;\">\r\n                        ");
#nullable restore
#line 50 "C:\Users\Amin\source\repos\TextMark\TextMark\Views\Admin_Assigned_TextClassificationsTB\ExportDetails.cshtml"
                   Write(Html.DisplayFor(modelItem => item.Assigned_TextClassifications_ToUsers_TB.Users_TB.Username));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                    </td>                   \r\n                </tr>\r\n");
#nullable restore
#line 53 "C:\Users\Amin\source\repos\TextMark\TextMark\Views\Admin_Assigned_TextClassificationsTB\ExportDetails.cshtml"
            }

#line default
#line hidden
#nullable disable
            WriteLiteral("        </tbody>\r\n    </table>\r\n");
#nullable restore
#line 56 "C:\Users\Amin\source\repos\TextMark\TextMark\Views\Admin_Assigned_TextClassificationsTB\ExportDetails.cshtml"
}

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n<div>\r\n    ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "c618b6a3d003f0f461ca205a5e264c7ac937fdf212136", async() => {
                WriteLiteral("Back to List");
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Action = (string)__tagHelperAttribute_4.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_4);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n</div>\r\n\r\n\r\n");
            DefineSection("Scripts", async() => {
                WriteLiteral(@"
    <script type=""text/javascript"">
        function tableToJson(table) {
            var data = [];
            var headers = [];
            for (var i = 0; i < table.rows[0].cells.length; i++) {
                /*headers[i] = table.rows[0].cells[i].innerHTML.toLowerCase().replace(/\s\s+/g, "" "");*/
                headers[i] = table.rows[0].cells[i].innerHTML.replace(/\s\s+/g, "" "");
            }
            for (var i = 1; i < table.rows.length; i++) {
                var tableRow = table.rows[i];
                var rowData = {};
                for (var j = 0; j < tableRow.cells.length; j++) {
                    /* rowData[headers[j]] = tableRow.cells[j].innerHTML;*/
                    rowData[headers[j]] = tableRow.cells[j].innerHTML.replace(/\s\s+/g, "" "");
                }
                data.push(rowData);
            }
            return data;
        }


        function downloadObjectAsJson(exportObj, exportName) {
            var dataStr = ""data:text/json;charset-utf-8,");
                WriteLiteral(@""" + encodeURIComponent(exportObj);
            var downloadAnchorNode = document.createElement('a');
            downloadAnchorNode.setAttribute(""href"", dataStr);
            downloadAnchorNode.setAttribute(""download"", exportName + "".json"");
            document.body.appendChild(downloadAnchorNode);
            downloadAnchorNode.click();
            downloadAnchorNode.remove();
        }

        document.getElementById('btn_dl_json_classifications').onclick = function () {
            var myjson = JSON.stringify(tableToJson(document.getElementById(""DataTableClassificationsTags"")));
            //var myjson = htmlToJson(document.getElementById(""DataTable""));
            //var myjson = htmlTableToJson(document.getElementById(""DataTable""));
            console.log(myjson.replace(/\\n/g, ''));
            downloadObjectAsJson(myjson, 'the-json-file');
        }
    </script>
");
            }
            );
            WriteLiteral("\r\n\r\n\r\n");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<TextMark.Models.Details_ClassifiedTexts_Tags> Html { get; private set; }
    }
}
#pragma warning restore 1591
