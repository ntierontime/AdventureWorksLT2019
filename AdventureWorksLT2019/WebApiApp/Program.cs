using Framework.Mvc.Identity.Data;
using Framework.Mvc.Identity;
using Framework.Mvc;
using Framework.Models;
using AdventureWorksLT2019.RepositoryContracts;
using AdventureWorksLT2019.EFCoreRepositories;
using AdventureWorksLT2019.ServiceContracts;
using AdventureWorksLT2019.Services;
using AdventureWorksLT2019.Models;
using AdventureWorksLT2019.EFCoreContext;
using System.Configuration;
using System.Globalization;
using Microsoft.AspNetCore.Localization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using System.Text.Json.Serialization;
using Swashbuckle.AspNetCore.SwaggerGen;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Identity;

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

// 1.3. Other Services
builder.Services.AddScoped<IEmailSender, EmailSender>();

// 2. Database
builder.Services.AddDbContext<EFDbContext>(options =>
        options.UseSqlServer(builder.Configuration.GetConnectionString("AdventureWorksLT2019"), x => { x.UseNetTopologySuite(); x.UseBulk(); }),  ServiceLifetime.Scoped);

// 3. Identity Authentication
builder.Services.AddDbContext<ApplicationDbContext>(options =>
        options.UseSqlServer(builder.Configuration.GetConnectionString("Identity")));
builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>();
var identitySecretSection = builder.Configuration.GetSection(nameof(IdentitySecret));
var identitySecret = identitySecretSection.Get<IdentitySecret>();
builder.Services.Configure<IdentitySecret>(identitySecretSection);
var key = Encoding.ASCII.GetBytes(identitySecret.Secret);
builder.Services.AddAuthentication()
    .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme, cfg => cfg.SlidingExpiration = true)
    .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, x =>
    {
        x.RequireHttpsMetadata = false;
        x.SaveToken = true;
        x.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(key),
            ValidateIssuer = false,
            ValidateAudience = false
        };
    });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseRequestLocalization(app.Services.GetRequiredService<IOptions<RequestLocalizationOptions>>().Value);

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();

static string? GetSwaggerCustomizedSchemaId(Type x)
{
    // 1.
    if(x == typeof(Response<PaginationResponse>))
    {
        return "Framework.Models.ResponsePaginationResponse";
    }
    // 2. Customized DefaultView SchemaIds

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
    // 3. Customized PagedResponse SchemaIds

    if (x == typeof(ListResponse<BuildVersionDataModel[]>))
    {
        return "AdventureWorksLT2019.Models.BuildVersionListResponse";
    }

    if (x == typeof(ListResponse<ErrorLogDataModel[]>))
    {
        return "AdventureWorksLT2019.Models.ErrorLogListResponse";
    }

    if (x == typeof(ListResponse<AddressDataModel[]>))
    {
        return "AdventureWorksLT2019.Models.AddressListResponse";
    }

    if (x == typeof(ListResponse<CustomerDataModel[]>))
    {
        return "AdventureWorksLT2019.Models.CustomerListResponse";
    }

    if (x == typeof(ListResponse<CustomerAddressDataModel.DefaultView[]>))
    {
        return "AdventureWorksLT2019.Models.CustomerAddressListResponse";
    }

    if (x == typeof(ListResponse<ProductDataModel.DefaultView[]>))
    {
        return "AdventureWorksLT2019.Models.ProductListResponse";
    }

    if (x == typeof(ListResponse<ProductCategoryDataModel.DefaultView[]>))
    {
        return "AdventureWorksLT2019.Models.ProductCategoryListResponse";
    }

    if (x == typeof(ListResponse<ProductDescriptionDataModel[]>))
    {
        return "AdventureWorksLT2019.Models.ProductDescriptionListResponse";
    }

    if (x == typeof(ListResponse<ProductModelDataModel[]>))
    {
        return "AdventureWorksLT2019.Models.ProductModelListResponse";
    }

    if (x == typeof(ListResponse<ProductModelProductDescriptionDataModel.DefaultView[]>))
    {
        return "AdventureWorksLT2019.Models.ProductModelProductDescriptionListResponse";
    }

    if (x == typeof(ListResponse<SalesOrderDetailDataModel.DefaultView[]>))
    {
        return "AdventureWorksLT2019.Models.SalesOrderDetailListResponse";
    }

    if (x == typeof(ListResponse<SalesOrderHeaderDataModel.DefaultView[]>))
    {
        return "AdventureWorksLT2019.Models.SalesOrderHeaderListResponse";
    }
    // 4. Customized BulkActionDynamicParamsRequests SchemaIds

    if (x == typeof(BatchActionRequest<CustomerIdentifier, CustomerDataModel>))
    {
        return "AdventureWorksLT2019.Models.CustomerBulkActionDynamicParamsRequest";
    }

    if (x == typeof(BatchActionRequest<SalesOrderHeaderIdentifier, SalesOrderHeaderDataModel.DefaultView>))
    {
        return "AdventureWorksLT2019.Models.SalesOrderHeaderBulkActionDynamicParamsRequest";
    }
    return x?.FullName;
}

