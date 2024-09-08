using Blazored.LocalStorage;
using DotNet8.EmailVerification.App;
using DotNet8.EmailVerification.App.Services;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MudBlazor.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(n => new HttpClient 
{ 
    BaseAddress=new Uri("https://localhost:7273")
});


//builder.Services.AddHttpClient()

builder.Services.AddScoped<HttpClientService>();

builder.Services.AddBlazoredLocalStorageAsSingleton();

builder.Services.AddMudServices();

await builder.Build().RunAsync();
