using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using SportsStore.Models;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);
// Add services to the container.

builder.Services.AddRazorPages();
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<StoreDbContext>(opts =>
{
    opts.UseSqlServer(builder.Configuration.GetValue<string>("ConnectionStrings:SportsStoreConnection"));
});

//EFStoreRepository should be used as implementation IStoreRepository
builder.Services.AddScoped<IStoreRepository, EFStoreRepository>();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.MapControllerRoute("catpage",
    "{category}/Page{ProductPage:int}",
    new {Controller = "Home", action = "Index"});
app.MapControllerRoute("page", "Page{prodcutPage:int}",
    new {Controller = "Home", action = "Index", productPage = 1});
app.MapControllerRoute("category","{category}",
    new {Controller = "Home", action = "Index", productPage = 1});
app.MapControllerRoute("pagination",
    "Products/Page{productPage}",
    new { Controller = "Home", action = "Index", productPage = 1});
app.UseDeveloperExceptionPage();
app.UseStatusCodePages();

app.UseRouting();
app.UseAuthorization();

app.UseEndpoints(endpoints => { endpoints.MapDefaultControllerRoute(); });
app.MapRazorPages();

SeedData.EnsurePopulated(app);
app.Run();
