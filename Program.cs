using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MyEshop.Areas.Identity.Data;
using Microsoft.AspNetCore.Session;
using MyEshop.Areas.Core;
using MyEshop.Areas.Core.Repositories;
using MyEshop.Repositories;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("MyEshopDBContextConnection");

builder.Services.AddDbContext<MyEshopDBContext>(options =>
    options.UseSqlServer(connectionString));builder.Services.AddDefaultIdentity<MyEshopUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<MyEshopDBContext>();
// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddMemoryCache();
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options => {
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});


#region Authorization

AddAuthorizationPolicies(builder.Services);

#endregion


AddScoped();



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
app.UseAuthentication();
app.UseAuthorization();
app.UseSession();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Products}/{action=ProductsForCustomers}/{id?}");
app.MapRazorPages();
app.Run();

void AddAuthorizationPolicies(IServiceCollection services)
{
    builder.Services.AddAuthorization(options =>

    {
        options.AddPolicy("EmployeeOnly", policy => policy.RequireClaim("EmployeeNumber"));
    });

    builder.Services.AddAuthorization(options =>

    {
        options.AddPolicy(Constants.Policies.RequireAdmin, policy => policy.RequireRole(Constants.Roles.Administrator));
        options.AddPolicy(Constants.Policies.RequireManager, policy => policy.RequireRole(Constants.Roles.Manager));
        options.AddPolicy(Constants.Policies.RequireUser, policy => policy.RequireRole(Constants.Roles.User));
    });

}

void AddScoped()
{
    builder.Services.AddScoped<IUserRepository, UserRepository>();
    builder.Services.AddScoped<IRoleRepository, RoleRepository>();
    builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
}