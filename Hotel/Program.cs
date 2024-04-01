

using HotelMinimalWebApi.Apis;
using HotelMinimalWebApi.Auth;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);
RegisterServices(builder.Services);

var app = builder.Build();

Configure(app);

new HotelApi().Register(app);
new AuthApi().Register(app);    
        
app.MapControllers();

app.Run();
 
void RegisterServices(IServiceCollection services)
{
    services.AddDbContext<HotelDb>(options =>
    {
        options.UseSqlServer(builder.Configuration.GetConnectionString("Default"));
    });

    services.AddScoped<IHotelRepository, HotelRepository>();
    services.AddSingleton<ITokenService, TokenService>();
    services.AddSingleton<IUserRepository, UserRepository>();
    services.AddAuthorization();
    services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
        .AddJwtBearer(options =>
        {
            options.TokenValidationParameters = new()
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
    // Add services to the container.

    services.AddControllers();
    // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
    services.AddEndpointsApiExplorer();
    services.AddSwaggerGen();
}
void Configure(WebApplication app)
{
    app.UseHttpsRedirection();
    app.UseAuthentication();
    app.UseAuthorization();
    if (app.Environment.IsDevelopment())
    {
        using var scope = app.Services.CreateScope();
        var db = scope.ServiceProvider.GetRequiredService<HotelDb>();
        db.Database.EnsureCreated();
        app.UseSwagger();
        app.UseSwaggerUI();
    }


}