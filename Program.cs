using ESSTaskBatool.Data;
using ESSTaskBatool.Repos;
using ESSTaskBatool.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.EntityFrameworkCore;
using System.Configuration;
using System;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
//using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNetCore.Builder;


var builder = WebApplication.CreateBuilder(args);
//Register dbcontext
builder.Services.AddDbContext<Tanent1Context>(options =>
{
    options.UseMySql(builder.Configuration.GetConnectionString("tanent1Connection"), Microsoft.EntityFrameworkCore.ServerVersion.Parse("8.0.34-mysql"));
});

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddSingleton<WeatherForecastService>();
builder.Services.AddScoped<ITanent, TanentServices>();
builder.Services.AddScoped<IUser, UserServices>();
builder.Services.AddAuthentication();
builder.Services.AddAuthorization();


builder.Services.AddIdentityCore<IdentityUser>(Options =>
{
    Options.Password.RequiredLength = 10;
    //Options.SignIn.RequireConfirmedEmail= true;

}).AddUserStore<Tanent1Context>();




var app = builder.Build();
// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
app.UseAuthentication();
app.UseAuthorization();
app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();
