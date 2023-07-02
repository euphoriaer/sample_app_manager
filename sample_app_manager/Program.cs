using Blazorise;
using SampleAppManager.Data;
using SampleAppManager.LiteDB;
using SampleAppManager.Model;
using Blazorise.Bootstrap;
using Blazorise.Icons.FontAwesome;
using Blazored.SessionStorage;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components;
using SampleAppManager.FTPServer;
using Microsoft.AspNetCore.SignalR;
using Microsoft.AspNetCore.Http.Features;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddRazorPages();
builder.Services.AddHttpClient();
builder.Services.AddServerSideBlazor();
builder.Services.AddAntDesign();
builder.Services.AddBlazoredSessionStorage();

for (int i = 0; i < args.Length; i++)
{
    Console.WriteLine("ÏÔÊ¾²ÎÊý£º"+args[i]);
    if (args[i].Contains("port"))
    {
        var port = args[i+1];
        FTPServerProvide.FTPPort = port;
	}
}

builder.Services.AddBootstrapBlazor();
builder.Services.Configure<HubOptions>(option => option.MaximumReceiveMessageSize = long.MaxValue);

builder.Services.Configure<FileStreamOptions>(option => option.BufferSize = int.MaxValue);

builder.Services.Configure<FormOptions>(options =>
{

	options.MultipartBodyLengthLimit = long.MaxValue;
});

builder.Services
    .AddBlazorise(options =>
    {
        options.Immediate = true;
    })
    .AddBootstrapProviders()
    .AddFontAwesomeIcons();

builder.Services.AddScoped<ProcessVersionWithStatus>();


builder.Services.AddScoped<AuthenticationStateProvider, CustomAuthStateProvider>();
builder.Services.AddAuthenticationCore();

builder.Services.AddScoped<LiteDbContext>((x) => {
    var path= Path.Combine(Environment.CurrentDirectory, "Data.db");
    return new LiteDbContext(path);
});

builder.Services.AddTransient<FTPServerProvide>();

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