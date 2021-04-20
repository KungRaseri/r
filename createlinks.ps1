$resources_path = "D:\fivem\server-data-redacted\resources"
$framework = "E:\Code\open-roleplay\roleplay-framework\server-data\resources\framework"
$loadscreen = "E:\Code\open-roleplay\roleplay-framework\server-data\resources\openrp-loadscreen"

cmd /c mklink /D "$resources_path\openrp-loadscreen" $loadscreen
cmd /c mklink /D "$resources_path\framework" $framework
