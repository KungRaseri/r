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

$user = "count"

$SolutionDir = Get-Location

$SolutionDir = $SolutionDir.Path + "\"

$source = "$SolutionDir" + "src"
$lib_s = "$SolutionDir" + "lib_s"
$lib_c = "$SolutionDir" + "lib_c"

$server = "\Server"
$client = "\Client"
$ui = "\Client.UI"

$output_client = "\bin\Debug"
$output_server = "\bin\Debug\netstandard2.1"
$output_ui = "\openrp-client-ui"

$serverdata = "$SolutionDir" + "server-data"
$resources = "$serverdata\resources"
$framework_client = "$resources\framework_client"
$framework_server = "$resources\framework_server"
$framework_ui = "$resources\framework_ui"

Write-Output "Solution: $SolutionDir"
Write-Output "Source files: $source"
Write-Output "Destination files: $serverdata"

Write-Output "Preparing server-data directory..."

New-Item -ItemType Directory -Path $serverdata -Force
New-Item -ItemType Directory -Path $resources -Force
New-Item -ItemType Directory -Path $framework_client -Force
New-Item -ItemType Directory -Path $framework_server -Force
New-Item -ItemType Directory -Path $framework_ui -Force

Write-Output "Copying resources to $serverdata ..."

# Client-side and Server-side resources
Copy-Item -Path $source$server$output_server/* -Destination $framework_server -Recurse -Force
Copy-Item -Path $source$client$output_client/* -Destination $framework_client -Recurse -Force
Copy-Item -Path $source$ui$output_ui/* -Destination $framework_ui -Recurse -Force

# Referenced Libraries
Copy-Item -Path $lib_s/* -Destination $framework_server -Recurse -Force
Copy-Item -Path $lib_c/* -Destination $framework_client -Recurse -Force
