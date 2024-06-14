using Application;
using GraphQL;
using GraphQL.Instrumentation;
using Infrastructure;
using Infrastructure.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using Api.GraphQL.Schemas;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddHttpContextAccessor();
builder.Logging.AddConsole();

builder.Services.AddApplicationServices();
builder.Services.AddInfrastructureServices(builder.Configuration);
builder.Services.AddSingleton<InstrumentFieldsMiddleware>();

builder.Services.AddAuthorization();

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
                                  .AddAuthorization(options =>
                                  {
                                      options.AddPolicy("Authorized", policy => policy.RequireAuthenticatedUser());
                                      options.AddPolicy("AdminPolicy", policy => policy.RequireClaim(ClaimTypes.Role, "Admin"));
                                      options.AddPolicy("ModeratorPolicy", policy => policy.RequireClaim(ClaimTypes.Role, "Moderator"));
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

app.Run();