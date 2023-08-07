var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllersWithViews();
//Session
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession();

var app = builder.Build();

//app.MapGet("/", () => "Hello World!");
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}"
    );

app.UseStaticFiles();
//Session
app.UseSession();

app.Run();