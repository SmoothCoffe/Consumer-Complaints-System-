
using consumer_complaints.Data;
using consumer_complaints.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorPages();
builder.Services.AddDbContext<ComplaintsDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("ComplaintsConnection"),
        Sql => Sql.EnableRetryOnFailure(3)));
builder.Services.AddMemoryCache();
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.SecurePolicy = CookieSecurePolicy.SameAsRequest;
});
builder.Services.Configure<EmailSettings>(builder.Configuration.GetSection("EmailSettings"));
builder.Services.Configure<AppSettings>(builder.Configuration.GetSection("AppSettings"));

//builder.services.addscoped<autoassignmentservice>();
//builder.services.addscoped<complaintintakeservice>();
//builder.services.addscoped<emailoutboxservice>();
//builder.services.addscoped<escalationservice>();
//builder.services.addscoped<trackingverificationservice>();

builder.Services.AddScoped<IEmailSender, LoggingEmailSender>();
//{
//    var settings = sp.getrequiredservice<microsoft.extensions.options.ioptions<emailsettings>>().value;

//    return settings.isconfigured
//        ? activatorutilities.createinstance<smtpemailsender>(sp)
//        : activatorutilities.createinstance<loggingemailsender>(sp);
//});
//builder.services.addhostedservice<escalationworker>();
//builder.services.addhostedservice<emailoutboxworker>();



var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}
else
{
    app.UseDeveloperExceptionPage();

    using var scope = app.Services.CreateScope();
    var db = scope.ServiceProvider.GetRequiredService<ComplaintsDbContext>();
    db.Database.Migrate();
}

app.UseStaticFiles();
app.UseRouting();
app.UseSession();
app.UseAuthorization();
app.MapRazorPages();

app.Run();
