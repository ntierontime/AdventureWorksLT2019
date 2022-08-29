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
    public class SalesOrderDetailController : Controller
    {
        private readonly ISalesOrderDetailService _thisService;
        private readonly SelectListHelper _selectListHelper;
        private readonly ViewFeaturesManager _viewFeatureManager;
        private readonly IDropDownListService _dropDownListService;
        private readonly OrderBysListHelper _orderBysListHelper;
        private readonly MvcItemViewModelHelper _mvcItemViewModelHelper;
        private readonly ListSearchViewModelHelper _pagedSearchViewModelHelper;
        private readonly IUIStrings _localizor;
        private readonly ILogger<SalesOrderDetailController> _logger;

        public SalesOrderDetailController(
            ISalesOrderDetailService thisService,
            SelectListHelper selectListHelper,
            ViewFeaturesManager viewFeatureManager,
            IDropDownListService dropDownListService,
            OrderBysListHelper orderBysListHelper,
            MvcItemViewModelHelper mvcItemViewModelHelper,
            ListSearchViewModelHelper pagedSearchViewModelHelper,
            IUIStrings localizor,
            ILogger<SalesOrderDetailController> logger)
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

        // GET: SalesOrderDetail
        [HttpGet] // from query string
        [HttpPost]// form post formdata
        public async Task<IActionResult> Index(SalesOrderDetailAdvancedQuery query, UIParams uiParams)
        {
            _viewFeatureManager.DefaultUIParamsIfNeeds(uiParams, ListViewOptions.Table);
            // UIParams.PagedViewOption is not null here
            query.PaginationOption = _viewFeatureManager.HardCodePaginationOption(uiParams.PagedViewOption!.Value, query.PaginationOption);
            if (string.IsNullOrEmpty(query.OrderBys))
            {
                query.OrderBys = _orderBysListHelper.GetDefaultSalesOrderDetailOrderBys();
            }

            var result = await _thisService.Search(query);

            var vm = await _pagedSearchViewModelHelper.GetSalesOrderDetailPagedSearchViewModel("index", uiParams, query, result, true, true);
            return View(vm);
        }

        // GET: SalesOrderDetail/AjaxLoadItems
        [HttpGet] // from query string
        [HttpPost]// form post formdata
        public async Task<IActionResult> AjaxLoadItems(SalesOrderDetailAdvancedQuery query, UIParams uiParams)
        {
            var result = await _thisService.Search(query);
            var pagedViewModel = new ListViewModel<SalesOrderDetailDataModel.DefaultView[]>
            {
                UIListSetting = _viewFeatureManager.GetSalesOrderDetailUIListSetting(String.Empty, uiParams),
                Result = result,
            };

            if(uiParams.Template == ViewItemTemplates.Create.ToString() || uiParams.Template == ViewItemTemplates.Edit.ToString())
            {                pagedViewModel.TopLevelDropDownListsFromDatabase = await _dropDownListService.GetSalesOrderDetailTopLevelDropDownListsFromDatabase();
            }

            if (uiParams.PagedViewOption == ListViewOptions.Table || uiParams.PagedViewOption == ListViewOptions.EditableTable)
            {
                return PartialView("~/Views/SalesOrderDetail/_Table.cshtml", pagedViewModel);
            }
            else if (uiParams.PagedViewOption == ListViewOptions.Tiles)
            {
                return PartialView("~/Views/SalesOrderDetail/_Tiles.cshtml", pagedViewModel);
            }
            //else // if (uiParams.PagedViewOption == ListViewOptions.SlideShow)
            // SlideShow
            return PartialView("~/Views/SalesOrderDetail/_SlideShow.cshtml", pagedViewModel);

        }

        [Route("[controller]/[action]/{SalesOrderID}/{SalesOrderDetailID}")] // Primary
        [HttpGet, ActionName("Dashboard")]
        // GET: SalesOrderDetail/Dashboard/{SalesOrderID}/{SalesOrderDetailID}
        public async Task<IActionResult> Dashboard([FromRoute]SalesOrderDetailIdentifier id)
        {
            var listItemRequests = new Dictionary<SalesOrderDetailCompositeModel.__DataOptions__, CompositeListItemRequest>();

            var result = await _thisService.GetCompositeModel(id, listItemRequests);

            result.UIParamsList.Add(
                SalesOrderDetailCompositeModel.__DataOptions__.__Master__,
                new UIParams { PagedViewOption = ListViewOptions.Card, Template = ViewItemTemplates.Details.ToString() });

            result.UIParamsList.Add(
                SalesOrderDetailCompositeModel.__DataOptions__.Product,
                new UIParams { PagedViewOption = ListViewOptions.Card, Template = ViewItemTemplates.Details.ToString() });

            result.UIParamsList.Add(
                SalesOrderDetailCompositeModel.__DataOptions__.ProductCategory,
                new UIParams { PagedViewOption = ListViewOptions.Card, Template = ViewItemTemplates.Details.ToString() });

            result.UIParamsList.Add(
                SalesOrderDetailCompositeModel.__DataOptions__.SalesOrderHeader,
                new UIParams { PagedViewOption = ListViewOptions.Card, Template = ViewItemTemplates.Details.ToString() });

            return View(result);
        }

        [Route("[controller]/[action]/{SalesOrderID}/{SalesOrderDetailID}")] // Primary
        // GET: SalesOrderDetail/AjaxLoadItem/{SalesOrderID}/{SalesOrderDetailID}
        [HttpGet, ActionName("AjaxLoadItem")]
        public async Task<IActionResult> AjaxLoadItem(
            ListViewOptions view,
            CrudViewContainers container,
            string template,
            int? index, // for EditableList
            SalesOrderDetailIdentifier id)
        {
            SalesOrderDetailDataModel.DefaultView? result;
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

            var itemViewModel = new Framework.Mvc.Models.MvcItemViewModel<SalesOrderDetailDataModel.DefaultView>
            {
                UIItemFeatures = _viewFeatureManager.GetSalesOrderDetailUIItemFeatures(),
                Status = System.Net.HttpStatusCode.OK,
                Template = template,
                IsCurrentItem = true,
                IndexInArray = index ?? 0,
                Model = result
            };

            // TODO: Maybe some special for Edit/Create
            if (template == ViewItemTemplates.Edit.ToString() || template == ViewItemTemplates.Create.ToString())
            {
                itemViewModel.TopLevelDropDownListsFromDatabase = await _dropDownListService.GetSalesOrderDetailTopLevelDropDownListsFromDatabase();
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
        // POST: SalesOrderDetail/AjaxCreate
        [HttpPost, ActionName("AjaxCreate")]
        [Route("[controller]/[action]")]
        public async Task<IActionResult> AjaxCreate(
            ListViewOptions view,
            CrudViewContainers container,
            ViewItemTemplates template,
            [Bind("SalesOrderID,OrderQty,ProductID,UnitPrice,UnitPriceDiscount,ModifiedDate")] SalesOrderDetailDataModel input)
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
                                new Tuple<string, object>("~/Views/SalesOrderDetail/_TableItemTr.cshtml",
                                    new Framework.Mvc.Models.MvcItemViewModel<SalesOrderDetailDataModel.DefaultView>{
                                        UIItemFeatures = _viewFeatureManager.GetSalesOrderDetailUIItemFeatures(),
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
                                    new Tuple<string, object>("~/Views/SalesOrderDetail/_Tile.cshtml",
                                        new Framework.Mvc.Models.MvcItemViewModel<SalesOrderDetailDataModel.DefaultView>
                                        {
                                            UIItemFeatures = _viewFeatureManager.GetSalesOrderDetailUIItemFeatures(),
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

        [Route("[controller]/[action]/{SalesOrderID}/{SalesOrderDetailID}")] // Primary
        [HttpPost, ActionName("AjaxDelete")]
        // POST: SalesOrderDetail/AjaxDelete/{SalesOrderID}/{SalesOrderDetailID}
        public async Task<IActionResult> AjaxDelete(
            ListViewOptions view,
            CrudViewContainers container,
            ViewItemTemplates template,
            [FromRoute] SalesOrderDetailIdentifier id)
        {
            var result = await _thisService.Delete(id);
            if (result.Status == System.Net.HttpStatusCode.OK)
                return PartialView("~/Views/Shared/_AjaxResponse.cshtml", new AjaxResponseViewModel { Status = System.Net.HttpStatusCode.OK, RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
            return PartialView("~/Views/Shared/_AjaxResponse.cshtml", new AjaxResponseViewModel { Status = result.Status, Message = result.StatusMessage, RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.

        [Route("[controller]/[action]/{SalesOrderID}/{SalesOrderDetailID}")] // Primary
        [HttpPost, ActionName("AjaxEdit")]
        // POST: SalesOrderDetail/AjaxEdit/{SalesOrderID}/{SalesOrderDetailID}
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> AjaxEdit(
            ListViewOptions view,
            CrudViewContainers container,
            ViewItemTemplates template,
            SalesOrderDetailIdentifier id,
            [Bind("SalesOrderID,SalesOrderDetailID,OrderQty,ProductID,UnitPrice,UnitPriceDiscount,ModifiedDate")] SalesOrderDetailDataModel.DefaultView input)
        {
            if (!id.SalesOrderID.HasValue && !id.SalesOrderDetailID.HasValue ||
                id.SalesOrderID.HasValue && id.SalesOrderID != input.SalesOrderID || id.SalesOrderDetailID.HasValue && id.SalesOrderDetailID != input.SalesOrderDetailID)
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
                                new Tuple<string, object>("~/Views/SalesOrderDetail/_TableDetailsItem.cshtml",
                                    new Framework.Mvc.Models.MvcItemViewModel<SalesOrderDetailDataModel.DefaultView>
                                    {
                                        UIItemFeatures = _viewFeatureManager.GetSalesOrderDetailUIItemFeatures(),
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
                                new Tuple<string, object>("~/Views/SalesOrderDetail/_TileDetailsItem.cshtml",
                                    new Framework.Mvc.Models.MvcItemViewModel<SalesOrderDetailDataModel.DefaultView>
                                    {
                                        UIItemFeatures = _viewFeatureManager.GetSalesOrderDetailUIItemFeatures(),
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

        // POST: SalesOrderDetail//AjaxBulkDelete
        [HttpPost, ActionName("AjaxBulkDelete")]
        [Route("[controller]/[action]")]
        public async Task<IActionResult> AjaxBulkDelete(
            [FromForm] BatchActionRequest<SalesOrderDetailIdentifier> data)
        {
            var result = await _thisService.BulkDelete(data.Ids);
            if (result.Status == System.Net.HttpStatusCode.OK)
                return PartialView("~/Views/Shared/_AjaxResponse.cshtml", new AjaxResponseViewModel { Status = System.Net.HttpStatusCode.OK, RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
            return PartialView("~/Views/Shared/_AjaxResponse.cshtml", new AjaxResponseViewModel { Status = result.Status, Message = result.StatusMessage, RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        // POST: SalesOrderDetail/AjaxMultiItemsCUDSubmit
        [HttpPost, ActionName("AjaxMultiItemsCUDSubmit")]
        [Route("[controller]/[action]")]
        public async Task<IActionResult> AjaxMultiItemsCUDSubmit(
            [FromQuery] ListViewOptions view,
            [FromForm] List<SalesOrderDetailDataModel.DefaultView> data)
        {
            if(data == null || !data.Any(t=> t.IsDeleted______ && t.ItemUIStatus______ != ItemUIStatus.New || !t.IsDeleted______ && t.ItemUIStatus______ == ItemUIStatus.New || !t.IsDeleted______ && t.ItemUIStatus______ == ItemUIStatus.Updated))
            {
                return PartialView("~/Views/Shared/_AjaxResponse.cshtml",
                    new AjaxResponseViewModel
                    {
                        Status = System.Net.HttpStatusCode.NoContent,
                        Message = "NoContent",
                        RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier
                    });
            }

            var multiItemsCUDModel = new MultiItemsCUDRequest<SalesOrderDetailIdentifier, SalesOrderDetailDataModel.DefaultView>
            {
                DeleteItems =
                    (from t in data
                    where t.IsDeleted______ && t.ItemUIStatus______ != ItemUIStatus.New
                    select new SalesOrderDetailIdentifier { SalesOrderID = t.SalesOrderID }).ToList(),
                NewItems =
                    (from t in data
                     where !t.IsDeleted______ && t.ItemUIStatus______ == ItemUIStatus.New
                     select t).ToList(),
                UpdateItems =
                    (from t in data
                     where !t.IsDeleted______ && t.ItemUIStatus______ == ItemUIStatus.Updated
                     select t).ToList(),
            };

            // although we have the NewItems and UpdatedITems in result, but we have to Mvc Core JQuery/Ajax refresh the whole list because array binding.
            var result = await _thisService.MultiItemsCUD(multiItemsCUDModel);

            return PartialView("~/Views/Shared/_AjaxResponse.cshtml",
                new AjaxResponseViewModel
                {
                    Status = result.Status,
                    ShowMessage = result.Status == System.Net.HttpStatusCode.OK,
                    Message = result.Status == System.Net.HttpStatusCode.OK ? _localizor.Get("Click Close To Reload this List") : result.StatusMessage,
                    RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier
                });
        }

        [Route("[controller]/[action]/{SalesOrderID}/{SalesOrderDetailID}")] // Primary
        //[HttpGet, ActionName("Edit")]
        // GET: SalesOrderDetail/Edit/{SalesOrderID}/{SalesOrderDetailID}
        public async Task<IActionResult> Edit([FromRoute]SalesOrderDetailIdentifier id)
        {
            if (!id.SalesOrderID.HasValue && !id.SalesOrderDetailID.HasValue)
            {
                var itemViewModel1 = new Framework.Mvc.Models.MvcItemViewModel<SalesOrderDetailDataModel.DefaultView>
                {
                    Status = System.Net.HttpStatusCode.NotFound,
                    StatusMessage = "Not Found",
                    Template = ViewItemTemplates.Edit.ToString(),
                };
                return View(itemViewModel1);
            }

            var result = await _thisService.Get(id);
            var topLevelDropDownListsFromDatabase = await _dropDownListService.GetSalesOrderDetailTopLevelDropDownListsFromDatabase();

            var itemViewModel = new Framework.Mvc.Models.MvcItemViewModel<SalesOrderDetailDataModel.DefaultView>
            {
                Status = result.Status,
                StatusMessage = result.StatusMessage,
                Template = ViewItemTemplates.Edit.ToString(),
                TopLevelDropDownListsFromDatabase = topLevelDropDownListsFromDatabase,
                Model = result.ResponseBody
            };
            return View(itemViewModel);
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]

        [Route("[controller]/[action]/{SalesOrderID}/{SalesOrderDetailID}")] // Primary
        // POST: SalesOrderDetail/Edit/{SalesOrderID}/{SalesOrderDetailID}
        public async Task<IActionResult> Edit(
            [FromRoute]SalesOrderDetailIdentifier id,
            [Bind("SalesOrderID,SalesOrderDetailID,OrderQty,ProductID,UnitPrice,UnitPriceDiscount,ModifiedDate")] SalesOrderDetailDataModel.DefaultView input)
        {
            var topLevelDropDownListsFromDatabase = await _dropDownListService.GetSalesOrderDetailTopLevelDropDownListsFromDatabase();

            if (!id.SalesOrderID.HasValue && !id.SalesOrderDetailID.HasValue ||
                id.SalesOrderID.HasValue && id.SalesOrderID != input.SalesOrderID || id.SalesOrderDetailID.HasValue && id.SalesOrderDetailID != input.SalesOrderDetailID)
            {
                var itemViewModel1 = new Framework.Mvc.Models.MvcItemViewModel<SalesOrderDetailDataModel.DefaultView>
                {
                    Status = System.Net.HttpStatusCode.NotFound,
                    StatusMessage = "Not Found",
                    Template = ViewItemTemplates.Edit.ToString(),
                    TopLevelDropDownListsFromDatabase = topLevelDropDownListsFromDatabase,
                    Model = input, // should GetbyId again and merge content not in postback
                };
                return View(itemViewModel1);
            }

            if (!ModelState.IsValid)
            {
                var itemViewModel1 = new Framework.Mvc.Models.MvcItemViewModel<SalesOrderDetailDataModel.DefaultView>
                {
                    Status = System.Net.HttpStatusCode.BadRequest,
                    StatusMessage = "Bad Request",
                    Template = ViewItemTemplates.Edit.ToString(),
                    TopLevelDropDownListsFromDatabase = topLevelDropDownListsFromDatabase,
                    Model = input, // should GetbyId again and merge content not in postback
                };
                return View(itemViewModel1);
            }

            var result = await _thisService.Update(id, input);
            var itemViewModel = new Framework.Mvc.Models.MvcItemViewModel<SalesOrderDetailDataModel.DefaultView>
            {
                Status = result.Status,
                StatusMessage = result.StatusMessage,
                Template = ViewItemTemplates.Edit.ToString(),
                TopLevelDropDownListsFromDatabase = topLevelDropDownListsFromDatabase,
                Model = result.ResponseBody,
            };
            return View(itemViewModel);
        }

        [Route("[controller]/[action]/{SalesOrderID}/{SalesOrderDetailID}")] // Primary
        // GET: SalesOrderDetail/Details/{SalesOrderID}/{SalesOrderDetailID}
        public async Task<IActionResult> Details([FromRoute]SalesOrderDetailIdentifier id)
        {
            var result = await _thisService.Get(id);
            var itemViewModel = new Framework.Mvc.Models.MvcItemViewModel<SalesOrderDetailDataModel.DefaultView>
            {
                Status = result.Status,
                StatusMessage = result.StatusMessage,
                Template = ViewItemTemplates.Details.ToString(),
                Model = result.ResponseBody,
            };
            return View(itemViewModel);
        }

        // GET: SalesOrderDetail/Create
        public async Task<IActionResult> Create()
        {
                var topLevelDropDownListsFromDatabase = await _dropDownListService.GetSalesOrderDetailTopLevelDropDownListsFromDatabase();

            var itemViewModel = await Task.FromResult(new Framework.Mvc.Models.MvcItemViewModel<SalesOrderDetailDataModel.DefaultView>
            {
                Status = System.Net.HttpStatusCode.OK,
                Template = ViewItemTemplates.Create.ToString(),
                Model = _thisService.GetDefault(),
                TopLevelDropDownListsFromDatabase = topLevelDropDownListsFromDatabase,
            });

                    itemViewModel.TopLevelDropDownListsFromDatabase = await _dropDownListService.GetSalesOrderDetailTopLevelDropDownListsFromDatabase();

            return View(itemViewModel);
        }

        // POST: SalesOrderDetail/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(
            [Bind("SalesOrderID,OrderQty,ProductID,UnitPrice,UnitPriceDiscount,ModifiedDate")] SalesOrderDetailDataModel.DefaultView input)
        {
                var topLevelDropDownListsFromDatabase = await _dropDownListService.GetSalesOrderDetailTopLevelDropDownListsFromDatabase();

            if (ModelState.IsValid)
            {
                var result = await _thisService.Create(input);
                var itemViewModel = new Framework.Mvc.Models.MvcItemViewModel<SalesOrderDetailDataModel.DefaultView>
                {
                    Status = result.Status,
                    StatusMessage = result.StatusMessage,
                    Template = ViewItemTemplates.Create.ToString(),
                    Model = result.ResponseBody,
                    TopLevelDropDownListsFromDatabase = topLevelDropDownListsFromDatabase,
                };
                return View(itemViewModel);
            }

            var itemViewModel1 = new Framework.Mvc.Models.MvcItemViewModel<SalesOrderDetailDataModel.DefaultView>
            {
                Status = System.Net.HttpStatusCode.BadRequest,
                StatusMessage = "Bad Request",
                Template = ViewItemTemplates.Create.ToString(),
                Model = input, // should GetbyId again and merge content not in postback
                TopLevelDropDownListsFromDatabase = topLevelDropDownListsFromDatabase,
            };
            return View(itemViewModel1);
        }

        [Route("[controller]/[action]/{SalesOrderID}/{SalesOrderDetailID}")] // Primary
        // GET: SalesOrderDetail/Delete/{SalesOrderID}/{SalesOrderDetailID}
        public async Task<IActionResult> Delete([FromRoute]SalesOrderDetailIdentifier id)
        {
            var result = await _thisService.Get(id);
            var itemViewModel = new Framework.Mvc.Models.MvcItemViewModel<SalesOrderDetailDataModel.DefaultView>
            {
                Status = result.Status,
                StatusMessage = result.StatusMessage,
                Template = ViewItemTemplates.Delete.ToString(),
                Model = result.ResponseBody,
            };
            return View(itemViewModel);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]

        [Route("[controller]/[action]/{SalesOrderID}/{SalesOrderDetailID}")] // Primary
        // POST: SalesOrderDetail/Delete/{SalesOrderID}/{SalesOrderDetailID}
        public async Task<IActionResult> DeleteConfirmed([FromRoute]SalesOrderDetailIdentifier id)
        {
            var result1 = await _thisService.Get(id);
            var result = await _thisService.Delete(id);
            var itemViewModel = new Framework.Mvc.Models.MvcItemViewModel<SalesOrderDetailDataModel.DefaultView>
            {
                Status = result.Status,
                StatusMessage = result.StatusMessage,
                Template = ViewItemTemplates.Delete.ToString(),
                Model = result1.ResponseBody,
            };
            return View(itemViewModel);
        }
    }
}

