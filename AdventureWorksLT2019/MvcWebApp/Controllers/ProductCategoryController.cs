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
    public class ProductCategoryController : Controller
    {
        private readonly IProductCategoryService _thisService;
        private readonly SelectListHelper _selectListHelper;
        private readonly ViewFeaturesManager _viewFeatureManager;
        private readonly IDropDownListService _dropDownListService;
        private readonly OrderBysListHelper _orderBysListHelper;
        private readonly MvcItemViewModelHelper _mvcItemViewModelHelper;
        private readonly ListSearchViewModelHelper _pagedSearchViewModelHelper;
        private readonly IUIStrings _localizor;
        private readonly ILogger<ProductCategoryController> _logger;

        public ProductCategoryController(
            IProductCategoryService thisService,
            SelectListHelper selectListHelper,
            ViewFeaturesManager viewFeatureManager,
            IDropDownListService dropDownListService,
            OrderBysListHelper orderBysListHelper,
            MvcItemViewModelHelper mvcItemViewModelHelper,
            ListSearchViewModelHelper pagedSearchViewModelHelper,
            IUIStrings localizor,
            ILogger<ProductCategoryController> logger)
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

        // GET: ProductCategory
        [HttpGet] // from query string
        [HttpPost]// form post formdata
        public async Task<IActionResult> Index(ProductCategoryAdvancedQuery query, UIParams uiParams)
        {
            _viewFeatureManager.DefaultUIParamsIfNeeds(uiParams, ListViewOptions.Table);
            // UIParams.PagedViewOption is not null here
            query.PaginationOption = _viewFeatureManager.HardCodePaginationOption(uiParams.PagedViewOption!.Value, query.PaginationOption);
            if (string.IsNullOrEmpty(query.OrderBys))
            {
                query.OrderBys = _orderBysListHelper.GetDefaultProductCategoryOrderBys();
            }

            var result = await _thisService.Search(query);

            var vm = await _pagedSearchViewModelHelper.GetProductCategoryPagedSearchViewModel("index", uiParams, query, result, true, true);
            return View(vm);
        }

        // GET: ProductCategory/AjaxLoadItems
        [HttpGet] // from query string
        [HttpPost]// form post formdata
        public async Task<IActionResult> AjaxLoadItems(ProductCategoryAdvancedQuery query, UIParams uiParams)
        {
            var result = await _thisService.Search(query);
            var pagedViewModel = new ListViewModel<ProductCategoryDataModel.DefaultView[]>
            {
                UIListSetting = _viewFeatureManager.GetProductCategoryUIListSetting(String.Empty, uiParams),
                Result = result,
            };

            if(uiParams.Template == ViewItemTemplates.Create.ToString() || uiParams.Template == ViewItemTemplates.Edit.ToString())
            {                pagedViewModel.TopLevelDropDownListsFromDatabase = await _dropDownListService.GetProductCategoryTopLevelDropDownListsFromDatabase();
            }

            if (uiParams.PagedViewOption == ListViewOptions.Table || uiParams.PagedViewOption == ListViewOptions.EditableTable)
            {
                return PartialView("~/Views/ProductCategory/_Table.cshtml", pagedViewModel);
            }
            else if (uiParams.PagedViewOption == ListViewOptions.Tiles)
            {
                return PartialView("~/Views/ProductCategory/_Tiles.cshtml", pagedViewModel);
            }
            //else // if (uiParams.PagedViewOption == ListViewOptions.SlideShow)
            // SlideShow
            return PartialView("~/Views/ProductCategory/_SlideShow.cshtml", pagedViewModel);

        }

        [Route("[controller]/[action]/{ProductCategoryID}")] // Primary
        // GET: ProductCategory/AjaxLoadItem/{ProductCategoryID}
        [HttpGet, ActionName("AjaxLoadItem")]
        public async Task<IActionResult> AjaxLoadItem(
            ListViewOptions view,
            CrudViewContainers container,
            string template,
            int? index, // for EditableList
            ProductCategoryIdentifier id)
        {
            ProductCategoryDataModel.DefaultView? result;
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

            var itemViewModel = new Framework.Mvc.Models.MvcItemViewModel<ProductCategoryDataModel.DefaultView>
            {
                UIItemFeatures = _viewFeatureManager.GetProductCategoryUIItemFeatures(),
                Status = System.Net.HttpStatusCode.OK,
                Template = template,
                IsCurrentItem = true,
                IndexInArray = index ?? 0,
                Model = result
            };

            // TODO: Maybe some special for Edit/Create
            if (template == ViewItemTemplates.Edit.ToString() || template == ViewItemTemplates.Create.ToString())
            {
                itemViewModel.TopLevelDropDownListsFromDatabase = await _dropDownListService.GetProductCategoryTopLevelDropDownListsFromDatabase();
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
        // POST: ProductCategory/AjaxCreate
        [HttpPost, ActionName("AjaxCreate")]
        [Route("[controller]/[action]")]
        public async Task<IActionResult> AjaxCreate(
            ListViewOptions view,
            CrudViewContainers container,
            ViewItemTemplates template,
            [Bind("ParentProductCategoryID,Name,ModifiedDate")] ProductCategoryDataModel input)
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
                                new Tuple<string, object>("~/Views/ProductCategory/_TableItemTr.cshtml",
                                    new Framework.Mvc.Models.MvcItemViewModel<ProductCategoryDataModel.DefaultView>{
                                        UIItemFeatures = _viewFeatureManager.GetProductCategoryUIItemFeatures(),
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
                                    new Tuple<string, object>("~/Views/ProductCategory/_Tile.cshtml",
                                        new Framework.Mvc.Models.MvcItemViewModel<ProductCategoryDataModel.DefaultView>
                                        {
                                            UIItemFeatures = _viewFeatureManager.GetProductCategoryUIItemFeatures(),
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

        [Route("[controller]/[action]/{ProductCategoryID}")] // Primary
        [HttpPost, ActionName("AjaxEdit")]
        // POST: ProductCategory/AjaxEdit/{ProductCategoryID}
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> AjaxEdit(
            ListViewOptions view,
            CrudViewContainers container,
            ViewItemTemplates template,
            ProductCategoryIdentifier id,
            [Bind("ProductCategoryID,ParentProductCategoryID,Name,ModifiedDate")] ProductCategoryDataModel.DefaultView input)
        {
            if (!id.ProductCategoryID.HasValue ||
                id.ProductCategoryID.HasValue && id.ProductCategoryID != input.ProductCategoryID)
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
                                new Tuple<string, object>("~/Views/ProductCategory/_TableDetailsItem.cshtml",
                                    new Framework.Mvc.Models.MvcItemViewModel<ProductCategoryDataModel.DefaultView>
                                    {
                                        UIItemFeatures = _viewFeatureManager.GetProductCategoryUIItemFeatures(),
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
                                new Tuple<string, object>("~/Views/ProductCategory/_TileDetailsItem.cshtml",
                                    new Framework.Mvc.Models.MvcItemViewModel<ProductCategoryDataModel.DefaultView>
                                    {
                                        UIItemFeatures = _viewFeatureManager.GetProductCategoryUIItemFeatures(),
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

    }
}

