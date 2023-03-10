using Azure.Storage.Blobs;
using MvcApiPersonajesSeries2023.Helpers;
using MvcApiPersonajesSeries2023.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
string apiSeries =
    builder.Configuration.GetValue<string>("ApiUrls:ApiSeriesPersonajes");
builder.Services.AddTransient<ServiceSeries>(z => new ServiceSeries(apiSeries));
builder.Services.AddTransient<HelperPathProvider>();
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
string azureKeys = builder.Configuration.GetConnectionString("azurestorage");
string personajesContainers = builder.Configuration.GetValue<string>("AzureContainers:personajescontainer");
BlobServiceClient blobServiceClient = new BlobServiceClient(azureKeys);
BlobContainerClient containerClient = blobServiceClient.GetBlobContainerClient(personajesContainers);
builder.Services.AddTransient<BlobContainerClient>(x => containerClient);
builder.Services.AddTransient<ServiceStorageBlobs>();
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
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
