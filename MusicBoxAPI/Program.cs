using System.Data.SqlClient;
using System.Data;
using MusicBoxAPI;
using MusicBoxAPI.Interfaces.IRepository;
using MusicBoxAPI.Repository;
using MusicBoxAPI.Interfaces.IService;
using MusicBoxAPI.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "MusicBox API", Version = "v1" });

    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "JWT Authorization header using the Bearer scheme."
    });

    // 修改這部分，使用 OperationFilter
    c.OperationFilter<SecurityRequirementsOperationFilter>();
});
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

// 添加資料庫連接和 Repository 注冊
builder.Services.AddScoped<IDbConnection>(sp =>
    new SqlConnection(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddScoped<IAlbumRepository, AlbumRepository>();
builder.Services.AddScoped<IMusicBoxRepository, MusicBoxRepository>();
builder.Services.AddScoped<IPlayListRepository, PlayListRepository>();  //
builder.Services.AddScoped<IUserRepository, UserRepository>();  //
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
        };
    });
// 注冊服務
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<DataBase>();


builder.Services.AddCors(options =>
{
    options.AddPolicy(name: "APICORS",
        policy =>
        {
            policy.AllowAnyMethod()
                  .AllowAnyHeader()    // 加入這行
                  .AllowAnyOrigin();
        });
});



var app = builder.Build();


app.UseCors("APICORS");

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.UseAuthentication();  
app.UseAuthorization();

app.MapControllers();

app.Run();
