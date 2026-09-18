using Microsoft.EntityFrameworkCore;
using pr9ASP.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<ShopDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("ShopDbConnection")));

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();