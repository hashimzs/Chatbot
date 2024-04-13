using Blazored.LocalStorage;
using Blazored.Toast;
using chatbot2;
using chatbot2.Services;
using Ljbc1994.Blazor.IntersectionObserver;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Toolbelt.Blazor.Extensions.DependencyInjection;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services
    .AddScoped(sp => new HttpClient { BaseAddress = new Uri("https://localhost:7127/") }
    .EnableIntercept(sp));

builder.Services.AddScoped<ChatService>();
builder.Services.AddScoped<AuthService>();
builder.Services.AddIntersectionObserver();
builder.Services.AddBlazoredToast();
builder.Services.AddBlazoredLocalStorage();
builder.Services.AddHttpClientInterceptor(); // <- Add this!


await builder.Build().RunAsync();
