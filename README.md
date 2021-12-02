# Annual Leave Request Tool
Simple Annual Leave Request Tool

* Allows users to create, update or delete annual leave requests.  
* The Annual leave requests are saved and read from a SQLServer database using Dapper as the ORM in the DAL (Data Access Layer) project  
* There are three GUI projects 
  *  Blazor Server/.Net 5
  *  Blazor WebAssembly/.Net 5 (uses Restful Web API project)
  *  MVC/.Net 5
* There are 2 Restful Web API projects
  * .Net 5 with controllers
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
