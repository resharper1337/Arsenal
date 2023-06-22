using Server.Models;
using Server.ServerHubs;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllersWithViews();
builder.Services.AddSignalR();
var app = builder.Build();
app.UseWebSockets();
app.UseRouting();
app.UseDefaultFiles();
app.UseStaticFiles();
app.UseCors(x => x.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
app.UseDeveloperExceptionPage();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}");
app.MapHub<MainHub>("/mainhub");
app.Run();