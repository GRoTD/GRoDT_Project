using FirebaseAdmin;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using SlippAPI.Authentication;
using SlippAPI.Options;
using SlippAPI.Services;
using SlippAPI.Services.Swagger;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

var constr = builder.Configuration.GetConnectionString(Path.Combine("SLipp_API_File_DB_Part1"));
constr += Directory.GetCurrentDirectory();
constr += builder.Configuration.GetConnectionString(Path.Combine("SLipp_API_File_DB_Part2"));

builder.Services.AddDbContext<SlippDbCtx>(options =>
    options.UseSqlite(
        constr,
        b => b.MigrationsAssembly("SlippAPI")));

//Using SQLite as of now. 

builder.Services.AddIdentityCore<DatabaseUser>(options => { options.Password.RequiredLength = 8; })
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<SlippDbCtx>();

builder.Services.AddScoped<TicketService>();
builder.Services.AddScoped<OrderService>();
builder.Services.AddScoped<Database>();
builder.Services.AddScoped<UserService>();
builder.Services.AddScoped<JwtService>();

RealtimeDbOptions realtimeDbOptions = new RealtimeDbOptions();
builder.Configuration.GetSection("RealtimeDbOptions").Bind(realtimeDbOptions);
builder.Services.AddSingleton<RealtimeDbOptions>(realtimeDbOptions);

builder.Services.AddSingleton(FirebaseApp.Create());


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("JwtAuth", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "Bearer",
        In = ParameterLocation.Header,
        Description = "Bearer Authorization header with JWT."
    });


    options.OperationFilter<SecurityRequirementsOperationFilter>();
});

builder.Services.AddCors();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddScheme<AuthenticationSchemeOptions, FirebaseAuthenticationHandler>(JwtBearerDefaults.AuthenticationScheme, (o) => { });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    using var scope = app.Services.CreateScope();
    var services = scope.ServiceProvider;
    var db = services.GetRequiredService<Database>();

    await db.RecreateAndSeed();
}

app.UseDeveloperExceptionPage();

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseCors(options => options.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();