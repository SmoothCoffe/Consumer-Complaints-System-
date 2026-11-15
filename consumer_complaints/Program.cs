// ===============================================================================
// Program.cs - نقطة الدخول للتطبيق (هيكل أولي فارغ)
// ===============================================================================
// هذا الملف يحتوي الحد الأدنى لتشغيل مشروع Razor Pages.
// أضف الخدمات (DbContext / Services / Session) تدريجيًا أثناء بناء المشروع.
// ===============================================================================

var builder = WebApplication.CreateBuilder(args);

// TODO: تسجيل ComplaintsDbContext عند إضافة Data/ComplaintsDbContext.cs
// TODO: تسجيل ComplaintsService / EmailService / SlaMonitoringService

builder.Services.AddRazorPages();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}
else
{
    app.UseDeveloperExceptionPage();
}

app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();

app.MapRazorPages();

// TODO: إضافة المسار الافتراضي (التحويل إلى /default أو /Setup)

app.Run();
