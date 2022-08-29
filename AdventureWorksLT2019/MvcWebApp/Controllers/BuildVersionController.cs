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
    public class BuildVersionController : Controller
    {
        private readonly IBuildVersionService _thisService;
        private readonly SelectListHelper _selectListHelper;
        private readonly ViewFeaturesManager _viewFeatureManager;
        private readonly IDropDownListService _dropDownListService;
        private readonly OrderBysListHelper _orderBysListHelper;
        private readonly MvcItemViewModelHelper _mvcItemViewModelHelper;
        private readonly ListSearchViewModelHelper _pagedSearchViewModelHelper;
        private readonly IUIStrings _localizor;
        private readonly ILogger<BuildVersionController> _logger;

        public BuildVersionController(
            IBuildVersionService thisService,
            SelectListHelper selectListHelper,
            ViewFeaturesManager viewFeatureManager,
            IDropDownListService dropDownListService,
            OrderBysListHelper orderBysListHelper,
            MvcItemViewModelHelper mvcItemViewModelHelper,
            ListSearchViewModelHelper pagedSearchViewModelHelper,
            IUIStrings localizor,
            ILogger<BuildVersionController> logger)
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

        // GET: BuildVersion
        [HttpGet] // from query string
        [HttpPost]// form post formdata
        public async Task<IActionResult> Index(BuildVersionAdvancedQuery query, UIParams uiParams)
        {
            _viewFeatureManager.DefaultUIParamsIfNeeds(uiParams, ListViewOptions.Table);
            // UIParams.PagedViewOption is not null here
            query.PaginationOption = _viewFeatureManager.HardCodePaginationOption(uiParams.PagedViewOption!.Value, query.PaginationOption);
            if (string.IsNullOrEmpty(query.OrderBys))
            {
                query.OrderBys = _orderBysListHelper.GetDefaultBuildVersionOrderBys();
            }

            var result = await _thisService.Search(query);

            var vm = await _pagedSearchViewModelHelper.GetBuildVersionPagedSearchViewModel("index", uiParams, query, result, true, true);
            return View(vm);
        }

        // GET: BuildVersion/AjaxLoadItems
        [HttpGet] // from query string
        [HttpPost]// form post formdata
        public async Task<IActionResult> AjaxLoadItems(BuildVersionAdvancedQuery query, UIParams uiParams)
        {
            var result = await _thisService.Search(query);
            var pagedViewModel = new ListViewModel<BuildVersionDataModel[]>
            {
                UIListSetting = _viewFeatureManager.GetBuildVersionUIListSetting(String.Empty, uiParams),
                Result = result,
            };

            if(uiParams.Template == ViewItemTemplates.Create.ToString() || uiParams.Template == ViewItemTemplates.Edit.ToString())
            {
            }

            if (uiParams.PagedViewOption == ListViewOptions.Table || uiParams.PagedViewOption == ListViewOptions.EditableTable)
            {
                return PartialView("~/Views/BuildVersion/_Table.cshtml", pagedViewModel);
            }
            else if (uiParams.PagedViewOption == ListViewOptions.Tiles)
            {
                return PartialView("~/Views/BuildVersion/_Tiles.cshtml", pagedViewModel);
            }
            //else // if (uiParams.PagedViewOption == ListViewOptions.SlideShow)
            // SlideShow
            return PartialView("~/Views/BuildVersion/_SlideShow.cshtml", pagedViewModel);

        }

        [Route("[controller]/[action]/{SystemInformationID}/{VersionDate}/{ModifiedDate}")] // Primary
        [HttpGet, ActionName("Dashboard")]
        // GET: BuildVersion/Dashboard/{SystemInformationID}/{VersionDate}/{ModifiedDate}
        public async Task<IActionResult> Dashboard([FromRoute]BuildVersionIdentifier id)
        {
            var listItemRequests = new Dictionary<BuildVersionCompositeModel.__DataOptions__, CompositeListItemRequest>();

            var result = await _thisService.GetCompositeModel(id, listItemRequests);

            result.UIParamsList.Add(
                BuildVersionCompositeModel.__DataOptions__.__Master__,
                new UIParams { PagedViewOption = ListViewOptions.Card, Template = ViewItemTemplates.Details.ToString() });

            return View(result);
        }

        [Route("[controller]/[action]/{SystemInformationID}/{VersionDate}/{ModifiedDate}")] // Primary
        // GET: BuildVersion/AjaxLoadItem/{SystemInformationID}/{VersionDate}/{ModifiedDate}
        [HttpGet, ActionName("AjaxLoadItem")]
        public async Task<IActionResult> AjaxLoadItem(
            ListViewOptions view,
            CrudViewContainers container,
            string template,
            int? index, // for EditableList
            BuildVersionIdentifier id)
        {
            BuildVersionDataModel? result;
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

            var itemViewModel = new Framework.Mvc.Models.MvcItemViewModel<BuildVersionDataModel>
            {
                UIItemFeatures = _viewFeatureManager.GetBuildVersionUIItemFeatures(),
                Status = System.Net.HttpStatusCode.OK,
                Template = template,
                IsCurrentItem = true,
                IndexInArray = index ?? 0,
                Model = result
            };

            // TODO: Maybe some special for Edit/Create
            if (template == ViewItemTemplates.Edit.ToString() || template == ViewItemTemplates.Create.ToString())
            {

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
        // POST: BuildVersion/AjaxCreate
        [HttpPost, ActionName("AjaxCreate")]
        [Route("[controller]/[action]")]
        public async Task<IActionResult> AjaxCreate(
            ListViewOptions view,
            CrudViewContainers container,
            ViewItemTemplates template,
            [Bind("Database_Version,VersionDate,ModifiedDate")] BuildVersionDataModel input)
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
                                new Tuple<string, object>("~/Views/BuildVersion/_TableItemTr.cshtml",
                                    new Framework.Mvc.Models.MvcItemViewModel<BuildVersionDataModel>{
                                        UIItemFeatures = _viewFeatureManager.GetBuildVersionUIItemFeatures(),
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
                                    new Tuple<string, object>("~/Views/BuildVersion/_Tile.cshtml",
                                        new Framework.Mvc.Models.MvcItemViewModel<BuildVersionDataModel>
                                        {
                                            UIItemFeatures = _viewFeatureManager.GetBuildVersionUIItemFeatures(),
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

        [Route("[controller]/[action]/{SystemInformationID}/{VersionDate}/{ModifiedDate}")] // Primary
        [HttpPost, ActionName("AjaxDelete")]
        // POST: BuildVersion/AjaxDelete/{SystemInformationID}/{VersionDate}/{ModifiedDate}
        public async Task<IActionResult> AjaxDelete(
            ListViewOptions view,
            CrudViewContainers container,
            ViewItemTemplates template,
            [FromRoute] BuildVersionIdentifier id)
        {
            var result = await _thisService.Delete(id);
            if (result.Status == System.Net.HttpStatusCode.OK)
                return PartialView("~/Views/Shared/_AjaxResponse.cshtml", new AjaxResponseViewModel { Status = System.Net.HttpStatusCode.OK, RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
            return PartialView("~/Views/Shared/_AjaxResponse.cshtml", new AjaxResponseViewModel { Status = result.Status, Message = result.StatusMessage, RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.

        [Route("[controller]/[action]/{SystemInformationID}/{VersionDate}/{ModifiedDate}")] // Primary
        [HttpPost, ActionName("AjaxEdit")]
        // POST: BuildVersion/AjaxEdit/{SystemInformationID}/{VersionDate}/{ModifiedDate}
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> AjaxEdit(
            ListViewOptions view,
            CrudViewContainers container,
            ViewItemTemplates template,
            BuildVersionIdentifier id,
            [Bind("SystemInformationID,VersionDate,ModifiedDate,Database_Version")] BuildVersionDataModel input)
        {
            if (!id.SystemInformationID.HasValue && !id.VersionDate.HasValue && !id.ModifiedDate.HasValue ||
                id.SystemInformationID.HasValue && id.SystemInformationID != input.SystemInformationID || id.VersionDate.HasValue && id.VersionDate != input.VersionDate || id.ModifiedDate.HasValue && id.ModifiedDate != input.ModifiedDate)
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
                                new Tuple<string, object>("~/Views/BuildVersion/_TableDetailsItem.cshtml",
                                    new Framework.Mvc.Models.MvcItemViewModel<BuildVersionDataModel>
                                    {
                                        UIItemFeatures = _viewFeatureManager.GetBuildVersionUIItemFeatures(),
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
                                new Tuple<string, object>("~/Views/BuildVersion/_TileDetailsItem.cshtml",
                                    new Framework.Mvc.Models.MvcItemViewModel<BuildVersionDataModel>
                                    {
                                        UIItemFeatures = _viewFeatureManager.GetBuildVersionUIItemFeatures(),
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

        // POST: BuildVersion//AjaxBulkDelete
        [HttpPost, ActionName("AjaxBulkDelete")]
        [Route("[controller]/[action]")]
        public async Task<IActionResult> AjaxBulkDelete(
            [FromForm] BatchActionRequest<BuildVersionIdentifier> data)
        {
            var result = await _thisService.BulkDelete(data.Ids);
            if (result.Status == System.Net.HttpStatusCode.OK)
                return PartialView("~/Views/Shared/_AjaxResponse.cshtml", new AjaxResponseViewModel { Status = System.Net.HttpStatusCode.OK, RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
            return PartialView("~/Views/Shared/_AjaxResponse.cshtml", new AjaxResponseViewModel { Status = result.Status, Message = result.StatusMessage, RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        // POST: BuildVersion/AjaxMultiItemsCUDSubmit
        [HttpPost, ActionName("AjaxMultiItemsCUDSubmit")]
        [Route("[controller]/[action]")]
        public async Task<IActionResult> AjaxMultiItemsCUDSubmit(
            [FromQuery] ListViewOptions view,
            [FromForm] List<BuildVersionDataModel> data)
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

            var multiItemsCUDModel = new MultiItemsCUDRequest<BuildVersionIdentifier, BuildVersionDataModel>
            {
                DeleteItems =
                    (from t in data
                    where t.IsDeleted______ && t.ItemUIStatus______ != ItemUIStatus.New
                    select new BuildVersionIdentifier { SystemInformationID = t.SystemInformationID }).ToList(),
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

        [Route("[controller]/[action]/{SystemInformationID}/{VersionDate}/{ModifiedDate}")] // Primary
        //[HttpGet, ActionName("Edit")]
        // GET: BuildVersion/Edit/{SystemInformationID}/{VersionDate}/{ModifiedDate}
        public async Task<IActionResult> Edit([FromRoute]BuildVersionIdentifier id)
        {
            if (!id.SystemInformationID.HasValue && !id.VersionDate.HasValue && !id.ModifiedDate.HasValue)
            {
                var itemViewModel1 = new Framework.Mvc.Models.MvcItemViewModel<BuildVersionDataModel>
                {
                    Status = System.Net.HttpStatusCode.NotFound,
                    StatusMessage = "Not Found",
                    Template = ViewItemTemplates.Edit.ToString(),
                };
                return View(itemViewModel1);
            }

            var result = await _thisService.Get(id);
            var itemViewModel = new Framework.Mvc.Models.MvcItemViewModel<BuildVersionDataModel>
            {
                Status = result.Status,
                StatusMessage = result.StatusMessage,
                Template = ViewItemTemplates.Edit.ToString(),

                Model = result.ResponseBody
            };
            return View(itemViewModel);
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]

        [Route("[controller]/[action]/{SystemInformationID}/{VersionDate}/{ModifiedDate}")] // Primary
        // POST: BuildVersion/Edit/{SystemInformationID}/{VersionDate}/{ModifiedDate}
        public async Task<IActionResult> Edit(
            [FromRoute]BuildVersionIdentifier id,
            [Bind("SystemInformationID,VersionDate,ModifiedDate,Database_Version")] BuildVersionDataModel input)
        {
            if (!id.SystemInformationID.HasValue && !id.VersionDate.HasValue && !id.ModifiedDate.HasValue ||
                id.SystemInformationID.HasValue && id.SystemInformationID != input.SystemInformationID || id.VersionDate.HasValue && id.VersionDate != input.VersionDate || id.ModifiedDate.HasValue && id.ModifiedDate != input.ModifiedDate)
            {
                var itemViewModel1 = new Framework.Mvc.Models.MvcItemViewModel<BuildVersionDataModel>
                {
                    Status = System.Net.HttpStatusCode.NotFound,
                    StatusMessage = "Not Found",
                    Template = ViewItemTemplates.Edit.ToString(),

                    Model = input, // should GetbyId again and merge content not in postback
                };
                return View(itemViewModel1);
            }

            if (!ModelState.IsValid)
            {
                var itemViewModel1 = new Framework.Mvc.Models.MvcItemViewModel<BuildVersionDataModel>
                {
                    Status = System.Net.HttpStatusCode.BadRequest,
                    StatusMessage = "Bad Request",
                    Template = ViewItemTemplates.Edit.ToString(),

                    Model = input, // should GetbyId again and merge content not in postback
                };
                return View(itemViewModel1);
            }

            var result = await _thisService.Update(id, input);
            var itemViewModel = new Framework.Mvc.Models.MvcItemViewModel<BuildVersionDataModel>
            {
                Status = result.Status,
                StatusMessage = result.StatusMessage,
                Template = ViewItemTemplates.Edit.ToString(),

                Model = result.ResponseBody,
            };
            return View(itemViewModel);
        }

        [Route("[controller]/[action]/{SystemInformationID}/{VersionDate}/{ModifiedDate}")] // Primary
        // GET: BuildVersion/Details/{SystemInformationID}/{VersionDate}/{ModifiedDate}
        public async Task<IActionResult> Details([FromRoute]BuildVersionIdentifier id)
        {
            var result = await _thisService.Get(id);
            var itemViewModel = new Framework.Mvc.Models.MvcItemViewModel<BuildVersionDataModel>
            {
                Status = result.Status,
                StatusMessage = result.StatusMessage,
                Template = ViewItemTemplates.Details.ToString(),
                Model = result.ResponseBody,
            };
            return View(itemViewModel);
        }

        // GET: BuildVersion/Create
        public async Task<IActionResult> Create()
        {
            var itemViewModel = await Task.FromResult(new Framework.Mvc.Models.MvcItemViewModel<BuildVersionDataModel>
            {
                Status = System.Net.HttpStatusCode.OK,
                Template = ViewItemTemplates.Create.ToString(),
                Model = _thisService.GetDefault(),

            });

            return View(itemViewModel);
        }

        // POST: BuildVersion/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(
            [Bind("Database_Version,VersionDate,ModifiedDate")] BuildVersionDataModel input)
        {
            if (ModelState.IsValid)
            {
                var result = await _thisService.Create(input);
                var itemViewModel = new Framework.Mvc.Models.MvcItemViewModel<BuildVersionDataModel>
                {
                    Status = result.Status,
                    StatusMessage = result.StatusMessage,
                    Template = ViewItemTemplates.Create.ToString(),
                    Model = result.ResponseBody,

                };
                return View(itemViewModel);
            }

            var itemViewModel1 = new Framework.Mvc.Models.MvcItemViewModel<BuildVersionDataModel>
            {
                Status = System.Net.HttpStatusCode.BadRequest,
                StatusMessage = "Bad Request",
                Template = ViewItemTemplates.Create.ToString(),
                Model = input, // should GetbyId again and merge content not in postback

            };
            return View(itemViewModel1);
        }

        [Route("[controller]/[action]/{SystemInformationID}/{VersionDate}/{ModifiedDate}")] // Primary
        // GET: BuildVersion/Delete/{SystemInformationID}/{VersionDate}/{ModifiedDate}
        public async Task<IActionResult> Delete([FromRoute]BuildVersionIdentifier id)
        {
            var result = await _thisService.Get(id);
            var itemViewModel = new Framework.Mvc.Models.MvcItemViewModel<BuildVersionDataModel>
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

        [Route("[controller]/[action]/{SystemInformationID}/{VersionDate}/{ModifiedDate}")] // Primary
        // POST: BuildVersion/Delete/{SystemInformationID}/{VersionDate}/{ModifiedDate}
        public async Task<IActionResult> DeleteConfirmed([FromRoute]BuildVersionIdentifier id)
        {
            var result1 = await _thisService.Get(id);
            var result = await _thisService.Delete(id);
            var itemViewModel = new Framework.Mvc.Models.MvcItemViewModel<BuildVersionDataModel>
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

