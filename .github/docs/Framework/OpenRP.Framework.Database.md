<a name='assembly'></a>
# OpenRP.Framework.Database

## Contents

- [Account](#T-OpenRP-Framework-Database-Document-Account 'OpenRP.Framework.Database.Document.Account')
- [ClientEvent](#T-OpenRP-Framework-Common-Enumeration-ClientEvent 'OpenRP.Framework.Common.Enumeration.ClientEvent')
  - [ADD_MESSAGE](#F-OpenRP-Framework-Common-Enumeration-ClientEvent-ADD_MESSAGE 'OpenRP.Framework.Common.Enumeration.ClientEvent.ADD_MESSAGE')
  - [COMMAND_TP](#F-OpenRP-Framework-Common-Enumeration-ClientEvent-COMMAND_TP 'OpenRP.Framework.Common.Enumeration.ClientEvent.COMMAND_TP')
- [DocumentRepository\`1](#T-OpenRP-Framework-Database-DocumentRepository`1 'OpenRP.Framework.Database.DocumentRepository`1')
  - [#ctor()](#M-OpenRP-Framework-Database-DocumentRepository`1-#ctor-MongoDB-Driver-IMongoDatabase- 'OpenRP.Framework.Database.DocumentRepository`1.#ctor(MongoDB.Driver.IMongoDatabase)')
- [IDocument](#T-OpenRP-Framework-Database-IDocument 'OpenRP.Framework.Database.IDocument')
- [IEvent](#T-OpenRP-Framework-Common-Interface-IEvent 'OpenRP.Framework.Common.Interface.IEvent')
- [NuiEvent](#T-OpenRP-Framework-Common-Enumeration-NuiEvent 'OpenRP.Framework.Common.Enumeration.NuiEvent')
  - [POST_MESSAGE](#F-OpenRP-Framework-Common-Enumeration-NuiEvent-POST_MESSAGE 'OpenRP.Framework.Common.Enumeration.NuiEvent.POST_MESSAGE')
  - [RESET_FOCUS](#F-OpenRP-Framework-Common-Enumeration-NuiEvent-RESET_FOCUS 'OpenRP.Framework.Common.Enumeration.NuiEvent.RESET_FOCUS')
- [ServerEvent](#T-OpenRP-Framework-Common-Enumeration-ServerEvent 'OpenRP.Framework.Common.Enumeration.ServerEvent')
  - [COMMAND_VALIDATE](#F-OpenRP-Framework-Common-Enumeration-ServerEvent-COMMAND_VALIDATE 'OpenRP.Framework.Common.Enumeration.ServerEvent.COMMAND_VALIDATE')

<a name='T-OpenRP-Framework-Database-Document-Account'></a>
## Account `type`

##### Namespace

OpenRP.Framework.Database.Document

##### Summary

The Account document

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

<a name='T-OpenRP-Framework-Database-DocumentRepository`1'></a>
## DocumentRepository\`1 `type`

##### Namespace

OpenRP.Framework.Database

##### Summary



##### Generic Types

| Name | Description |
| ---- | ----------- |
| TDocument |  |

<a name='M-OpenRP-Framework-Database-DocumentRepository`1-#ctor-MongoDB-Driver-IMongoDatabase-'></a>
### #ctor() `constructor`

##### Summary



##### Parameters

This constructor has no parameters.

<a name='T-OpenRP-Framework-Database-IDocument'></a>
## IDocument `type`

##### Namespace

OpenRP.Framework.Database

##### Summary

Document interface

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
