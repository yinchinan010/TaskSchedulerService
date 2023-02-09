using Hangfire;
using TaskSchedulerService;

var builder = WebApplication.CreateBuilder();

var appSettings = new AppSettings();
builder.Configuration.Bind(appSettings);
builder.Services.AddSingleton(appSettings);
builder.Services.AddResponseCompression();
builder.Services.AddControllers();
builder.Services.AddHangfire(configuration => configuration.UseInMemoryStorage());
builder.Services.AddHangfireServer();

var application = builder.Build();

application.UseHsts();
application.UseHttpsRedirection();
application.UseRouting();
application.UseResponseCompression();
application.MapControllers();
application.UseHangfireDashboard();

_ = new HangfireService(appSettings);

application.Run();
