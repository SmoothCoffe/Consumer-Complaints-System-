# Consumer Complaints — Skeleton

هيكل مجلدات فارغ لمشروع **نظام إدارة شكاوى المستهلكين** (ASP.NET Core Razor Pages / .NET 8)، جاهز للرفع إلى GitHub ثم البدء بإضافة الملفات تدريجيًا.

## المتطلبات

- Visual Studio 2022 (17.8 أو أحدث) مع workload **ASP.NET and web development**
- .NET SDK 8.0

## فتح المشروع

1. افتح `consumer_complaints.sln` في Visual Studio.
2. من Terminal:

```powershell
cd consumer_complaints
dotnet restore
dotnet build
dotnet run
```

## هيكل المشروع

```
ConsumerComplaints/
├── .gitattributes
├── .gitignore
├── README.md
├── consumer_complaints.sln
└── consumer_complaints/
    ├── consumer_complaints.csproj
    ├── Program.cs
    ├── appsettings.json
    ├── appsettings.Development.json
    ├── Data/                       ← فارغ (ComplaintsDbContext.cs, DbInitializer.cs)
    ├── Migrations/                 ← فارغ (تُنشأ بـ dotnet ef migrations add)
    ├── Models/                     ← فارغ (AppUser.cs, Consumer.cs, ... إلخ)
    ├── Pages/
    │   ├── _ViewImports.cshtml
    │   ├── _ViewStart.cshtml
    │   └── Shared/
    │       └── _Layout.cshtml
    ├── Properties/
    │   └── launchSettings.json
    ├── Services/                   ← فارغ (ComplaintsService.cs, ...)
    └── wwwroot/
        ├── css/site.css
        └── lib/bootstrap/
            ├── css/bootstrap.min.css
            └── js/bootstrap.bundle.min.js
```

> **ملاحظة:** المجلدات الفارغة تحتوي ملف `.gitkeep` حتى يحتفظ Git بها عند الرفع، لأن Git لا يتتبع المجلدات الفارغة.

## الصفحات المخطط إضافتها داخل `Pages/`

`default` · `Setup` · `Welcome_JE` · `complaint_form` · `assignment` · `assignmentSuccess` · `closeComplaint` · `insertionSuccess` · `Links` · `logout` · `ManageDepartments` · `ManageUsers` · `Notifications` · `report_rangeSelection` · `Error` · `errorPage`

## الرفع إلى GitHub

### الطريقة الأولى — من Visual Studio

1. من شريط القوائم: **Git → Create Git Repository...**
2. اختر **Local only** ثم حدد مسار المجلد `ConsumerComplaints`.
3. اضغط **Create**، ثم من نافذة **Git Changes** اكتب رسالة الـ commit واضغط **Commit All**.
4. اضغط **Push** ثم اختر **Create new GitHub repository** (يتطلب تسجيل الدخول إلى GitHub).

### الطريقة الثانية — من سطر الأوامر (يتطلب تثبيت Git)

```powershell
cd ConsumerComplaints
git init
git add .
git commit -m "Initial project skeleton"
git branch -M main
git remote add origin https://github.com/<username>/<repo>.git
git push -u origin main
```

## الخطوات التالية

1. أضف الـ Models داخل `Models/`.
2. أضف `ComplaintsDbContext` و`DbInitializer` داخل `Data/`.
3. أضف الخدمات داخل `Services/`.
4. أنشئ الصفحات داخل `Pages/`.
5. فعّل الخدمات في `Program.cs`.
6. أنشئ أول Migration:

```powershell
dotnet ef migrations add InitialCreate
dotnet ef database update
```
