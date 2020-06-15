@echo off
dotnet restore HtmlAnalysis.Api/HtmlAnalysis.Api.csproj
dotnet restore HtmlAnalysis.Web/HtmlAnalysis.Web.csproj

dotnet build

start dotnet run --project ./HtmlAnalysis.Api/HtmlAnalysis.Api.csproj --urls=https://localhost:44343
start dotnet run --project ./HtmlAnalysis.Web/HtmlAnalysis.Web.csproj --urls=https://localhost:44383