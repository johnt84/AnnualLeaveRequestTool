# Annual Leave Request Tool
Simple Annual Leave Request Tool

* Allows users to create, update or delete annual leave requests.  
* The Annual leave requests are saved and read from a SQLServer database using Dapper as the ORM in the DAL project  
* There are three GUI projects 
  *  Blazor Server/.Net 5
  *  Blazor WebAssembly/.Net 5 (uses API project)
  *  MVC/.Net 5
* There is a Restful Web API developed using .Net 5
* There is a SQLServer database project which can be used to set up an AnnualLeave DB on SSMS which can be used by the Annual Leave Request tool
* Live web app is deployed to an Azure web app and the database is deployed to an Azure SQL Database

## Blazor Server App

### Overview Page

![](Images/OverviewPage.png)

### Create Page

![](Images/CreatePage.png)

### Edit Page

![](Images/EditPage.png)
