using AparmentManager2023.Areas.Identity;
using AparmentManager2023.Data;
using AparmentManager2023.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using System.Configuration;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Json;
using MudBlazor.Services;
using AparmentManager2023.Areas.Identity.Data;
using Syncfusion.Blazor;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<ApplicationDBContext>(options => 
options.UseNpgsql(builder.Configuration.GetConnectionString("MyConnection")).LogTo((message) => Console.WriteLine(message))

);

builder.Services.AddDefaultIdentity<AparmentManager2023User>(options => options.SignIn.RequireConfirmedAccount = true).AddEntityFrameworkStores<ApplicationDBContext>();
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddScoped<OwnerService>();
builder.Services.AddScoped<ApartmentService>();
builder.Services.AddScoped<BillService>();
builder.Services.AddScoped<Person_ApartmentService>();
builder.Services.AddScoped<Monthly_BillService>();
builder.Services.AddMudServices();
builder.Services.AddSyncfusionBlazor();

Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("MTQ4NDQxMEAzMjMxMmUzMTJlMzMzNWtFaERUSWdzTjY1M3gwaEVock16NjcrbUtVUnBtV3lKNGl3ckMxYzIwcFk9");


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllers();
app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();
