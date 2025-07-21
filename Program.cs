using BookStore.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add Razor Pages and SQLite
builder.Services.AddRazorPages();
builder.Services.AddDbContext<BookStoreContext>(options =>
    options.UseSqlite("Data Source=wwwroot/data/BookStore.db"));

var app = builder.Build();

// Middleware
app.UseStaticFiles();
app.UseRouting();
app.MapRazorPages();

app.Run();