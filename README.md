# StockQuoteChat
Playing with Backend Technologies .Net Core, SignalR, Queue (RabbitMq), Entity Framework, Identity

1. Prerequisites
- .Net Core SDK 3.0
- Docker installed and running 

2. How to use 
- Run RabbitMQ Docker Image
  - $> docker pull rabbitmq:3-management   
  - $> docker run -d --hostname my-rabbit --name my-rabbit -p 15672:15672 -p  5672:5672 rabbitmq:3-management   
- Run SqlServer Docker Image 
  - $> docker pull mcr.microsoft.com/mssql/server:latest
  - $> docker run 'ACCEPT_EULA=Y' 'SA_PASSWORD=Password1!' 'MSSQL_PID=Express' --name sqlserver -p 1433:1433 -d mcr.microsoft.com/mssql/server:latest
- Run App 
  - git clone [repository]
  - here steps for build web image...
  
