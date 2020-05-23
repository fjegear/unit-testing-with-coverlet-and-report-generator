dotnet test /p:CollectCoverage=true
dotnet test /p:CollectCoverage=true /p:CoverletOutputFormat=cobertura /p:CoverletOutput=$env:USERPROFILE\.coverage\coverage.xml
dotnet $env:USERPROFILE\.nuget\packages\reportgenerator\4.5.8\tools\netcoreapp2.1\ReportGenerator.dll "-reports:$env:USERPROFILE\.coverage\coverage.xml" "-targetdir:$env:USERPROFILE\.coverage\coveragereport" -reporttypes:Html
