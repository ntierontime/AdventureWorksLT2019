CALL _VariableDefinition.bat
copy /y /v %sourceRootFolder%ReactTSClientApp\src\generated\apiClients\CustomerAddressApi.ts %destRootFolder%ReactTSClientApp\src\generated\apiClients\CustomerAddressApi.ts
copy /y /v %sourceRootFolder%ReactTSClientApp\src\dataModels\ICustomerAddressDataModel.ts %destRootFolder%ReactTSClientApp\src\dataModels\ICustomerAddressDataModel.ts
copy /y /v %sourceRootFolder%ReactTSClientApp\src\dataModels\ICustomerAddressQueries.ts %destRootFolder%ReactTSClientApp\src\dataModels\ICustomerAddressQueries.ts
copy /y /v %sourceRootFolder%ReactTSClientApp\src\generated\slices\CustomerAddressSlice.ts %destRootFolder%ReactTSClientApp\src\generated\slices\CustomerAddressSlice.ts
copy /y /v %sourceRootFolder%EFCoreContext\CustomerAddress.cs %destRootFolder%EFCoreContext\CustomerAddress.cs
copy /y /v %sourceRootFolder%EFCoreRepositories\CustomerAddressRepository.cs %destRootFolder%EFCoreRepositories\CustomerAddressRepository.cs
copy /y /v %sourceRootFolder%Models\CustomerAddressDataModel.cs %destRootFolder%Models\CustomerAddressDataModel.cs
copy /y /v %sourceRootFolder%Models\CustomerAddressQueries.cs %destRootFolder%Models\CustomerAddressQueries.cs
copy /y /v %sourceRootFolder%RepositoryContracts\ICustomerAddressRepository.cs %destRootFolder%RepositoryContracts\ICustomerAddressRepository.cs
copy /y /v %sourceRootFolder%ServiceContracts\ICustomerAddressService.cs %destRootFolder%ServiceContracts\ICustomerAddressService.cs
copy /y /v %sourceRootFolder%Services\CustomerAddressService.cs %destRootFolder%Services\CustomerAddressService.cs
copy /y /v %sourceRootFolder%WebApiAdminControllers\CustomerAddressApiController.cs %destRootFolder%WebApiAdminControllers\CustomerAddressApiController.cs
