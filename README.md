# Readme

This is an API built for a challenge. You may find instructions to run the api and the webApp locally

## How to Run the App

### What you need
Locally you need to install Visual Studio , and .Net Core 7.0, SQL Server. 

### Data 
You can use In Memory Data or an Actual Database, by setting the key "UseDataBase" in the appsettings.json (in the BackEnd project) to either True or False.

#### InMemory Data

This was added only for developing and testing purposes. If you use this approach, the data will not persist after you stop running the application, and will be resestablished to the same default values everytime the app is executed. Use this approach if you don't want to (or cannot) create a local database.

#### Database
If you want to use the database you can Publish the database project in your local machine. SQL Server was the database engine chosen for this project. Note that a PostDeployment script will insert default Data.

Take into account that tha .sqlproj is independent from the dotnet solution. 

### What to expect
You should run 2 projects:  the API and the front-end. Both of them were developed with .Net Core 7.0. You should run both solutions. By running the Backend solution, you will see a swagger. By running the Web Application, you will see the home page (or the login page if you are not logged in yet)

