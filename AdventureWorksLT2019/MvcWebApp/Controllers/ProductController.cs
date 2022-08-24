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
    public class ProductController : Controller
    {
        private readonly IProductService _thisService;
        private readonly SelectListHelper _selectListHelper;
        private readonly ViewFeaturesManager _viewFeatureManager;
        private readonly IDropDownListService _dropDownListService;
        private readonly OrderBysListHelper _orderBysListHelper;
        private readonly MvcItemViewModelHelper _mvcItemViewModelHelper;
        private readonly PagedSearchViewModelHelper _pagedSearchViewModelHelper;
        private readonly IUIStrings _localizor;
        private readonly ILogger<ProductController> _logger;

        public ProductController(
            IProductService thisService,
            SelectListHelper selectListHelper,
            ViewFeaturesManager viewFeatureManager,
            IDropDownListService dropDownListService,
            OrderBysListHelper orderBysListHelper,
            MvcItemViewModelHelper mvcItemViewModelHelper,
            PagedSearchViewModelHelper pagedSearchViewModelHelper,
            IUIStrings localizor,
            ILogger<ProductController> logger)
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

        // GET: Product
        [HttpGet] // from query string
        [HttpPost]// form post formdata
        public async Task<IActionResult> Index(ProductAdvancedQuery query, UIParams uiParams)
        {
            _viewFeatureManager.DefaultUIParamsIfNeeds(uiParams, PagedViewOptions.Table);
            // UIParams.PagedViewOption is not null here
            query.PaginationOption = _viewFeatureManager.HardCodePaginationOption(uiParams.PagedViewOption!.Value, query.PaginationOption);
            if (string.IsNullOrEmpty(query.OrderBys))
            {
                query.OrderBys = _orderBysListHelper.GetDefaultProductOrderBys();
            }

            var result = await _thisService.Search(query);

            var vm = await _pagedSearchViewModelHelper.GetProductPagedSearchViewModel("index", uiParams, query, result, true, true);
            return View(vm);
        }

        // GET: Product/AjaxLoadItems
        [HttpGet] // from query string
        [HttpPost]// form post formdata
        public async Task<IActionResult> AjaxLoadItems(ProductAdvancedQuery query, UIParams uiParams)
        {
            var result = await _thisService.Search(query);
            var pagedViewModel = new PagedViewModel<ProductDataModel.DefaultView[]>
            {
                UIListSetting = _viewFeatureManager.GetProductUIListSetting(String.Empty, uiParams),
                Result = result,
            };

            if(uiParams.Template == ViewItemTemplateNames.Create || uiParams.Template == ViewItemTemplateNames.Edit)
            {                pagedViewModel.TopLevelDropDownListsFromDatabase = await _dropDownListService.GetProductTopLevelDropDownListsFromDatabase();
            }

            if (uiParams.PagedViewOption == PagedViewOptions.Table || uiParams.PagedViewOption == PagedViewOptions.EditableTable)
            {
                return PartialView("~/Views/Product/_Table.cshtml", pagedViewModel);
            }
            else if (uiParams.PagedViewOption == PagedViewOptions.Tiles)
            {
                return PartialView("~/Views/Product/_Tiles.cshtml", pagedViewModel);
            }
            //else // if (uiParams.PagedViewOption == PagedViewOptions.SlideShow)
            // SlideShow
            return PartialView("~/Views/Product/_SlideShow.cshtml", pagedViewModel);

        }

        [Route("[controller]/[action]/{ProductID}")] // Primary
        [HttpGet, ActionName("Dashboard")]
        // GET: Product/Dashboard/{ProductID}
        public async Task<IActionResult> Dashboard([FromRoute]ProductIdentifier id)
        {
            var result = await _thisService.GetCompositeModel(id);

            result.UIParamsList.Add(
                ProductCompositeModel.__DataOptions__.__Master__,
                new UIParams { PagedViewOption = PagedViewOptions.Card, Template = ViewItemTemplateNames.Details });

            result.UIParamsList.Add(
                ProductCompositeModel.__DataOptions__.SalesOrderDetails_Via_ProductID,
                new UIParams { PagedViewOption = PagedViewOptions.Table, Template = ViewItemTemplateNames.Details });

            result.UIParamsList.Add(
                ProductCompositeModel.__DataOptions__.ProductCategory,
                new UIParams { PagedViewOption = PagedViewOptions.Card, Template = ViewItemTemplateNames.Details });

            return View(result);
        }

        [Route("[controller]/[action]/{ProductID}")] // Primary
        // GET: Product/AjaxLoadItem/{ProductID}
        [HttpGet, ActionName("AjaxLoadItem")]
        public async Task<IActionResult> AjaxLoadItem(
            PagedViewOptions view,
            CrudViewContainers container,
            string template,
            int? index, // for EditableList
            ProductIdentifier id)
        {
            ProductDataModel.DefaultView? result;
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

            var itemViewModel = new AdventureWorksLT2019.MvcWebApp.Models.MvcItemViewModel<ProductDataModel.DefaultView>
            {
                UIItemFeatures = _viewFeatureManager.GetProductUIItemFeatures(),
                Status = System.Net.HttpStatusCode.OK,
                Template = template,
                IsCurrentItem = true,
                IndexInArray = index ?? 0,
                Model = result
            };

            // TODO: Maybe some special for Edit/Create
            if (template == ViewItemTemplateNames.Edit.ToString() || template == ViewItemTemplateNames.Create.ToString())
            {
                itemViewModel.TopLevelDropDownListsFromDatabase = await _dropDownListService.GetProductTopLevelDropDownListsFromDatabase();
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
        // POST: Product/AjaxCreate
        [HttpPost, ActionName("AjaxCreate")]
        [Route("[controller]/[action]")]
        public async Task<IActionResult> AjaxCreate(
            PagedViewOptions view,
            CrudViewContainers container,
            ViewItemTemplateNames template,
            [Bind("Name,ProductNumber,Color,StandardCost,ListPrice,Size,Weight,ProductCategoryID,ProductModelID,SellStartDate,SellEndDate,DiscontinuedDate,ThumbNailPhoto,ThumbnailPhotoFileName,ModifiedDate")] ProductDataModel input)
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
                                new Tuple<string, object>("~/Views/Product/_TableItemTr.cshtml",
                                    new AdventureWorksLT2019.MvcWebApp.Models.MvcItemViewModel<ProductDataModel.DefaultView>{
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
                                    new Tuple<string, object>("~/Views/Product/_Tile.cshtml",
                                        new AdventureWorksLT2019.MvcWebApp.Models.MvcItemViewModel<ProductDataModel.DefaultView>
                                        {
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

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.

        [Route("[controller]/[action]/{ProductID}")] // Primary
        [HttpPost, ActionName("AjaxEdit")]
        // POST: Product/AjaxEdit/{ProductID}
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> AjaxEdit(
            PagedViewOptions view,
            CrudViewContainers container,
            ViewItemTemplateNames template,
            ProductIdentifier id,
            [Bind("ProductID,Name,ProductNumber,Color,StandardCost,ListPrice,Size,Weight,ProductCategoryID,ProductModelID,SellStartDate,SellEndDate,DiscontinuedDate,ThumbNailPhoto,ThumbnailPhotoFileName,ModifiedDate")] ProductDataModel.DefaultView input)
        {
            if (!id.ProductID.HasValue ||
                id.ProductID.HasValue && id.ProductID != input.ProductID)
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
                                new Tuple<string, object>("~/Views/Product/_TableDetailsItem.cshtml",
                                    new AdventureWorksLT2019.MvcWebApp.Models.MvcItemViewModel<ProductDataModel.DefaultView>
                                    {
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
                                new Tuple<string, object>("~/Views/Product/_TileDetailsItem.cshtml",
                                    new AdventureWorksLT2019.MvcWebApp.Models.MvcItemViewModel<ProductDataModel.DefaultView>
                                    {
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

        [Route("[controller]/[action]/{ProductID}")] // Primary
        //[HttpGet, ActionName("Edit")]
        // GET: Product/Edit/{ProductID}
        public async Task<IActionResult> Edit([FromRoute]ProductIdentifier id)
        {
            if (!id.ProductID.HasValue)
            {
                var itemViewModel1 = new AdventureWorksLT2019.MvcWebApp.Models.MvcItemViewModel<ProductDataModel.DefaultView>
                {
                    Status = System.Net.HttpStatusCode.NotFound,
                    StatusMessage = "Not Found",
                    Template = ViewItemTemplateNames.Edit.ToString(),
                };
                return View(itemViewModel1);
            }

            var result = await _thisService.Get(id);
            var topLevelDropDownListsFromDatabase = await _dropDownListService.GetProductTopLevelDropDownListsFromDatabase();

            var itemViewModel = new AdventureWorksLT2019.MvcWebApp.Models.MvcItemViewModel<ProductDataModel.DefaultView>
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

        [Route("[controller]/[action]/{ProductID}")] // Primary
        // POST: Product/Edit/{ProductID}
        public async Task<IActionResult> Edit(
            [FromRoute]ProductIdentifier id,
            [Bind("ProductID,Name,ProductNumber,Color,StandardCost,ListPrice,Size,Weight,ProductCategoryID,ProductModelID,SellStartDate,SellEndDate,DiscontinuedDate,ThumbNailPhoto,ThumbnailPhotoFileName,ModifiedDate")] ProductDataModel.DefaultView input)
        {
            var topLevelDropDownListsFromDatabase = await _dropDownListService.GetProductTopLevelDropDownListsFromDatabase();

            if (!id.ProductID.HasValue ||
                id.ProductID.HasValue && id.ProductID != input.ProductID)
            {
                var itemViewModel1 = new AdventureWorksLT2019.MvcWebApp.Models.MvcItemViewModel<ProductDataModel.DefaultView>
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
                var itemViewModel1 = new AdventureWorksLT2019.MvcWebApp.Models.MvcItemViewModel<ProductDataModel.DefaultView>
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
            var itemViewModel = new AdventureWorksLT2019.MvcWebApp.Models.MvcItemViewModel<ProductDataModel.DefaultView>
            {
                Status = result.Status,
                StatusMessage = result.StatusMessage,
                Template = ViewItemTemplateNames.Edit.ToString(),
                TopLevelDropDownListsFromDatabase = topLevelDropDownListsFromDatabase,
                Model = result.ResponseBody,
            };
            return View(itemViewModel);
        }

        [Route("[controller]/[action]/{ProductID}")] // Primary
        // GET: Product/Details/{ProductID}
        public async Task<IActionResult> Details([FromRoute]ProductIdentifier id)
        {
            var result = await _thisService.Get(id);
            var itemViewModel = new AdventureWorksLT2019.MvcWebApp.Models.MvcItemViewModel<ProductDataModel.DefaultView>
            {
                Status = result.Status,
                StatusMessage = result.StatusMessage,
                Template = ViewItemTemplateNames.Details.ToString(),
                Model = result.ResponseBody,
            };
            return View(itemViewModel);
        }

        // GET: Product/Create
        public async Task<IActionResult> Create()
        {
                var topLevelDropDownListsFromDatabase = await _dropDownListService.GetProductTopLevelDropDownListsFromDatabase();

            var itemViewModel = await Task.FromResult(new AdventureWorksLT2019.MvcWebApp.Models.MvcItemViewModel<ProductDataModel.DefaultView>
            {
                Status = System.Net.HttpStatusCode.OK,
                Template = ViewItemTemplateNames.Create.ToString(),
                Model = _thisService.GetDefault(),
                TopLevelDropDownListsFromDatabase = topLevelDropDownListsFromDatabase,
            });

                    itemViewModel.TopLevelDropDownListsFromDatabase = await _dropDownListService.GetProductTopLevelDropDownListsFromDatabase();

            return View(itemViewModel);
        }

        // POST: Product/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(
            [Bind("Name,ProductNumber,Color,StandardCost,ListPrice,Size,Weight,ProductCategoryID,ProductModelID,SellStartDate,SellEndDate,DiscontinuedDate,ThumbNailPhoto,ThumbnailPhotoFileName,ModifiedDate")] ProductDataModel.DefaultView input)
        {
                var topLevelDropDownListsFromDatabase = await _dropDownListService.GetProductTopLevelDropDownListsFromDatabase();

            if (ModelState.IsValid)
            {
                var result = await _thisService.Create(input);
                var itemViewModel = new AdventureWorksLT2019.MvcWebApp.Models.MvcItemViewModel<ProductDataModel.DefaultView>
                {
                    Status = result.Status,
                    StatusMessage = result.StatusMessage,
                    Template = ViewItemTemplateNames.Create.ToString(),
                    Model = result.ResponseBody,
                    TopLevelDropDownListsFromDatabase = topLevelDropDownListsFromDatabase,
                };
                return View(itemViewModel);
            }

            var itemViewModel1 = new AdventureWorksLT2019.MvcWebApp.Models.MvcItemViewModel<ProductDataModel.DefaultView>
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

