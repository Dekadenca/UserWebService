IF OBJECT_ID(N'dbo.Users', N'U') IS NULL
CREATE TABLE "Users" (
    Id int IDENTITY(1,1) PRIMARY KEY NOT NULL,
    UserName varchar(255) UNIQUE,
    FullName varchar(255),
    Email varchar(255),
    MobileNumber varchar(63),
    Language varchar(2),
    Culture varchar(63),
    Password varchar(63),
);

IF OBJECT_ID(N'dbo.ApiKeys', N'U') IS NULL
CREATE TABLE "ApiKeys" (
	"Id" int IDENTITY(1,1) PRIMARY KEY NOT NULL,
    "Value" varchar(255) NOT NULL,
    "Client" varchar(255) UNIQUE,
);

INSERT INTO "ApiKeys" ("Value", "Client") VALUES ('ea37e310-c1b6-4426-9376-a427d08854ca', 'Emil D.O.O.')
INSERT INTO "ApiKeys" ("Value", "Client") VALUES ('c82a3e52-4381-4ceb-8b85-e58d0722f021', 'Mikrocop d.o.o.')
INSERT INTO "ApiKeys" ("Value", "Client") VALUES ('547adf6d-1124-4271-8ad6-0ef025b62e76', 'Secret SP')
