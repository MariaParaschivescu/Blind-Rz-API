using Application.DTOs;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Text;
using System.Text.Json.Serialization;
using Microsoft.IdentityModel.Tokens;
using BlindrzAPI.Extensions;
using Application.UnitOfWork;
using Persistence;
using Persistence.Services;
using Application.Services;
using Persistence.Context;
using Microsoft.EntityFrameworkCore;
using Application.Auth;
using ISession = Domain.Auth.ISession;
using Application.Common.Helpers;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
//builder.Services.AddApplicationDbContext(builder.Configuration);
var connection = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=BlindRZ;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
builder.Services.AddDbContext<BlindRZContext>(options => options.UseSqlServer(connection));


builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.IgnoreNullValues = true;
    options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
});

var tokenConfig = builder.Configuration.GetSection("TokenConfiguration");
builder.Services.Configure<TokenConfiguration>(tokenConfig);
var trueAppSettings = builder.Configuration.GetSection("AppSettings");
builder.Services.Configure<AppSettings>(trueAppSettings);

var appSettings = tokenConfig.Get<TokenConfiguration>();
var key = Encoding.ASCII.GetBytes(appSettings.Secret);
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options => 
{
    options.SaveToken = true;
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(key),
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidIssuer = appSettings.Issuer,
        ValidAudience = appSettings.Audience
    };

});
builder.Services.AddAutoMapperSetup();
builder.Services.AddHttpContextAccessor();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IEmailService, EmailService>();
builder.Services.AddScoped<ISession, Session>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.UseCors(x => x
        .WithOrigins("http://localhost:8100")
        .AllowAnyMethod()
        .AllowAnyHeader()
        .AllowCredentials());//(builder => builder.WithOrigins("http://localhost:8100").AllowAnyHeader().AllowAnyMethod().AllowCredentials());

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});

//added request logging


app.UseHttpsRedirection();


//app.UseResponseCompression();

app.Run();
