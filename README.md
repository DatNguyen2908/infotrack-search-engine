# Introtrack Search Engine

## Purpose
- Supports the CTO of Infotrack to check their website rankings in Google (Might support to search by other providers later).
  
![alt text](https://github.com/DatNguyen2908/infotrack-search-engine/blob/main/Screenshots/Main%20page.PNG)

## Technical Overview:
- .NET 8 LTS (In particular, Web API using ASP.NET Core Web API & Web UI using ASP.NET Core Web App).
- Docker (In case we would like to minimum configuration).
- JQuery & ChartJS to draw the chart.

## How to excute:
- Option 1:
  - Setups SQL Express in your local machine.
  - Configures the connection string in appsettings.json in Infotrack.Sales.SearchEngine.WebApi project. 
    (In particular, replace the Server, user Id & password with your SQL Server information).
  - I shared the migration script in this source code as requested but seems you don't need to run it manually. Because when you run the API project, it will run the migration automatically. 
  - Runs Infotrack.Sales.SearchEngine.WebApi & Infotrack.Sales.SearchEngine.WebApp with IIS (Run with multiple startup projects configuration).
- Option 2:
  - If option 1 is too cumbersome, please use Docker :). I've configured everything in docker compose already. Just right click to docker compose project then sets it as 'Startup project'. Then starts it with Visual Studio as usual. 
  **Note: When we run docker compose with Visual Studio, I've set up that WebApi will be launched by default with the address http://localhost:8000/. Please browses the UI with the address http://localhost:8001/ later.

## What I have done so far:
- In this project, the CTO can easily search their website rankings in Google.
- I've already written the neccessary unit tests for Google Search so the production code has been covered as well.
- The CTO can view historical search results in the Chart (supports hour, daily, weekly basis as of now). 
- In case CTO requests to search their website rankings by another provider, the developer can easily develop and extend it. I've already applied Strategy Pattern to support it.
