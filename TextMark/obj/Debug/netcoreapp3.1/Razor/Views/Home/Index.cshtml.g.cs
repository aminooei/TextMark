#pragma checksum "C:\Users\Amin\source\repos\TextMark\TextMark\Views\Home\Index.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "a37a3ed22c0baf2206a31e33eb5e2543c1ddb4e0"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Home_Index), @"mvc.1.0.view", @"/Views/Home/Index.cshtml")]
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"a37a3ed22c0baf2206a31e33eb5e2543c1ddb4e0", @"/Views/Home/Index.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"42febadfbd89ac97c00366c611a07bf2235dd9d3", @"/Views/_ViewImports.cshtml")]
    public class Views_Home_Index : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<TextMark.Models.Login>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("class", new global::Microsoft.AspNetCore.Html.HtmlString("w3-light-grey"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
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
        private global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.BodyTagHelper __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_BodyTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#nullable restore
#line 2 "C:\Users\Amin\source\repos\TextMark\TextMark\Views\Home\Index.cshtml"
  
    ViewData["Title"] = "Home Page";

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n\r\n");
#nullable restore
#line 7 "C:\Users\Amin\source\repos\TextMark\TextMark\Views\Home\Index.cshtml"
   if (TempData.Peek("Loggedin_Username") != null)
    {

#line default
#line hidden
#nullable disable
            WriteLiteral("        <p>Welcome to your page ");
#nullable restore
#line 9 "C:\Users\Amin\source\repos\TextMark\TextMark\Views\Home\Index.cshtml"
                           Write(TempData.Peek("Loggedin_Username"));

#line default
#line hidden
#nullable disable
            WriteLiteral(" </p>\r\n");
#nullable restore
#line 10 "C:\Users\Amin\source\repos\TextMark\TextMark\Views\Home\Index.cshtml"
    }

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n\r\n");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("body", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "a37a3ed22c0baf2206a31e33eb5e2543c1ddb4e04327", async() => {
                WriteLiteral(@"
    <nav class=""navbar has-shadow"">
        <div class=""container"">
            <div class=""navbar-brand"">
                <a class=""navbar-item"" href=""/"">
                    <img src=""/static/images/logo.01217b605171.png"" width=""32"" height=""32"">
                    <b>doccano</b>
                </a>



            </div>
            <div id=""topNav"" class=""navbar-menu"">
                <div class=""navbar-end"">

                    <div class=""navbar-item has-dropdown is-hoverable"">
                        <a class=""navbar-link"">
                            <span>Live Demo</span>
                        </a>
                        <div class=""navbar-dropdown"">
                            <a href=""/demo/text-classification/"" class=""navbar-item"">
                                Sentiment Analysis
                            </a>
                            <a href=""/demo/named-entity-recognition/"" class=""navbar-item"">
                                Named Entity Recognition
        ");
                WriteLiteral(@"                    </a>
                            <a href=""/demo/translation/"" class=""navbar-item"">
                                Translation
                            </a>
                        </div>
                    </div>
                    <a class=""navbar-item"" href=""https://github.com/chakki-works/doccano"">
                        <span>GitHub</span>
                    </a>



                    <a class=""navbar-item"" href=""/login/"">
                        <span>Login</span>
                    </a>

                </div>
            </div>
        </div>
    </nav>


    <div class=""columns"" id=""mail-app"" v-cloak>
        <aside class=""column is-3 aside hero is-fullheight"">
            <div>

                <div class=""main pr20 pl20"">
                    <div class=""field has-addons"">
                        <div class=""control is-expanded"">
                            <input class=""input"" type=""text"" placeholder=""Search document"" v-model=""searchQuery"" ");
#nullable restore
#line 68 "C:\Users\Amin\source\repos\TextMark\TextMark\Views\Home\Index.cshtml"
                                                                                                            Write(keyup.enter);

#line default
#line hidden
#nullable disable
                WriteLiteral(@"=""submit"" style=""border-right:none;box-shadow: none;-webkit-box-shadow: none;"">
                        </div>

                        <div class=""control"">
                            <div class=""dropdown is-hoverable is-right"">
                                <div class=""dropdown-trigger"">
                                    <button class=""button"" aria-haspopup=""true"" aria-controls=""dropdown-menu"" style=""border-left:none"">
                                        <span class=""icon has-text-grey pr0"">
                                            <i class=""fas fa-angle-down"" aria-hidden=""true""></i>
                                        </span>
                                    </button>
                                </div>
                                <div class=""dropdown-menu pt0"" id=""dropdown-menu"" role=""menu"">
                                    <div class=""dropdown-content"">
                                        <a class=""dropdown-item"">
                                           ");
                WriteLiteral(@" <label class=""radio"">
                                                <input type=""radio"" value=""all"" v-model=""picked"" checked> All
                                            </label>
                                        </a>
                                        <a class=""dropdown-item"">
                                            <label class=""radio"">
                                                <input type=""radio"" value=""active"" v-model=""picked""> Active
                                            </label>
                                        </a>
                                        <a class=""dropdown-item"">
                                            <label class=""radio"">
                                                <input type=""radio"" value=""completed"" v-model=""picked""> Completed
                                            </label>
                                        </a>
                                    </div>
                                </div>
             ");
                WriteLiteral(@"               </div>
                        </div>
                    </div>
                </div>

                <div class=""main pt0 pb0 pr20 pl20"">
                    <span>About [[ count ]] results</span>
                </div>

                <div class=""main"">
                    <a href=""#"" class=""item"" v-for=""(doc, index) in docs"" v-bind:class=""{ active: index == pageNumber }"" v-on:click=""pageNumber = index""
                       v-bind:data-preview-id=""index"">
                        <span class=""icon"">
                            <i class=""fa fa-check"" v-show=""annotations[index] && annotations[index].length""></i>
                        </span>
                        <span class=""name"">[[ doc.text.slice(0, 60) ]]...</span>
                    </a>
                </div>
            </div>
        </aside>
        <div class=""column is-7 is-offset-1 message hero is-fullheight"" id=""message-pane"">

            <!-- Modal card for creating project. -->
            <div c");
                WriteLiteral(@"lass=""modal"" :class=""{ 'is-active': isActive }"">
                <div class=""modal-background""></div>
                <div class=""modal-card"">
                    <header class=""modal-card-head"">
                        <p class=""modal-card-title"">Annotation Guideline</p>
                        <button class=""delete"" aria-label=""close"" ");
#nullable restore
#line 127 "C:\Users\Amin\source\repos\TextMark\TextMark\Views\Home\Index.cshtml"
                                                             Write(click);

#line default
#line hidden
#nullable disable
                WriteLiteral(@"=""isActive=!isActive""></button>
                    </header>
                    <section class=""modal-card-body modal-card-body-footer content""
                             style=""line-height:150%""
                             v-html=""compiledMarkdown""></section>
                </div>
            </div>

            <div class=""columns is-multiline is-gapless is-mobile is-vertical-center"">
                <div class=""column is-3"">
                    <progress class=""progress is-inline-block"" v-bind:class=""progressColor"" v-bind:value=""achievement"" max=""100"">30%</progress>
                </div>
                <div class=""column is-8"">
                    <span class=""ml10""><strong>[[ total - remaining ]]</strong></span>/<span>[[ total ]]</span>
                </div>
                <div class=""column is-1 has-text-right"">
                    <a class=""button"" ");
#nullable restore
#line 143 "C:\Users\Amin\source\repos\TextMark\TextMark\Views\Home\Index.cshtml"
                                 Write(click);

#line default
#line hidden
#nullable disable
                WriteLiteral(@"=""isActive=!isActive"">
                        <span class=""icon"">
                            <i class=""fas fa-book""></i>
                        </span>
                    </a>
                </div>
            </div>


            <div class=""card"">
                <header class=""card-header"">
                    <div class=""card-header-title has-background-royalblue"" style=""padding:1.5rem;"">
                        <div class=""field is-grouped is-grouped-multiline"">
                            <div class=""control"" v-for=""label in labels"">
                                <div class=""tags has-addons"">
                                    <a class=""tag is-medium"" v-bind:style=""{ color: label.text_color, backgroundColor: label.background_color }"" v-on:click=""annotate(label.id)""
                                       v-shortkey.once=""[ label.shortcut ]"" ");
#nullable restore
#line 159 "C:\Users\Amin\source\repos\TextMark\TextMark\Views\Home\Index.cshtml"
                                                                       Write(shortkey);

#line default
#line hidden
#nullable disable
                WriteLiteral(@"=""annotate(label.id)"">
                                        [[ label.text ]]
                                    </a>
                                    <span class=""tag is-medium"">[[ label.shortcut ]]</span>
                                </div>
                            </div>
                        </div>
                    </div>
                </header>
                <div class=""card-content"">
                    <div class=""content"" v-if=""docs[pageNumber] && annotations[pageNumber]"">
                        <annotator ref=""annotator"" v-bind:labels=""labels"" v-bind:entity-positions=""annotations[pageNumber]"" v-bind:text=""docs[pageNumber].text""
                                   ");
#nullable restore
#line 171 "C:\Users\Amin\source\repos\TextMark\TextMark\Views\Home\Index.cshtml"
                              Write(remove);

#line default
#line hidden
#nullable disable
                WriteLiteral(" -label=\"removeLabel\" ");
#nullable restore
#line 171 "C:\Users\Amin\source\repos\TextMark\TextMark\Views\Home\Index.cshtml"
                                                           Write(add);

#line default
#line hidden
#nullable disable
                WriteLiteral(@" -label=""addLabel""></annotator>
                    </div>
                </div>
            </div>


            <div class=""level mt30"">
                <a class=""button level-left"" v-on:click=""prevPage"" v-shortkey=""{prev1: ['ctrl', 'p'], prev2: ['arrowup'], prev3: ['arrowleft']}""
                   ");
#nullable restore
#line 179 "C:\Users\Amin\source\repos\TextMark\TextMark\Views\Home\Index.cshtml"
              Write(shortkey);

#line default
#line hidden
#nullable disable
                WriteLiteral(@"=""prevPage"">
                    <span class=""icon"">
                        <i class=""fas fa-chevron-left""></i>
                    </span>
                    <span>Prev</span>
                </a>

                <a class=""button level-right"" v-on:click=""nextPage"" v-shortkey=""{next1: ['ctrl', 'n'], next2: ['arrowdown'], next3: ['arrowright']}""
                   ");
#nullable restore
#line 187 "C:\Users\Amin\source\repos\TextMark\TextMark\Views\Home\Index.cshtml"
              Write(shortkey);

#line default
#line hidden
#nullable disable
                WriteLiteral(@"=""nextPage"">
                    <span>Next</span>
                    <span class=""icon"">
                        <i class=""fas fa-chevron-right""></i>
                    </span>
                </a>
            </div>

        </div>
    </div>


    <!-- Page Container -->
    <!--<div class=""w3-content w3-margin-top"" style=""max-width:1400px;"">-->
    <!-- The Grid -->
    <!--<div class=""w3-row-padding"">-->
    <!-- Left Column -->
    <!--<div class=""w3-third"">

        <div class=""w3-display-container"">
            <div class=""w3-white w3-text-grey w3-card-4"">
                 <img src=""https://www.w3schools.com/w3images/avatar_hat.jpg"" style=""width:100%"" alt=""Avatar"">
                <div class=""w3-display-bottomleft w3-container w3-text-black"">
                    <h2>Jane Doe</h2>
                </div>
            </div>
            <div class=""w3-container"">
                <p><i class=""fa fa-briefcase fa-fw w3-margin-right w3-large w3-text-teal""></i>Designer</p>
       ");
                WriteLiteral(@"         <p><i class=""fa fa-home fa-fw w3-margin-right w3-large w3-text-teal""></i>London, UK</p>
                <p><i class=""fa fa-envelope fa-fw w3-margin-right w3-large w3-text-teal""></i>ex@mail.com</p>
                <p><i class=""fa fa-phone fa-fw w3-margin-right w3-large w3-text-teal""></i>1224435534</p>
                <hr>

                <p class=""w3-large""><b><i class=""fa fa-asterisk fa-fw w3-margin-right w3-text-teal""></i>Skills</b></p>
                <p>Adobe Photoshop</p>
                <div class=""w3-light-grey w3-round-xlarge w3-small"">
                    <div class=""w3-container w3-center w3-round-xlarge w3-teal"" style=""width:90%"">90%</div>
                </div>
                <p>Photography</p>
                <div class=""w3-light-grey w3-round-xlarge w3-small"">
                    <div class=""w3-container w3-center w3-round-xlarge w3-teal"" style=""width:80%"">
                        <div class=""w3-center w3-text-white"">80%</div>
                    </div>
                <");
                WriteLiteral(@"/div>
                <p>Illustrator</p>
                <div class=""w3-light-grey w3-round-xlarge w3-small"">
                    <div class=""w3-container w3-center w3-round-xlarge w3-teal"" style=""width:75%"">75%</div>
                </div>
                <p>Media</p>
                <div class=""w3-light-grey w3-round-xlarge w3-small"">
                    <div class=""w3-container w3-center w3-round-xlarge w3-teal"" style=""width:50%"">50%</div>
                </div>
                <br>

                <p class=""w3-large w3-text-theme""><b><i class=""fa fa-globe fa-fw w3-margin-right w3-text-teal""></i>Languages</b></p>
                <p>English</p>
                <div class=""w3-light-grey w3-round-xlarge"">
                    <div class=""w3-round-xlarge w3-teal"" style=""height:24px;width:100%""></div>
                </div>
                <p>Spanish</p>
                <div class=""w3-light-grey w3-round-xlarge"">
                    <div class=""w3-round-xlarge w3-teal"" style=""height:24px;width");
                WriteLiteral(@":55%""></div>
                </div>
                <p>German</p>
                <div class=""w3-light-grey w3-round-xlarge"">
                    <div class=""w3-round-xlarge w3-teal"" style=""height:24px;width:25%""></div>
                </div>
                <br>
            </div>
        </div><br>-->
    <!-- End Left Column -->
    <!--</div>-->
    <!-- Right Column -->
    <!--<div class=""card"">
        <header class=""card-header"">
            <div class=""card-header-title has-background-royalblue"" style=""padding: 1.5rem;"">
                <div class=""field is-grouped is-grouped-multiline"">
                    <div class=""control"">
                        <div class=""tags has-addons"">
                            <a class=""tag is-medium"" style=""color: rgb(255, 255, 255); background-color: rgb(32, 156, 238);"">
                                Person
                            </a> <span class=""tag is-medium"">p</span>
                        </div>
                    </div><div class");
                WriteLiteral(@"=""control"">
                        <div class=""tags has-addons"">
                            <a class=""tag is-medium"" style=""color: rgb(51, 51, 51); background-color: rgb(255, 204, 0);"">
                                Loc
                            </a> <span class=""tag is-medium"">l</span>
                        </div>
                    </div><div class=""control"">
                        <div class=""tags has-addons"">
                            <a class=""tag is-medium"" style=""color: rgb(255, 255, 255); background-color: rgb(51, 51, 51);"">
                                Org
                            </a> <span class=""tag is-medium"">o</span>
                        </div>
                    </div><div class=""control"">
                        <div class=""tags has-addons"">
                            <a class=""tag is-medium"" style=""color: rgb(255, 255, 255); background-color: rgb(51, 204, 153);"">
                                Event
                            </a> <span class=""tag is-m");
                WriteLiteral(@"edium"">e</span>
                        </div>
                    </div><div class=""control"">
                        <div class=""tags has-addons"">
                            <a class=""tag is-medium"" style=""color: rgb(255, 255, 255); background-color: rgb(255, 51, 51);"">
                                Date
                            </a> <span class=""tag is-medium"">d</span>
                        </div>
                    </div><div class=""control"">
                        <div class=""tags has-addons"">
                            <a class=""tag is-medium"" style=""color: rgb(255, 255, 255); background-color: rgb(153, 51, 255);"">
                                Other
                            </a> <span class=""tag is-medium"">z</span>
                        </div>
                    </div>
                </div>
            </div>
        </header>
        <div class=""card-content"">
        <div class=""content"">
            <div>
            <span class="""">-->
    <!---->
    <!--<");
                WriteLiteral(@"/span>
    <span class=""tag"" style=""color: rgb(255, 255, 255); background-color: rgb(32, 156, 238);"">
        Barack Hussein Obama II
        <button class=""
                is-small""></button>
    </span><span class="""">
        (born-->
    <!---->
    <!--</span>
    <span class=""tag"" style=""color: rgb(255, 255, 255); background-color: rgb(255, 51, 51);"">
        August 4, 1961
        <button class=""delete is-small""></button>
        </span>
    <span class="""">) is an-->
    <!---->
    <!--</span>
    <span class=""tag"" style=""color: rgb(255, 255, 255); background-color: rgb(153, 51, 255);"">
        American
        <button class=""delete is-small""></button>
        </span>
    <span class=""""> attorney and politician who served as the 44th President of-->
    <!---->
    <!--</span>
    <span class=""tag"" style=""color: rgb(51, 51, 51); background-color: rgb(255, 204, 0);"">
        the United States
        <button class=""delete is-small""></button>
        </span>
    <span class=""""");
                WriteLiteral(@">
        from-->
    <!---->
    <!--</span>
    <span class=""tag"" style=""color: rgb(255, 255, 255); background-color: rgb(255, 51, 51);"">
        January 20, 2009
        <button class=""delete is-small""></button>
        </span>
    <span class="""">, to-->
    <!---->
    <!--</span>
    <span class=""tag"" style=""color: rgb(255, 255, 255); background-color: rgb(255, 51, 51);"">
        January 20, 2017
        <button class=""delete is-small""></button>
        </span>
    <span class="""">. A member of the-->
    <!---->
    <!--</span>
    <span class=""tag"" style=""color: rgb(255, 255, 255); background-color: rgb(51, 51, 51);"">
        Democratic Party
        <button class=""delete is-small""></button>
        </span>
    <span class="""">, he was the first-->
    <!---->
    <!--</span>
    <span class=""tag"" style=""color: rgb(255, 255, 255); background-color: rgb(153, 51, 255);"">
        African American
        <button class=""delete is-small""></button>
        </span>
    <span class=""");
                WriteLiteral(@"""> to serve as president. He was previously a-->
    <!---->
    <!--</span>
    <span class=""tag"" style=""color: rgb(255, 255, 255); background-color: rgb(153, 51, 255);"">
        United States Senator
        <button class=""delete is-small""></button>
        </span>
    <span class=""""> from-->
    <!---->
    <!--</span>
    <span class=""tag"" style=""color: rgb(51, 51, 51); background-color: rgb(255, 204, 0);"">
        Illinois
        <button class=""delete is-small""></button>
        </span>
    <span class=""""> and a member of the-->
    <!---->
    <!--</span>
    <span class=""tag"" style=""color: rgb(255, 255, 255); background-color: rgb(51, 51, 51);"">
        Illinois State Senate<button class=""delete is-small""></button>
        </span>
    <span class="""">.-->
    <!---->
    <!--</span>
                </div>
            </div>
        </div>
    </div>-->
    <!-- End Grid -->
    <!--</div>-->
    <!-- End Page Container -->
    <!--</div>

    <footer class=""w3-container ");
                WriteLiteral(@"w3-teal w3-center w3-margin-top"">
        <p>Find me on social media.</p>
        <i class=""fa fa-facebook-official w3-hover-opacity""></i>
        <i class=""fa fa-instagram w3-hover-opacity""></i>
        <i class=""fa fa-snapchat w3-hover-opacity""></i>
        <i class=""fa fa-pinterest-p w3-hover-opacity""></i>
        <i class=""fa fa-twitter w3-hover-opacity""></i>
        <i class=""fa fa-linkedin w3-hover-opacity""></i>
    </footer>-->

");
            }
            );
            __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_BodyTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.BodyTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_BodyTagHelper);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_0);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n\r\n");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<TextMark.Models.Login> Html { get; private set; }
    }
}
#pragma warning restore 1591
