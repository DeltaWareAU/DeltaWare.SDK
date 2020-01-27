$projectsDir = "C:\Projects"
$nugetDir = "C:\#Stuff\nuget"
$solution = "DeltaWare.SDK"

if(-Not (Test-Path -Path "$nugetDir\$solution")){
    New-Item -Path "$nugetDir" -Name $solution -ItemType directory
}

Get-ChildItem -Path "$projectsDir\$solution\src\*.nupkg" -Recurse | Move-Item -Destination "$nugetDir\$solution\" -ErrorAction SilentlyContinue


