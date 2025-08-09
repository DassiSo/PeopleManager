using Microsoft.EntityFrameworkCore;
using PeopleManagementApi.Models;

var builder = WebApplication.CreateBuilder(args);


//builder.Services.AddCors(options =>
//{
//    options.AddPolicy("AllowAngularApp", policy =>
//    {
//        policy.WithOrigins("http://localhost:55939")
//              .AllowAnyMethod()
//              .AllowAnyHeader();
//    });
//});
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAngularApp", policy =>
    {
        policy.AllowAnyOrigin() 
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.UseCors("AllowAngularApp");

app.UseHttpsRedirection();
app.UseRouting();
app.UseAuthorization();
app.MapControllers();

app.Run();