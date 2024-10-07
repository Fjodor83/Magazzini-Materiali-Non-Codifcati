using Blazored.LocalStorage;
using MagazziniMaterialiCLient;
using MagazziniMaterialiCLient.Services;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");
builder.Services.AddScoped<MaterialeService>();
builder.Services.AddScoped<MagazzinoService>();
builder.Services.AddScoped<AccountService>();
builder.Services.AddScoped<UserService>();
builder.Services.AddScoped<MovimentazioneService>();


builder.Services.AddBlazoredLocalStorage();
builder.Services.AddAuthorizationCore();
builder.Services.AddScoped<AuthenticationStateProvider, CustomAuthenticationStateProvider>();
builder.Services.AddScoped(sp => new HttpClient 
{ 
    BaseAddress = new Uri("https://localhost:7089") 
});

await builder.Build().RunAsync();
