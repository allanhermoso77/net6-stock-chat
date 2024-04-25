FROM mcr.microsoft.com/dotnet/aspnet:6.0-alpine3.15-amd64 AS base
WORKDIR /app
EXPOSE 5002/tcp

RUN apk add terminus-font && \
    apk add --no-cache icu-libs
ENV DOTNET_SYSTEM_GLOBALIZATION_INVARIANT false

FROM mcr.microsoft.com/dotnet/sdk:6.0-alpine3.15-amd64 AS build-env
COPY ["./Stock.Chat.sln", "./"]
COPY ["./Stock.Chat.CrossCutting/Stock.Chat.CrossCutting.csproj", "./Stock.Chat.CrossCutting/" ]
COPY ["./Stock.Chat.CrossCutting/Stock.Chat.CrossCutting.csproj", "./Stock.Chat.CrossCutting/" ]
COPY ["./Stock.Chat.Web/Stock.Chat.Web.csproj", "./Stock.Chat.Web/" ]
RUN dotnet restore "./Stock.Chat.Web/Stock.Chat.Web.csproj"
COPY ./ .

RUN dotnet build "./Stock.Chat.Web/Stock.Chat.Web.csproj" --packages ./.nuget/packages -c Release -o /app/web

FROM build-env AS publish
RUN dotnet publish "./Stock.Chat.Web/Stock.Chat.Web.csproj" -c Release -o /app/publish


FROM base AS final
WORKDIR /app/web
RUN chmod +x ./

COPY --from=publish /app/publish .

ENTRYPOINT ["dotnet", "Stock.Chat.Web.dll", "--server.urls", "http://*:5002"]