FROM mcr.microsoft.com/dotnet/aspnet:6.0-alpine3.15-amd64 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:6.0-alpine3.15-amd64 AS build-env
COPY ["./Stock.Chat.sln", "./"]
COPY ["./Stock.Chat.CrossCutting/Stock.Chat.CrossCutting.csproj", "./Stock.Chat.CrossCutting/" ]
COPY ["./Stock.Chat.Domain/Stock.Chat.Domain.csproj", "./Stock.Chat.Domain/"]
COPY ["./Stock.Chat.CrossCutting/Stock.Chat.CrossCutting.csproj", "./Stock.Chat.CrossCutting/"]
COPY ["./Stock.Chat.Infrastructure/Stock.Chat.Infrastructure.csproj", "./Stock.Chat.Infrastructure/"]
COPY ["./Stock.Chat.RabbitMq/Stock.Chat.RabbitMq.csproj", "./Stock.Chat.RabbitMq/"]
#RUN dotnet restore "./Stock.Chat.Api/Stock.Chat.Api.csproj"
COPY ./ .

#RUN dotnet build "./Stock.Chat.Api/Stock.Chat.Api.csproj" --packages ./.nuget/packages -c Release -o /app/build

#RUN dotnet test

FROM build-env AS publish
RUN dotnet publish "./Stock.Chat.RabbitMq/Stock.Chat.RabbitMq.csproj" -c Production -o /app/publish


FROM base AS final
WORKDIR /app/build
RUN chmod +x ./

COPY --from=publish /app/publish .

ENTRYPOINT ["dotnet", "Stock.Chat.RabbitMq.dll"]