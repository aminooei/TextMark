#pragma checksum "C:\Users\Amin\source\repos\TextMark\TextMark\Views\Admin_Assigned_AnnotationsTB\ExportDetails.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "bc0c94b24c911f46c89c51702c1f52c403312adf"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Admin_Assigned_AnnotationsTB_ExportDetails), @"mvc.1.0.view", @"/Views/Admin_Assigned_AnnotationsTB/ExportDetails.cshtml")]
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
#line 3 "C:\Users\Amin\source\repos\TextMark\TextMark\Views\Admin_Assigned_AnnotationsTB\ExportDetails.cshtml"
using Microsoft.AspNetCore.Http;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"bc0c94b24c911f46c89c51702c1f52c403312adf", @"/Views/Admin_Assigned_AnnotationsTB/ExportDetails.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"42febadfbd89ac97c00366c611a07bf2235dd9d3", @"/Views/_ViewImports.cshtml")]
    public class Views_Admin_Assigned_AnnotationsTB_ExportDetails : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<TextMark.Models.Details_AnnotatedTags>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("id", new global::Microsoft.AspNetCore.Html.HtmlString("btn_dl_json_annotations"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("class", new global::Microsoft.AspNetCore.Html.HtmlString("btn btn-warning btn-lg float-right "), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_2 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "Export", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_3 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-controller", "Admin_Assigned_AnnotationsTB", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
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
#line 5 "C:\Users\Amin\source\repos\TextMark\TextMark\Views\Admin_Assigned_AnnotationsTB\ExportDetails.cshtml"
  
    ViewData["Title"] = "Export Details of Annotations";
    Layout = "~/Views/Shared/_Layout_Admin.cshtml";

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n");
#nullable restore
#line 10 "C:\Users\Amin\source\repos\TextMark\TextMark\Views\Admin_Assigned_AnnotationsTB\ExportDetails.cshtml"
 if (Model.Annotated_Tags.Count() > 0)
{

#line default
#line hidden
#nullable disable
            WriteLiteral("    <h2> All Annotations Details</h2>\r\n    <table class=\"table\" id=\"DataTableTags\">\r\n        <thead>\r\n            <tr>\r\n                <th>\r\n                    ");
#nullable restore
#line 17 "C:\Users\Amin\source\repos\TextMark\TextMark\Views\Admin_Assigned_AnnotationsTB\ExportDetails.cshtml"
               Write(Html.DisplayNameFor(model => model.Annotated_Tags[0].Assigned_TextAnnotation_ID));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                </th>\r\n\r\n                <th>\r\n                    ");
#nullable restore
#line 21 "C:\Users\Amin\source\repos\TextMark\TextMark\Views\Admin_Assigned_AnnotationsTB\ExportDetails.cshtml"
               Write(Html.DisplayNameFor(model => model.Annotated_Tags[0].Labels_TB.Label_Text));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                </th>\r\n                <th>\r\n                    ");
#nullable restore
#line 24 "C:\Users\Amin\source\repos\TextMark\TextMark\Views\Admin_Assigned_AnnotationsTB\ExportDetails.cshtml"
               Write(Html.DisplayNameFor(model => model.Annotated_Tags[0].Annotated_Substring));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                </th>\r\n                <th>\r\n                    ");
#nullable restore
#line 27 "C:\Users\Amin\source\repos\TextMark\TextMark\Views\Admin_Assigned_AnnotationsTB\ExportDetails.cshtml"
               Write(Html.DisplayNameFor(model => model.Annotated_Tags[0].Label_Start_Index));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                </th>\r\n                <th>\r\n                    ");
#nullable restore
#line 30 "C:\Users\Amin\source\repos\TextMark\TextMark\Views\Admin_Assigned_AnnotationsTB\ExportDetails.cshtml"
               Write(Html.DisplayNameFor(model => model.Annotated_Tags[0].Label_End_Index));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                </th>\r\n                <th>\r\n                    ");
#nullable restore
#line 33 "C:\Users\Amin\source\repos\TextMark\TextMark\Views\Admin_Assigned_AnnotationsTB\ExportDetails.cshtml"
               Write(Html.DisplayNameFor(model => model.Annotated_Tags[0].Assigned_Annotations_ToUsers_TB.Users_TB.Username));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                </th>\r\n                <th style=\"display:none;\">\r\n                    ");
#nullable restore
#line 36 "C:\Users\Amin\source\repos\TextMark\TextMark\Views\Admin_Assigned_AnnotationsTB\ExportDetails.cshtml"
               Write(Html.DisplayNameFor(model => model.Annotated_Tags[0].Assigned_Annotations_ToUsers_TB.Projects_TB.Project_Name));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                </th>\r\n                <th>\r\n                    ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "bc0c94b24c911f46c89c51702c1f52c403312adf8681", async() => {
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
#line 46 "C:\Users\Amin\source\repos\TextMark\TextMark\Views\Admin_Assigned_AnnotationsTB\ExportDetails.cshtml"
             foreach (var item in Model.Annotated_Tags)
            {

#line default
#line hidden
#nullable disable
            WriteLiteral("                <tr>\r\n                    <td>\r\n                        ");
#nullable restore
#line 50 "C:\Users\Amin\source\repos\TextMark\TextMark\Views\Admin_Assigned_AnnotationsTB\ExportDetails.cshtml"
                   Write(Html.DisplayFor(modelItem => item.Assigned_TextAnnotation_ID));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                    </td>\r\n\r\n                    <td>\r\n                        ");
#nullable restore
#line 54 "C:\Users\Amin\source\repos\TextMark\TextMark\Views\Admin_Assigned_AnnotationsTB\ExportDetails.cshtml"
                   Write(Html.DisplayFor(modelItem => item.Labels_TB.Label_Text));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                    </td>\r\n                    <td>\r\n                        ");
#nullable restore
#line 57 "C:\Users\Amin\source\repos\TextMark\TextMark\Views\Admin_Assigned_AnnotationsTB\ExportDetails.cshtml"
                   Write(Html.DisplayFor(modelItem => item.Annotated_Substring));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                    </td>\r\n                    <td>\r\n                        ");
#nullable restore
#line 60 "C:\Users\Amin\source\repos\TextMark\TextMark\Views\Admin_Assigned_AnnotationsTB\ExportDetails.cshtml"
                   Write(Html.DisplayFor(modelItem => item.Label_Start_Index));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                    </td>\r\n                    <td>\r\n                        ");
#nullable restore
#line 63 "C:\Users\Amin\source\repos\TextMark\TextMark\Views\Admin_Assigned_AnnotationsTB\ExportDetails.cshtml"
                   Write(Html.DisplayFor(modelItem => item.Label_End_Index));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                    </td>\r\n                    <td>\r\n                        ");
#nullable restore
#line 66 "C:\Users\Amin\source\repos\TextMark\TextMark\Views\Admin_Assigned_AnnotationsTB\ExportDetails.cshtml"
                   Write(Html.DisplayFor(modelItem => item.Assigned_Annotations_ToUsers_TB.Users_TB.Username));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                    </td>\r\n                    <td style=\"display:none;\">\r\n                        ");
#nullable restore
#line 69 "C:\Users\Amin\source\repos\TextMark\TextMark\Views\Admin_Assigned_AnnotationsTB\ExportDetails.cshtml"
                   Write(Html.DisplayFor(modelItem => item.Assigned_Annotations_ToUsers_TB.Projects_TB.Project_Name));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                    </td>\r\n                </tr>\r\n");
#nullable restore
#line 72 "C:\Users\Amin\source\repos\TextMark\TextMark\Views\Admin_Assigned_AnnotationsTB\ExportDetails.cshtml"
            }

#line default
#line hidden
#nullable disable
            WriteLiteral("        </tbody>\r\n    </table>\r\n");
#nullable restore
#line 75 "C:\Users\Amin\source\repos\TextMark\TextMark\Views\Admin_Assigned_AnnotationsTB\ExportDetails.cshtml"
}

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n<div>\r\n    ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "bc0c94b24c911f46c89c51702c1f52c403312adf14012", async() => {
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
            WriteLiteral("\r\n</div>\r\n\r\n");
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


        //function htmlToJson(table) {
        //    var obj = {},
        //        itemsLength = $(table.find('tbody");
                WriteLiteral(@" tr')[0]).find('th').length;
        //    for (i = 0; i < itemsLength; i++) {
        //        key = $(table.find('tbody tr')[0]).find('th').eq(i).text();
        //        value = $(table.find('tbody tr')[1]).find('td').eq(i).text();
        //        obj[key] = value;
        //    }
        //    return JSON.stringify(obj)
        //}

        //function htmlTableToJson(table, edit = 0, del = 0) {
        //    // If exists the cols: ""edit"" and ""del"" to remove from JSON just pass values = 1 to edit and del params
        //    var minus = edit + del;
        //    var data = [];
        //    var colsLength = $(table.find('thead tr')[0]).find('th').length - minus;
        //    var rowsLength = $(table.find('tbody tr')).length;

        //    // first row needs to be headers
        //    var headers = [];
        //    for (var i = 0; i < colsLength; i++) {
        //        headers[i] = $(table.find('thead tr')[0]).find('th').eq(i).text();
        //    }

        //    // go thro");
                WriteLiteral(@"ugh cells
        //    for (var i = 0; i < rowsLength; i++) {
        //        var tableRow = $(table.find('tbody tr')[i]);
        //        var rowData = {};
        //        for (var j = 0; j < colsLength; j++) {
        //            rowData[headers[j]] = tableRow.find('td').eq(j).text();
        //        }
        //        data.push(rowData);
        //    }
        //    return data;
        //}


        function downloadObjectAsJson(exportObj, exportName) {
            var dataStr = ""data:text/json;charset-utf-8,"" + encodeURIComponent(exportObj);
            var downloadAnchorNode = document.createElement('a');
            downloadAnchorNode.setAttribute(""href"", dataStr);
            downloadAnchorNode.setAttribute(""download"", exportName + "".json"");
            document.body.appendChild(downloadAnchorNode);
            downloadAnchorNode.click();
            downloadAnchorNode.remove();
        }

        document.getElementById('btn_dl_json_annotations').onclick = function");
                WriteLiteral(@" () {
            var myjson = JSON.stringify(tableToJson(document.getElementById(""DataTableTags"")));
            //var myjson = htmlToJson(document.getElementById(""DataTable""));
            //var myjson = htmlTableToJson(document.getElementById(""DataTable""));
            console.log(myjson.replace(/\\n/g, ''));
            downloadObjectAsJson(myjson, 'the-json-file');
        }
    </script>
");
            }
            );
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<TextMark.Models.Details_AnnotatedTags> Html { get; private set; }
    }
}
#pragma warning restore 1591