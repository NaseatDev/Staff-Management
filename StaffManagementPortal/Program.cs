using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MudBlazor.Services;
using OfficeOpenXml;
using StaffManagement.Portal;
using StaffManagement.Portal.Extensions;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");
ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
builder.Services.AddMudServices();
builder.Services.AddApiClientService(builder.Configuration);
//builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

await builder.Build().RunAsync();
