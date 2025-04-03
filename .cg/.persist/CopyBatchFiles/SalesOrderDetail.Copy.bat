CALL _VariableDefinition.bat
copy /y /v %sourceRootFolder%ReactTSClientApp\src\generated\apiClients\SalesOrderDetailApi.ts %destRootFolder%ReactTSClientApp\src\generated\apiClients\SalesOrderDetailApi.ts
copy /y /v %sourceRootFolder%ReactTSClientApp\src\dataModels\ISalesOrderDetailDataModel.ts %destRootFolder%ReactTSClientApp\src\dataModels\ISalesOrderDetailDataModel.ts
copy /y /v %sourceRootFolder%ReactTSClientApp\src\dataModels\ISalesOrderDetailQueries.ts %destRootFolder%ReactTSClientApp\src\dataModels\ISalesOrderDetailQueries.ts
copy /y /v %sourceRootFolder%ReactTSClientApp\src\generated\slices\SalesOrderDetailSlice.ts %destRootFolder%ReactTSClientApp\src\generated\slices\SalesOrderDetailSlice.ts
copy /y /v %sourceRootFolder%EFCoreContext\SalesOrderDetail.cs %destRootFolder%EFCoreContext\SalesOrderDetail.cs
copy /y /v %sourceRootFolder%EFCoreRepositories\SalesOrderDetailRepository.cs %destRootFolder%EFCoreRepositories\SalesOrderDetailRepository.cs
copy /y /v %sourceRootFolder%Models\SalesOrderDetailDataModel.cs %destRootFolder%Models\SalesOrderDetailDataModel.cs
copy /y /v %sourceRootFolder%Models\SalesOrderDetailQueries.cs %destRootFolder%Models\SalesOrderDetailQueries.cs
copy /y /v %sourceRootFolder%RepositoryContracts\ISalesOrderDetailRepository.cs %destRootFolder%RepositoryContracts\ISalesOrderDetailRepository.cs
copy /y /v %sourceRootFolder%ServiceContracts\ISalesOrderDetailService.cs %destRootFolder%ServiceContracts\ISalesOrderDetailService.cs
copy /y /v %sourceRootFolder%Services\SalesOrderDetailService.cs %destRootFolder%Services\SalesOrderDetailService.cs
copy /y /v %sourceRootFolder%WebApiAdminControllers\SalesOrderDetailApiController.cs %destRootFolder%WebApiAdminControllers\SalesOrderDetailApiController.cs
