using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Rating.Data;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("SkillDbContextSqlServer") 
    ?? throw new InvalidOperationException("Connection string 'SkillDbContextSqlServer' not found.");

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<SkillDbContext>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddScoped<SkillService>();
builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = false)
   .AddEntityFrameworkStores<SkillDbContext>();

var app = builder.Build();


// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();
// app.UseAuthentication();
// app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Skills}/{action=Index}/{id?}");

app.Run();
