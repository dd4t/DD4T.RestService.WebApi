[![AppVeyor](https://ci.appveyor.com/api/projects/status/github/dd4t/DD4T.RestService.WebApi?branch=master&svg=true&passingText=master)](https://ci.appveyor.com/project/DD4T/dd4t-restservice)

[![AppVeyor](https://ci.appveyor.com/api/projects/status/github/dd4t/DD4T.RestService.WebApi?branch=develop&svg=true&passingText=develop)](https://ci.appveyor.com/project/DD4T/dd4t-restservice)


# DD4T.RestService.WebApi

DD4T RestService WebApi

**Install:**

1. Create a New project in Visual studio. 
	1. Select The ASP.NET Web Application template.
	2. Choose the Empty template.
3. Install one of the available DD4T Providers for Tridion.
4. Add The Tridion assemblies to the project references.
5. Install the DD4T.RestService.WebApi nuget package. `Install-Package DD4T.RestService.WebApi` 
6. Add Tridion needed configuration and jar files to the bin folder.
6. Build your application.


**Example**

To request a page from the Tridion broker.

    http://myservice/page/getcontentbyurl/{publicationId}/{extension}/{url} 

	http://myservice/page/getcontentbyurl/5/html/index 

