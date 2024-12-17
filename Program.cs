using ElectronNET.API;
using ElectronNET.API.Entities;

var builder = WebApplication.CreateBuilder(args);
builder.WebHost.UseElectron(args);

// Is optional, but you can use the Electron.NET API-Classes directly with DI (relevant if you want more encoupled code)
builder.Services.AddElectron();

// Add services to the container.
builder.Services.AddRazorPages();

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

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

//app.Run();

await app.StartAsync();

// Open the Electron-Window here
await Electron.WindowManager.CreateWindowAsync();

app.WaitForShutdown();