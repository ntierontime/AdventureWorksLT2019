using AdventureWorksLT2019.RepositoryContracts;
using AdventureWorksLT2019.EFCoreRepositories;
using AdventureWorksLT2019.ServiceContracts;
using AdventureWorksLT2019.Services;
using AdventureWorksLT2019.EFCoreContext;
using AdventureWorksLT2019.MvcWebApp.Models;
using System.Configuration;
using System.Globalization;
using Microsoft.AspNetCore.Localization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews()
    .AddViewLocalization(Microsoft.AspNetCore.Mvc.Razor.LanguageViewLocationExpanderFormat.Suffix)
    .AddDataAnnotationsLocalization(options =>
    {
        options.DataAnnotationLocalizerProvider = (type, factory) => factory.Create(typeof(AdventureWorksLT2019.Resx.Resources.UIStrings));
    });

builder.Services.AddHttpContextAccessor();

builder.Services.AddLocalization(options => { options.ResourcesPath = "Resources"; });

// http://blog.mohnady.com/2017/05/how-to-aspnet-core-resource-files-in.html
//
builder.Services.Configure<RequestLocalizationOptions>(options =>
{
    var supportedCultures = new List<CultureInfo>
        {
            new CultureInfo("en"),
            new CultureInfo("fr")
        };
    options.DefaultRequestCulture = new RequestCulture("fr", "fr");
    options.SupportedCultures = supportedCultures;
    options.SupportedUICultures = supportedCultures;
});

builder.Services.AddSingleton<AdventureWorksLT2019.Resx.IUIStrings, AdventureWorksLT2019.Resx.UIStrings>();

// 1.1. IoC Repositories
builder.Services.AddScoped<IBuildVersionRepository, BuildVersionRepository>();
builder.Services.AddScoped<IErrorLogRepository, ErrorLogRepository>();
builder.Services.AddScoped<IAddressRepository, AddressRepository>();
builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();
builder.Services.AddScoped<ICustomerAddressRepository, CustomerAddressRepository>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IProductCategoryRepository, ProductCategoryRepository>();
builder.Services.AddScoped<IProductDescriptionRepository, ProductDescriptionRepository>();
builder.Services.AddScoped<IProductModelRepository, ProductModelRepository>();
builder.Services.AddScoped<IProductModelProductDescriptionRepository, ProductModelProductDescriptionRepository>();
builder.Services.AddScoped<ISalesOrderDetailRepository, SalesOrderDetailRepository>();
builder.Services.AddScoped<ISalesOrderHeaderRepository, SalesOrderHeaderRepository>();

// 1.2. IoC Services
builder.Services.AddScoped<IBuildVersionService, BuildVersionService>();
builder.Services.AddScoped<IErrorLogService, ErrorLogService>();
builder.Services.AddScoped<IAddressService, AddressService>();
builder.Services.AddScoped<ICustomerService, CustomerService>();
builder.Services.AddScoped<ICustomerAddressService, CustomerAddressService>();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<IProductCategoryService, ProductCategoryService>();
builder.Services.AddScoped<IProductDescriptionService, ProductDescriptionService>();
builder.Services.AddScoped<IProductModelService, ProductModelService>();
builder.Services.AddScoped<IProductModelProductDescriptionService, ProductModelProductDescriptionService>();
builder.Services.AddScoped<ISalesOrderDetailService, SalesOrderDetailService>();
builder.Services.AddScoped<ISalesOrderHeaderService, SalesOrderHeaderService>();

// 1.3. Other Services
builder.Services.AddScoped<IDropDownListService, DropDownListService>();

// 1.4. UI Related
builder.Services.AddScoped<SelectListHelper>();
builder.Services.AddScoped<UIAvailableFeaturesManager>();
builder.Services.AddScoped<ViewFeaturesManager>();
builder.Services.AddScoped<DashboardViewModelHelper>();
builder.Services.AddScoped<OrderBysListHelper>();
builder.Services.AddScoped<MvcItemViewModelHelper>();
builder.Services.AddScoped<PagedSearchViewModelHelper>();

builder.Services.AddDbContext<EFDbContext>(options =>
        options.UseSqlServer(builder.Configuration.GetConnectionString("AdventureWorksLT2019"), x => { x.UseNetTopologySuite(); x.UseBulk(); }),  ServiceLifetime.Scoped);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRequestLocalization(app.Services.GetRequiredService<IOptions<RequestLocalizationOptions>>().Value);

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();

