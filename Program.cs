using NemoTravel_TestTask;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();


builder.Services.AddScoped<ICurrencyConversionService, CurrencyConversionService>();
builder.Services.AddSingleton<IWalletService, WalletService>();
builder.Services.AddScoped<ICurrencyCalculatorService, CurrencyCalculatorService>();
builder.Services.AddScoped<ICurrencyRatesXmlService, CurrencyRatesXmlService>();


var app = builder.Build();

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

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Wallet}/{action=Index}/{id?}");

app.Run();
