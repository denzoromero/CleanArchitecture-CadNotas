using ApplicationCore;
using CleanCadNotas.Configurations;
using CleanCadNotas.Extensions;
using Infrastructure;



var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.AddInfrastructureServices();
builder.AddApplicationServices();
builder.AddWebServices();

builder.Services.AddCookieAuthentication();

builder.Services.AddExceptionHandler<ProblemDetailsExceptionHandler>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    await app.SeedDatabaseAsync();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseExceptionHandler(options => { });

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Login}/{id?}")
    .WithStaticAssets();


app.Run();
