// AppDbContexts 檔案中，處理資料庫連線的 namespace EmployeeAPI.Data

using EmployeeAPI.Data;
using Microsoft.AspNetCore.Connections;
using Microsoft.EntityFrameworkCore;


var builder = WebApplication.CreateBuilder(args);


// 取得 appsettings.json 裡的連線字串
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");


// 註冊 DbContext，並指定使用 SQL Server
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(connectionString));



// Add services to the container.
builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();



var app = builder.Build();



// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.UseAuthorization();
app.MapControllers();


app.Run();
