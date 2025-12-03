
# معماری Modular Monolith پلتفرم رفاهی  
## نسخه کامل‌شده شامل Refahi.Shared و قوانین فدرال معماری

این سند مرجع رسمی برای معماری **Modular Monolith** در پلتفرم رفاهی است.  
هدف این سند ایجاد یک ساختار پایدار، مقیاس‌پذیر، قابل توسعه و کاملاً ماژولار است که در آینده بدون هزینه زیاد قابلیت تبدیل‌شدن به Microservices واقعی را داشته باشد.

---

# 1. مقدمه

معماری پلتفرم رفاهی یک **Modular Monolith با آمادگی کامل برای Microservice شدن** است.  
هر ماژول یک *Bounded Context* مستقل است و هیچ وابستگی مستقیمی به اجرای داخلی سایر ماژول‌ها ندارد.  

هر ماژول دارای لایه‌های کامل Domain / Application / Contracts / Infrastructure / Presentation است و فقط از طریق **Application.Contracts** و **Bus داخل برنامه (فعلاً MediatR)** با سایر ماژول‌ها تعامل دارد.

هنگام افزایش ترافیک یا نیاز سازمانی، هر ماژول می‌تواند با کمترین تغییر به یک Microservice مستقل تبدیل شود.

این سند قوانین رسمی (قوانین فدرال) معماری را توضیح می‌دهد که تمام ماژول‌ها باید رعایت کنند.

---

# 2. ساختار کلی Solution

ساختار کامل و توصیه‌شده:

```
Refahi.sln
 ├── Refahi.Api                          # Entry Point
 ├── Refahi.Shared                       # قوانین فدرال مشترک
 │     ├── Interfaces
 │     │     ├── ISharedStateCache.cs
 │     │     └── ...
 │     └── Abstracts
 │           └── ...
 ├── Refahi.Modules.Hotels
 │     ├── Hotels.Domain
 │     ├── Hotels.Application
 │     ├── Hotels.Application.Contracts
 │     ├── Hotels.Infrastructure
 │     └── Hotels.Presentation
 ├── Refahi.Modules.Wallet
 │     ├── Wallet.Domain
 │     ├── Wallet.Application
 │     ├── Wallet.Application.Contracts
 │     ├── Wallet.Infrastructure
 │     └── Wallet.Presentation
 ├── Refahi.Modules.Users
 ├── Refahi.Modules.Organizations
 └── ...
```

---

# 3. Refahi.Shared – قوانین فدرال

## 3.1 فلسفه

**Refahi.Shared** تنها محل تعریف استانداردهای اجباری سیستم است.  
هیچ Business Logic‌ در این پروژه وجود ندارد.  
هیچ کدی نباید از Shared به ماژول‌ها درز کند (Leak).  
Shared فقط شامل موارد زیر است:

- Interfaceهای مشترک (استانداردها)
- Abstract classهایی که معماری را enforce می‌کنند
- Policyهای معماری
- هر موردی که تمام ماژول‌ها باید رعایت کنند

> Shared هیچ پیاده‌سازی ندارد.  
> پیاده‌سازی همیشه در Refahi.Api انجام می‌شود.

---

# 4. Shared State & Distributed Cache

## 4.1 اصل بنیادین: ماژول‌ها باید Stateless باشند

هیچ ماژولی مجاز نیست state را داخل حافظه local نگهداری کند.

دلایل:

1. قابلیت **Horizontal Scaling**  
2. Load Balancing بدون session affinity  
3. Fault Tolerance  
4. آمادگی کامل برای Microservice شدن  

---

## 4.2 ISharedStateCache (در Refahi.Shared)

این interface پایه مشترک تمام ماژول‌ها است:

### مثال:

```csharp
public interface ISharedStateCache
{
    Task SetAsync<T>(string key, T value, TimeSpan ttl);
    Task<T?> GetAsync<T>(string key);
    Task RemoveAsync(string key);
}
```

### نکات:
- هیچ پیاده‌سازی در Shared انجام نمی‌شود.
- Dev → InMemory  
- Prod → Redis  
- Refahi.Api این interface را Resolve می‌کند و در DI قرار می‌دهد.
- تمام ماژول‌ها فقط از این interface استفاده می‌کنند.

---

## 4.3 خط لوله (Pipeline) ثبت Shared و Module Registration

Flow صحیح:

1. Refahi.Api شروع می‌شود  
2. Shared Services ثبت می‌شوند:

```
builder.Services.AddSharedInfrastructure(configuration);
builder.Services.AddSharedPolicies(configuration);
```

3. سپس ماژول‌ها ثبت می‌شوند:

