using lesohem;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<LesohemContext>(op => op.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(op => op.LoginPath = "/Account/Index");
builder.Services.AddAuthorization();



var app = builder.Build();

app.MapControllerRoute
    (
        name : "default",
        pattern : "{controller=Home}/{action=Index}/{id?}"
    );
app.UseAuthentication();
app.UseAuthorization();
app.UseStaticFiles();
app.UseDefaultFiles();


app.Run();
