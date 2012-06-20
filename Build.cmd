@echo off
if "%1"=="PublishPackages" goto publish

%WINDIR%\Microsoft.NET\Framework\v4.0.30319\msbuild.exe build\build.proj %*
goto end

:end
pause