using Brewery_SCADA_System.Database;
using Brewery_SCADA_System.Hubs;
using Brewery_SCADA_System.Infrastructure;
using Brewery_SCADA_System.Repository;
using Brewery_SCADA_System.Services;
using Microsoft.AspNetCore.Authentication.Cookies;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// builder.Services.AddDbContext<DatabaseContext>();
builder.Services.AddSingleton<DatabaseContext>();

//Repositories
builder.Services.AddSingleton<IUserRepository, UserRepository>();
builder.Services.AddSingleton<IDeviceRepository, DeviceRepository>();
builder.Services.AddSingleton<IAnalogInputRepository, AnalogInputRepository>();
builder.Services.AddSingleton<IDigitalInputRepository, DigitalInputRepository>();
builder.Services.AddSingleton<IIODigitalDataRepository, IODigitalDataRepository>();
builder.Services.AddSingleton<IIOAnalogDataRepository, IOAnalogDataRepository>();
builder.Services.AddSingleton<IAlarmRepository, AlarmRepository>();
builder.Services.AddSingleton<IAlarmAlertRepository, AlarmAlertRepository>();
//Services
builder.Services.AddSingleton<IUserService, UserService>();
builder.Services.AddSingleton<IDeviceService, DeviceService>();
builder.Services.AddSingleton<ITagService, TagService>();
builder.Services.AddSingleton<IAlarmService, AlarmService>();

//Security
builder.Services.AddTransient<CustomCookieAuthenticationEvents>();

builder.Services.AddHostedService<StartupService>();
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowReactApp",
        builder =>
        {
            builder.WithOrigins("http://localhost:5173")
                .AllowAnyHeader()
                .AllowAnyMethod()
                .AllowCredentials();
        });
});

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
       .AddCookie(options =>
       {
           options.Cookie.SameSite = SameSiteMode.None;
           options.Cookie.Name = "auth";
           options.SlidingExpiration = true;
           options.ExpireTimeSpan = TimeSpan.FromMinutes(20);
           options.Cookie.MaxAge = options.ExpireTimeSpan;
           options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
           options.EventsType = typeof(CustomCookieAuthenticationEvents);
       });

builder.Services.AddAuthorization();

//websockets
builder.Services.AddSignalR();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("AllowReactApp");
app.UseMiddleware<ExceptionMiddleware>(true);
app.UseAuthentication();
app.UseAuthorization();

app.UseHttpsRedirection();
app.MapControllers();
app.MapHub<TagHub>("/Hub/tag");
app.MapHub<AlarmHub>("/Hub/alarm");

app.Run();
