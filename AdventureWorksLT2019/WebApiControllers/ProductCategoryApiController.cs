using AdventureWorksLT2019.ServiceContracts;
using Framework.Mvc;
using AdventureWorksLT2019.Models;
using Framework.Models;

using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Net;

namespace AdventureWorksLT2019.WebApiControllers
{
    // [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [ApiController]
    [Route("/api/[controller]/[action]")]
    public partial class ProductCategoryApiController : BaseApiController
    {
        private readonly IProductCategoryService _thisService;
        private readonly IServiceProvider _serviceProvider;
        private readonly ILogger<ProductCategoryApiController> _logger;

        public ProductCategoryApiController(IProductCategoryService thisService, IServiceProvider serviceProvider, ILogger<ProductCategoryApiController> logger)
        {
            this._serviceProvider = serviceProvider;
            this._thisService = thisService;
            this._logger = logger;
        }

        // [Authorize]
        [HttpGet]
        [HttpPost]
        public async Task<ActionResult<ListResponse<ProductCategoryDataModel.DefaultView[]>>> Search(
            ProductCategoryAdvancedQuery query)
        {
            var serviceResponse = await _thisService.Search(query);
            return ReturnActionResult(serviceResponse);
        }

        // [Authorize]
        [Route("{ProductCategoryID}")]
        [HttpGet]
        public async Task<ActionResult<ProductCategoryCompositeModel>> GetCompositeModel([FromRoute]ProductCategoryIdentifier id)
        {
            var listItemRequests = new Dictionary<ProductCategoryCompositeModel.__DataOptions__, CompositeListItemRequest>();

            listItemRequests.Add(ProductCategoryCompositeModel.__DataOptions__.Products_Via_ProductCategoryID,
                new CompositeListItemRequest()
                {
                    PageSize = 100,
                    OrderBys = "SellStartDate",
                    PaginationOption = PaginationOptions.NoPagination,
                });

            listItemRequests.Add(ProductCategoryCompositeModel.__DataOptions__.ProductCategories_Via_ParentProductCategoryID,
                new CompositeListItemRequest()
                {
                    PageSize = 100,
                    OrderBys = "ModifiedDate",
                    PaginationOption = PaginationOptions.NoPagination,
                });

            var serviceResponse = await _thisService.GetCompositeModel(id, listItemRequests);
            return Ok(serviceResponse);
        }

        /*
        // [Authorize]
        [HttpGet, ActionName("HeartBeat")]
        public Task<ActionResult> HeartBeat()
        {
            return Ok();
        }
        */
    }
}

