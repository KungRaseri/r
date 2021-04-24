fx_version 'adamant'

games{'gta5'}

description 'Open Roleplay Framework'

ui_page 'ui/index.html/#/'

files{
	'ui/css/*.*',
	'ui/js/*.*',
	'ui/img/*.*',
	'ui/index.html',
	'client/Newtonsoft.Json.dll',
	'client/OpenRP.Framework.Common.dll'
}

client_scripts{
    'client/OpenRP.Framework.Client.net.dll'
}

server_scripts{
    'OpenRP.Framework.Server.net.dll'
}

shared_scripts{
}
