# Nancy.Template.Webservice [![Mit License][mit-img]][mit]

Dotnet template library used to create web services using the Nancy web Framework.

## Builds

| Appveyor  |
| :---:     |
| [![Build status][build-img]][build] |

## Packages

| NuGet (Stable) | MyGet (Prerelease) |
| :---: | :---: |
| [![NuGet][nuget-img]][nuget] | [![MyGet][myget-img]][myget] |

### Purpose

A quick scaffolder for web services build using Nancyfx as the web framework. It provides the basic tools for validations, endpoint creation and database usage. As is the case with Nancy, it is relatively customizable.  

It can be altered to use:

1. multiple types of logs (as based on the serilog sinks).
1. different types of caches (as based on the rapid cache lib).
1. other types of ORMs (i. e. Dapper) by replacing the Repository class with byo.
1. alternatively move to swagger (legacy version) from openapi replacing the nancy.metadata.openapi with nancy.metadata.swagger library from nuget.

### Install

For installation via the dotnet install command:

`dotnet new -i "Nancy.Template.Webservice::*"`

For myget installations you can specify the source on the dotnet command:

`dotnet new -i "Nancy.Template.Webservice::*" --nuget-source https://www.myget.org/F/nancy-template-webservice/api/v3/index.json`

Then you can freely use it by executing the following dotnet command:

`dotnet new nancyws -o MySampleWs`

### Uninstall

To uninstall simply execute:

`dotnet new -u "Nancy.Template.Webservice"`

These projects target dotnet core 3.1. The following libraries are included as part of the projects:

* [Nancy](https://github.com/NancyFx/Nancy)
* [Nancy.Metadata.OpenApi](https://github.com/Jaxelr/Nancy.Metadata.OpenApi)
* [Nancy.RapidCache](https://github.com/Jaxelr/Nancy.RapidCache)
* [Nancy.Serilog](https://github.com/Zaid-Ajaj/Nancy.Serilog)
* [Serilog](https://github.com/serilog/serilog)
* [Microsoft.AspNetCore.Diagnostics.HealthChecks](https://github.com/dotnet/aspnetcore/tree/master/src/HealthChecks/HealthChecks)
* [Insight.Database](https://github.com/jonwagner/Insight.Database)
* [Xunit](https://github.com/xunit/xunit)
* [NSubstitute](https://github.com/nsubstitute/NSubstitute)
* [Altcover](https://github.com/SteveGilham/altcover)
* [ReportGenerator](https://github.com/danielpalme/ReportGenerator)

For further information on custom templates, refer to the [Microsoft documentation][docs].

[mit-img]: http://img.shields.io/badge/License-MIT-blue.svg
[mit]: https://github.com/Jaxelr/Nancy.Template.Webservice/blob/master/LICENSE
[build-img]: https://ci.appveyor.com/api/projects/status/4q831j12p00mkeij/branch/master?svg=true
[build]: https://ci.appveyor.com/project/Jaxelr/nancy-template-webservice/branch/master
[nuget-img]: https://img.shields.io/nuget/v/Nancy.Template.Webservice.svg
[nuget]: https://www.nuget.org/packages/Nancy.Template.Webservice/
[myget-img]: https://img.shields.io/myget/nancy-template-webservice/v/Nancy.Template.Webservice.svg
[myget]: https://www.myget.org/feed/nancy-template-webservice/package/nuget/Nancy.Template.Webservice
[docs]: https://docs.microsoft.com/en-us/dotnet/core/tools/custom-templates
