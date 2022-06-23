using DotNetCore.EntityFrameworkCore;
using ShippingMgr.Api.Extensions;
using DotNetCore.AspNetCore;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.SpaServices.Extensions;
using Microsoft.AspNetCore.SpaServices.AngularCli;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
ConfigurationManager configuration = builder.Configuration; // allows both to access and to set up the config
builder.Services.AddControllersWithViews();
builder.Services.AddContext();
builder.Services.ConfigureIISIntegration();
//builder.Services.AddAutoMapper(typeof(Program));
builder.Services.AddServices(configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
    app.UseDeveloperExceptionPage();
else
{
    app.Use(async (context, next) =>
    {
        await next();
        if (context.Response.StatusCode == 404 && !Path.HasExtension(context.Request.Path.Value))
        {
            context.Request.Path = "/index.html"; await next();
        }
    });
    app.UseHsts();
}
app.UseHttpsRedirection();


app.UseRequestLocalization();
app.UseCors("AllowOrigin");
app.UseStaticFiles();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.UseEndpointsMapControllers();
app.MapFallbackToFile("index.html");
if (!app.Environment.IsDevelopment())
{
    app.UseSpaStaticFiles();
}
app.UseSpa(spa =>
{
    // To learn more about options for serving an Angular SPA
    // see https://go.microsoft.com/fwlink/?linkid=864501
    spa.Options.SourcePath = "ClientApp";
    if (app.Environment.IsDevelopment())
    {
        spa.UseAngularCliServer(npmScript: "start");
    }
});

app.Run();

public partial class Program{ }