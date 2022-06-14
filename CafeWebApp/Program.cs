using CafeWebApp.Data.Interfaces;
using CafeWebApp.Data.Mocks;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddMvc();
builder.Services.AddTransient<IProducts, MockProduct>();
builder.Services.AddTransient<ICombos, MockCombo>();
builder.Services.AddMvc(option => option.EnableEndpointRouting = false);


var app = builder.Build();

//app.MapGet("/", [ListProducts]() => { "Hello World1!"});
app.MapGet("/", () => { return "Hello World1!"; });

app.UseRouting();



app.Run();

//private IConfigurationRoot _confSstring;



//app.UseStatusCodePages();
//app.UseDeveloperExceptionPage();
//app.UseStaticFiles();
//app.UseMvcWithDefaultRoute();

//void ConfigureServices(IServiceCollection services)
//{
//    services.AddMvc();
//    services.AddTransient<IProducts, MockProduct>();
//    services.AddTransient<ICombos, MockCombo>();
//}