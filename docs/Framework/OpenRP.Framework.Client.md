<a name='assembly'></a>
# OpenRP.Framework.Client.net

## Contents

- [ChatController](#T-OpenRP-Framework-Client-Controllers-ChatController 'OpenRP.Framework.Client.Controllers.ChatController')
  - [AddMessage(r,g,b,message)](#M-OpenRP-Framework-Client-Controllers-ChatController-AddMessage-System-Int32,System-Int32,System-Int32,System-String- 'OpenRP.Framework.Client.Controllers.ChatController.AddMessage(System.Int32,System.Int32,System.Int32,System.String)')
- [ClientEvent](#T-OpenRP-Framework-Common-Enumeration-ClientEvent 'OpenRP.Framework.Common.Enumeration.ClientEvent')
  - [ADD_MESSAGE](#F-OpenRP-Framework-Common-Enumeration-ClientEvent-ADD_MESSAGE 'OpenRP.Framework.Common.Enumeration.ClientEvent.ADD_MESSAGE')
  - [COMMAND_TP](#F-OpenRP-Framework-Common-Enumeration-ClientEvent-COMMAND_TP 'OpenRP.Framework.Common.Enumeration.ClientEvent.COMMAND_TP')
  - [POST_MESSAGE](#F-OpenRP-Framework-Common-Enumeration-ClientEvent-POST_MESSAGE 'OpenRP.Framework.Common.Enumeration.ClientEvent.POST_MESSAGE')
  - [RESET_FOCUS](#F-OpenRP-Framework-Common-Enumeration-ClientEvent-RESET_FOCUS 'OpenRP.Framework.Common.Enumeration.ClientEvent.RESET_FOCUS')
- [ClientMain](#T-OpenRP-Framework-Client-ClientMain 'OpenRP.Framework.Client.ClientMain')
  - [Chat](#F-OpenRP-Framework-Client-ClientMain-Chat 'OpenRP.Framework.Client.ClientMain.Chat')
  - [Event](#F-OpenRP-Framework-Client-ClientMain-Event 'OpenRP.Framework.Client.ClientMain.Event')
  - [InitializeFiveMEvents()](#M-OpenRP-Framework-Client-ClientMain-InitializeFiveMEvents 'OpenRP.Framework.Client.ClientMain.InitializeFiveMEvents')
- [Commands](#T-OpenRP-Framework-Client-InternalPlugins-Commands 'OpenRP.Framework.Client.InternalPlugins.Commands')
- [EventController](#T-OpenRP-Framework-Client-Controllers-EventController 'OpenRP.Framework.Client.Controllers.EventController')
  - [RegisterEvent(eventName,callback)](#M-OpenRP-Framework-Client-Controllers-EventController-RegisterEvent-System-Enum,System-MulticastDelegate- 'OpenRP.Framework.Client.Controllers.EventController.RegisterEvent(System.Enum,System.MulticastDelegate)')
  - [RegisterNuiEvent(eventName,callback)](#M-OpenRP-Framework-Client-Controllers-EventController-RegisterNuiEvent-System-Enum,System-MulticastDelegate- 'OpenRP.Framework.Client.Controllers.EventController.RegisterNuiEvent(System.Enum,System.MulticastDelegate)')
  - [TriggerEvent(eventName,args)](#M-OpenRP-Framework-Client-Controllers-EventController-TriggerEvent-System-Enum,System-Object[]- 'OpenRP.Framework.Client.Controllers.EventController.TriggerEvent(System.Enum,System.Object[])')
  - [TriggerServerEvent(eventName,args)](#M-OpenRP-Framework-Client-Controllers-EventController-TriggerServerEvent-System-Enum,System-Object[]- 'OpenRP.Framework.Client.Controllers.EventController.TriggerServerEvent(System.Enum,System.Object[])')
- [GridCoord](#T-OpenRP-Framework-Client-Classes-GridCoord 'OpenRP.Framework.Client.Classes.GridCoord')
  - [GetHashCode()](#M-OpenRP-Framework-Client-Classes-GridCoord-GetHashCode 'OpenRP.Framework.Client.Classes.GridCoord.GetHashCode')
- [IEvent](#T-OpenRP-Framework-Common-Interface-IEvent 'OpenRP.Framework.Common.Interface.IEvent')
- [ServerEvent](#T-OpenRP-Framework-Common-Enumeration-ServerEvent 'OpenRP.Framework.Common.Enumeration.ServerEvent')
  - [COMMAND_VALIDATE](#F-OpenRP-Framework-Common-Enumeration-ServerEvent-COMMAND_VALIDATE 'OpenRP.Framework.Common.Enumeration.ServerEvent.COMMAND_VALIDATE')

<a name='T-OpenRP-Framework-Client-Controllers-ChatController'></a>
## ChatController `type`

##### Namespace

OpenRP.Framework.Client.Controllers

<a name='M-OpenRP-Framework-Client-Controllers-ChatController-AddMessage-System-Int32,System-Int32,System-Int32,System-String-'></a>
### AddMessage(r,g,b,message) `method`

##### Summary

Adds a new message bubble to the Message Box.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| r | [System.Int32](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Int32 'System.Int32') | Red value of the background color. |
| g | [System.Int32](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Int32 'System.Int32') | Green value of the background color. |
| b | [System.Int32](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Int32 'System.Int32') | Blue value of the background color. |
| message | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | The message to display in the message bubble. |

<a name='T-OpenRP-Framework-Common-Enumeration-ClientEvent'></a>
## ClientEvent `type`

##### Namespace

OpenRP.Framework.Common.Enumeration

##### Summary

Enum to house Client Event names

<a name='F-OpenRP-Framework-Common-Enumeration-ClientEvent-ADD_MESSAGE'></a>
### ADD_MESSAGE `constants`

##### Summary

Executes the OpenRP.Framework.Client.Controllers.MessageBox.AddMessage() method.

<a name='F-OpenRP-Framework-Common-Enumeration-ClientEvent-COMMAND_TP'></a>
### COMMAND_TP `constants`

##### Summary



<a name='F-OpenRP-Framework-Common-Enumeration-ClientEvent-POST_MESSAGE'></a>
### POST_MESSAGE `constants`

##### Summary

Triggers server event COMMAND_VALIDATE when the Message Box module posts user input.

<a name='F-OpenRP-Framework-Common-Enumeration-ClientEvent-RESET_FOCUS'></a>
### RESET_FOCUS `constants`

##### Summary

Returns focus from NUI back to the game.

<a name='T-OpenRP-Framework-Client-ClientMain'></a>
## ClientMain `type`

##### Namespace

OpenRP.Framework.Client

##### Summary

Main client-side entry point for the OpenRP Framework

<a name='F-OpenRP-Framework-Client-ClientMain-Chat'></a>
### Chat `constants`

##### Summary

Controller that handles the Message Box.

<a name='F-OpenRP-Framework-Client-ClientMain-Event'></a>
### Event `constants`

##### Summary

Controller that handles event registration and invocation.

<a name='M-OpenRP-Framework-Client-ClientMain-InitializeFiveMEvents'></a>
### InitializeFiveMEvents() `method`

##### Summary

Initializes Event Handlers for FiveM provided events.

##### Parameters

This method has no parameters.

<a name='T-OpenRP-Framework-Client-InternalPlugins-Commands'></a>
## Commands `type`

##### Namespace

OpenRP.Framework.Client.InternalPlugins

##### Summary

Commands internal plugin.

<a name='T-OpenRP-Framework-Client-Controllers-EventController'></a>
## EventController `type`

##### Namespace

OpenRP.Framework.Client.Controllers

<a name='M-OpenRP-Framework-Client-Controllers-EventController-RegisterEvent-System-Enum,System-MulticastDelegate-'></a>
### RegisterEvent(eventName,callback) `method`

##### Summary

Register a new client event.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| eventName | [System.Enum](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Enum 'System.Enum') | Event name from an enum. |
| callback | [System.MulticastDelegate](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.MulticastDelegate 'System.MulticastDelegate') | Method to call when the event is triggered. |

<a name='M-OpenRP-Framework-Client-Controllers-EventController-RegisterNuiEvent-System-Enum,System-MulticastDelegate-'></a>
### RegisterNuiEvent(eventName,callback) `method`

##### Summary

Register a new NUI event.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| eventName | [System.Enum](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Enum 'System.Enum') | Event name from an enum. |
| callback | [System.MulticastDelegate](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.MulticastDelegate 'System.MulticastDelegate') | Method to call when the event is triggered. |

<a name='M-OpenRP-Framework-Client-Controllers-EventController-TriggerEvent-System-Enum,System-Object[]-'></a>
### TriggerEvent(eventName,args) `method`

##### Summary

Triggers a registered client event.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| eventName | [System.Enum](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Enum 'System.Enum') | Event name from an enum. |
| args | [System.Object[]](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Object[] 'System.Object[]') | Arguments to pass to the triggered event callback. |

<a name='M-OpenRP-Framework-Client-Controllers-EventController-TriggerServerEvent-System-Enum,System-Object[]-'></a>
### TriggerServerEvent(eventName,args) `method`

##### Summary

Triggers a registered server event.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| eventName | [System.Enum](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Enum 'System.Enum') | Event name from an enum. |
| args | [System.Object[]](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Object[] 'System.Object[]') | Arguments to pass to the triggered event callback. |

<a name='T-OpenRP-Framework-Client-Classes-GridCoord'></a>
## GridCoord `type`

##### Namespace

OpenRP.Framework.Client.Classes

<a name='M-OpenRP-Framework-Client-Classes-GridCoord-GetHashCode'></a>
### GetHashCode() `method`

##### Summary

Get a unique grid ID.

##### Returns



##### Parameters

This method has no parameters.

<a name='T-OpenRP-Framework-Common-Interface-IEvent'></a>
## IEvent `type`

##### Namespace

OpenRP.Framework.Common.Interface

##### Summary



<a name='T-OpenRP-Framework-Common-Enumeration-ServerEvent'></a>
## ServerEvent `type`

##### Namespace

OpenRP.Framework.Common.Enumeration

##### Summary



<a name='F-OpenRP-Framework-Common-Enumeration-ServerEvent-COMMAND_VALIDATE'></a>
### COMMAND_VALIDATE `constants`

##### Summary

Validates inputs from the MessageBox.
