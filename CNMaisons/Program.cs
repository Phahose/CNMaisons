using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container
builder.Services.AddRazorPages();
builder.Services.AddSession();
//builder.Services.AddHostedService<BackgroundWorkerService>();

builder.Services.AddRazorPages().AddRazorPagesOptions(o =>
{
    o.Conventions.ConfigureFilter(new IgnoreAntiforgeryTokenAttribute());
});

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
.AddCookie(options =>
{
    options.LoginPath = "/Login";
});
builder.Services.AddAuthorization();
var app = builder.Build();

//Configure the HTTP reqest pipeline
if (!app.Environment.IsDevelopment()) // check for any enviromnet that is not a development 
{
    app.UseDeveloperExceptionPage();
    // app.UseExceptionHandler("/Error"); //customised error page use for final release
}
else
{
    app.UseDeveloperExceptionPage(); // not for production, remove this line when all production testing is done 
}
app.UseStaticFiles(); // add for wwroot
app.UseRouting();
app.UseSession();
app.MapRazorPages();
app.UseAuthentication();
app.UseAuthorization();
app.Run();
