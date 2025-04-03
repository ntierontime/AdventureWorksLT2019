CALL _VariableDefinition.bat
copy /y /v %sourceRootFolder%ReactTSClientApp\src\generated\apiClients\BuildVersionApi.ts %destRootFolder%ReactTSClientApp\src\generated\apiClients\BuildVersionApi.ts
copy /y /v %sourceRootFolder%ReactTSClientApp\src\dataModels\IBuildVersionDataModel.ts %destRootFolder%ReactTSClientApp\src\dataModels\IBuildVersionDataModel.ts
copy /y /v %sourceRootFolder%ReactTSClientApp\src\dataModels\IBuildVersionQueries.ts %destRootFolder%ReactTSClientApp\src\dataModels\IBuildVersionQueries.ts
copy /y /v %sourceRootFolder%ReactTSClientApp\src\generated\slices\BuildVersionSlice.ts %destRootFolder%ReactTSClientApp\src\generated\slices\BuildVersionSlice.ts
copy /y /v %sourceRootFolder%EFCoreContext\BuildVersion.cs %destRootFolder%EFCoreContext\BuildVersion.cs
copy /y /v %sourceRootFolder%EFCoreRepositories\BuildVersionRepository.cs %destRootFolder%EFCoreRepositories\BuildVersionRepository.cs
copy /y /v %sourceRootFolder%Models\BuildVersionDataModel.cs %destRootFolder%Models\BuildVersionDataModel.cs
copy /y /v %sourceRootFolder%Models\BuildVersionQueries.cs %destRootFolder%Models\BuildVersionQueries.cs
copy /y /v %sourceRootFolder%RepositoryContracts\IBuildVersionRepository.cs %destRootFolder%RepositoryContracts\IBuildVersionRepository.cs
copy /y /v %sourceRootFolder%ServiceContracts\IBuildVersionService.cs %destRootFolder%ServiceContracts\IBuildVersionService.cs
copy /y /v %sourceRootFolder%Services\BuildVersionService.cs %destRootFolder%Services\BuildVersionService.cs
copy /y /v %sourceRootFolder%WebApiAdminControllers\BuildVersionApiController.cs %destRootFolder%WebApiAdminControllers\BuildVersionApiController.cs
