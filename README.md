# Nancy.Template.Core [![Mit License][mit-img]][mit]

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

I tend to need a quick scaffolder for web services that provide various types of data structures in a brief time. Sometimes as a prototype, other times for production purposes, so i developed this dotnet template in order to use it as a quick checklist of the needs i accustome to include on my WS. It is very opinionated, but so far has given me the results i need, with decent performance.

It can be customized to use:

1. multiple types of logs (as based on the serilog infrastructure).
1. different types of caches (as based on the rapid cache lib).
1. other types of ORMs (i. e. Dapper) by replacing the Repository class with byo.
1. alternatively move from swagger to openapi replace the nancy.metadata.swagger with nancy.metadata.openapi.

### Install

For installation via the dotnet install command:

`dotnet new -i "Nancy.Template.Webservice::*"`

Then you can freely use it by executing the following dotnet command:

`dotnet new nancyws -o MySampleWs`

### Uninstall

To uninstall simply execute:

`dotnet new -u "Nancy.Template.Webservice"`

These projects target dotnet core 2.2. The following libraries are included as part of the projects:

* [Nancy](https://github.com/NancyFx/Nancy)
* [Nancy.Metadata.Swagger.Core](https://github.com/Jaxelr/Nancy.Metadata.Swagger.Core)
* [Nancy.RapidCache](https://github.com/Jaxelr/Nancy.RapidCache)
* [Nancy.Serilog.Core](https://github.com/Zaid-Ajaj/Nancy.Serilog)
* [Serilog](https://github.com/serilog/serilog)
* [Newtonsoft.Json](https://github.com/JamesNK/Newtonsoft.Json)
* [Microsoft.AspNetCore.HealthChecks](https://github.com/dotnet-architecture/HealthChecks)
* [Insight.Database](https://github.com/jonwagner/Insight.Database)
* [Xunit](https://github.com/xunit/xunit)
* [NSubstitute](https://github.com/nsubstitute/NSubstitute)
* [Altcover](https://github.com/SteveGilham/altcover)
* [ReportGenerator](https://github.com/danielpalme/ReportGenerator)

For further information on custom templates, refer to the [Microsoft documentation][docs].


[mit-img]: http://img.shields.io/badge/License-MIT-blue.svg
[mit]: https://github.com/Jaxelr/VueSimpleTemplate/blob/master/LICENSE
[build-img]: https://ci.appveyor.com/api/projects/status/5jqqkr53l24b6ccj/branch/master?svg=true
[build]: https://ci.appveyor.com/project/Jaxelr/nancy-template-aspnetcore/branch/master
[nuget-img]: https://img.shields.io/nuget/v/Nancy.Template.Webservice.svg
[nuget]: https://www.nuget.org/packages/Nancy.Template.Webservice/
[myget-img]: https://img.shields.io/myget/nancy-template-webservice/v/Nancy.Template.Webservice.svg
[myget]: https://www.myget.org/feed/nancy-template-webservice/package/nuget/Nancy.Template.Webservice
[docs]: https://docs.microsoft.com/en-us/dotnet/core/tools/custom-templates
