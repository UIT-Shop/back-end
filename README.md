# Back-end shop

This software provides the server for the shop.

## Content

- [1. Install](#caidat)
- [2. Contact](#lienhe)

<a name="caidat"></a>
## Install

### Connect to DB
* B1: Go to file appsettings.json
* B2: Rename server of DefaultConnection
![appsettings.json](https://res.cloudinary.com/nam-duong/image/upload/v1675175113/github/%E1%BA%A2nh1_owuz7l.png)

### Migration code to DB
* B0: Remove old folder Migrations (if exists)
* B1: Open terminal
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

### Add fake database
* B1: Migration code to DB
* B2: Copy all file in folder Fake to D:// (or any where you want)
* B3: Open Sql Server ([install](https://www.microsoft.com/en-us/sql-server/sql-server-downloads))

* B3: Open file .sql
* B3.1: Change code if you not follow step 2
* B4: Click execute 

<a name="lienhe"></a>

## Contact

| STT | Fullname            | MSSV     | Contact                                             |
| --- | -------------------- | -------- | --------------------------------------------------- |
| 1   | Nguyễn Duy Phúc      | 19522038 | [duyphuc](https://github.com/NguyenDuyPhuc01012001) |
| 2   | Hoàng Quốc Trọng | 19522408| [quoctrong](https://github.com/hoangquoctrong)         |
