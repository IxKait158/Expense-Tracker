using ExpenseTracker.API.Extensions;
using ExpenseTracker.API.Middleware;
using ExpenseTracker.API.Services;
using ExpenseTracker.Application.Common.DependencyInjection;
using ExpenseTracker.Application.Interfaces;
using ExpenseTracker.Infrastructure;
using Microsoft.AspNetCore.CookiePolicy;

var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;

services.AddControllers();
services.AddHttpContextAccessor();

services.AddSwaggerGen();

services.AddJwtAuthentication(builder.Configuration);
services.AddApiAuthentication();

services.AddInfrastructure(builder.Configuration);
services.AddApplication();

services.AddSingleton<ICurrentUserService, CurrentUserService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<ExceptionHandlingMiddleware>();

app.UseDefaultFiles();
app.UseStaticFiles();

app.UseRouting();

app.UseCookiePolicy(new CookiePolicyOptions
{
    Secure = CookieSecurePolicy.Always,
    HttpOnly = HttpOnlyPolicy.Always,
    MinimumSameSitePolicy = SameSiteMode.Strict
});

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();