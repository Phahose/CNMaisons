using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container
builder.Services.AddRazorPages();
builder.Services.AddSession();
var app = builder.Build();

//Configure the HTTP reqest pipeline
if (!app.Environment.IsDevelopment()) // check for any enviromnet that is not a development 
{
    app.UseDeveloperExceptionPage(); // not for production, remove this line when all production testing is done 
                                     // app.UseExceptionHandler("/Error"); customised error page use for final release
}
app.UseStaticFiles(); // add for wwroot
app.UseRouting();
app.UseSession();
app.MapRazorPages();

app.Run();
