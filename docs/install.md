> **_Note:_** Ditto has been developed against **Umbraco v6.2.5** and will support that version and above.

Ditto can be installed from either Our Umbraco or NuGet package repositories, or build manually from the source-code:

## Our Umbraco package repository

To install from Our Umbraco, please download the package from:

> [http://our.umbraco.org/projects/developer-tools/ditto](http://our.umbraco.org/projects/developer-tools/ditto) 

## NuGet package repository

To [install from NuGet](https://www.nuget.org/packages/Our.Umbraco.Ditto), you can run the following command from within Visual Studio:

```bash
PM> Install-Package Our.Umbraco.Ditto
```

We also have a [MyGet package repository](https://www.myget.org/gallery/umbraco-packages) - for bleeding-edge / development releases.

## Manual build

If you prefer, you can compile Ditto yourself, you'll need:

* [Visual Studio 2017 (or above, including Community Editions)](https://www.visualstudio.com/downloads/)
* Microsoft Build Tools 2015 (aka [MSBuild 15](https://www.microsoft.com/en-us/download/details.aspx?id=48159))

To clone it locally run the following git commands:

```bash
git clone https://github.com/umco/umbraco-ditto.git umbraco-ditto
cd umbraco-ditto
.\build.cmd
```
