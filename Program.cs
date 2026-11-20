var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddSingleton<T1CAMPOSCRUZJHOSEPANTONY.Data.ConexionBD>();
//Agregamos cada interfaz que se crea en el proyecto
builder.Services.AddScoped<T1CAMPOSCRUZJHOSEPANTONY.Repository.ILibroRepository, T1CAMPOSCRUZJHOSEPANTONY.Repository.LibroRepository>();
builder.Services.AddScoped<T1CAMPOSCRUZJHOSEPANTONY.Service.ILibroService, T1CAMPOSCRUZJHOSEPANTONY.Service.LibroService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();


app.Run();
