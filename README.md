# Test for PanSoft

## Prerequisites
1. Visual Studio 2019 Community, .Net Core 3.1
2. Visual Studio code setup for Angular 6+ development (npm, node, ng cli) See: https://angular.io/guide/setup-local

## Backend
- Open the SqlAPI.sln in VS 2019
- Update the "DefaultConnection" in the appsettings.json file with a connection string to your SQL database

## Frontend
- open up the Website/sql-app folder in VS code and type ng serve to host the UI
- Navigate to the mentioned url (generally http://localhost:4200) to visit the web app.  
- Note: In case the backend was hosted on some other port, modify the Website/sql-app/src/app/Services/constants.ts file with the right API base url. 
