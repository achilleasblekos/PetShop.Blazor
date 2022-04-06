using Microsoft.AspNetCore.ResponseCompression;
using PetShop.EF.Context;
using PetShop.EF.MockRepositories;
using PetShop.EF.Repositories;
using PetShop.Model;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();


//Dababase Setup
builder.Services.AddDbContext<PetShopContext>();
var useMocks = Boolean.Parse(builder.Configuration["UseMocks"]);

if(!useMocks) {
    //Database Setup
    builder.Services.AddTransient<IEntityRepo<Customer>, CustomerRepo>();
    builder.Services.AddTransient<IEntityRepo<Pet>, PetRepo>();
    builder.Services.AddTransient<IEntityRepo<PetFood>, PetFoodRepo>();
    builder.Services.AddTransient<IEntityRepo<Employee>, EmployeeRepo>();
    builder.Services.AddTransient<IEntityRepo<Transaction>, TransactionRepo>();
}
else {
    //Mock Setup
    builder.Services.AddSingleton<IEntityRepo<Customer>, MockCustomerRepo>();
    builder.Services.AddSingleton<IEntityRepo<Pet>, MockPetRepo>();
    builder.Services.AddSingleton<IEntityRepo<PetFood>, MockPetFoodRepo>();
    builder.Services.AddSingleton<IEntityRepo<Transaction>, MockTransactionRepo>();
    builder.Services.AddSingleton<IEntityRepo<Employee>, MockEmployeeRepo>();
}



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
}
else
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseBlazorFrameworkFiles();
app.UseStaticFiles();

app.UseRouting();


app.MapRazorPages();
app.MapControllers();
app.MapFallbackToFile("index.html");

app.Run();
