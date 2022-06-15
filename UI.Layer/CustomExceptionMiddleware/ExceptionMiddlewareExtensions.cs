using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using System.Net;

namespace UI.Layer.CustomExceptionMiddleware
{
    public static class ExceptionMiddlewareExtensions
    {
        public static void ConfigureExceptionHandler(this IApplicationBuilder app)
        {
            app.UseExceptionHandler(x =>
            {
                x.Run(async context =>
                {
                    context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    context.Response.ContentType = "application/json";
                    var contextfuilder = context.Features.Get<IExceptionHandlerFeature>();
                    if (contextfuilder != null)
                    {
                        await context.Response.WriteAsync(new ErrorDetail
                        {
                            StatusCode=context.Response.StatusCode,
                            Message="Beklenmedik Bir Hata Oluştu"
                        }.ToString());
                    }
                });
            });
        }
    }
}
