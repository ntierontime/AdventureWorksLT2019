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
    public class ErrorLogController : Controller
    {
        private readonly IErrorLogService _thisService;
        private readonly SelectListHelper _selectListHelper;
        private readonly ViewFeaturesManager _viewFeatureManager;
        private readonly IDropDownListService _dropDownListService;
        private readonly OrderBysListHelper _orderBysListHelper;
        private readonly MvcItemViewModelHelper _mvcItemViewModelHelper;
        private readonly PagedSearchViewModelHelper _pagedSearchViewModelHelper;
        private readonly IUIStrings _localizor;
        private readonly ILogger<ErrorLogController> _logger;

        public ErrorLogController(
            IErrorLogService thisService,
            SelectListHelper selectListHelper,
            ViewFeaturesManager viewFeatureManager,
            IDropDownListService dropDownListService,
            OrderBysListHelper orderBysListHelper,
            MvcItemViewModelHelper mvcItemViewModelHelper,
            PagedSearchViewModelHelper pagedSearchViewModelHelper,
            IUIStrings localizor,
            ILogger<ErrorLogController> logger)
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

        // GET: ErrorLog
        [HttpGet] // from query string
        [HttpPost]// form post formdata
        public async Task<IActionResult> Index(ErrorLogAdvancedQuery query, UIParams uiParams)
        {
            _viewFeatureManager.DefaultUIParamsIfNeeds(uiParams, PagedViewOptions.Table);
            // UIParams.PagedViewOption is not null here
            query.PaginationOption = _viewFeatureManager.HardCodePaginationOption(uiParams.PagedViewOption!.Value, query.PaginationOption);
            if (string.IsNullOrEmpty(query.OrderBys))
            {
                query.OrderBys = _orderBysListHelper.GetDefaultErrorLogOrderBys();
            }

            var result = await _thisService.Search(query);

            var vm = await _pagedSearchViewModelHelper.GetErrorLogPagedSearchViewModel("index", uiParams, query, result, true, true);
            return View(vm);
        }

        // GET: ErrorLog/AjaxLoadItems
        [HttpGet] // from query string
        [HttpPost]// form post formdata
        public async Task<IActionResult> AjaxLoadItems(ErrorLogAdvancedQuery query, UIParams uiParams)
        {
            var result = await _thisService.Search(query);
            var pagedViewModel = new PagedViewModel<ErrorLogDataModel[]>
            {
                UIListSetting = _viewFeatureManager.GetErrorLogUIListSetting(String.Empty, uiParams),
                Result = result,
            };

            if(uiParams.Template == ViewItemTemplateNames.Create.ToString() || uiParams.Template == ViewItemTemplateNames.Edit.ToString())
            {
            }

            if (uiParams.PagedViewOption == PagedViewOptions.Table || uiParams.PagedViewOption == PagedViewOptions.EditableTable)
            {
                return PartialView("~/Views/ErrorLog/_Table.cshtml", pagedViewModel);
            }
            else if (uiParams.PagedViewOption == PagedViewOptions.Tiles)
            {
                return PartialView("~/Views/ErrorLog/_Tiles.cshtml", pagedViewModel);
            }
            //else // if (uiParams.PagedViewOption == PagedViewOptions.SlideShow)
            // SlideShow
            return PartialView("~/Views/ErrorLog/_SlideShow.cshtml", pagedViewModel);

        }

        [Route("[controller]/[action]/{ErrorLogID}")] // Primary
        [HttpGet, ActionName("Dashboard")]
        // GET: ErrorLog/Dashboard/{ErrorLogID}
        public async Task<IActionResult> Dashboard([FromRoute]ErrorLogIdentifier id)
        {
            var listItemRequests = new Dictionary<ErrorLogCompositeModel.__DataOptions__, CompositeListItemRequest>();

            var result = await _thisService.GetCompositeModel(id, listItemRequests);

            result.UIParamsList.Add(
                ErrorLogCompositeModel.__DataOptions__.__Master__,
                new UIParams { PagedViewOption = PagedViewOptions.Card, Template = ViewItemTemplateNames.Details.ToString() });

            return View(result);
        }

        [Route("[controller]/[action]/{ErrorLogID}")] // Primary
        // GET: ErrorLog/AjaxLoadItem/{ErrorLogID}
        [HttpGet, ActionName("AjaxLoadItem")]
        public async Task<IActionResult> AjaxLoadItem(
            PagedViewOptions view,
            CrudViewContainers container,
            string template,
            int? index, // for EditableList
            ErrorLogIdentifier id)
        {
            ErrorLogDataModel? result;
            if (template == ViewItemTemplateNames.Create.ToString())
            {
                result = _thisService.GetDefault();
                ViewBag.Status = System.Net.HttpStatusCode.OK;
            }
            else
            {
                var response = await _thisService.Get(id);
                result = response.ResponseBody;
            }

            var itemViewModel = new AdventureWorksLT2019.MvcWebApp.Models.MvcItemViewModel<ErrorLogDataModel>
            {
                UIItemFeatures = _viewFeatureManager.GetErrorLogUIItemFeatures(),
                Status = System.Net.HttpStatusCode.OK,
                Template = template,
                IsCurrentItem = true,
                IndexInArray = index ?? 0,
                Model = result
            };

            // TODO: Maybe some special for Edit/Create
            if (template == ViewItemTemplateNames.Edit.ToString() || template == ViewItemTemplateNames.Create.ToString())
            {

            }

            if ((view == PagedViewOptions.Table || view == PagedViewOptions.EditableTable) && container == CrudViewContainers.Inline)
            {
                if (template == ViewItemTemplateNames.Create.ToString())
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
            if (view == PagedViewOptions.Tiles && container == CrudViewContainers.Inline)
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
        // POST: ErrorLog/AjaxCreate
        [HttpPost, ActionName("AjaxCreate")]
        [Route("[controller]/[action]")]
        public async Task<IActionResult> AjaxCreate(
            PagedViewOptions view,
            CrudViewContainers container,
            ViewItemTemplateNames template,
            [Bind("ErrorTime,UserName,ErrorNumber,ErrorSeverity,ErrorState,ErrorProcedure,ErrorLine,ErrorMessage")] ErrorLogDataModel input)
        {
            if (ModelState.IsValid)
            {
                var result = await _thisService.Create(input);

                if (result.Status == System.Net.HttpStatusCode.OK)
                {
                    if (view == PagedViewOptions.Table) // Html Table
                    {
                        return PartialView("~/Views/Shared/_AjaxResponse.cshtml",
                            new AjaxResponseViewModel
                            {
                                Status = System.Net.HttpStatusCode.OK, RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier,
                                PartialViews = new List<Tuple<string, object>> {
                                new Tuple<string, object>("~/Views/ErrorLog/_TableItemTr.cshtml",
                                    new AdventureWorksLT2019.MvcWebApp.Models.MvcItemViewModel<ErrorLogDataModel>{
                                        UIItemFeatures = _viewFeatureManager.GetErrorLogUIItemFeatures(),
                                        Status = System.Net.HttpStatusCode.OK,
                                        Template = ViewItemTemplateNames.Details.ToString(),
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
                                    new Tuple<string, object>("~/Views/ErrorLog/_Tile.cshtml",
                                        new AdventureWorksLT2019.MvcWebApp.Models.MvcItemViewModel<ErrorLogDataModel>
                                        {
                                            UIItemFeatures = _viewFeatureManager.GetErrorLogUIItemFeatures(),
                                            Status = System.Net.HttpStatusCode.OK,
                                            Template = ViewItemTemplateNames.Details.ToString(),
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

        [Route("[controller]/[action]/{ErrorLogID}")] // Primary
        [HttpPost, ActionName("AjaxDelete")]
        // POST: ErrorLog/AjaxDelete/{ErrorLogID}
        public async Task<IActionResult> AjaxDelete(
            PagedViewOptions view,
            CrudViewContainers container,
            ViewItemTemplateNames template,
            [FromRoute] ErrorLogIdentifier id)
        {
            var result = await _thisService.Delete(id);
            if (result.Status == System.Net.HttpStatusCode.OK)
                return PartialView("~/Views/Shared/_AjaxResponse.cshtml", new AjaxResponseViewModel { Status = System.Net.HttpStatusCode.OK, RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
            return PartialView("~/Views/Shared/_AjaxResponse.cshtml", new AjaxResponseViewModel { Status = result.Status, Message = result.StatusMessage, RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.

        [Route("[controller]/[action]/{ErrorLogID}")] // Primary
        [HttpPost, ActionName("AjaxEdit")]
        // POST: ErrorLog/AjaxEdit/{ErrorLogID}
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> AjaxEdit(
            PagedViewOptions view,
            CrudViewContainers container,
            ViewItemTemplateNames template,
            ErrorLogIdentifier id,
            [Bind("ErrorLogID,ErrorTime,UserName,ErrorNumber,ErrorSeverity,ErrorState,ErrorProcedure,ErrorLine,ErrorMessage")] ErrorLogDataModel input)
        {
            if (!id.ErrorLogID.HasValue ||
                id.ErrorLogID.HasValue && id.ErrorLogID != input.ErrorLogID)
            {
                return PartialView("~/Views/Shared/_AjaxResponse.cshtml", new AjaxResponseViewModel {
                    Status = System.Net.HttpStatusCode.NotFound, RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
            }

            if (ModelState.IsValid)
            {
                var result = await _thisService.Update(id, input);
                if (result.Status == System.Net.HttpStatusCode.OK)
                {
                    if (view == PagedViewOptions.Table) // Html Table
                    {
                        return PartialView("~/Views/Shared/_AjaxResponse.cshtml", new AjaxResponseViewModel
                        {
                            Status = System.Net.HttpStatusCode.OK,
                            RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier,
                            PartialViews = new List<Tuple<string, object>>
                            {
                                new Tuple<string, object>("~/Views/ErrorLog/_TableDetailsItem.cshtml",
                                    new AdventureWorksLT2019.MvcWebApp.Models.MvcItemViewModel<ErrorLogDataModel>
                                    {
                                        UIItemFeatures = _viewFeatureManager.GetErrorLogUIItemFeatures(),
                                        Status = System.Net.HttpStatusCode.OK,
                                        Template = ViewItemTemplateNames.Details.ToString(),
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
                                new Tuple<string, object>("~/Views/ErrorLog/_TileDetailsItem.cshtml",
                                    new AdventureWorksLT2019.MvcWebApp.Models.MvcItemViewModel<ErrorLogDataModel>
                                    {
                                        UIItemFeatures = _viewFeatureManager.GetErrorLogUIItemFeatures(),
                                        Status = System.Net.HttpStatusCode.OK,
                                        Template = ViewItemTemplateNames.Details.ToString(),
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

        // POST: ErrorLog//AjaxBulkDelete
        [HttpPost, ActionName("AjaxBulkDelete")]
        [Route("[controller]/[action]")]
        public async Task<IActionResult> AjaxBulkDelete(
            [FromForm] BatchActionViewModel<ErrorLogIdentifier> data)
        {
            var result = await _thisService.BulkDelete(data.Ids);
            if (result.Status == System.Net.HttpStatusCode.OK)
                return PartialView("~/Views/Shared/_AjaxResponse.cshtml", new AjaxResponseViewModel { Status = System.Net.HttpStatusCode.OK, RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
            return PartialView("~/Views/Shared/_AjaxResponse.cshtml", new AjaxResponseViewModel { Status = result.Status, Message = result.StatusMessage, RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        // POST: ErrorLog/AjaxMultiItemsCUDSubmit
        [HttpPost, ActionName("AjaxMultiItemsCUDSubmit")]
        [Route("[controller]/[action]")]
        public async Task<IActionResult> AjaxMultiItemsCUDSubmit(
            [FromQuery] PagedViewOptions view,
            [FromForm] List<ErrorLogDataModel> data)
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

            var multiItemsCUDModel = new MultiItemsCUDModel<ErrorLogIdentifier, ErrorLogDataModel>
            {
                DeleteItems =
                    (from t in data
                    where t.IsDeleted______ && t.ItemUIStatus______ != ItemUIStatus.New
                    select new ErrorLogIdentifier { ErrorLogID = t.ErrorLogID }).ToList(),
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

        [Route("[controller]/[action]/{ErrorLogID}")] // Primary
        //[HttpGet, ActionName("Edit")]
        // GET: ErrorLog/Edit/{ErrorLogID}
        public async Task<IActionResult> Edit([FromRoute]ErrorLogIdentifier id)
        {
            if (!id.ErrorLogID.HasValue)
            {
                var itemViewModel1 = new AdventureWorksLT2019.MvcWebApp.Models.MvcItemViewModel<ErrorLogDataModel>
                {
                    Status = System.Net.HttpStatusCode.NotFound,
                    StatusMessage = "Not Found",
                    Template = ViewItemTemplateNames.Edit.ToString(),
                };
                return View(itemViewModel1);
            }

            var result = await _thisService.Get(id);
            var itemViewModel = new AdventureWorksLT2019.MvcWebApp.Models.MvcItemViewModel<ErrorLogDataModel>
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

        [Route("[controller]/[action]/{ErrorLogID}")] // Primary
        // POST: ErrorLog/Edit/{ErrorLogID}
        public async Task<IActionResult> Edit(
            [FromRoute]ErrorLogIdentifier id,
            [Bind("ErrorLogID,ErrorTime,UserName,ErrorNumber,ErrorSeverity,ErrorState,ErrorProcedure,ErrorLine,ErrorMessage")] ErrorLogDataModel input)
        {
            if (!id.ErrorLogID.HasValue ||
                id.ErrorLogID.HasValue && id.ErrorLogID != input.ErrorLogID)
            {
                var itemViewModel1 = new AdventureWorksLT2019.MvcWebApp.Models.MvcItemViewModel<ErrorLogDataModel>
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
                var itemViewModel1 = new AdventureWorksLT2019.MvcWebApp.Models.MvcItemViewModel<ErrorLogDataModel>
                {
                    Status = System.Net.HttpStatusCode.BadRequest,
                    StatusMessage = "Bad Request",
                    Template = ViewItemTemplateNames.Edit.ToString(),

                    Model = input, // should GetbyId again and merge content not in postback
                };
                return View(itemViewModel1);
            }

            var result = await _thisService.Update(id, input);
            var itemViewModel = new AdventureWorksLT2019.MvcWebApp.Models.MvcItemViewModel<ErrorLogDataModel>
            {
                Status = result.Status,
                StatusMessage = result.StatusMessage,
                Template = ViewItemTemplateNames.Edit.ToString(),

                Model = result.ResponseBody,
            };
            return View(itemViewModel);
        }

        [Route("[controller]/[action]/{ErrorLogID}")] // Primary
        // GET: ErrorLog/Details/{ErrorLogID}
        public async Task<IActionResult> Details([FromRoute]ErrorLogIdentifier id)
        {
            var result = await _thisService.Get(id);
            var itemViewModel = new AdventureWorksLT2019.MvcWebApp.Models.MvcItemViewModel<ErrorLogDataModel>
            {
                Status = result.Status,
                StatusMessage = result.StatusMessage,
                Template = ViewItemTemplateNames.Details.ToString(),
                Model = result.ResponseBody,
            };
            return View(itemViewModel);
        }

        // GET: ErrorLog/Create
        public async Task<IActionResult> Create()
        {
            var itemViewModel = await Task.FromResult(new AdventureWorksLT2019.MvcWebApp.Models.MvcItemViewModel<ErrorLogDataModel>
            {
                Status = System.Net.HttpStatusCode.OK,
                Template = ViewItemTemplateNames.Create.ToString(),
                Model = _thisService.GetDefault(),

            });

            return View(itemViewModel);
        }

        // POST: ErrorLog/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(
            [Bind("ErrorTime,UserName,ErrorNumber,ErrorSeverity,ErrorState,ErrorProcedure,ErrorLine,ErrorMessage")] ErrorLogDataModel input)
        {
            if (ModelState.IsValid)
            {
                var result = await _thisService.Create(input);
                var itemViewModel = new AdventureWorksLT2019.MvcWebApp.Models.MvcItemViewModel<ErrorLogDataModel>
                {
                    Status = result.Status,
                    StatusMessage = result.StatusMessage,
                    Template = ViewItemTemplateNames.Create.ToString(),
                    Model = result.ResponseBody,

                };
                return View(itemViewModel);
            }

            var itemViewModel1 = new AdventureWorksLT2019.MvcWebApp.Models.MvcItemViewModel<ErrorLogDataModel>
            {
                Status = System.Net.HttpStatusCode.BadRequest,
                StatusMessage = "Bad Request",
                Template = ViewItemTemplateNames.Create.ToString(),
                Model = input, // should GetbyId again and merge content not in postback

            };
            return View(itemViewModel1);
        }

        [Route("[controller]/[action]/{ErrorLogID}")] // Primary
        // GET: ErrorLog/Delete/{ErrorLogID}
        public async Task<IActionResult> Delete([FromRoute]ErrorLogIdentifier id)
        {
            var result = await _thisService.Get(id);
            var itemViewModel = new AdventureWorksLT2019.MvcWebApp.Models.MvcItemViewModel<ErrorLogDataModel>
            {
                Status = result.Status,
                StatusMessage = result.StatusMessage,
                Template = ViewItemTemplateNames.Delete.ToString(),
                Model = result.ResponseBody,
            };
            return View(itemViewModel);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]

        [Route("[controller]/[action]/{ErrorLogID}")] // Primary
        // POST: ErrorLog/Delete/{ErrorLogID}
        public async Task<IActionResult> DeleteConfirmed([FromRoute]ErrorLogIdentifier id)
        {
            var result1 = await _thisService.Get(id);
            var result = await _thisService.Delete(id);
            var itemViewModel = new AdventureWorksLT2019.MvcWebApp.Models.MvcItemViewModel<ErrorLogDataModel>
            {
                Status = result.Status,
                StatusMessage = result.StatusMessage,
                Template = ViewItemTemplateNames.Delete.ToString(),
                Model = result1.ResponseBody,
            };
            return View(itemViewModel);
        }
    }
}

