$SolutionFolderPath = $PSScriptRoot 
cd $SolutionFolderPath

$sign = Read-Host 'Enter sign'

cd .\AspNetCore.Http\bin\Release
nuget push DashboardCode.AspNetCore.3.0.2.nupkg $sign -Source https://api.nuget.org/v3/index.json

