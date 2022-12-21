# Connect to DB
* B1: Go to file appsettings.json
* B2: Rename server of DefaultConnection
 
# Migration code to DB
* B0: Remove old folder Migrations
* B1: Open teminal
* B2: Go to folder MyShop
* B3: Generate file Migrations by use this command: 
``` sh
dotnet ef migrations add Init
```
> Install `dotnet ef` if not install before
* B4: Migration code to DB by use this command: 
``` sh
dotnet ef database update
```

# Add fake database
* B1: Migration code to DB
* B2: Copy all file in folder Fake to D://
* B3: Open Sql Server
* B3: Open file .sql
* B4: Click excecute
