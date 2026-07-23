FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
 
COPY src/shop.dto.prj ./shop.dto.prj
COPY src/Utilities ./Utilities
COPY src/shop.server.prj ./shop.server.prj 
  
RUN dotnet restore shop.server.prj/shop.server.csproj 
RUN dotnet publish shop.server.prj/shop.server.csproj -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS final
WORKDIR /app
EXPOSE 80
EXPOSE 443

COPY --from=build /app/publish . 
ENTRYPOINT ["dotnet", "shop.server.dll"]