$SolutionFolderPath = $PSScriptRoot 
cd $SolutionFolderPath

$sign = Read-Host 'Enter sign'

cd .\AspNetCore.Http\bin\Release
nuget push DashboardCode.AspNetCore.Http.3.0.0.nupkg $sign -Source https://api.nuget.org/v3/index.json

