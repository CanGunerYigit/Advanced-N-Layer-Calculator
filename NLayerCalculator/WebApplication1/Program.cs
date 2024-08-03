using BusinessLayer;
using Calculator.Business;
using DataAccessLayer.Data;
using DataAccessLayer.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

builder.Services.AddDbContext<AppDbContext>(context => context.UseSqlServer(builder.Configuration.GetConnectionString("DbUrl")));

builder.Services.AddScoped<ICalculationHistoryRepository, CalculationHistoryRepository>();
builder.Services.AddScoped<ICalculatorService, CalculatorService>();


var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}"); //Açýlacak Sayfa

app.Run();
