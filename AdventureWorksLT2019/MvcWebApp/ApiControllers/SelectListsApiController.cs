using AdventureWorksLT2019.Models;
using AdventureWorksLT2019.RepositoryContracts;
using Framework.Models;

using Microsoft.AspNetCore.Mvc;

namespace AdventureWorksLT2019.MvcWebApp.ApiControllers
{
    [ApiController]
    [Route("/[controller]/[action]")]
    public class SelectListsApiController : Controller
    {
        private readonly IServiceScopeFactory _serviceScopeFactor;
        private readonly ILogger<SelectListsApiController> _logger;

        public SelectListsApiController(
            IServiceScopeFactory serviceScopeFactor,
            ILogger<SelectListsApiController> logger)
        {
            _serviceScopeFactor = serviceScopeFactor;
            _logger = logger;
        }

        // [Authorize]
        public async Task<ActionResult<PagedResponse<NameValuePair[]>>> GetProductCategoryCodeList(
            [FromQuery]ProductCategoryAdvancedQuery query)
        {
            using (var scope = _serviceScopeFactor.CreateScope())
            {
                var productCategoryRepository = scope.ServiceProvider.GetRequiredService<IProductCategoryRepository>();
                var serviceResponse = await productCategoryRepository.GetCodeList(query);
                return serviceResponse;
            }
        }

        // [Authorize]
        public async Task<ActionResult<PagedResponse<NameValuePair[]>>> GetProductDescriptionCodeList(
            [FromQuery]ProductDescriptionAdvancedQuery query)
        {
            using (var scope = _serviceScopeFactor.CreateScope())
            {
                var productDescriptionRepository = scope.ServiceProvider.GetRequiredService<IProductDescriptionRepository>();
                var serviceResponse = await productDescriptionRepository.GetCodeList(query);
                return serviceResponse;
            }
        }

        // [Authorize]
        public async Task<ActionResult<PagedResponse<NameValuePair[]>>> GetProductModelCodeList(
            [FromQuery]ProductModelAdvancedQuery query)
        {
            using (var scope = _serviceScopeFactor.CreateScope())
            {
                var productModelRepository = scope.ServiceProvider.GetRequiredService<IProductModelRepository>();
                var serviceResponse = await productModelRepository.GetCodeList(query);
                return serviceResponse;
            }
        }

    }
}

