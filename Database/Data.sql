CREATE DATABASE CoffeeShopManager;
GO
USE CoffeeShopManager;
GO

--Drink
--Table
--Category
--Account
--Bill
--BillInfo

CREATE TABLE TableCoffee
(
    idTable INT IDENTITY(1, 1) PRIMARY KEY,
    name NVARCHAR(50) NOT NULL
        DEFAULT N'Enter name of table!',
    status NVARCHAR(20) NOT NULL
        DEFAULT 0, --1:active, 0:offline
);
GO

CREATE TABLE Account
(
    userName NVARCHAR(100) NOT NULL PRIMARY KEY,
    passWord NVARCHAR(1000) NOT NULL
        DEFAULT N'12345',
    displayName NVARCHAR(100) NOT NULL,
    type INT NOT NULL 
        DEFAULT 0 --1:Admin, 0: Staff
);
GO



CREATE TABLE Category
(
    idCategory INT IDENTITY(1, 1) PRIMARY KEY,
    name NVARCHAR(50) NOT NULL
        DEFAULT N'Enter name of Category!',
);
GO

CREATE TABLE Drinks
(
    idDrinks INT IDENTITY(1, 1) PRIMARY KEY,
    name NVARCHAR(50) NOT NULL
        DEFAULT N'Enter name of Drinks!',
    idCategory INT NOT NULL
        FOREIGN KEY REFERENCES dbo.Category (idCategory),
    price FLOAT NOT NULL
);

CREATE TABLE Bill
(
    idBill INT IDENTITY(1, 1) PRIMARY KEY,
    timeCheckin DATE NOT NULL
        DEFAULT GETDATE(),
    dateCheckout DATE,
    idTable INT NOT NULL
        FOREIGN KEY REFERENCES dbo.TableCoffee (idTable),
    status INT NOT NULL
        DEFAULT 0 --1: paid, 0:unpaid
);
GO

CREATE TABLE BillInfo
(
    idBillInfo INT IDENTITY(1, 1) PRIMARY KEY,
    idBill INT NOT NULL
        FOREIGN KEY REFERENCES dbo.Bill (idBill),
    idDrinks INT NOT NULL
        FOREIGN KEY REFERENCES dbo.Drinks (idDrinks),
    count INT NOT NULL
        DEFAULT 0
);
GO