using Application;
using GraphQL;
using Infrastructure;
using Infrastructure.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using TimeTracker.GraphQL.Schemas;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews().AddRazorRuntimeCompilation();
builder.Services.AddHttpContextAccessor();
builder.Services.AddSingleton<ILogger>(svc => svc.GetRequiredService<ILogger<ApplicationLogs>>());

builder.Services.AddApplicationServices();
builder.Services.AddInfrastructureServices(builder.Configuration);
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,

        ValidIssuer = builder.Configuration["JWT:Author"],
        ValidAudience = builder.Configuration["JWT:Audience"],
        IssuerSigningKey = AuthOptions.GetSymmetricSecurityKey(builder.Configuration["JWT:Key"]),
        ClockSkew = TimeSpan.Zero,
        RoleClaimType = "authorities"
    };
});

builder.Services.AddCors();



builder.Services.AddGraphQL(c => c.AddSystemTextJson()
                                  .AddSchema<MainShema>()
                                  .AddSchema<IdentitySchema>()
                                  .AddAuthorization(setting =>
                                  {
                                      setting.AddPolicy("Authorized", p => p.RequireAuthenticatedUser());
                                  })
                                  .AddGraphTypes(typeof(MainShema).Assembly)
                                  .AddGraphTypes(typeof(IdentitySchema).Assembly)
                                  );

var app = builder.Build();


app.UseCors(builder => builder.WithOrigins("http://localhost:3000")
                 //.AllowAnyOrigin()
                 .AllowAnyHeader()
                 .WithMethods("POST")
                 .AllowCredentials());
// Configure the HTTP request pipeline.

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();


app.UseAuthentication();
app.UseAuthorization();

app.UseStaticFiles();

app.UseRouting();

app.UseGraphQL<MainShema>("/graphql", (config) =>
{

});


app.UseGraphQL<IdentitySchema>("/graphql-login", (config) =>
{

});

app.UseGraphQLAltair("/");

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run(); 
public class ApplicationLogs
{
}