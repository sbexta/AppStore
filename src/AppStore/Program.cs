using AppStore.Models.Domain;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using AppStore.Services.Abstract;
using AppStore.Services.Implementation;
using AppStore.Repositories.Implementation;
using AppStore.Repositories.Interfaz;
using AppStore.Repositories.Abstract;

var builder = WebApplication.CreateBuilder(args);

// Add Repository
builder.Services.AddScoped<ICategoriaRepository, CategoriaRepository>();
builder.Services.AddScoped<ILibroRepository, LibroRepository>();
builder.Services.AddScoped<IFileRepository, FileRepository>();
builder.Services.AddScoped<IUserAuthenticationRepository, UserAuthenticationRepository>();

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddScoped<IUserAuthenticationService, UserAuthenticationService>();
builder.Services.AddScoped<IFileService, FileService>();
builder.Services.AddScoped<ILibroService, LibroService>();
builder.Services.AddScoped<ICategoriaService, CategoriaService>();

// builder.Services.AddDbContext<DataBaseContext>(op =>
//     op.LogTo(Console.WriteLine, new[] {
//         DbLoggerCategory.Database.Command.Name},
//         LogLevel.Information).EnableSensitiveDataLogging()
//         .UseSqlite(builder.Configuration.GetConnectionString("DefaultConnections"))
// );

builder.Services.AddDbContext<DataBaseContext>(op =>
    op.LogTo(Console.WriteLine, new[] {
        DbLoggerCategory.Database.Command.Name},
        LogLevel.Information).EnableSensitiveDataLogging()
        .UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnections"))
);

builder.Services.AddIdentity<AplicationUser, IdentityRole>()
    .AddEntityFrameworkStores<DataBaseContext>()
    .AddDefaultTokenProviders();



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

app.UseAuthentication(); //Se agrega esto para que funcione la autenticación
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        var context = services.GetRequiredService<DataBaseContext>();
        var userManager = services.GetRequiredService<UserManager<AplicationUser>>();
        var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();

        await context.Database.MigrateAsync();
        await LoadDatabase.InsertData(context, userManager, roleManager);
    }
    catch (Exception ex)
    {
        var logger = services.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "An error occurred while seeding the database.");
    }
}

app.Run();
