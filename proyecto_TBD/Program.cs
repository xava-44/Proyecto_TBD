using Microsoft.EntityFrameworkCore;
using MySqlConnector;
using proyecto_TBD.Models;
using System;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

//conexion a la Bd

builder.Services.AddDbContext<MydbContext>(options =>
    options.UseMySql(builder.Configuration.GetConnectionString ("MySqlConnection"),
        new MySqlServerVersion(new Version(8, 0,40)) // versión real de tu MySQL
    ));
builder.Services.AddSession();
var app = builder.Build();

app.UseSession();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

// Este metodo llama al controlador principal , que a su vez llama a la ventana principal 
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Login}/{id?}");
//app.MapControllerRoute(
//    name: "default",
//    pattern: "{controller=Cuenta}/{action=Login}/{id?}");


app.Run();