```
builder.Services.AddHotelsModule();
builder.Services.AddWalletModule();
...
```

ماژول‌ها مجاز نیستند:

- Redis را مستقیم صدا بزنند  
- Cache خودشان داشته باشند  
- State را در حافظه داخلی نگه دارند  

---

# 5. معماری هر ماژول

هر ماژول یک Bounded Context است و ساختار زیر را دارد:

```
Refahi.Modules.<ModuleName>
 ├── <ModuleName>.Domain
 │     ├── Aggregates
 │     ├── Entities
 │     ├── ValueObjects
 │     ├── DomainEvents
 │     └── Exceptions
 ├── <ModuleName>.Application
 │     ├── CommandHandlers
 │     ├── QueryHandlers
 │     └── Mapping
 ├── <ModuleName>.Application.Contracts
 │     ├── Commands
 │     ├── Queries
 │     └── DTOs
 ├── <ModuleName>.Infrastructure
 │     ├── EF Core / DbContext
 │     ├── Provider Adapters
 │     ├── Repositories
 │     └── Configuration
 └── <ModuleName>.Presentation
       ├── Controllers / MinimalApi
       └── ModuleStartup (RegisterModule)
```

---

# 6. قوانین ارتباط بین ماژول‌ها

## 6.1 اصول:

- ماژول‌ها **نمی‌توانند** مستقیماً یکدیگر را reference کنند.  
- ارتباط فقط از طریق **Application.Contracts** انجام می‌شود.  
- اجرای command/query از طریق MediatR (Bus داخلی) انجام می‌شود.  
- ماژول Domain یک ماژول دیگر را نمی‌شناسد.  
- هیچ EF DbContextای از ماژول دیگر query نمی‌گیرد.  

---

# 7. CQRS

### در تمام ماژول‌ها:
- Commands عملیات تغییر state هستند  
- Queries فقط read-only هستند  
- Validation با FluentValidation  
- MediatR برای dispatch داخلی  

---

# 8. ذخیره‌سازی و پایگاه داده

## 8.1 اصل: هر ماژول DbContext اختصاصی دارد

هیچ ماژولی مجاز نیست:

- از DbContext ماژول دیگر استفاده کند  
- به جدول‌های سایر ماژول‌ها query مستقیم بزند  
- بین دو ماژول Foreign Key ایجاد کند  

این اصل یکی از پایه‌های جداسازی ماژول‌هاست.

---

# 9. اصول مهندسی و Best Practices

- Domain باید غنی (Rich Domain Model) و فاقد وابستگی خارجی باشد  
- Constructorهای domain private/ protected باشند  
- ValueObjectها immutable باشند  
- DomainEventها POCO ساده باشند  
- Infrastructure تنها جایی است که EF/HttpClient دارد  
- Presentation فقط ورودی/خروجی HTTP را مدیریت می‌کند  
- Application orchestrator بین Domain، Provider و SharedCache است  

---

# 10. اضافه‌کردن ماژول جدید

مراحل افزودن یک ماژول جدید:

1. ایجاد پروژه‌های زیر:
```
X.Domain
X.Application
X.Application.Contracts
X.Infrastructure
X.Presentation
```

2. نوشتن Startup Extension:  
```csharp
public static IServiceCollection AddXModule(this IServiceCollection services)
```

3. رجیستر در Refahi.Api:
```csharp
builder.Services.AddXModule();
```

---

# 11. جریان بوت‌استرپ در Refahi.Api

نمونه:

```csharp
var builder = WebApplication.CreateBuilder(args);

// Shared
builder.Services.AddSharedInfrastructure(builder.Configuration);
builder.Services.AddSharedPolicies(builder.Configuration);

// Modules
builder.Services.AddHotelsModule();
builder.Services.AddWalletModule();
builder.Services.AddUsersModule();
// ...

var app = builder.Build();
app.MapControllers();
app.Run();
```

---

# 12. نتیجه‌گیری

این معماری:

- کاملاً ماژولار  
- قابل گسترش  
- آماده تبدیل به میکروسرویس  
- بدون وابستگی داخلی  
- stateless و دارای پشتیبانی کامل برای load balancing  
- قابل نگهداری توسط تیم‌های مختلف  

می‌باشد.

حالا هر ماژول از جمله **Refahi.Hotels** می‌تواند روی این معماری با سرعت و نظم بالا توسعه پیدا کند.

---

اگر نیاز به نسخه PDF، فایل جداگانه Markdown یا افزودن دیاگرام‌های Mermaid داری، اشاره کن تا بلافاصله ایجاد کنم.
