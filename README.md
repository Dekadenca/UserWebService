# A Simple .NET solution for an User Web API

## Intent
User api works as a second hand api for authentication of users. A client would rent this service and use its functionality to escape the problem of having his own database and functionality for checking users. 
All endpoints are secured with an api key which is saved in the database and is forwarded to the client. Service has some basic logging.

## Configuration
For getting the code to work, you will need MS Visual Studio with .NET support. You will also need an SQL database connected to the project.

In appsetting.json you can find connection strings for database which you have to change for it to work in your environment.

UserInsert.sql should be run to set all the tables needed for the SQL database to work.
