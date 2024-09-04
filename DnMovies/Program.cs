using DnMovies.Repository;
using Refit;

var builder = WebApplication.CreateBuilder(args);
var tokenApi = "eyJhbGciOiJIUzI1NiJ9.eyJhdWQiOiJhNjVhZDUxZjU2NmQ3M2UwZmExYjFiOTIzNTRkNDJjZCIsIm5iZiI6MTcyNTM5Mzg4NS40NTA4OTIsInN1YiI6IjY2ZDYzMTg0ODJmY2NiYzQwYzA0MWU4NSIsInNjb3BlcyI6WyJhcGlfcmVhZCJdLCJ2ZXJzaW9uIjoxfQ.0AD3R7ps0WKwhVBL9slYKwddHxkayhToT_vDHGkquuE";
// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddRefitClient<IMovieRepository>()
    .ConfigureHttpClient(c =>
    {
        c.BaseAddress = new Uri("https://api.themoviedb.org/3");
        c.DefaultRequestHeaders.Add("Authorization", $"Bearer {tokenApi}");
        c.DefaultRequestHeaders.Add("Accept", "application/json");
    });

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