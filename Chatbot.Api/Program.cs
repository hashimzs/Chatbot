using Chatbot.Api;
using Chatbot.Api.Data;
using Chatbot.Api.Middleware;
using Chatbot.Api.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("OnlineConnection"))
);

//builder.Services.AddDbContext<AppDbContext>(options =>
//    options.UseMySQL(builder.Configuration.GetConnectionString("OnlineConnection"))
//);


//builder.Services.AddDbContext<AppDbContext>(options =>
//    options.UseNpgsql(builder.Configuration.GetConnectionString("OnlineConnection"))
//);

builder.Services.AddScoped<UserRepository>();
builder.Services.AddScoped<ChatRepository>();
builder.Services.AddScoped<TokenManager>();

var app = builder.Build();

app.UseCors(o =>
    o.AllowAnyHeader()
    .AllowAnyMethod()
    .WithExposedHeaders("token")
    .AllowAnyOrigin()
);

app.UseMiddleware<ErrorHandlerMiddleware>();
app.UseMiddleware<JwtMiddleware>();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
