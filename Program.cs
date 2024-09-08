using System.Text;
using DeliveryU.Api.Orders.Application;
using DeliveryU.Api.Orders.Domain.Repositories;
using DeliveryU.Api.Orders.Domain.Services;
using DeliveryU.Api.Orders.Infrastructure.Adapters;
using DeliveryU.Api.OrdersDetails.Application;
using DeliveryU.Api.OrdersDetails.Domain.Repositories;
using DeliveryU.Api.OrdersDetails.Domain.Services;
using DeliveryU.Api.OrdersDetails.Infrastructure.Adapter;
using DeliveryU.Api.People.Application;
using DeliveryU.Api.People.Domain.Repositories;
using DeliveryU.Api.People.Domain.Services;
using DeliveryU.Api.People.Infrastructure.Adapters;
using DeliveryU.Api.Products.Application;
using DeliveryU.Api.Products.Domain.Repositories;
using DeliveryU.Api.Products.Domain.Services;
using DeliveryU.Api.Products.Infrastructure.Adapters;
using DeliveryU.Api.Security.AccessControl.Application;
using DeliveryU.Api.Security.AccessControl.Domain.Repositories;
using DeliveryU.Api.Security.AccessControl.Domain.Services;
using DeliveryU.Api.Security.AccessControl.Infrastructure.Adapters;
using DeliveryU.Api.Stores.Application;
using DeliveryU.Api.Stores.Domain.Repositories;
using DeliveryU.Api.Stores.Domain.Services;
using DeliveryU.Api.Stores.Infrastructure.Adapters;
using DeliveryU.Notifications;
using DeliveryU.Persistence.Database;
using FirebaseAdmin;
using Google.Apis.Auth.OAuth2;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.


builder.Services.AddControllers();

FirebaseApp App = FirebaseApp.Create(new AppOptions()
{
    Credential = GoogleCredential.FromFile("firebase.json")
});

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(
    option =>
    {
        option.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateAudience = true,
            ValidateIssuer = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["Token:Issuer"],
            ValidAudience = builder.Configuration["Token:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF32.GetBytes(builder.Configuration["Token:SecretKey"]!))
        };
    }
);

builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IUserRepository, PostgresUserRepository>();
builder.Services.AddScoped<ICryptoService, CryptoService>();
builder.Services.AddScoped<ITokenService, TokenService>();
builder.Services.AddScoped<IStoreRepository, PostgresStoreRepository>();
builder.Services.AddScoped<IStoreService, StoreService> ();
builder.Services.AddScoped<IProductService, ProductService> ();
builder.Services.AddScoped<IProductRepository, PostgresProductRepository> ();
builder.Services.AddScoped<IPersonRepository, PostgresPersonRepository> ();
builder.Services.AddScoped<IPersonService, PersonService> ();
builder.Services.AddScoped<IOrderDetailRepository, PostgresOrderDetailRepository> ();
builder.Services.AddScoped<IOrderDetailService, OrderDetailService> ();
builder.Services.AddScoped<IOrderService, OrderService> ();
builder.Services.AddScoped<IOrderRepository, PostgresOrderRepository> ();
builder.Services.AddScoped<INotificationService, NotificationService>();




builder.Services.AddDbContext<DatabaseManager>(options => options.UseNpgsql(builder.Configuration["DbConnection"]));
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


var app = builder.Build();

app.UseCors(options => options.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin());

// Configure the HTTP request pipeline.
app.UseSwagger();
app.UseSwaggerUI();


app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<DatabaseManager>();
    context.Database.EnsureCreated();
}

app.Run();
