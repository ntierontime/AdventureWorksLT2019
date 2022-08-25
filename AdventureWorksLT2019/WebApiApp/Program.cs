using AdventureWorksLT2019.RepositoryContracts;
using AdventureWorksLT2019.EFCoreRepositories;
using AdventureWorksLT2019.ServiceContracts;
using AdventureWorksLT2019.Services;
using AdventureWorksLT2019.EFCoreContext;
using System.Configuration;
using System.Globalization;
using Microsoft.AspNetCore.Localization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using System.Text.Json.Serialization;
using Swashbuckle.AspNetCore.SwaggerGen;
using Framework.Models;
using AdventureWorksLT2019.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
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

builder.Services
    .AddControllers()
    .AddJsonOptions(options =>
        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()));

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.ConfigureSwaggerGen((Action<SwaggerGenOptions>)(options =>
{
    options.CustomSchemaIds((Func<Type, string>)(x =>
    {
        return GetSwaggerCustomizedSchemaId(x);
    }));
}));
builder.Services.AddSwaggerGen();

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

builder.Services.AddDbContext<EFDbContext>(options =>
        options.UseSqlServer(builder.Configuration.GetConnectionString("AdventureWorksLT2019"), x => { x.UseNetTopologySuite(); x.UseBulk(); }),  ServiceLifetime.Scoped);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseRequestLocalization(app.Services.GetRequiredService<IOptions<RequestLocalizationOptions>>().Value);

app.UseAuthorization();

app.MapControllers();

app.Run();

static string GetSwaggerCustomizedSchemaId(Type x)
{
        // 1. Customized DefaultView SchemaIds

        if (x == typeof(CustomerAddressDataModel.DefaultView))
        {
            return "AdventureWorksLT2019.Models.CustomerAddressDataModelDefaultView";
        }

        if (x == typeof(ProductDataModel.DefaultView))
        {
            return "AdventureWorksLT2019.Models.ProductDataModelDefaultView";
        }

        if (x == typeof(ProductCategoryDataModel.DefaultView))
        {
            return "AdventureWorksLT2019.Models.ProductCategoryDataModelDefaultView";
        }

        if (x == typeof(ProductModelProductDescriptionDataModel.DefaultView))
        {
            return "AdventureWorksLT2019.Models.ProductModelProductDescriptionDataModelDefaultView";
        }

        if (x == typeof(SalesOrderDetailDataModel.DefaultView))
        {
            return "AdventureWorksLT2019.Models.SalesOrderDetailDataModelDefaultView";
        }

        if (x == typeof(SalesOrderHeaderDataModel.DefaultView))
        {
            return "AdventureWorksLT2019.Models.SalesOrderHeaderDataModelDefaultView";
        }
        // 2. Customized PagedResponse SchemaIds

        if (x == typeof(PagedResponse<BuildVersionDataModel[]>))
        {
            return "AdventureWorksLT2019.Models.BuildVersionPagedResponse";
        }

        if (x == typeof(PagedResponse<ErrorLogDataModel[]>))
        {
            return "AdventureWorksLT2019.Models.ErrorLogPagedResponse";
        }

        if (x == typeof(PagedResponse<AddressDataModel[]>))
        {
            return "AdventureWorksLT2019.Models.AddressPagedResponse";
        }

        if (x == typeof(PagedResponse<CustomerDataModel[]>))
        {
            return "AdventureWorksLT2019.Models.CustomerPagedResponse";
        }

        if (x == typeof(PagedResponse<CustomerAddressDataModel.DefaultView[]>))
        {
            return "AdventureWorksLT2019.Models.CustomerAddressPagedResponse";
        }

        if (x == typeof(PagedResponse<ProductDataModel.DefaultView[]>))
        {
            return "AdventureWorksLT2019.Models.ProductPagedResponse";
        }

        if (x == typeof(PagedResponse<ProductCategoryDataModel.DefaultView[]>))
        {
            return "AdventureWorksLT2019.Models.ProductCategoryPagedResponse";
        }

        if (x == typeof(PagedResponse<ProductDescriptionDataModel[]>))
        {
            return "AdventureWorksLT2019.Models.ProductDescriptionPagedResponse";
        }

        if (x == typeof(PagedResponse<ProductModelDataModel[]>))
        {
            return "AdventureWorksLT2019.Models.ProductModelPagedResponse";
        }

        if (x == typeof(PagedResponse<ProductModelProductDescriptionDataModel.DefaultView[]>))
        {
            return "AdventureWorksLT2019.Models.ProductModelProductDescriptionPagedResponse";
        }

        if (x == typeof(PagedResponse<SalesOrderDetailDataModel.DefaultView[]>))
        {
            return "AdventureWorksLT2019.Models.SalesOrderDetailPagedResponse";
        }

        if (x == typeof(PagedResponse<SalesOrderHeaderDataModel.DefaultView[]>))
        {
            return "AdventureWorksLT2019.Models.SalesOrderHeaderPagedResponse";
        }
        // 3. Customized BulkActionDynamicParamsRequests SchemaIds

        if (x == typeof(BatchActionViewModel<CustomerIdentifier, CustomerDataModel>))
        {
            return "AdventureWorksLT2019.Models.CustomerBulkActionDynamicParamsRequest";
        }

        if (x == typeof(BatchActionViewModel<SalesOrderHeaderIdentifier, SalesOrderHeaderDataModel.DefaultView>))
        {
            return "AdventureWorksLT2019.Models.SalesOrderHeaderBulkActionDynamicParamsRequest";
        }
    return x?.FullName;
}

