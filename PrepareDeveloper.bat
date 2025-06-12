@echo off
echo [1/3] Restoring NuGet packages...
dotnet restore

echo [2/3] Building Debug config...
dotnet build -c Debug

echo [3/3] Open solution...
start Speech2Twitch.sln

echo Готово!
pause