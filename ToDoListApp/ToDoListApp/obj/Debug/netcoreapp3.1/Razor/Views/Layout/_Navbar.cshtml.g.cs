#pragma checksum "B:\Project\ToDoListApp\ToDoListApp\Views\Layout\_Navbar.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "fca1b8baec499657d15ec69cc83c9f9062f16569"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Layout__Navbar), @"mvc.1.0.view", @"/Views/Layout/_Navbar.cshtml")]
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"fca1b8baec499657d15ec69cc83c9f9062f16569", @"/Views/Layout/_Navbar.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"52f945efe976b1d64c78b43e826c929a1c767a43", @"/Views/_ViewImports.cshtml")]
    public class Views_Layout__Navbar : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<dynamic>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral(@"<nav class=""main-header navbar navbar-expand navbar-white navbar-light"">
    <!-- Left navbar links -->
    <ul class=""navbar-nav"">
        <li class=""nav-item"">
            <a class=""nav-link"" data-widget=""pushmenu"" href=""#""><i class=""fas fa-bars""></i></a>
        </li>
        <li class=""nav-item d-none d-sm-inline-block"">
            <a href=""/"" class=""nav-link"">Home</a>
        </li>
    </ul>

    <!-- Right navbar links -->
    <ul class=""navbar-nav ml-auto"">
        <li class=""nav-item"">
            <a class=""nav-link"" href=""/"">
                <i class=""fas fa-sign-out-alt""></i>
            </a>
        </li>
    </ul>
</nav>");
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