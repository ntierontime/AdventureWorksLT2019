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
    }
}

