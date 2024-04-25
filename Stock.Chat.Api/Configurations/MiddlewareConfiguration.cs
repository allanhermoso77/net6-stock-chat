using Stock.Chat.Api.Middlewares;

namespace Stock.Chat.Api.Configurations
{
    public static class MiddlewareConfiguration
    {
        public static void AddMiddlewares(this IApplicationBuilder app)
        {
            app.UseMiddleware<RequestHandlerMiddleware>();
            app.UseMiddleware<ExceptionHandlerMiddleware>();
            app.UseMiddleware<ResponseHandlerMiddleware>();
        }
    }
}
