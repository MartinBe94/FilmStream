using FilmStream.Membership.UI;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");


builder.Services.AddHttpClient<MembershipHttpClient>(client =>
    client.BaseAddress = new Uri("https://localhost:7001/api/"));

builder.Services.AddScoped<IMembershipService, MembershipService>();

await builder.Build().RunAsync();
