@echo off

for /f "tokens=2-4 delims=/ " %%a in ('date /T') do set year=%%c
for /f "tokens=2-4 delims=/ " %%a in ('date /T') do set month=%%a
for /f "tokens=2-4 delims=/ " %%a in ('date /T') do set day=%%b
set TODAY=%year%-%month%-%day%

for /f "tokens=1 delims=: " %%h in ('time /T') do set hour=%%h
for /f "tokens=2 delims=: " %%m in ('time /T') do set minutes=%%m
for /f "tokens=3 delims=: " %%a in ('time /T') do set ampm=%%a
set NOW=%hour%-%minutes%-%ampm%

set DIR=packed-on-%TODAY%-%NOW%

mkdir packed\%DIR%

echo packing nuspec files to packed\%DIR%
nuget pack package\SimpleProxy.nuspec -Exclude *.exe+*.nuspec+*.cmd+*.bat -OutputDirectory packed\%DIR%

echo publishing nuget packages to \\sqldev\d$\Nuget
copy packed\%DIR%\*.nupkg \\sqldev\d$\Nuget /y

