using Forum.Data.Repositories;
using Forum.Data.Repositories.Implementations;
using Forum.Data.Repositories.Interfaces;
using Forum.Services.Implementations;
using Forum.Services.Interfaces;
using GraphQL;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.DataProtection.KeyManagement;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using TimeTracker.GraphQL.Schemas;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews().AddRazorRuntimeCompilation();

builder.Services.AddSingleton<DapperContext>();
builder.Services.AddSingleton<IUserRepository, UserRepository>();
builder.Services.AddSingleton<ITokenRepository, TokenRepository>();
builder.Services.AddSingleton<ITokenFactory, TokenFactory>();


builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(x =>
    {
        x.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,

            ValidIssuer = builder.Configuration["JWT:Author"],
            ValidAudience = builder.Configuration["JWT:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:Key"])),
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