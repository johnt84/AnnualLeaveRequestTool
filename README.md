# Annual Leave Request Tool
Simple Annual Leave Request Tool

* Allows users to create, update or delete annual leave requests.  
* The Annual leave requests are saved and read from a SQLServer database using an ORM (default is Dapper) in the DAL (Data Access Layer) project  
* There are two DAL projects
  * Dapper ORM
  * Entity Framework 6
* There are 6 GUI projects (web application frameworks)
  *  Blazor Server Dapper/.Net 6 (communicates with Dapper DAL)
  *  Blazor Server EF/.Net 6 (communicates with EF DAL)
  *  Blazor WebAssembly/.Net 6 (uses Restful Web API project)
  *  MVC/.Net 6
  *  Razor Pages/.Net 6
  *  WebForms/.Net 4.8 (uses Restful Web API project)
* There are 2 Restful Web API projects
  * .Net 6 with controllers
  * .Net 6 Minimal API
* There is a SQLServer database project which can be used to set up an AnnualLeave DB on SSMS which can be used by the Annual Leave Request tool
* Live Blazor Server web app is deployed to an Azure web app and the database is deployed to an Azure SQL Database

## Blazor Server App

### Overview Page

![](Images/OverviewPage.png)

### Create Page

![](Images/CreatePage.png)

### Edit Page

![](Images/EditPage.png)
