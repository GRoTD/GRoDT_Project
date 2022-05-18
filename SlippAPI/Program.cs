using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SlippAPI.Services;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddDbContext<SlippDbCtx>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("SlippDb")));
builder.Services.AddIdentityCore<DatabaseUser>()
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<SlippDbCtx>();
builder.Services.AddScoped<TicketService>();
builder.Services.AddScoped<Database>();
builder.Services.AddScoped<UserService>();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    using var scope = app.Services.CreateScope();
    var services = scope.ServiceProvider;
    var db = services.GetRequiredService<Database>();

    await db.RecreateAndSeed();
}

app.UseHttpsRedirection();


app.UseCors(options => options.AllowAnyOrigin().AllowAnyMethod());
app.UseAuthorization();

app.MapControllers();

app.Run();