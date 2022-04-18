using Microsoft.AspNetCore.Diagnostics;
using NLayer.Core.DTOs;
using NLayer.Service.Exceptions;
using System.Text.Json;

namespace NLayer.API.Middlewares
{
    public static class UseCostomExceptionHandler
    {
        public static void UseCustomException(this IApplicationBuilder app)
        {
            app.UseExceptionHandler(config =>
            {
                //Run sonlandırıcı middleware dir. Hata olduğunda bundan ileri gitmez adımlarda.
                config.Run(async context =>
                {
                    context.Response.ContentType = "application/json"; // cevabın dönüş tipi 

                    var exceptionFeature = context.Features.Get<IExceptionHandlerFeature>(); //hatayı aldım

                    var statusCode = exceptionFeature.Error switch
                    {
                        ClientSideException => 400,
                        NotFoundException => 404,
                        _ => 500
                    };
                    context.Response.StatusCode = statusCode;

                    var response = CustomResponseDTO<NoContentDTO>.Fail(statusCode, exceptionFeature.Error.Message);

                    await context.Response.WriteAsync(JsonSerializer.Serialize(response));
                });
            });
        }
    }
}
