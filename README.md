<h1 align="center">
Chat with C# dotnet, SignalR, RabbitMQ, Identity using DDD and clean architecture. 
</h1>

## Core

- Visual Studio 2022
- Domain Driven Design / Clean Architecture: C# .NET 6.0
- AutoMapper
- Authentication with AspNetCore Identity
- Message Queue with RabbitMQ and MassTransit
- Chat with AspNetCore SignalR
- CQRS (Command Query Responsibility Segregation) com MediatR
- Middlewares: Error, Request and Response
- Dependency Injection
- Repository Pattern and Unit of Work Pattern
- Unit Tests with Xunit and Moq

## Presentation

- Visual Studio 2022
- C# .NET 6.0 Web API
- AspNetCore Identity Client
- Blazor
- CSS
- Bootstrap
- Javascript

## Installer

- Docker Desktop
- Docker File 
- Docker Compose

## Installer

Open CMD, navigate to "\src" folder, and execute the command: "docker-compose build"

You may need to include the "dns" specification in Docker Desktop Config -> Docker Engine to allow installation of nuget packages:

{
  "builder": {
    "gc": {
      "defaultKeepStorage": "20GB",
      "enabled": true
    }
  },
  "dns": [
    "1.1.1.1",
    "8.8.8.8"
  ],  
  "experimental": false
} 

2. Type "docker-compose up -d" to start the containers.

## Run

FrontEnd: http://localhost:8080

To login with two users at the same time, use the incognito window.
Send the message "/stock=AAPL" to get a stock value.

Swagger: http://localhost:8082/swagger
