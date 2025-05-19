using DotNetWebApi.Context;
using DotNetWebApi.Entitie;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// 👇 Read configuration from appsettings.json
ConfigurationManager configuration = builder.Configuration;

// 👇 Add DbContext (pointing to your existing DB)
builder.Services.AddDbContext<TenantAppContext>(options =>
    options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

// 👇 Configure Identity with your custom ApplicationUser
builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
    .AddEntityFrameworkStores<TenantAppContext>()
    .AddDefaultTokenProviders();

// 👇 Add JWT Authentication
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.RequireHttpsMetadata = false;
    options.SaveToken = true;

    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = configuration["Jwt:Issuer"],
        ValidAudience = configuration["Jwt:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]))
    };
});

// 👇 Allow Angular frontend (CORS)
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAngularApp",
        policy => policy.WithOrigins("http://localhost:4200") // Replace with your Angular URL
                        .AllowAnyHeader()
                        .AllowAnyMethod());
});

// 👇 Add controllers and Swagger
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();
//app.Environment.IsDevelopment()
// 👇 Use Swagger in Development
if (true)
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// 👇 Add CORS, Auth and Controllers
app.UseHttpsRedirection();

app.UseCors("AllowAngularApp");

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
