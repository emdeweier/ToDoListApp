#pragma checksum "B:\Project\ToDoListApp\ToDoListApp\Views\Layout\_Footer.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "f7c20e9f7ef5e6f06aa20e57826f1f49b97f0523"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Layout__Footer), @"mvc.1.0.view", @"/Views/Layout/_Footer.cshtml")]
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
#line 1 "B:\Project\ToDoListApp\ToDoListApp\Views\_ViewImports.cshtml"
using ToDoListApp;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "B:\Project\ToDoListApp\ToDoListApp\Views\_ViewImports.cshtml"
using ToDoListApp.Models;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"f7c20e9f7ef5e6f06aa20e57826f1f49b97f0523", @"/Views/Layout/_Footer.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"52f945efe976b1d64c78b43e826c929a1c767a43", @"/Views/_ViewImports.cshtml")]
    public class Views_Layout__Footer : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<dynamic>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("<footer class=\"main-footer\">\r\n    Copyright &copy; ");
#nullable restore
#line 2 "B:\Project\ToDoListApp\ToDoListApp\Views\Layout\_Footer.cshtml"
                Write(DateTime.Now.Year);

#line default
#line hidden
#nullable disable
            WriteLiteral(@" <a href=""/"">To Do List App</a>. Template by <a href=""http://adminlte.io"">AdminLTE.io</a>.
    All rights reserved.
    <div class=""float-right d-none d-sm-inline-block"">
        Code With <i class=""fa fa-heart""></i> by #bapaknyajono
    </div>
</footer>");
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
