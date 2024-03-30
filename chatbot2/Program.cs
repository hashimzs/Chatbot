using Blazored.LocalStorage;
using chatbot2;
using chatbot2.Services;
using Ljbc1994.Blazor.IntersectionObserver;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("https://localhost:7127/") });
builder.Services.AddScoped<ChatService>();
builder.Services.AddScoped<AuthService>();
builder.Services.AddIntersectionObserver();
builder.Services.AddBlazoredLocalStorage();

await builder.Build().RunAsync();
