<a name='assembly'></a>
# OpenRP.Framework.Server.net

## Contents

- [ClientEvent](#T-OpenRP-Framework-Common-Enumeration-ClientEvent 'OpenRP.Framework.Common.Enumeration.ClientEvent')
  - [ADD_MESSAGE](#F-OpenRP-Framework-Common-Enumeration-ClientEvent-ADD_MESSAGE 'OpenRP.Framework.Common.Enumeration.ClientEvent.ADD_MESSAGE')
  - [COMMAND_TP](#F-OpenRP-Framework-Common-Enumeration-ClientEvent-COMMAND_TP 'OpenRP.Framework.Common.Enumeration.ClientEvent.COMMAND_TP')
- [CommandController](#T-OpenRP-Framework-Server-Controllers-CommandController 'OpenRP.Framework.Server.Controllers.CommandController')
  - [Register(name,command,help,args)](#M-OpenRP-Framework-Server-Controllers-CommandController-Register-System-String,System-MulticastDelegate,System-String,System-Collections-Generic-List{System-String}- 'OpenRP.Framework.Server.Controllers.CommandController.Register(System.String,System.MulticastDelegate,System.String,System.Collections.Generic.List{System.String})')
  - [Unregister(name)](#M-OpenRP-Framework-Server-Controllers-CommandController-Unregister-System-String- 'OpenRP.Framework.Server.Controllers.CommandController.Unregister(System.String)')
- [Commands](#T-OpenRP-Framework-Server-InternalPlugins-Commands 'OpenRP.Framework.Server.InternalPlugins.Commands')
- [EventController](#T-OpenRP-Framework-Server-Controllers-EventController 'OpenRP.Framework.Server.Controllers.EventController')
  - [RegisterEvent(eventName,callback)](#M-OpenRP-Framework-Server-Controllers-EventController-RegisterEvent-System-Enum,System-MulticastDelegate- 'OpenRP.Framework.Server.Controllers.EventController.RegisterEvent(System.Enum,System.MulticastDelegate)')
  - [TriggerClientEvent(player,eventName,args)](#M-OpenRP-Framework-Server-Controllers-EventController-TriggerClientEvent-CitizenFX-Core-Player,System-Enum,System-Object[]- 'OpenRP.Framework.Server.Controllers.EventController.TriggerClientEvent(CitizenFX.Core.Player,System.Enum,System.Object[])')
  - [TriggerClientEvent(eventName,args)](#M-OpenRP-Framework-Server-Controllers-EventController-TriggerClientEvent-System-Enum,System-Object[]- 'OpenRP.Framework.Server.Controllers.EventController.TriggerClientEvent(System.Enum,System.Object[])')
  - [TriggerEvent(eventName,args)](#M-OpenRP-Framework-Server-Controllers-EventController-TriggerEvent-System-Enum,System-Object[]- 'OpenRP.Framework.Server.Controllers.EventController.TriggerEvent(System.Enum,System.Object[])')
- [IEvent](#T-OpenRP-Framework-Common-Interface-IEvent 'OpenRP.Framework.Common.Interface.IEvent')
- [NuiEvent](#T-OpenRP-Framework-Common-Enumeration-NuiEvent 'OpenRP.Framework.Common.Enumeration.NuiEvent')
  - [POST_MESSAGE](#F-OpenRP-Framework-Common-Enumeration-NuiEvent-POST_MESSAGE 'OpenRP.Framework.Common.Enumeration.NuiEvent.POST_MESSAGE')
  - [RESET_FOCUS](#F-OpenRP-Framework-Common-Enumeration-NuiEvent-RESET_FOCUS 'OpenRP.Framework.Common.Enumeration.NuiEvent.RESET_FOCUS')
- [ServerEvent](#T-OpenRP-Framework-Common-Enumeration-ServerEvent 'OpenRP.Framework.Common.Enumeration.ServerEvent')
  - [COMMAND_VALIDATE](#F-OpenRP-Framework-Common-Enumeration-ServerEvent-COMMAND_VALIDATE 'OpenRP.Framework.Common.Enumeration.ServerEvent.COMMAND_VALIDATE')
- [ServerMain](#T-OpenRP-Framework-Server-ServerMain 'OpenRP.Framework.Server.ServerMain')

<a name='T-OpenRP-Framework-Common-Enumeration-ClientEvent'></a>
## ClientEvent `type`

##### Namespace

OpenRP.Framework.Common.Enumeration

<a name='F-OpenRP-Framework-Common-Enumeration-ClientEvent-ADD_MESSAGE'></a>
### ADD_MESSAGE `constants`

##### Summary

Executes the OpenRP.Framework.Client.Controllers.MessageBox.AddMessage() method.

<a name='F-OpenRP-Framework-Common-Enumeration-ClientEvent-COMMAND_TP'></a>
### COMMAND_TP `constants`

##### Summary

Invokes the teleport command.

<a name='T-OpenRP-Framework-Server-Controllers-CommandController'></a>
## CommandController `type`

##### Namespace

OpenRP.Framework.Server.Controllers

##### Summary

Controller object that handles server commands.

<a name='M-OpenRP-Framework-Server-Controllers-CommandController-Register-System-String,System-MulticastDelegate,System-String,System-Collections-Generic-List{System-String}-'></a>
### Register(name,command,help,args) `method`

##### Summary

Registers a server command.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| name | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | The name of the command. Can only contain lowercase letters. |
| command | [System.MulticastDelegate](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.MulticastDelegate 'System.MulticastDelegate') | A method that is called when the command is triggered. |
| help | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | A description of what the command does. |
| args | [System.Collections.Generic.List{System.String}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Collections.Generic.List 'System.Collections.Generic.List{System.String}') | A list of descriptions for each argument passed with the command. |

<a name='M-OpenRP-Framework-Server-Controllers-CommandController-Unregister-System-String-'></a>
### Unregister(name) `method`

##### Summary

Unregisters a server command.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| name | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | The name of the command to unregister. |

<a name='T-OpenRP-Framework-Server-InternalPlugins-Commands'></a>
## Commands `type`

##### Namespace

OpenRP.Framework.Server.InternalPlugins

##### Summary

Commands internal plugin.

<a name='T-OpenRP-Framework-Server-Controllers-EventController'></a>
## EventController `type`

##### Namespace

OpenRP.Framework.Server.Controllers

<a name='M-OpenRP-Framework-Server-Controllers-EventController-RegisterEvent-System-Enum,System-MulticastDelegate-'></a>
### RegisterEvent(eventName,callback) `method`

##### Summary

Register a new server event.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| eventName | [System.Enum](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Enum 'System.Enum') | Event name from an enum. |
| callback | [System.MulticastDelegate](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.MulticastDelegate 'System.MulticastDelegate') | Method to call when the event is triggered. |

<a name='M-OpenRP-Framework-Server-Controllers-EventController-TriggerClientEvent-CitizenFX-Core-Player,System-Enum,System-Object[]-'></a>
### TriggerClientEvent(player,eventName,args) `method`

##### Summary

Triggers a registered client event and sends it to a sepcific client.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| player | [CitizenFX.Core.Player](#T-CitizenFX-Core-Player 'CitizenFX.Core.Player') | The player object of the client to invoke the callback on. |
| eventName | [System.Enum](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Enum 'System.Enum') | Event name from an enum. |
| args | [System.Object[]](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Object[] 'System.Object[]') | Arguments to pass to the triggered event callback. |

<a name='M-OpenRP-Framework-Server-Controllers-EventController-TriggerClientEvent-System-Enum,System-Object[]-'></a>
### TriggerClientEvent(eventName,args) `method`

##### Summary

Triggers a registered client event and sends it to all clients.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| eventName | [System.Enum](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Enum 'System.Enum') | Event name from an enum. |
| args | [System.Object[]](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Object[] 'System.Object[]') | Arguments to pass to the triggered event callback. |

<a name='M-OpenRP-Framework-Server-Controllers-EventController-TriggerEvent-System-Enum,System-Object[]-'></a>
### TriggerEvent(eventName,args) `method`

##### Summary

Triggers a registered server event.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| eventName | [System.Enum](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Enum 'System.Enum') | Event name from an enum. |
| args | [System.Object[]](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Object[] 'System.Object[]') | Arguments to pass to the triggered event callback. |

<a name='T-OpenRP-Framework-Common-Interface-IEvent'></a>
## IEvent `type`

##### Namespace

OpenRP.Framework.Common.Interface

##### Summary



<a name='T-OpenRP-Framework-Common-Enumeration-NuiEvent'></a>
## NuiEvent `type`

##### Namespace

OpenRP.Framework.Common.Enumeration

<a name='F-OpenRP-Framework-Common-Enumeration-NuiEvent-POST_MESSAGE'></a>
### POST_MESSAGE `constants`

##### Summary

Triggers server event COMMAND_VALIDATE when the Message Box module posts user input.

<a name='F-OpenRP-Framework-Common-Enumeration-NuiEvent-RESET_FOCUS'></a>
### RESET_FOCUS `constants`

##### Summary

Returns focus from NUI back to the game.

<a name='T-OpenRP-Framework-Common-Enumeration-ServerEvent'></a>
## ServerEvent `type`

##### Namespace

OpenRP.Framework.Common.Enumeration

<a name='F-OpenRP-Framework-Common-Enumeration-ServerEvent-COMMAND_VALIDATE'></a>
### COMMAND_VALIDATE `constants`

##### Summary

Validates inputs from the MessageBox.

<a name='T-OpenRP-Framework-Server-ServerMain'></a>
## ServerMain `type`

##### Namespace

OpenRP.Framework.Server

##### Summary


