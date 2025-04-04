using CarRentalApp.Business.Extensions;
using CarRentalApp.Business.Operations;
using CarRentalApp.Business.Security;
using CarRentalApp.Business.Services;
using CarRentalApp.Data;
using CarRentalApp.Data.Entities;
using CarRentalApp.Data.Repositories;
using CarRentalApp.Data.UnitOfWork;
using CarRentalApp.WebApi.Filters;
using CarRentalApp.WebApi.Middleware;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Veritabanı bağlantısını ekle
builder.Services.AddDbContext<CarRentalDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

//Repository ve UnitOfWork bağımlılıklarını çözümle
builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<ICarManager, CarManager>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<DataProtectionService>();

//  Identity kullanıcı yönetimini ayarla
builder.Services.AddIdentity<User, IdentityRole>(options =>
{
    options.Password.RequiredLength = 6;
    options.Password.RequireDigit = true;
    options.Password.RequireNonAlphanumeric = true;
})
.AddEntityFrameworkStores<CarRentalDbContext>()
.AddDefaultTokenProviders();

//  JWT Authentication yapılandırması
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidateAudience = true,
            ValidAudience = builder.Configuration["Jwt:Audience"],
            ValidateLifetime = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:SecretKey"]!))
        };
    });

//  Swagger (API Dokümantasyonu) yapılandırması
builder.Services.AddSwaggerGen(options =>
{
    var jwtSecurityScheme = new OpenApiSecurityScheme
    {
        Scheme = "Bearer",
        BearerFormat = "JWT",
        Name = "JWT Authentication",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.Http,
        Description = "API'yi kullanmak için aşağıya 'Bearer {token}' yazın.",
        Reference = new OpenApiReference
        {
            Type = ReferenceType.SecurityScheme,
            Id = "Bearer"
        }
    };

    options.AddSecurityDefinition(jwtSecurityScheme.Reference.Id, jwtSecurityScheme);
    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        { jwtSecurityScheme, Array.Empty<string>() }
    });
});

//  CORS Politikası
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        policy => policy
            .AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader());
});

//  MVC Controllerları ve Middleware filtrelerini ekle
builder.Services.AddControllers(options =>
{
    options.Filters.Add<ValidationFilter>();
});

var app = builder.Build();

// Middleware (Ara Katman) Ayarları
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors("AllowAll");

//  Özel Middleware Kullanımı (Global Hata Yakalama ve Loglama)
app.UseMiddleware<GlobalExceptionMiddleware>();
app.UseMiddleware<RequestLoggingMiddleware>();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
