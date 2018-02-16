# centralconfig-webapi [![Build status](https://ci.appveyor.com/api/projects/status/816dvi07m8tg9i8k?svg=true)](https://ci.appveyor.com/project/danesparza/centralconfig-webapi)
ASP.NET WebAPI and Entity Framework version of the centralconfig service.  Suitable for IIS based deployments

## Quick start

Grab the [latest release](https://github.com/cagedtornado/centralconfig-webapi/releases/latest), unzip, and [create an IIS site](https://support.microsoft.com/en-us/help/323972/how-to-set-up-your-first-iis-web-site) that points to the unzipped location.

Don't forget to [create the database](https://github.com/cagedtornado/centralconfig-webapi/blob/master/sql/centralconfig.sql) and set the [web.config connection strings](https://github.com/cagedtornado/centralconfig-webapi/blob/master/centralconfig-webapi/Web.config#L14) 

