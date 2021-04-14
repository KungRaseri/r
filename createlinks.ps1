$resources_path = "D:\fivem\server-data-redacted\resources"
$framework_server = "E:\Code\open-roleplay\roleplay-framework\server-data\resources\framework_server"
$framework_client = "E:\Code\open-roleplay\roleplay-framework\server-data\resources\framework_client"

cmd /c mklink /D "$resources_path\framework_server" $framework_server
cmd /c mklink /D "$resources_path\framework_client" $framework_client
