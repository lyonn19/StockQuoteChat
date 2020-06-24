# StockQuoteChat
Playing with Backend Technologies .Net Core, SignalR, Event Messaging(RabbitMq), Entity Framework, Identity

1. Prerequisites
- .Net Core SDK 3.0
- Docker installed and running 
- GitBash Console 

2. How to use 
- Run RabbitMQ Docker Image
  - $> docker pull rabbitmq:3-management   
  - $> docker run -d --hostname my-rabbit --name my-rabbit -p 15672:15672 -p  5672:5672 rabbitmq:3-management   
- Run SqlServer Docker Image 
  - $> docker pull mcr.microsoft.com/mssql/server:latest
  - $> docker run -e 'ACCEPT_EULA=Y' -e 'SA_PASSWORD=Password1!' -e 'MSSQL_PID=Express' --name sqlserver -p 1433:1433 -d mcr.microsoft.com/mssql/server:latest
  
- Run App 
  - $> git clone https://github.com/lyonn19/StockQuoteChat.git
  - $> cd StockQuoteChat
  - $> mkdir -p app/build
  - $> mkdir -p app/publish
  - $> dotnet restore "StockQuoteChat/StockQuoteChat.csproj"
  - $> dotnet build "StockQuoteChat/StockQuoteChat.csproj" -c Release -o app/build
  - $> dotnet publish "StockQuoteChat/StockQuoteChat.csproj" -c Release -o app/publish
  - $> cd app/publish
  - $> dotnet StockQuoteChat.dll
     - Now listening on: http://localhost:5000
     - Now listening on: https://localhost:5001
  
