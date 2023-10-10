using DocConnect.Business.Abstraction.Factories;
using DocConnect.Business.Abstraction.Helpers;
using DocConnect.Business.Abstraction.Services;
using DocConnect.Business.Factories;
using DocConnect.Business.Helpers;
using DocConnect.Business.Models.Options;
using DocConnect.Business.Profiles;
using DocConnect.Business.Services;
using DocConnect.Data;
using DocConnect.Data.Abstraction.Helpers;
using DocConnect.Data.Abstraction.Repositories;
using DocConnect.Data.Abstraction.Seeder;
using DocConnect.Data.Helpers;
using DocConnect.Data.Models.Entities;
using DocConnect.Data.Repositories;
using DocConnect.Data.Seeder;
using DocConnect.Presentation.API.Extensions;
using DocConnect.Presentation.API.Utilities;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;
using System.Text.Json.Serialization;
using TokenHandler = DocConnect.Business.Helpers.TokenHandler;
using TokenOptions = DocConnect.Business.Models.Options.TokenOptions;

var builder = WebApplication.CreateBuilder(args);

var connectionString = string.Empty;

if (builder.Environment.IsDevelopment())
{
    connectionString = builder.Configuration
                .GetConnectionString(AppConfigConstants.DefaultConnection);
}
else
{
    var serverName = builder.Configuration.GetValue<string>(AppConfigConstants.DatabaseUrl);
    var databaseName = builder.Configuration.GetValue<string>(AppConfigConstants.DatabaseName);
    var userName = builder.Configuration.GetValue<string>(AppConfigConstants.DatabaseUser);
    var password = builder.Configuration.GetValue<string>(AppConfigConstants.DatabasePassword);
    connectionString = $"Server={serverName};Database={databaseName};Uid={userName};Pwd={password};";
}

builder.Services.AddDbContext<DocConnectContext>(options =>
{
    var serverVersion = ServerVersion.AutoDetect(connectionString);
    options.UseMySql(connectionString, serverVersion);
});

builder.Services
    .AddIdentity<User, Role>()
    .AddEntityFrameworkStores<DocConnectContext>()
    .AddDefaultTokenProviders();

var tokenOptions = builder.Configuration.GetSection(nameof(TokenOptions)).Get<TokenOptions>();
var emailOptions = builder.Configuration.GetSection(nameof(EmailOptions)).Get<EmailOptions>();
var imageOptions = builder.Configuration.GetSection(nameof(ImageOptions)).Get<ImageOptions>();

if (!builder.Environment.IsDevelopment())
{
    tokenOptions.SecurityKey = Environment.GetEnvironmentVariable(AppConfigConstants.AzureSecretKeyName);
    emailOptions.ConnectionString = Environment.GetEnvironmentVariable(AppConfigConstants.EmailConnectionStringName);
    emailOptions.Sender = Environment.GetEnvironmentVariable(AppConfigConstants.EmailSenderName);
    imageOptions.DomainName = "https://" + Environment.GetEnvironmentVariable(AppConfigConstants.AzureImageDomainName);
}

builder.Services.Configure<CookieAuthenticationOptions>(options =>
{
    options.ExpireTimeSpan = TimeSpan.FromHours(1);
});

builder.Services.AddSingleton(tokenOptions);
builder.Services.AddSingleton(emailOptions);
builder.Services.AddSingleton(imageOptions);
builder.Services.AddScoped<IEmailSender, EmailSender>();
builder.Services.AddScoped<IEmailFactory, EmailFactory>();
builder.Services.AddScoped<IEmailService, EmailService>();
builder.Services.AddScoped<IDbInitializer, DbInitializer>();
builder.Services.AddScoped<ITokenHandler, TokenHandler>();
builder.Services.AddTransient<ITokenRepository, TokenRepository>();
builder.Services.AddTransient<ITokenService, TokenService>();
builder.Services.AddScoped<IDocConnectSignInManager, DocConnectSignInManager>();
builder.Services.AddScoped<IDocConnectUserManager, DocConnectUserManager>();
builder.Services.AddScoped<IAuthorizeService, AuthorizeService>();
builder.Services.AddScoped<IPatientRepository, PatientRepository>();
builder.Services.AddScoped<ILocationRepository, LocationRepository>();
builder.Services.AddScoped<ILocationService, LocationService>();
builder.Services.AddScoped<IDoctorRepository, DoctorRepository>();
builder.Services.AddScoped<IDoctorService, DoctorService>();
builder.Services.AddScoped<IAppointmentRepository, AppointmentRepository>();
builder.Services.AddScoped<IAppointmentService, AppointmentService>();
builder.Services.AddScoped<ISpecialityRepository, SpecialityRepository>();
builder.Services.AddScoped<ISpecialityService, SpecialityService>();
builder.Services.AddScoped<ICityRepository, CityRepository>();
builder.Services.AddScoped<ICityService, CityService>();
builder.Services.AddScoped<IResultFactory, ResultFactory>();
builder.Services.AddAutoMapper(typeof(DocConnectProfile));

builder.Services.AddCors(options =>
{
    options.AddPolicy(AppConfigConstants.AppAllowedOriginsName,
             p => p.WithOrigins(builder.Configuration.GetValue<string>(AppConfigConstants.CorsKey))
                   .AllowAnyMethod()
                   .AllowAnyHeader());
});

builder.Services
    .AddControllers()
    .AddJsonOptions(options =>
{
    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
    options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
});

builder.Services.AddEndpointsApiExplorer();

builder.Services
    .AddSwaggerGen(c =>
    {
        c.AddSecurityDefinition(JwtBearerDefaults.AuthenticationScheme, new OpenApiSecurityScheme
        {
            In = ParameterLocation.Header,
            Type = SecuritySchemeType.ApiKey
        });
        
        c.AddSecurityRequirement(new OpenApiSecurityRequirement
        {
            {
                new OpenApiSecurityScheme
                {
                    Reference = new OpenApiReference
                    {
                        Type = ReferenceType.SecurityScheme,
                        Id = JwtBearerDefaults.AuthenticationScheme
                    }
                },
                Array.Empty<string>()
            }
        });
    }
);

builder.Services
    .AddAuthentication(options =>
    {
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    })
    .AddJwtBearer(configuration =>
    {
        configuration.RequireHttpsMetadata = true;
        configuration.TokenValidationParameters = new TokenValidationParameters
        {
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(tokenOptions.SecurityKey)),
            ValidateAudience = true,
            ValidAudience = tokenOptions.Audience,
            ValidateIssuer = true,
            ValidIssuer = tokenOptions.Issuer,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ClockSkew = TimeSpan.Zero
        };
    }
);

var app = builder.Build();

await app.InitializeDBContext();

app.UseDeveloperExceptionPage();

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseCors(AppConfigConstants.AppAllowedOriginsName);

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();