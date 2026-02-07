@echo off
dotnet pack Maple2.File.Unity\Maple2.File.Unity.csproj -c Release -o artifacts
if %errorlevel% neq 0 (
    echo Build failed.
    exit /b %errorlevel%
)
echo Package output to artifacts\
