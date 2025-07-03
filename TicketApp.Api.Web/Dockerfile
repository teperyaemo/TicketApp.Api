FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8030
EXPOSE 8035

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ./nuget.config .
COPY ["TicketApp.Api.Web/TicketApp.Api.Web.csproj", "TicketApp.Api.Web/"]
COPY ["TicketApp.Api.Domain/TicketApp.Api.Domain.csproj", "TicketApp.Api.Domain/"]
COPY ["TicketApp.Api.Persistence/TicketApp.Api.Persistence.csproj", "TicketApp.Api.Persistence/"]
COPY ["TicketApp.Api.Application/TicketApp.Api.Application.csproj", "TicketApp.Api.Appication/"]
RUN dotnet restore "TicketApp.Api.Web/TicketApp.Api.Web.csproj"
COPY . .
WORKDIR "/src/TicketApp.Api.Web"
RUN dotnet build "TicketApp.Api.Web.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "TicketApp.Api.Web.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "TicketApp.Api.Web.dll"]
