using System.Configuration;
using HrMnager_mvc.Context;
using HrMnager_mvc.Repositories;
using HrMnager_mvc.Repositories.Implementations;
using HrMnager_mvc.Repositories.Interfaces;
using HrMnager_mvc.Services.Implementations;
using HrMnager_mvc.Services.Interfaces;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<HrManagerContext>(options => options.UseMySQL(builder.Configuration.GetConnectionString("HrManagerString")));
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IDepartmentRepository, DepartmentRepository>();
builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();
builder.Services.AddScoped<IHrManagerRepository, HrManagerRepository>();
builder.Services.AddScoped<IRequestRepository, RequestRepository>();

builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IDepartmentService, DepartmentService>();
builder.Services.AddScoped<IEmployeeService, EmployeeService>();
builder.Services.AddScoped<IHrManagerService, HrManagerService>();
builder.Services.AddScoped<IRequestService, RequestService>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/User/Login";
        options.LogoutPath = "/User/Logout";
        options.AccessDeniedPath = "/Home/Index";
    });
    builder.Services.AddAuthorization();

    builder.Services.AddHttpContextAccessor();
// builder.Services.AddAuthorization(options =>
//    {
//        options.AddPolicy("AdminPolicy", policy => policy.RequireRole("Admin"));
//        options.AddPolicy("HRManagerPolicy", policy => policy.RequireRole("HRManager"));
//    });


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
app.UseSession();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
