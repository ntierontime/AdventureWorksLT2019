#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["AdventureWorksLT2019/MvcWebApp/AdventureWorksLT2019.MvcWebApp.csproj", "AdventureWorksLT2019/MvcWebApp/"]
RUN dotnet restore "AdventureWorksLT2019/MvcWebApp/AdventureWorksLT2019.MvcWebApp.csproj"
COPY . .
WORKDIR "/src/AdventureWorksLT2019/MvcWebApp/AdventureWorksLT2019.MvcWebApp.csproj"
RUN dotnet build "AdventureWorksLT2019.MvcWebApp.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "AdventureWorksLT2019.MvcWebApp.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "AdventureWorksLT2019.MvcWebApp.dll"]

