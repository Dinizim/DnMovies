using DnMovies.Repository;
using Refit;

var builder = WebApplication.CreateBuilder(args);
var tokenApi = builder.Configuration["TMDB:ApiKey"];

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddRefitClient<IMovieRepository>()
    .ConfigureHttpClient(c =>
    {
        c.BaseAddress = new Uri(builder.Configuration["TMDB:BaseUrl"]);
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