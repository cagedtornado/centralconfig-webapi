# centralconfig-webapi [![Build status](https://ci.appveyor.com/api/projects/status/816dvi07m8tg9i8k?svg=true)](https://ci.appveyor.com/project/danesparza/centralconfig-webapi)
Easy centralized application configuration for .NET apps.  This is an ASP.NET WebAPI and Entity Framework version of the [centralconfig service](https://github.com/cagedtornado/centralconfig).  Suitable for IIS based deployments.

## Quick start

* Grab the [latest release](https://github.com/cagedtornado/centralconfig-webapi/releases/latest), 
* Unzip 
* [Create an IIS site](https://support.microsoft.com/en-us/help/323972/how-to-set-up-your-first-iis-web-site) that points to the unzipped location
* [Create the database using the script](https://github.com/cagedtornado/centralconfig-webapi/blob/master/sql/centralconfig.sql) (or the dacpac) and set the [web.config connection strings](https://github.com/cagedtornado/centralconfig-webapi/blob/master/centralconfig-webapi/Web.config#L14) 

## API server information

API path              | Description
----------            | -----------
/                     | Redirects to the /UI path (and the [web based user interface](https://github.com/cagedtornado/centralconfig-ui))
[/config/get](https://github.com/cagedtornado/centralconfig-webapi#configget)           | Gets a single configuration item
[/config/set](https://github.com/cagedtornado/centralconfig-webapi#configset)           | Sets (creates or updates) a configuration item
[/config/remove](https://github.com/cagedtornado/centralconfig-webapi#configremove)        | Removes a configuration item
[/config/getall](https://github.com/cagedtornado/centralconfig-webapi#configgetall)      | Gets all configuration items
[/config/getallforapp](https://github.com/cagedtornado/centralconfig-webapi#configgetallforapp)  | Get all configuration items for a single application (plus the default * application)
[/applications/getall](https://github.com/cagedtornado/centralconfig-webapi#applicationsgetall)  | Get all applications

#### Requests
Most API operations expect a configitem object in the POST body that will be used to either filter (in a get operation), update or create (in a set operation), or remove an item (in a remove operation).  

For example:

```json
{
    "application" : "AccountingReports",
    "name": "ShowObscureFactoids",
    "value": "true"
}
```

#### Responses
All operations will return an object that contain the fields status, message, and data.  

For Example:
```json
{
  "status": 200,
  "message": "Config item found",
  "data": {
    "id": 6,
    "application": "AccountingReports",
    "machine": "",
    "name": "ShowFooterDates",
    "value": "true",
    "updated": "2016-08-11T14:49:38.1555535-04:00"
  }
}
```


### /config/get

This operation retrieves a single configuration item.  If it doesn't exist for the given application, it attemps to get it for the default application (*). 

This is an HTTP `POST` request

###### Example request:
```json
{
    "application" : "AccountingReports",
    "name" : "ShowFooterDates" 
}
```

###### Example response:
```json
{
  "status": 200,
  "message": "Config item found",
  "data": {
    "id": 6,
    "application": "AccountingReports",
    "machine": "",
    "name": "ShowFooterDates",
    "value": "true",
    "updated": "2016-08-11T14:49:38.1555535-04:00"
  }
}
```

### /config/set

This operation sets the value of a single configuration item

This is an HTTP `POST` request

###### Example request:
```json
{
    "application" : "AccountingReports",
    "name" : "ShowHeaderValues",
    "value": "false"
}
```

###### Example response:
```json
{
  "status": 200,
  "message": "Config item updated",
  "data": {
    "id": 10,
    "application": "AccountingReports",
    "machine": "",
    "name": "ShowHeaderValues",
    "value": "false",
    "updated": "2016-08-11T14:58:16.0132648-04:00"
  }
}
```

### /config/remove

This operation removes a single configuration item

###### Example request:
```json
{
    "application" : "AccountingReports",
    "name" : "ShowHeaderValues"
}
```

###### Example response:
```json
{
  "status": 200,
  "message": "Config item removed",
  "data": {
    "id": 0,
    "application": "AccountingReports",
    "machine": "",
    "name": "ShowHeaderValues",
    "value": "",
    "updated": "0001-01-01T00:00:00Z"
  }
}
```

### /config/getall

This operation retrieves all configuration items. 

This is a `GET` request.

###### Example response:
```json
{
  "status": 200,
  "message": "Config items found",
  "data": [
    {
      "id": 7,
      "application": "AccountingReports",
      "machine": "",
      "name": "Name",
      "value": "Accounting reporting system",
      "updated": "2016-08-11T14:50:17.3451641-04:00"
    },
    {
      "id": 6,
      "application": "AccountingReports",
      "machine": "",
      "name": "ShowFooterDates",
      "value": "true",
      "updated": "2016-08-11T14:49:38.1555535-04:00"
    },
    {
      "id": 8,
      "application": "ITSupportDesk",
      "machine": "",
      "name": "ShowEmailLinks",
      "value": "false",
      "updated": "2016-08-11T14:50:51.4152237-04:00"
    },
    {
      "id": 9,
      "application": "ITSupportDesk",
      "machine": "",
      "name": "SupportNumber",
      "value": "1 (415) 344-3200",
      "updated": "2016-08-11T14:52:53.4456194-04:00"
    }
  ]
}
```

### /config/getallforapp

This operation retrieves all configuration items for a specified application

This is an HTTP `POST` operation

###### Example request:
```json
{
    "application" : "AccountingReports"
}
```

###### Example response:
```json
{
  "status": 200,
  "message": "Config items found",
  "data": [
    {
      "id": 7,
      "application": "AccountingReports",
      "machine": "",
      "name": "Name",
      "value": "Accounting reporting system",
      "updated": "2016-08-11T14:50:17.3451641-04:00"
    },
    {
      "id": 6,
      "application": "AccountingReports",
      "machine": "",
      "name": "ShowFooterDates",
      "value": "true",
      "updated": "2016-08-11T14:49:38.1555535-04:00"
    }
  ]
}
```

### /applications/getall

This operation retrieves all applications

This is an HTTP `GET` operation.

###### Example response:
```json
{
  "status": 200,
  "message": "Applications found",
  "data": [
    "AccountingReports",
    "AnotherApp",
    "DouglasAdams",
    "FormBuilder",
    "ITSupportDesk",
    "SomeOtherAppEntirely"
  ]
}
```
