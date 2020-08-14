#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["DemoTemplating.Api/DemoTemplating.Api.csproj", "DemoTemplating.Api/"]
COPY ["DemoTemplating.RendererLib/DemoTemplating.RendererLib.csproj", "DemoTemplating.RendererLib/"]
RUN dotnet restore "DemoTemplating.Api/DemoTemplating.Api.csproj"
COPY . .
WORKDIR "/src/DemoTemplating.Api"
RUN dotnet build "DemoTemplating.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "DemoTemplating.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "DemoTemplating.Api.dll"]