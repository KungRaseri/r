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
$docs = "$SolutionDir" + "docs"

$server = "\Server"
$client = "\Client"
$database = "\Database"

$output_client = "\bin\Debug"
$output_server = "\bin\Debug\netstandard2.1"

$serverdata = "$SolutionDir" + "server-data"
$resources = "$serverdata\resources"
$framework_client = "$resources\framework_client"
$framework_server = "$resources\framework_server"

Write-Output "Solution: $SolutionDir"
Write-Output "Source files: $source"
Write-Output "Destination files: $serverdata"

Write-Output "Preparing server-data directory..."

New-Item -ItemType Directory -Path $serverdata -Force
New-Item -ItemType Directory -Path $resources -Force
New-Item -ItemType Directory -Path $framework_client -Force
New-Item -ItemType Directory -Path $framework_server -Force
New-Item -ItemType Directory -Path "$docs/client" -Force
New-Item -ItemType Directory -Path "$docs/server" -Force
New-Item -ItemType Directory -Path "$docs/database" -Force

Write-Output "Copying resources to $serverdata ..."

# redacted client-side and server-side resources
Copy-Item -Path $source$server$output_server/* -Destination $framework_server -Recurse -Force
Copy-Item -Path $source$client$output_client/* -Destination $framework_client -Recurse -Force

Copy-Item -Path $source$server$output_server/*.md -Destination "$docs/server" -Recurse -Force
Copy-Item -Path $source$client$output_client/*.md -Destination "$docs/client" -Recurse -Force
Copy-Item -Path $source$database$output_server/*.md -Destination "$docs/database" -Recurse -Force
