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
    public class SalesOrderHeaderController : Controller
    {
        private readonly ISalesOrderHeaderService _thisService;
        private readonly SelectListHelper _selectListHelper;
        private readonly ViewFeaturesManager _viewFeatureManager;
        private readonly IDropDownListService _dropDownListService;
        private readonly OrderBysListHelper _orderBysListHelper;
        private readonly MvcItemViewModelHelper _mvcItemViewModelHelper;
        private readonly PagedSearchViewModelHelper _pagedSearchViewModelHelper;
        private readonly IUIStrings _localizor;
        private readonly ILogger<SalesOrderHeaderController> _logger;

        public SalesOrderHeaderController(
            ISalesOrderHeaderService thisService,
            SelectListHelper selectListHelper,
            ViewFeaturesManager viewFeatureManager,
            IDropDownListService dropDownListService,
            OrderBysListHelper orderBysListHelper,
            MvcItemViewModelHelper mvcItemViewModelHelper,
            PagedSearchViewModelHelper pagedSearchViewModelHelper,
            IUIStrings localizor,
            ILogger<SalesOrderHeaderController> logger)
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

        // GET: SalesOrderHeader
        [HttpGet] // from query string
        [HttpPost]// form post formdata
        public async Task<IActionResult> Index(SalesOrderHeaderAdvancedQuery query, UIParams uiParams)
        {
            _viewFeatureManager.DefaultUIParamsIfNeeds(uiParams, PagedViewOptions.Table);
            // UIParams.PagedViewOption is not null here
            query.PaginationOption = _viewFeatureManager.HardCodePaginationOption(uiParams.PagedViewOption!.Value, query.PaginationOption);
            if (string.IsNullOrEmpty(query.OrderBys))
            {
                query.OrderBys = _orderBysListHelper.GetDefaultSalesOrderHeaderOrderBys();
            }

            var result = await _thisService.Search(query);

            var vm = await _pagedSearchViewModelHelper.GetSalesOrderHeaderPagedSearchViewModel("index", uiParams, query, result, true, true);
            return View(vm);
        }

        // GET: SalesOrderHeader/AjaxLoadItems
        [HttpGet] // from query string
        [HttpPost]// form post formdata
        public async Task<IActionResult> AjaxLoadItems(SalesOrderHeaderAdvancedQuery query, UIParams uiParams)
        {
            var result = await _thisService.Search(query);
            var pagedViewModel = new PagedViewModel<SalesOrderHeaderDataModel.DefaultView[]>
            {
                UIListSetting = _viewFeatureManager.GetSalesOrderHeaderUIListSetting(String.Empty, uiParams),
                Result = result,
            };

            if(uiParams.Template == ViewItemTemplateNames.Create || uiParams.Template == ViewItemTemplateNames.Edit)
            {                pagedViewModel.TopLevelDropDownListsFromDatabase = await _dropDownListService.GetSalesOrderHeaderTopLevelDropDownListsFromDatabase();
            }

            if (uiParams.PagedViewOption == PagedViewOptions.Table || uiParams.PagedViewOption == PagedViewOptions.EditableTable)
            {
                return PartialView("~/Views/SalesOrderHeader/_Table.cshtml", pagedViewModel);
            }
            else if (uiParams.PagedViewOption == PagedViewOptions.Tiles)
            {
                return PartialView("~/Views/SalesOrderHeader/_Tiles.cshtml", pagedViewModel);
            }
            //else // if (uiParams.PagedViewOption == PagedViewOptions.SlideShow)
            // SlideShow
            return PartialView("~/Views/SalesOrderHeader/_SlideShow.cshtml", pagedViewModel);

        }

        // POST: ElmahError/AjaxBulkUpdateOnlineOrderFlag
        [HttpPost, ActionName("AjaxBulkUpdateOnlineOrderFlag")]
        [Route("[controller]/[action]")]
        public async Task<IActionResult> AjaxBulkUpdateOnlineOrderFlag(
            [FromQuery] Framework.Models.PagedViewOptions view,
            [FromForm] List<SalesOrderHeaderIdentifier> ids,
            [Bind("OnlineOrderFlag")] [FromForm] SalesOrderHeaderDataModel.DefaultView data)
        {
            var result = await _thisService.BulkUpdate(
                new BatchActionViewModel<SalesOrderHeaderIdentifier, SalesOrderHeaderDataModel.DefaultView>
                {
                    Ids = ids,
                    ActionName = "OnlineOrderFlag",
                    ActionData = data
                });
            if (result.Status == System.Net.HttpStatusCode.OK)
            {
                return PartialView("~/Views/Shared/_AjaxResponse.cshtml",
                    new AjaxResponseViewModel
                    {

                        Status = System.Net.HttpStatusCode.OK, RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier,
                        PartialViews =
                            (from t in result.ResponseBody
                            select new Tuple<string, object>
                            (
                                view == PagedViewOptions.Tiles
                                    ? "~/Views/SalesOrderHeader/_Tile.cshtml"
                                    : "~/Views/SalesOrderHeader/_TableItemTr.cshtml"
                                ,
                                new AdventureWorksLT2019.MvcWebApp.Models.MvcItemViewModel<SalesOrderHeaderDataModel.DefaultView>
                                {
                                    Template = ViewItemTemplateNames.Details.ToString(),
                                    Model = t,
                                    Status = System.Net.HttpStatusCode.OK,
                                    BulkSelected = true,
                                }
                            )).ToList()
                    });
            }
            if (result.Status == System.Net.HttpStatusCode.OK)
                return PartialView("~/Views/Shared/_AjaxResponse.cshtml", new AjaxResponseViewModel { Status = System.Net.HttpStatusCode.OK, RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
            return PartialView("~/Views/Shared/_AjaxResponse.cshtml", new AjaxResponseViewModel { Status = result.Status, Message = result.StatusMessage, RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [Route("[controller]/[action]/{SalesOrderID}")] // Primary
        //[HttpGet, ActionName("Edit")]
        // GET: SalesOrderHeader/Edit/{SalesOrderID}
        public async Task<IActionResult> Edit([FromRoute]SalesOrderHeaderIdentifier id)
        {
            if (!id.SalesOrderID.HasValue)
            {
                var itemViewModel1 = new AdventureWorksLT2019.MvcWebApp.Models.MvcItemViewModel<SalesOrderHeaderDataModel.DefaultView>
                {
                    Status = System.Net.HttpStatusCode.NotFound,
                    StatusMessage = "Not Found",
                    Template = ViewItemTemplateNames.Edit.ToString(),
                };
                return View(itemViewModel1);
            }

            var result = await _thisService.Get(id);
            var topLevelDropDownListsFromDatabase = await _dropDownListService.GetSalesOrderHeaderTopLevelDropDownListsFromDatabase();

            var itemViewModel = new AdventureWorksLT2019.MvcWebApp.Models.MvcItemViewModel<SalesOrderHeaderDataModel.DefaultView>
            {
                Status = result.Status,
                StatusMessage = result.StatusMessage,
                Template = ViewItemTemplateNames.Edit.ToString(),
                TopLevelDropDownListsFromDatabase = topLevelDropDownListsFromDatabase,
                Model = result.ResponseBody
            };
            return View(itemViewModel);
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]

        [Route("[controller]/[action]/{SalesOrderID}")] // Primary
        // POST: SalesOrderHeader/Edit/{SalesOrderID}
        public async Task<IActionResult> Edit(
            [FromRoute]SalesOrderHeaderIdentifier id,
            [Bind("SalesOrderID,RevisionNumber,OrderDate,DueDate,ShipDate,Status,OnlineOrderFlag,PurchaseOrderNumber,AccountNumber,CustomerID,ShipToAddressID,BillToAddressID,ShipMethod,CreditCardApprovalCode,SubTotal,TaxAmt,Freight,Comment,ModifiedDate")] SalesOrderHeaderDataModel.DefaultView input)
        {
            var topLevelDropDownListsFromDatabase = await _dropDownListService.GetSalesOrderHeaderTopLevelDropDownListsFromDatabase();

            if (!id.SalesOrderID.HasValue ||
                id.SalesOrderID.HasValue && id.SalesOrderID != input.SalesOrderID)
            {
                var itemViewModel1 = new AdventureWorksLT2019.MvcWebApp.Models.MvcItemViewModel<SalesOrderHeaderDataModel.DefaultView>
                {
                    Status = System.Net.HttpStatusCode.NotFound,
                    StatusMessage = "Not Found",
                    Template = ViewItemTemplateNames.Edit.ToString(),
                    TopLevelDropDownListsFromDatabase = topLevelDropDownListsFromDatabase,
                    Model = input, // should GetbyId again and merge content not in postback
                };
                return View(itemViewModel1);
            }

            if (!ModelState.IsValid)
            {
                var itemViewModel1 = new AdventureWorksLT2019.MvcWebApp.Models.MvcItemViewModel<SalesOrderHeaderDataModel.DefaultView>
                {
                    Status = System.Net.HttpStatusCode.BadRequest,
                    StatusMessage = "Bad Request",
                    Template = ViewItemTemplateNames.Edit.ToString(),
                    TopLevelDropDownListsFromDatabase = topLevelDropDownListsFromDatabase,
                    Model = input, // should GetbyId again and merge content not in postback
                };
                return View(itemViewModel1);
            }

            var result = await _thisService.Update(id, input);
            var itemViewModel = new AdventureWorksLT2019.MvcWebApp.Models.MvcItemViewModel<SalesOrderHeaderDataModel.DefaultView>
            {
                Status = result.Status,
                StatusMessage = result.StatusMessage,
                Template = ViewItemTemplateNames.Edit.ToString(),
                TopLevelDropDownListsFromDatabase = topLevelDropDownListsFromDatabase,
                Model = result.ResponseBody,
            };
            return View(itemViewModel);
        }

        [Route("[controller]/[action]/{SalesOrderID}")] // Primary
        // GET: SalesOrderHeader/Details/{SalesOrderID}
        public async Task<IActionResult> Details([FromRoute]SalesOrderHeaderIdentifier id)
        {
            var result = await _thisService.Get(id);
            var itemViewModel = new AdventureWorksLT2019.MvcWebApp.Models.MvcItemViewModel<SalesOrderHeaderDataModel.DefaultView>
            {
                Status = result.Status,
                StatusMessage = result.StatusMessage,
                Template = ViewItemTemplateNames.Details.ToString(),
                Model = result.ResponseBody,
            };
            return View(itemViewModel);
        }

        // GET: SalesOrderHeader/Create
        public async Task<IActionResult> Create()
        {
                var topLevelDropDownListsFromDatabase = await _dropDownListService.GetSalesOrderHeaderTopLevelDropDownListsFromDatabase();

            var itemViewModel = await Task.FromResult(new AdventureWorksLT2019.MvcWebApp.Models.MvcItemViewModel<SalesOrderHeaderDataModel.DefaultView>
            {
                Status = System.Net.HttpStatusCode.OK,
                Template = ViewItemTemplateNames.Create.ToString(),
                Model = _thisService.GetDefault(),
                TopLevelDropDownListsFromDatabase = topLevelDropDownListsFromDatabase,
            });

                    itemViewModel.TopLevelDropDownListsFromDatabase = await _dropDownListService.GetSalesOrderHeaderTopLevelDropDownListsFromDatabase();

            return View(itemViewModel);
        }

        // POST: SalesOrderHeader/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(
            [Bind("RevisionNumber,OrderDate,DueDate,ShipDate,Status,OnlineOrderFlag,PurchaseOrderNumber,AccountNumber,CustomerID,ShipToAddressID,BillToAddressID,ShipMethod,CreditCardApprovalCode,SubTotal,TaxAmt,Freight,Comment,ModifiedDate")] SalesOrderHeaderDataModel.DefaultView input)
        {
                var topLevelDropDownListsFromDatabase = await _dropDownListService.GetSalesOrderHeaderTopLevelDropDownListsFromDatabase();

            if (ModelState.IsValid)
            {
                var result = await _thisService.Create(input);
                var itemViewModel = new AdventureWorksLT2019.MvcWebApp.Models.MvcItemViewModel<SalesOrderHeaderDataModel.DefaultView>
                {
                    Status = result.Status,
                    StatusMessage = result.StatusMessage,
                    Template = ViewItemTemplateNames.Create.ToString(),
                    Model = result.ResponseBody,
                    TopLevelDropDownListsFromDatabase = topLevelDropDownListsFromDatabase,
                };
                return View(itemViewModel);
            }

            var itemViewModel1 = new AdventureWorksLT2019.MvcWebApp.Models.MvcItemViewModel<SalesOrderHeaderDataModel.DefaultView>
            {
                Status = System.Net.HttpStatusCode.BadRequest,
                StatusMessage = "Bad Request",
                Template = ViewItemTemplateNames.Create.ToString(),
                Model = input, // should GetbyId again and merge content not in postback
                TopLevelDropDownListsFromDatabase = topLevelDropDownListsFromDatabase,
            };
            return View(itemViewModel1);
        }
    }
}

