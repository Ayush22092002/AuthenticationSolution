using AdminUI.Services;
namespace AdminUi
{
    public class Program
    {
        public static void Main(string[] args)
        { 
var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        builder.Services.AddControllersWithViews();

            builder.Services.AddHttpClient<IAdminUIServices, AdminUIServices>();

            var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
    app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=AdminUI}/{action=Index}/{id?}");

app.Run();
        }
}
}


