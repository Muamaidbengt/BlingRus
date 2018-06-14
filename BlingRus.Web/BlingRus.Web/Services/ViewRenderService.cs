using System;
using System.IO;
using System.Threading.Tasks;
using BlingRus.Domain.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Routing;

namespace BlingRus.Web.Services
{
    public class ViewRenderService : IViewRenderService
    {
        private readonly IRazorViewEngine _viewEngine;
        private readonly ITempDataProvider _tempDataProvider;
        private readonly IServiceProvider _serviceProvider;

        public ViewRenderService(IRazorViewEngine viewEngine,
            ITempDataProvider tempDataProvider,
            IServiceProvider serviceProvider)
        {
            _viewEngine = viewEngine;
            _tempDataProvider = tempDataProvider;
            _serviceProvider = serviceProvider;
        }

        public string RenderToString<TModel>(string viewPath, TModel model)
        {
            var viewEngineResult = _viewEngine.GetView("~/", viewPath, false);

            if (!viewEngineResult.Success)
            {
                throw new InvalidOperationException($"Couldn't find view {viewPath}");
            }

            var view = viewEngineResult.View;

            using (var sw = new StringWriter())
            {
                var viewContext = new ViewContext()
                {
                    HttpContext =
                        new DefaultHttpContext { RequestServices = _serviceProvider },
                    ViewData = new ViewDataDictionary<TModel>(new EmptyModelMetadataProvider(),
                        new ModelStateDictionary())
                    { Model = model },
                    Writer = sw
                };
                view.RenderAsync(viewContext).GetAwaiter().GetResult();
                return sw.ToString();
            }
        }

        public async Task<string> RenderToStringAsync<TModel>(string viewName, TModel model)
        {
            var httpContext = new DefaultHttpContext { RequestServices = _serviceProvider };
            var actionContext = new ActionContext(httpContext, new RouteData(), new ActionDescriptor());

            using (var sw = new StringWriter())
            {
                var viewResult = _viewEngine.FindView(actionContext, viewName, false);

                if (viewResult.View == null)
                {
                    throw new ArgumentNullException($"{viewName} does not match any available view");
                }

                var viewDictionary =
                    new ViewDataDictionary(new EmptyModelMetadataProvider(), new ModelStateDictionary())
                    {
                        Model = model
                    };

                var viewContext = new ViewContext(
                    actionContext,
                    viewResult.View,
                    viewDictionary,
                    new TempDataDictionary(actionContext.HttpContext, _tempDataProvider),
                    sw,
                    new HtmlHelperOptions()
                );

                await viewResult.View.RenderAsync(viewContext);
                return sw.ToString();
            }
        }

        public string RenderToString(string viewPath)
        {
            return RenderToString(viewPath, string.Empty);
        }

        public Task<string> RenderToStringAsync(string viewName)
        {
            return RenderToStringAsync<string>(viewName, string.Empty);
        }
    }
}