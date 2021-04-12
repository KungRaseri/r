<a name='assembly'></a>
# OpenRP.Framework.Server.net

## Contents

- [ClientEvent](#T-ClientEvent 'ClientEvent')
- [CommandController](#T-OpenRP-Framework-Server-Controllers-CommandController 'OpenRP.Framework.Server.Controllers.CommandController')
  - [#ctor(server)](#M-OpenRP-Framework-Server-Controllers-CommandController-#ctor-OpenRP-Framework-Server-ServerMain- 'OpenRP.Framework.Server.Controllers.CommandController.#ctor(OpenRP.Framework.Server.ServerMain)')
  - [Commands](#P-OpenRP-Framework-Server-Controllers-CommandController-Commands 'OpenRP.Framework.Server.Controllers.CommandController.Commands')
  - [Register(name,command,help,args)](#M-OpenRP-Framework-Server-Controllers-CommandController-Register-System-String,System-Action{System-Int32},System-String,System-Collections-Generic-List{System-String}- 'OpenRP.Framework.Server.Controllers.CommandController.Register(System.String,System.Action{System.Int32},System.String,System.Collections.Generic.List{System.String})')
  - [Unregister(name)](#M-OpenRP-Framework-Server-Controllers-CommandController-Unregister-System-String- 'OpenRP.Framework.Server.Controllers.CommandController.Unregister(System.String)')
- [EventType](#T-EventType 'EventType')
- [IDocument](#T-IDocument 'IDocument')
- [IEvent](#T-IEvent 'IEvent')
- [ServerCommand](#T-OpenRP-Framework-Server-Controllers-ServerCommand 'OpenRP.Framework.Server.Controllers.ServerCommand')
  - [#ctor(name,command,help,args)](#M-OpenRP-Framework-Server-Controllers-ServerCommand-#ctor-System-String,System-Action{System-Int32},System-String,System-Collections-Generic-List{System-String}- 'OpenRP.Framework.Server.Controllers.ServerCommand.#ctor(System.String,System.Action{System.Int32},System.String,System.Collections.Generic.List{System.String})')
  - [Args](#P-OpenRP-Framework-Server-Controllers-ServerCommand-Args 'OpenRP.Framework.Server.Controllers.ServerCommand.Args')
  - [Command](#P-OpenRP-Framework-Server-Controllers-ServerCommand-Command 'OpenRP.Framework.Server.Controllers.ServerCommand.Command')
  - [Help](#P-OpenRP-Framework-Server-Controllers-ServerCommand-Help 'OpenRP.Framework.Server.Controllers.ServerCommand.Help')
  - [Name](#P-OpenRP-Framework-Server-Controllers-ServerCommand-Name 'OpenRP.Framework.Server.Controllers.ServerCommand.Name')
- [ServerEvent](#T-ServerEvent 'ServerEvent')

<a name='T-ClientEvent'></a>
## ClientEvent `type`

##### Namespace



##### Summary



<a name='T-OpenRP-Framework-Server-Controllers-CommandController'></a>
## CommandController `type`

##### Namespace

OpenRP.Framework.Server.Controllers

##### Summary

A controller that handles server commands.

<a name='M-OpenRP-Framework-Server-Controllers-CommandController-#ctor-OpenRP-Framework-Server-ServerMain-'></a>
### #ctor(server) `constructor`

##### Summary

A controller that handles server commands.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| server | [OpenRP.Framework.Server.ServerMain](#T-OpenRP-Framework-Server-ServerMain 'OpenRP.Framework.Server.ServerMain') | The server instance. |

<a name='P-OpenRP-Framework-Server-Controllers-CommandController-Commands'></a>
### Commands `property`

##### Summary

Holds all of the registered server commands.

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

<a name='T-EventType'></a>
## EventType `type`

##### Namespace



##### Summary



<a name='T-IDocument'></a>
## IDocument `type`

##### Namespace



##### Summary



<a name='T-IEvent'></a>
## IEvent `type`

##### Namespace



##### Summary



<a name='T-OpenRP-Framework-Server-Controllers-ServerCommand'></a>
## ServerCommand `type`

##### Namespace

OpenRP.Framework.Server.Controllers

##### Summary

Class that contains server command definitions.

<a name='M-OpenRP-Framework-Server-Controllers-ServerCommand-#ctor-System-String,System-Action{System-Int32},System-String,System-Collections-Generic-List{System-String}-'></a>
### #ctor(name,command,help,args) `constructor`

##### Summary

Class that contains server command definitions.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| name | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | The name of the command. Can only contain lowercase letters. |
| command | [System.Action{System.Int32}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Action 'System.Action{System.Int32}') | A method that is called when the command is triggered. |
| help | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | A description of what the command does. |
| args | [System.Collections.Generic.List{System.String}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Collections.Generic.List 'System.Collections.Generic.List{System.String}') | A list of descriptions for each argument passed with the command. |

<a name='P-OpenRP-Framework-Server-Controllers-ServerCommand-Args'></a>
### Args `property`

##### Summary

Argument descriptions.

<a name='P-OpenRP-Framework-Server-Controllers-ServerCommand-Command'></a>
### Command `property`

##### Summary

The method called when executing the command.

<a name='P-OpenRP-Framework-Server-Controllers-ServerCommand-Help'></a>
### Help `property`

##### Summary

Description of the command.

<a name='P-OpenRP-Framework-Server-Controllers-ServerCommand-Name'></a>
### Name `property`

##### Summary

The name of the command.

<a name='T-ServerEvent'></a>
## ServerEvent `type`

##### Namespace



##### Summary


