using Microsoft.AspNetCore.Authentication.Cookies;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
//builder.Services.AddRazorPages();

builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
.AddCookie(options =>
{
options.LoginPath = "/Login/";
});

builder.Services.AddSession();
builder.Services.AddDistributedMemoryCache();
builder.Services.AddMvc();
builder.Services.AddControllers(options => options.EnableEndpointRouting = false);



var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
}
app.UseStaticFiles();

app.UseAuthorization();


app.UseAuthentication();

app.UseSession();
app.UseMvcWithDefaultRoute();
app.Run();