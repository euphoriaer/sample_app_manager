using Blazorise;
using SampleAppManager.Data;
using SampleAppManager.LiteDB;
using SampleAppManager.Model;
using Blazorise.Bootstrap;
using Blazorise.Icons.FontAwesome;
using Blazored.SessionStorage;
using Microsoft.AspNetCore.Components.Authorization;
using SampleAppManager.AuthenticationSpace;
using Blazored.LocalStorage;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddRazorPages();
builder.Services.AddHttpClient();
builder.Services.AddServerSideBlazor();
builder.Services.AddAntDesign();
builder.Services.AddBlazoredSessionStorage();
builder.Services.AddBlazoredLocalStorage();

builder.Services
    .AddBlazorise(options =>
    {
        options.Immediate = true;
    })
    .AddBootstrapProviders()
    .AddFontAwesomeIcons();

builder.Services.AddScoped<ProcessVersionWithStatus>();
builder.Services.AddScoped<Authentication>((x) =>
{
	var env = x.GetRequiredService<ISessionStorageService>();
    return new Authentication(env);
});

builder.Services.AddScoped<AuthenticationStateProvider, CustomAuthStateProvider>();
builder.Services.AddAuthenticationCore();

builder.Services.AddScoped<LiteDbContext>((x) => {
    var env= x.GetRequiredService<IWebHostEnvironment>();
    var path= Path.Combine(env.ContentRootPath,"Data.db");
    return new LiteDbContext(path);
});

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
	// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
	app.UseHsts();
}

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<LiteDbContext>();
    if (db.Users.GetUser().Count==0)
    {
        SeedData.InitializeUser(db);

    }

	
}


app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");
app.MapControllerRoute("default", "{controller=Home}/{action=Index}/{id?}");
app.Run();
