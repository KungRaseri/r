<#
.Synopsis
   All-in-one script to help with the local and remote build process
.DESCRIPTION
   This script compiles all of the resources into the server-data-redacted folder.
.PROJECT
   [REDACTED] Roleplay Framework
.AUTHOR
   KungRaseri
.CONTRIBUTORS
   N/A
.COPYRIGHT
   Copyright © 2021 KungRaseri Productions, LLC. All Rights Reserved.
#>

$SolutionDir = Get-Location

$SolutionDir = $SolutionDir.Path + "\"

$source = "$SolutionDir" + "src"
$lib = "$SolutionDir" + "lib"

$server = "\Server"
$client = "\Client"
$ui = "\UI"
$loadscreen = "\Loadscreen"

$output_client = "\bin\Debug"
$output_server = "\bin\Debug\netstandard2.1"
$output_ui = "\openrp-client-ui"
$output_loadscreen = "\openrp-loadscreen-ui"

$serverdata = "$SolutionDir" + "server-data"
$resources = "$serverdata\resources"
$framework = "$resources\framework"
$loadscreen_resource = "$resources\openrp-loadscreen"

Write-Output "Solution: $SolutionDir"
Write-Output "Source files: $source"
Write-Output "Destination files: $serverdata"

Write-Output "Preparing server-data directory..."

New-Item -ItemType Directory -Path $serverdata -Force
New-Item -ItemType Directory -Path $resources -Force
New-Item -ItemType Directory -Path $framework -Force
New-Item -ItemType Directory -Path $loadscreen_resource -Force
New-Item -ItemType Directory -Path "$framework/ui" -Force

Write-Output "Copying resources to $serverdata ..."

# Client-side and Server-side resources
Copy-Item -Path $source$server$output_server/* -Destination "$framework" -Recurse -Force
Copy-Item -Path $source$client$output_client/* -Destination "$framework" -Recurse -Force
Copy-Item -Path $source$ui$output_ui/* -Destination "$framework/ui" -Recurse -Force
Copy-Item -Path $source$loadscreen$output_loadscreen/* -Destination "$loadscreen_resource" -Recurse -Force

# Referenced Libraries
Copy-Item -Path $lib/* -Destination "$framework" -Recurse -Force
