\# Find My Cleaner



\## Project Overview



Find My Cleaner is a full-stack ASP.NET Core application developed using the Model-View-Controller (MVC) architecture and a RESTful Web API. The project demonstrates how a modern web application communicates with a Web API to manage cleaning service information stored in MongoDB Atlas.



The application allows users to:



\* Register and log into the system using ASP.NET Core Identity.

\* View all available cleaning services.

\* Create new cleaning service records.

\* Edit existing cleaning service information.

\* Delete cleaning service records.

\* Search cleaning services by keyword.

\* Filter services by suburb, service type, and maximum price.

\* Sort cleaning services by hourly price.

\* Demonstrate API Versioning (Version 1 and Version 2).

\* Store business data in MongoDB Atlas while authentication data is managed separately by SQL Server.



\---



\## ⚡ Project Status: MVP (Minimum Viable Product)



This application was developed as a Minimum Viable Product (MVP) to satisfy the assessment requirements. The core functionality has been implemented, tested, and documented.



\---



\## ⚠️ Current Limitations



The current version of Find My Cleaner was developed as an MVP. Several limitations exist in the current implementation:



\* Business data is stored in MongoDB Atlas, while user authentication data is stored separately in SQL Server using ASP.NET Core Identity.

\* Basic input validation is implemented in the MVC Web Application to improve user experience. The Web API performs only essential request validation and returns appropriate HTTP status codes.

\* The project does not currently implement role-based authorization.

\* Search functionality supports filtering, keyword searching, and sorting but does not include pagination for large datasets.

\* Logging and exception handling are implemented at a basic level suitable for an assessment project.

\* No automated unit testing or integration testing has been implemented.



\---



\## Submitted Project Structure



```text





FindMyCleaner/

├── FindMyCleaner.sln

├── Controllers

├── Models

├── Services

├── Program.cs

├── appsettings.json

└── FindMyCleanerAPI.csproj



FindMyCleanerWeb/

├── Controllers

├── Models

├── Views

├── wwwroot

├── Program.cs

├── appsettings.json

└── FindMyCleanerWeb.csproj



README.md

```



\---



\## 📌 Prerequisites



To build and run the application, ensure the following software is installed:



\* Visual Studio 2022 or later

\* .NET 8 SDK (or the project target framework)

\* SQL Server LocalDB (for ASP.NET Core Identity)

\* MongoDB Atlas account

\* Internet connection to restore NuGet packages



\---



\## Required NuGet Packages



The project depends on the following major NuGet packages.



\### FindMyCleanerAPI



\- MongoDB.Driver

\- Asp.Versioning.Mvc

\- Microsoft.AspNetCore.OpenApi

\- Swashbuckle.AspNetCore



\### FindMyCleanerWeb



\- Microsoft.AspNetCore.Identity.EntityFrameworkCore

\- Microsoft.EntityFrameworkCore.SqlServer

\- Microsoft.EntityFrameworkCore.Tools

\- Microsoft.EntityFrameworkCore.Design



Restore all NuGet packages before building the solution.



\---



\## 🛠️ How to Build



1\. Open \*\*FindMyCleaner.sln\*\* in Visual Studio.

2\. Restore all NuGet packages.

3\. Contact the project author to obtain the required \*\*appsettings.json\*\* file containing the MongoDB Atlas connection settings (username and password).

4\. Replace the existing \*\*appsettings.json\*\* files in both \*\*FindMyCleanerAPI\*\* and \*\*FindMyCleanerWeb\*\* with the provided configuration.

5\. Build the solution.

6\. Verify that both the Web API and MVC Web Application build successfully.

\---



\## 🛠️ How to Run



1\. Start \*\*FindMyCleanerAPI\*\*.

2\. Start \*\*FindMyCleanerWeb\*\*.

3\. Open the web application in your browser.

4\. Use the application to manage cleaning service records.



\---



\## 🛠️ Features



\### User Authentication



\* User Registration

\* User Login

\* ASP.NET Core Identity

\* SQL Server authentication database



\### Cleaning Service Management



Users can:



\* View all services

\* Create cleaning service records

\* Edit existing services

\* Delete services



\### Search and Filtering



Users can search using one of the following methods:



\- \*\*Criteria Search\*\* – Search by Suburb, Service Type, and Maximum Hourly Price.

\- \*\*Keyword Search\*\* – Search across multiple fields using a keyword.



To provide clear and predictable search results, the application supports only one search method at a time. 

Selecting one method automatically clears the other search inputs.



The application also supports price or suburb sorting and API Version selection.





\### RESTful Web API



The API provides:



\* GET

\* POST

\* PUT

\* DELETE

\* Query String Filtering

\* Keyword Search using MongoDB Regular Expressions (Regex)

\* Price Sorting

\* API Versioning (v1 \& v2)

\* Appropriate HTTP Status Codes



\---



\## 🛠️ Maintenance Notes



Future developers should follow the existing separation of responsibilities:



\* \*\*FindMyCleanerWeb\*\* manages the user interface and communicates with the API using HttpClient.

\* \*\*FindMyCleanerAPI Controllers\*\* handle HTTP requests and responses.

\* \*\*CleaningServicesService\*\* manages MongoDB database operations.

\* \*\*MongoDB Atlas\*\* stores business data.

\* \*\*ASP.NET Core Identity\*\* manages authentication and authorization using SQL Server.



When modifying the application:



\* Maintain the separation between the MVC application and the Web API.

\* Keep authentication data separate from business data.

\* Preserve API Versioning compatibility.

\* Continue using asynchronous programming (`async`/`await`) for all API calls.

\* Implement additional API validation and role-based authorization for production environments.



\---



\## Future Improvements



Potential future enhancements include:



\* Role-based authorization

\* FluentValidation for API validation

\* Pagination for large datasets

\* More comprehensive indexing strategy for improved search and filtering performance

\* Improved MongoDB document structure and data modelling for future scalability

\* Docker container deployment

\* CI/CD pipeline

\* API documentation



\---



\## Author



\*\*Yvonne (Fu-Lan) Chen\*\*



ICT50220 Diploma of Information Technology



TAFE International Western Australia



Assessment Project – ASP.NET Core MVC, RESTful Web API and MongoDB Atlas



