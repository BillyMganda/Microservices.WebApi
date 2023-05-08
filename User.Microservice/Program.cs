using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;
using System.Text.Json.Serialization;
using User.Microservice.Authorization;
using User.Microservice.Helpers;
using User.Microservice.Services;

var builder = WebApplication.CreateBuilder(args);

var env = builder.Environment;
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors();
builder.Services.AddControllers().AddJsonOptions(x => x.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull);
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IJwtUtils, JwtUtils>();
builder.Services.Configure<AppSettings>(builder.Configuration.GetSection("AppSettings"));

//builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
builder.Services.AddDbContext<DataContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("SQLServerConnection")));
//builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options => {
//    options.TokenValidationParameters = new TokenValidationParameters
//    {
//        ValidateIssuerSigningKey = true,
//        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration.GetSection("Token").Value!)),
//        ValidateIssuer = false,
//        ValidateAudience = false
//    };
//});
builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
    {
        Description = "Standard authorization header using the bearer scheme",
        In = ParameterLocation.Header,
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey
    });
    options.OperationFilter<SecurityRequirementsOperationFilter>();
    options.SwaggerDoc("v1", new OpenApiInfo { Title = "Microservices Auth API", Version = "v1" });
});


var app = builder.Build();
app.UseHttpsRedirection();


//app.UseMiddleware<ExceptionMiddleware>();
// add hardcoded test user to db on startup
//using (var scope = app.Services.CreateScope())
//{
//    var context = scope.ServiceProvider.GetRequiredService<DataContext>();
//    var testUser = new UserModel
//    {
//        FirstName = "Test",
//        LastName = "User",
//        Username = "test",
//        PasswordHash = BCrypt.Net.BCrypt.HashPassword("test")
//    };
//    context.Users.Add(testUser);
//    context.SaveChanges();
//}

app.UseMiddleware<ErrorHandlerMiddleware>();
app.UseMiddleware<JwtMiddleware>();
app.UseCors(x => x
        .SetIsOriginAllowed(origin => true)
        .AllowAnyMethod()
        .AllowAnyHeader()
        .AllowCredentials());
app.UseAuthentication();
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Microservices Users Auth API v1");
});

app.UseAuthorization();
app.MapControllers();
app.Run();
