using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;

namespace Regression.API.Extensions
{
    public static class RootSpaExtensions
    {
        public static void UseRootSpa(this IApplicationBuilder app, string entryFilePath)
        {
            app.UseDefaultFiles();
            app.UseStaticFiles();

            app.Run(async (context) =>
            {
                context.Response.ContentType = "text/html";
                await context.Response.SendFileAsync(entryFilePath);
            });
        }
    }
}