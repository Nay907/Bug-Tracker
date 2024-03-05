using capstone_backend.Middleware;
using capstone_backend.Repository;
using capstone_backend.Repository.interfaces;
using capstone_backend.Services;
using capstone_backend.Services.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
    };
});

builder.Services.AddAuthorization();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IBugsRepo, BugsRepo>();
builder.Services.AddTransient<IBugService, BugServiceImpl>();

builder.Services.AddScoped<IUserRepo, UserRepo>();
builder.Services.AddTransient<IUserService, UserService>();

builder.Services.AddScoped<ICommentRepo, CommentRepo>();
builder.Services.AddTransient<ICommentService, CommentService>();

builder.Services.AddScoped<IProjectRepo, ProjectRepo>();
builder.Services.AddTransient<IProjectService, ProjectService>();




builder.Services.AddCors((o) =>
{
    o.AddPolicy("corsPolicy", b => b.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseMiddleware<JwtAuthMiddleware>();

app.UseAuthentication();

app.UseAuthorization();

app.UseCors("corsPolicy");

app.MapControllers();

app.Run();
