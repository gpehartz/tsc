@echo off
rem call dnvm use defaultcore
rem call dotnet run -p Tsc.WebApi --server.urls http://localhost:8081
dotnet run -p Tsc.Server\Tsc.WebApi --server.urls http://localhost:8081
