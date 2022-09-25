using AdventureWorksLT2019.MvcWebApp.Models;
using Framework.Mvc.Models;
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
        private readonly ListSearchViewModelHelper _pagedSearchViewModelHelper;
        private readonly IUIStrings _localizor;
        private readonly ILogger<SalesOrderHeaderController> _logger;

        public SalesOrderHeaderController(
            ISalesOrderHeaderService thisService,
            SelectListHelper selectListHelper,
            ViewFeaturesManager viewFeatureManager,
            IDropDownListService dropDownListService,
            OrderBysListHelper orderBysListHelper,
            MvcItemViewModelHelper mvcItemViewModelHelper,
            ListSearchViewModelHelper pagedSearchViewModelHelper,
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
            _viewFeatureManager.DefaultUIParamsIfNeeds(uiParams, ListViewOptions.Table);
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
            var pagedViewModel = new ListViewModel<SalesOrderHeaderDataModel.DefaultView[]>
            {
                UIListSetting = _viewFeatureManager.GetSalesOrderHeaderUIListSetting(String.Empty, uiParams),
                Result = result,
            };

            if(uiParams.Template == ViewItemTemplates.Create.ToString() || uiParams.Template == ViewItemTemplates.Edit.ToString())
            {                pagedViewModel.TopLevelDropDownListsFromDatabase = await _dropDownListService.GetSalesOrderHeaderTopLevelDropDownListsFromDatabase();
            }

            if (uiParams.PagedViewOption == ListViewOptions.Table || uiParams.PagedViewOption == ListViewOptions.EditableTable)
            {
                return PartialView("~/Views/SalesOrderHeader/_Table.cshtml", pagedViewModel);
            }
            else if (uiParams.PagedViewOption == ListViewOptions.Tiles)
            {
                return PartialView("~/Views/SalesOrderHeader/_Tiles.cshtml", pagedViewModel);
            }
            //else // if (uiParams.PagedViewOption == ListViewOptions.SlideShow)
            // SlideShow
            return PartialView("~/Views/SalesOrderHeader/_SlideShow.cshtml", pagedViewModel);

        }

        [Route("[controller]/[action]/{SalesOrderID}")] // Primary
        [HttpGet, ActionName("Dashboard")]
        // GET: SalesOrderHeader/Dashboard/{SalesOrderID}
        public async Task<IActionResult> Dashboard([FromRoute]SalesOrderHeaderIdentifier id)
        {
            var listItemRequests = new Dictionary<SalesOrderHeaderCompositeModel.__DataOptions__, CompositeListItemRequest>();

            listItemRequests.Add(SalesOrderHeaderCompositeModel.__DataOptions__.SalesOrderDetails_Via_SalesOrderID,
                new CompositeListItemRequest()
                {
                    PageSize = 10,
                    OrderBys = _orderBysListHelper.GetDefaultSalesOrderDetailOrderBys(),
                    PaginationOption = PaginationOptions.PageIndexesAndAllButtons,
                });

            var result = await _thisService.GetCompositeModel(id, listItemRequests);

            result.UIParamsList.Add(
                SalesOrderHeaderCompositeModel.__DataOptions__.__Master__,
                new UIParams { PagedViewOption = ListViewOptions.Card, Template = ViewItemTemplates.Details.ToString() });

            result.UIParamsList.Add(
                SalesOrderHeaderCompositeModel.__DataOptions__.SalesOrderDetails_Via_SalesOrderID,
                new UIParams { PagedViewOption = ListViewOptions.Table, Template = ViewItemTemplates.Details.ToString() });

            return View(result);
        }

        [Route("[controller]/[action]/{SalesOrderID}")] // Primary
        // GET: SalesOrderHeader/AjaxLoadItem/{SalesOrderID}
        [HttpGet, ActionName("AjaxLoadItem")]
        public async Task<IActionResult> AjaxLoadItem(
            ListViewOptions view,
            CrudViewContainers container,
            string template,
            int? index, // for EditableList
            SalesOrderHeaderIdentifier id)
        {
            SalesOrderHeaderDataModel.DefaultView? result;
            if (template == ViewItemTemplates.Create.ToString())
            {
                result = _thisService.GetDefault();
                ViewBag.Status = System.Net.HttpStatusCode.OK;
            }
            else
            {
                var response = await _thisService.Get(id);
                result = response.ResponseBody;
            }

            var itemViewModel = new Framework.Mvc.Models.MvcItemViewModel<SalesOrderHeaderDataModel.DefaultView>
            {
                UIItemFeatures = _viewFeatureManager.GetSalesOrderHeaderUIItemFeatures(view),
                Status = System.Net.HttpStatusCode.OK,
                Template = template,
                IsCurrentItem = true,
                IndexInArray = index ?? 0,
                Model = result
            };

            // TODO: Maybe some special for Edit/Create
            if (template == ViewItemTemplates.Edit.ToString() || template == ViewItemTemplates.Create.ToString())
            {
                itemViewModel.TopLevelDropDownListsFromDatabase = await _dropDownListService.GetSalesOrderHeaderTopLevelDropDownListsFromDatabase();
            }

            if ((view == ListViewOptions.Table || view == ListViewOptions.EditableTable) && container == CrudViewContainers.Inline)
            {
                if (template == ViewItemTemplates.Create.ToString())
                {
                    return PartialView($"_TableItemTr", itemViewModel);
                }
                else
                {
                    // By Default: _Table{template}Item.cshtml
                    // Developer can customize template name
                    return PartialView($"_Table{template}Item", itemViewModel);
                }
            }
            if (view == ListViewOptions.Tiles && container == CrudViewContainers.Inline)
            {
                // By Default: _List{template}Item.cshtml
                // Developer can customize template name
                return PartialView($"_Tile{template}Item", itemViewModel);
            }
            // By Default: _{template}.cshtml
            // Developer can customize template name
            return PartialView($"_{template}", itemViewModel);
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        // POST: SalesOrderHeader/AjaxCreate
        [HttpPost, ActionName("AjaxCreate")]
        [Route("[controller]/[action]")]
        public async Task<IActionResult> AjaxCreate(
            ListViewOptions view,
            CrudViewContainers container,
            ViewItemTemplates template,
            [Bind("RevisionNumber,OrderDate,DueDate,ShipDate,Status,OnlineOrderFlag,PurchaseOrderNumber,AccountNumber,CustomerID,ShipToAddressID,BillToAddressID,ShipMethod,CreditCardApprovalCode,SubTotal,TaxAmt,Freight,Comment,ModifiedDate")] SalesOrderHeaderDataModel input)
        {
            if (ModelState.IsValid)
            {
                var result = await _thisService.Create(input);

                if (result.Status == System.Net.HttpStatusCode.OK)
                {
                    if (view == ListViewOptions.Table) // Html Table
                    {
                        return PartialView("~/Views/Shared/_AjaxResponse.cshtml",
                            new AjaxResponseViewModel
                            {
                                Status = System.Net.HttpStatusCode.OK, RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier,
                                PartialViews = new List<Tuple<string, object>> {
                                new Tuple<string, object>("~/Views/SalesOrderHeader/_TableItemTr.cshtml",
                                    new Framework.Mvc.Models.MvcItemViewModel<SalesOrderHeaderDataModel.DefaultView>{
                                        UIItemFeatures = _viewFeatureManager.GetSalesOrderHeaderUIItemFeatures(view),
                                        Status = System.Net.HttpStatusCode.OK,
                                        Template = ViewItemTemplates.Details.ToString(),
                                        IsCurrentItem = true,
                                        Model = result.ResponseBody!
                                    })
                            }});
                    }
                    //else // Tiles
                    {
                        return PartialView("~/Views/Shared/_AjaxResponse.cshtml",
                            new AjaxResponseViewModel
                            {
                                Status = System.Net.HttpStatusCode.OK,
                                RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier,
                                PartialViews = new List<Tuple<string, object>>
                                {
                                    new Tuple<string, object>("~/Views/SalesOrderHeader/_Tile.cshtml",
                                        new Framework.Mvc.Models.MvcItemViewModel<SalesOrderHeaderDataModel.DefaultView>
                                        {
                                            UIItemFeatures = _viewFeatureManager.GetSalesOrderHeaderUIItemFeatures(view),
                                            Status = System.Net.HttpStatusCode.OK,
                                            Template = ViewItemTemplates.Details.ToString(),
                                            IsCurrentItem = true,
                                            Model = result.ResponseBody!
                                        })
                                }
                            });
                    }
                }
                return PartialView("~/Views/Shared/_AjaxResponse.cshtml", new AjaxResponseViewModel { Status = result.Status, Message = result.StatusMessage, RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
            }

            return PartialView("~/Views/Shared/_AjaxResponse.cshtml", new AjaxResponseViewModel { Status = System.Net.HttpStatusCode.BadRequest, RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.

        [Route("[controller]/[action]/{SalesOrderID}")] // Primary
        [HttpPost, ActionName("AjaxEdit")]
        // POST: SalesOrderHeader/AjaxEdit/{SalesOrderID}
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> AjaxEdit(
            ListViewOptions view,
            CrudViewContainers container,
            ViewItemTemplates template,
            SalesOrderHeaderIdentifier id,
            [Bind("SalesOrderID,RevisionNumber,OrderDate,DueDate,ShipDate,Status,OnlineOrderFlag,PurchaseOrderNumber,AccountNumber,CustomerID,ShipToAddressID,BillToAddressID,ShipMethod,CreditCardApprovalCode,SubTotal,TaxAmt,Freight,Comment,ModifiedDate")] SalesOrderHeaderDataModel.DefaultView input)
        {
            if (!id.SalesOrderID.HasValue ||
                id.SalesOrderID.HasValue && id.SalesOrderID != input.SalesOrderID)
            {
                return PartialView("~/Views/Shared/_AjaxResponse.cshtml", new AjaxResponseViewModel {
                    Status = System.Net.HttpStatusCode.NotFound, RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
            }

            if (ModelState.IsValid)
            {
                var result = await _thisService.Update(id, input);
                if (result.Status == System.Net.HttpStatusCode.OK)
                {
                    if (view == ListViewOptions.Table) // Html Table
                    {
                        return PartialView("~/Views/Shared/_AjaxResponse.cshtml", new AjaxResponseViewModel
                        {
                            Status = System.Net.HttpStatusCode.OK,
                            RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier,
                            PartialViews = new List<Tuple<string, object>>
                            {
                                new Tuple<string, object>("~/Views/SalesOrderHeader/_TableDetailsItem.cshtml",
                                    new Framework.Mvc.Models.MvcItemViewModel<SalesOrderHeaderDataModel.DefaultView>
                                    {
                                        UIItemFeatures = _viewFeatureManager.GetSalesOrderHeaderUIItemFeatures(view),
                                        Status = System.Net.HttpStatusCode.OK,
                                        Template = ViewItemTemplates.Details.ToString(),
                                        IsCurrentItem = true,
                                        Model = result.ResponseBody!
                                    })
                            }
                        });
                    }
                    else // Tiles
                    {
                        return PartialView("~/Views/Shared/_AjaxResponse.cshtml", new AjaxResponseViewModel
                        {
                            Status = System.Net.HttpStatusCode.OK,
                            RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier,
                            PartialViews = new List<Tuple<string, object>>
                            {
                                new Tuple<string, object>("~/Views/SalesOrderHeader/_TileDetailsItem.cshtml",
                                    new Framework.Mvc.Models.MvcItemViewModel<SalesOrderHeaderDataModel.DefaultView>
                                    {
                                        UIItemFeatures = _viewFeatureManager.GetSalesOrderHeaderUIItemFeatures(view),
                                        Status = System.Net.HttpStatusCode.OK,
                                        Template = ViewItemTemplates.Details.ToString(),
                                        IsCurrentItem = true,
                                        Model = result.ResponseBody!
                                    })
                            }
                        });
                    }
                }
                return PartialView("~/Views/Shared/_AjaxResponse.cshtml", new AjaxResponseViewModel {
                    Status = result.Status, Message = result.StatusMessage, RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier, ShowRequestId = false });
            }

            return PartialView("~/Views/Shared/_AjaxResponse.cshtml", new AjaxResponseViewModel { Status = System.Net.HttpStatusCode.BadRequest, RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        // POST: ElmahError/AjaxBulkUpdateOnlineOrderFlag
        [HttpPost, ActionName("AjaxBulkUpdateOnlineOrderFlag")]
        [Route("[controller]/[action]")]
        public async Task<IActionResult> AjaxBulkUpdateOnlineOrderFlag(
            [FromQuery] ListViewOptions view,
            [FromForm] List<SalesOrderHeaderIdentifier> ids,
            [Bind("OnlineOrderFlag")] [FromForm] SalesOrderHeaderDataModel.DefaultView data)
        {
            var result = await _thisService.BulkUpdate(
                new BatchActionRequest<SalesOrderHeaderIdentifier, SalesOrderHeaderDataModel.DefaultView>
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
                                view == ListViewOptions.Tiles
                                    ? "~/Views/SalesOrderHeader/_Tile.cshtml"
                                    : "~/Views/SalesOrderHeader/_TableItemTr.cshtml"
                                ,
                                new Framework.Mvc.Models.MvcItemViewModel<SalesOrderHeaderDataModel.DefaultView>
                                {
                                    Template = ViewItemTemplates.Details.ToString(),
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

