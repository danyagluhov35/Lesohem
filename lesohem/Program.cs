using lesohem;
using lesohem.Models.Context;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<LesohemContext>(op => op.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(op => op.LoginPath = "/Account/Login");
builder.Services.AddAuthorization();

builder.Services.AddResponseCompression(op => op.EnableForHttps = true);


var app = builder.Build();

app.MapControllerRoute
    (
        name : "default",
        pattern : "{controller=Home}/{action=Index}/{id?}"
    );
app.MapControllerRoute
    (
        name : "Areas",
        pattern : "{area:exists}/{controller=Home}/{action=Index}/{id?}"
    );
app.UseAuthentication();
app.UseAuthorization();

app.UseStaticFiles();
app.UseDefaultFiles();

app.UseResponseCompression();

app.Run();




public class DeflateCompression : ICompressionProvider
{
    public string EncodingName => throw new NotImplementedException();

    public bool SupportsFlush => throw new NotImplementedException();

    public Stream CreateStream(Stream outputStream)
    {
        throw new NotImplementedException();
    }
}