<a name='assembly'></a>
# OpenRP.Framework.Server.net

## Contents

- [ClientEvent](#T-OpenRP-Framework-Common-Enumeration-ClientEvent 'OpenRP.Framework.Common.Enumeration.ClientEvent')
- [CommandController](#T-OpenRP-Framework-Server-Controllers-CommandController 'OpenRP.Framework.Server.Controllers.CommandController')
  - [Register(name,command,help,args)](#M-OpenRP-Framework-Server-Controllers-CommandController-Register-System-String,System-Action{System-Int32},System-String,System-Collections-Generic-List{System-String}- 'OpenRP.Framework.Server.Controllers.CommandController.Register(System.String,System.Action{System.Int32},System.String,System.Collections.Generic.List{System.String})')
  - [Unregister(name)](#M-OpenRP-Framework-Server-Controllers-CommandController-Unregister-System-String- 'OpenRP.Framework.Server.Controllers.CommandController.Unregister(System.String)')
- [EventType](#T-OpenRP-Framework-Common-Enumeration-EventType 'OpenRP.Framework.Common.Enumeration.EventType')
- [IEvent](#T-OpenRP-Framework-Common-Interface-IEvent 'OpenRP.Framework.Common.Interface.IEvent')
- [ServerEvent](#T-OpenRP-Framework-Common-Enumeration-ServerEvent 'OpenRP.Framework.Common.Enumeration.ServerEvent')
- [ServerMain](#T-OpenRP-Framework-Server-ServerMain 'OpenRP.Framework.Server.ServerMain')

<a name='T-OpenRP-Framework-Common-Enumeration-ClientEvent'></a>
## ClientEvent `type`

##### Namespace

OpenRP.Framework.Common.Enumeration

##### Summary

Enum to house Client Event names

<a name='T-OpenRP-Framework-Server-Controllers-CommandController'></a>
## CommandController `type`

##### Namespace

OpenRP.Framework.Server.Controllers

##### Summary

Controller object that handles server commands.

<a name='M-OpenRP-Framework-Server-Controllers-CommandController-Register-System-String,System-Action{System-Int32},System-String,System-Collections-Generic-List{System-String}-'></a>
### Register(name,command,help,args) `method`

##### Summary

Registers a server command.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| name | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | The name of the command. Can only contain lowercase letters. |
| command | [System.Action{System.Int32}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Action 'System.Action{System.Int32}') | A method that is called when the command is triggered. |
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

<a name='T-OpenRP-Framework-Common-Enumeration-EventType'></a>
## EventType `type`

##### Namespace

OpenRP.Framework.Common.Enumeration

##### Summary



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



<a name='T-OpenRP-Framework-Server-ServerMain'></a>
## ServerMain `type`

##### Namespace

OpenRP.Framework.Server

##### Summary


