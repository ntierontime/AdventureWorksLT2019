using AdventureWorksLT2019.MvcWebApp.Models;
using AdventureWorksLT2019.ServiceContracts;
using AdventureWorksLT2019.Resx;
using AdventureWorksLT2019.Models.Definitions;
using AdventureWorksLT2019.Models;
using Framework.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace AdventureWorksLT2019.MvcWebApp.Controllers
{
    public class AddressController : Controller
    {
        private readonly IAddressService _thisService;
        private readonly SelectListHelper _selectListHelper;
        private readonly ViewFeaturesManager _viewFeatureManager;
        private readonly IDropDownListService _dropDownListService;
        private readonly OrderBysListHelper _orderBysListHelper;
        private readonly MvcItemViewModelHelper _mvcItemViewModelHelper;
        private readonly PagedSearchViewModelHelper _pagedSearchViewModelHelper;
        private readonly IUIStrings _localizor;
        private readonly ILogger<AddressController> _logger;

        public AddressController(
            IAddressService thisService,
            SelectListHelper selectListHelper,
            ViewFeaturesManager viewFeatureManager,
            IDropDownListService dropDownListService,
            OrderBysListHelper orderBysListHelper,
            MvcItemViewModelHelper mvcItemViewModelHelper,
            PagedSearchViewModelHelper pagedSearchViewModelHelper,
            IUIStrings localizor,
            ILogger<AddressController> logger)
        {
            _thisService = thisService;
            _selectListHelper = selectListHelper;
            _viewFeatureManager = viewFeatureManager;
            _dropDownListService = dropDownListService;
            _orderBysListHelper = orderBysListHelper;
            _mvcItemViewModelHelper = mvcItemViewModelHelper;
            _pagedSearchViewModelHelper = pagedSearchViewModelHelper;
            _localizor = localizor;
            _logger = logger;
        }

        // GET: Address
        [HttpGet] // from query string
        [HttpPost]// form post formdata
        public async Task<IActionResult> Index(AddressAdvancedQuery query, UIParams uiParams)
        {
            _viewFeatureManager.DefaultUIParamsIfNeeds(uiParams, PagedViewOptions.Table);
            // UIParams.PagedViewOption is not null here
            query.PaginationOption = _viewFeatureManager.HardCodePaginationOption(uiParams.PagedViewOption!.Value, query.PaginationOption);
            if (string.IsNullOrEmpty(query.OrderBys))
            {
                query.OrderBys = _orderBysListHelper.GetDefaultAddressOrderBys();
            }

            var result = await _thisService.Search(query);

            var vm = await _pagedSearchViewModelHelper.GetAddressPagedSearchViewModel("index", uiParams, query, result, true, true);
            return View(vm);
        }

        // GET: Address/AjaxLoadItems
        [HttpGet] // from query string
        [HttpPost]// form post formdata
        public async Task<IActionResult> AjaxLoadItems(AddressAdvancedQuery query, UIParams uiParams)
        {
            var result = await _thisService.Search(query);
            var pagedViewModel = new PagedViewModel<AddressDataModel[]>
            {
                UIListSetting = _viewFeatureManager.GetAddressUIListSetting(String.Empty, uiParams),
                Result = result,
            };

            if(uiParams.Template == ViewItemTemplateNames.Create || uiParams.Template == ViewItemTemplateNames.Edit)
            {
            }

            if (uiParams.PagedViewOption == PagedViewOptions.Table || uiParams.PagedViewOption == PagedViewOptions.EditableTable)
            {
                return PartialView("~/Views/Address/_Table.cshtml", pagedViewModel);
            }
            else if (uiParams.PagedViewOption == PagedViewOptions.Tiles)
            {
                return PartialView("~/Views/Address/_Tiles.cshtml", pagedViewModel);
            }
            //else // if (uiParams.PagedViewOption == PagedViewOptions.SlideShow)
            // SlideShow
            return PartialView("~/Views/Address/_SlideShow.cshtml", pagedViewModel);

        }

        [Route("[controller]/[action]/{AddressID}")] // Primary
        //[HttpGet, ActionName("Edit")]
        // GET: Address/Edit/{AddressID}
        public async Task<IActionResult> Edit([FromRoute]AddressIdentifier id)
        {
            if (!id.AddressID.HasValue)
            {
                var itemViewModel1 = new AdventureWorksLT2019.MvcWebApp.Models.MvcItemViewModel<AddressDataModel>
                {
                    Status = System.Net.HttpStatusCode.NotFound,
                    StatusMessage = "Not Found",
                    Template = ViewItemTemplateNames.Edit.ToString(),
                };
                return View(itemViewModel1);
            }

            var result = await _thisService.Get(id);
            var itemViewModel = new AdventureWorksLT2019.MvcWebApp.Models.MvcItemViewModel<AddressDataModel>
            {
                Status = result.Status,
                StatusMessage = result.StatusMessage,
                Template = ViewItemTemplateNames.Edit.ToString(),

                Model = result.ResponseBody
            };
            return View(itemViewModel);
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]

        [Route("[controller]/[action]/{AddressID}")] // Primary
        // POST: Address/Edit/{AddressID}
        public async Task<IActionResult> Edit(
            [FromRoute]AddressIdentifier id,
            [Bind("AddressID,AddressLine1,AddressLine2,City,StateProvince,CountryRegion,PostalCode,ModifiedDate")] AddressDataModel input)
        {
            if (!id.AddressID.HasValue ||
                id.AddressID.HasValue && id.AddressID != input.AddressID)
            {
                var itemViewModel1 = new AdventureWorksLT2019.MvcWebApp.Models.MvcItemViewModel<AddressDataModel>
                {
                    Status = System.Net.HttpStatusCode.NotFound,
                    StatusMessage = "Not Found",
                    Template = ViewItemTemplateNames.Edit.ToString(),

                    Model = input, // should GetbyId again and merge content not in postback
                };
                return View(itemViewModel1);
            }

            if (!ModelState.IsValid)
            {
                var itemViewModel1 = new AdventureWorksLT2019.MvcWebApp.Models.MvcItemViewModel<AddressDataModel>
                {
                    Status = System.Net.HttpStatusCode.BadRequest,
                    StatusMessage = "Bad Request",
                    Template = ViewItemTemplateNames.Edit.ToString(),

                    Model = input, // should GetbyId again and merge content not in postback
                };
                return View(itemViewModel1);
            }

            var result = await _thisService.Update(id, input);
            var itemViewModel = new AdventureWorksLT2019.MvcWebApp.Models.MvcItemViewModel<AddressDataModel>
            {
                Status = result.Status,
                StatusMessage = result.StatusMessage,
                Template = ViewItemTemplateNames.Edit.ToString(),

                Model = result.ResponseBody,
            };
            return View(itemViewModel);
        }

        [Route("[controller]/[action]/{AddressID}")] // Primary
        // GET: Address/Details/{AddressID}
        public async Task<IActionResult> Details([FromRoute]AddressIdentifier id)
        {
            var result = await _thisService.Get(id);
            var itemViewModel = new AdventureWorksLT2019.MvcWebApp.Models.MvcItemViewModel<AddressDataModel>
            {
                Status = result.Status,
                StatusMessage = result.StatusMessage,
                Template = ViewItemTemplateNames.Details.ToString(),
                Model = result.ResponseBody,
            };
            return View(itemViewModel);
        }

        // GET: Address/Create
        public async Task<IActionResult> Create()
        {
            var itemViewModel = await Task.FromResult(new AdventureWorksLT2019.MvcWebApp.Models.MvcItemViewModel<AddressDataModel>
            {
                Status = System.Net.HttpStatusCode.OK,
                Template = ViewItemTemplateNames.Create.ToString(),
                Model = _thisService.GetDefault(),

            });

            return View(itemViewModel);
        }

        // POST: Address/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(
            [Bind("AddressLine1,AddressLine2,City,StateProvince,CountryRegion,PostalCode,ModifiedDate")] AddressDataModel input)
        {
            if (ModelState.IsValid)
            {
                var result = await _thisService.Create(input);
                var itemViewModel = new AdventureWorksLT2019.MvcWebApp.Models.MvcItemViewModel<AddressDataModel>
                {
                    Status = result.Status,
                    StatusMessage = result.StatusMessage,
                    Template = ViewItemTemplateNames.Create.ToString(),
                    Model = result.ResponseBody,

                };
                return View(itemViewModel);
            }

            var itemViewModel1 = new AdventureWorksLT2019.MvcWebApp.Models.MvcItemViewModel<AddressDataModel>
            {
                Status = System.Net.HttpStatusCode.BadRequest,
                StatusMessage = "Bad Request",
                Template = ViewItemTemplateNames.Create.ToString(),
                Model = input, // should GetbyId again and merge content not in postback

            };
            return View(itemViewModel1);
        }
    }
}

