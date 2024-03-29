global using Microsoft.EntityFrameworkCore;
global using MyShop.Data;
global using MyShop.Models;
global using MyShop.Models.MAddress;
global using MyShop.Services.AddressService;
global using MyShop.Services.AuthService;
global using MyShop.Services.BrandService;
global using MyShop.Services.CartService;
global using MyShop.Services.CategoryService;
global using MyShop.Services.ColorService;
global using MyShop.Services.CommentService;
global using MyShop.Services.ImageService;
global using MyShop.Services.OrderService;
global using MyShop.Services.PaymentService;
global using MyShop.Services.ProductService;
global using MyShop.Services.ProductVariantService;
global using MyShop.Services.ProductVariantStoreService;
global using MyShop.Services.RatingService;
global using MyShop.Services.SaleService;
global using MyShop.Services.UserService;
global using MyShop.Services.WarehouseService;
using FirebaseAdmin;
using Google.Apis.Auth.OAuth2;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);
var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";
var configurationBuilder = new ConfigurationBuilder()
                            .SetBasePath(builder.Environment.ContentRootPath)
                            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                            .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", optional: true)
                            .AddEnvironmentVariables();

builder.Configuration.AddConfiguration(configurationBuilder.Build());
// Add services to the container.

//var defaultConnectionString = string.Empty;

//if (builder.Environment.EnvironmentName == "Development")
//{
//    defaultConnectionString = builder.Configuration.GetConnectionString("DefaultConnection");
//}
//else
//{
//    // Use connection string provided at runtime by Heroku.
//    var connectionUrl = Environment.GetEnvironmentVariable("DATABASE_URL");

//    connectionUrl = connectionUrl.Replace("postgres://", string.Empty);
//    var userPassSide = connectionUrl.Split("@")[0];
//    var hostSide = connectionUrl.Split("@")[1];

//    var user = userPassSide.Split(":")[0];
//    var password = userPassSide.Split(":")[1];
//    var host = hostSide.Split("/")[0];
//    var database = hostSide.Split("/")[1].Split("?")[0];
//    defaultConnectionString = $"Host={host};Database={database};Username={user};Password={password};SSL Mode=Require;Trust Server Certificate=true";
//}
builder.Services.AddDbContext<DataContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
    //options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"));
});
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(option =>
{
    option.SwaggerDoc("v1", new OpenApiInfo { Title = "Demo API", Version = "v1" });
    option.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please enter a valid token",
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
        Scheme = "Bearer"
    });
    option.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type=ReferenceType.SecurityScheme,
                    Id="Bearer"
                }
            },
            new string[]{}
        }
    });
});

builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<IBrandService, BrandService>();
builder.Services.AddScoped<IRatingService, RatingService>();
builder.Services.AddScoped<IAddressService, AddressService>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<ICartService, CartService>();
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<IOrderService, OrderService>();
builder.Services.AddScoped<IPaymentService, PaymentService>();
builder.Services.AddScoped<IProductVariantService, ProductVariantService>();
builder.Services.AddScoped<IProductVariantStoreService, ProductVariantStoreService>();
builder.Services.AddScoped<IColorService, ColorService>();
builder.Services.AddScoped<IImageService, ImageService>();
builder.Services.AddScoped<ICommentService, CommentService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<ISaleService, SaleService>();
builder.Services.AddScoped<IWarehouseService, WarehouseService>();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey =
                new SymmetricSecurityKey(System.Text.Encoding.UTF8
                .GetBytes(builder.Configuration.GetSection("AppSettings:Token").Value)),
            ValidateIssuer = false,
            ValidateAudience = false
        };
    });
builder.Services.AddHttpContextAccessor();
var provider = builder.Services.BuildServiceProvider();
var configuration = provider.GetService<IConfiguration>();

builder.Services.AddCors(options =>
{
    var frontend_URL = configuration.GetValue<string>("frontend_url");
    var admin_URL = configuration.GetValue<string>("admin_url");
    options.AddDefaultPolicy(builder =>
    {
        //builder.WithOrigins(frontend_URL, admin_URL).AllowAnyMethod().AllowAnyHeader(); 
        builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
    });
});

builder.Services.AddSingleton(FirebaseApp.Create(new AppOptions()
{
    Credential = GoogleCredential.FromJson(configuration.GetValue<string>("FIREBASE_CONFIG"))
}));
//builder.Services.AddFirebaseAuthentication();

builder.Services.AddControllersWithViews()
    .AddNewtonsoftJson(options =>
    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors();

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.MapFallbackToFile("index.html");

app.Run();
