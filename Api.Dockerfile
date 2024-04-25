FROM mcr.microsoft.com/dotnet/aspnet:6.0-alpine3.15-amd64 AS base
WORKDIR /app
EXPOSE 5001/tcp

RUN apk add terminus-font && \
    apk add --no-cache icu-libs
ENV DOTNET_SYSTEM_GLOBALIZATION_INVARIANT false
ENV ASPNETCORE_ENVIRONMENT=Production
#ENV ConnectionStrings:StockChatConnection="server=stock-db;database=stock;user=sa;password=dev@1234;convert zero datetime=True;"s

FROM mcr.microsoft.com/dotnet/sdk:6.0-alpine3.15-amd64 AS build-env
COPY ["./Stock.Chat.sln", "./"]
COPY ["./Stock.Chat.CrossCutting/Stock.Chat.CrossCutting.csproj", "./Stock.Chat.CrossCutting/" ]
COPY ["./Stock.Chat.CrossCutting/Stock.Chat.CrossCutting.csproj", "./Stock.Chat.CrossCutting/" ]
COPY ["./Stock.Chat.Infrastructure/Stock.Chat.Infrastructure.csproj", "./Stock.Chat.Infrastructure/" ]
COPY ["./Stock.Chat.Domain/Stock.Chat.Domain.csproj", "./Stock.Chat.Domain/" ]
COPY ["./Stock.Chat.Application/Stock.Chat.Application.csproj", "./Stock.Chat.Application/" ]
COPY ["./Stock.Chat.Api/Stock.Chat.Api.csproj", "./Stock.Chat.Api/" ]
#RUN dotnet restore "./Stock.Chat.Api/Stock.Chat.Api.csproj"
COPY ./ .

#RUN dotnet build "./Stock.Chat.Api/Stock.Chat.Api.csproj" --packages ./.nuget/packages -c Production -o /app/build

#RUN dotnet test

FROM build-env AS publish
RUN dotnet publish "./Stock.Chat.Api/Stock.Chat.Api.csproj" -c Production -o /app/publish


FROM base AS final
WORKDIR /app/build
RUN chmod +x ./

COPY --from=publish /app/publish .

ENTRYPOINT ["dotnet", "Stock.Chat.Api.dll", "--server.urls", "http://*:5001"]