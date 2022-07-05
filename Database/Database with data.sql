USE [master]
GO
/****** Object:  Database [CoffeeShopManager]    Script Date: 7/5/2022 6:27:50 PM ******/
CREATE DATABASE [CoffeeShopManager]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'CoffeeShopManager', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.SQLEXPRESS\MSSQL\DATA\CoffeeShopManager.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'CoffeeShopManager_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.SQLEXPRESS\MSSQL\DATA\CoffeeShopManager_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO
ALTER DATABASE [CoffeeShopManager] SET COMPATIBILITY_LEVEL = 150
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [CoffeeShopManager].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [CoffeeShopManager] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [CoffeeShopManager] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [CoffeeShopManager] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [CoffeeShopManager] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [CoffeeShopManager] SET ARITHABORT OFF 
GO
ALTER DATABASE [CoffeeShopManager] SET AUTO_CLOSE ON 
GO
ALTER DATABASE [CoffeeShopManager] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [CoffeeShopManager] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [CoffeeShopManager] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [CoffeeShopManager] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [CoffeeShopManager] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [CoffeeShopManager] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [CoffeeShopManager] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [CoffeeShopManager] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [CoffeeShopManager] SET  ENABLE_BROKER 
GO
ALTER DATABASE [CoffeeShopManager] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [CoffeeShopManager] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [CoffeeShopManager] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [CoffeeShopManager] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [CoffeeShopManager] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [CoffeeShopManager] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [CoffeeShopManager] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [CoffeeShopManager] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [CoffeeShopManager] SET  MULTI_USER 
GO
ALTER DATABASE [CoffeeShopManager] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [CoffeeShopManager] SET DB_CHAINING OFF 
GO
ALTER DATABASE [CoffeeShopManager] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [CoffeeShopManager] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [CoffeeShopManager] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [CoffeeShopManager] SET QUERY_STORE = OFF
GO
USE [CoffeeShopManager]
GO
/****** Object:  Table [dbo].[Account]    Script Date: 7/5/2022 6:27:50 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Account](
	[userName] [nvarchar](100) NOT NULL,
	[passWord] [nvarchar](1000) NOT NULL,
	[displayName] [nvarchar](100) NOT NULL,
	[type] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[userName] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Bill]    Script Date: 7/5/2022 6:27:50 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Bill](
	[idBill] [int] IDENTITY(1,1) NOT NULL,
	[timeCheckin] [date] NOT NULL,
	[dateCheckout] [date] NULL,
	[idTable] [int] NOT NULL,
	[status] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[idBill] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[BillInfo]    Script Date: 7/5/2022 6:27:50 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[BillInfo](
	[idBillInfo] [int] IDENTITY(1,1) NOT NULL,
	[idBill] [int] NOT NULL,
	[idDrinks] [int] NOT NULL,
	[count] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[idBillInfo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Category]    Script Date: 7/5/2022 6:27:50 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Category](
	[idCategory] [int] IDENTITY(1,1) NOT NULL,
	[name] [nvarchar](50) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[idCategory] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Drinks]    Script Date: 7/5/2022 6:27:50 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Drinks](
	[idDrinks] [int] IDENTITY(1,1) NOT NULL,
	[name] [nvarchar](50) NOT NULL,
	[idCategory] [int] NOT NULL,
	[price] [float] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[idDrinks] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TableCoffee]    Script Date: 7/5/2022 6:27:50 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TableCoffee](
	[idTable] [int] IDENTITY(1,1) NOT NULL,
	[name] [nvarchar](50) NOT NULL,
	[status] [nvarchar](20) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[idTable] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
INSERT [dbo].[Account] ([userName], [passWord], [displayName], [type]) VALUES (N'a', N'1234', N'Staff A', 0)
INSERT [dbo].[Account] ([userName], [passWord], [displayName], [type]) VALUES (N'admin', N'admin', N'Admin', 1)
INSERT [dbo].[Account] ([userName], [passWord], [displayName], [type]) VALUES (N'b', N'abcd', N'Staff B', 0)
GO
SET IDENTITY_INSERT [dbo].[Bill] ON 

INSERT [dbo].[Bill] ([idBill], [timeCheckin], [dateCheckout], [idTable], [status]) VALUES (10, CAST(N'2022-07-05' AS Date), NULL, 1, 1)
SET IDENTITY_INSERT [dbo].[Bill] OFF
GO
SET IDENTITY_INSERT [dbo].[BillInfo] ON 

INSERT [dbo].[BillInfo] ([idBillInfo], [idBill], [idDrinks], [count]) VALUES (16, 10, 3, 1)
SET IDENTITY_INSERT [dbo].[BillInfo] OFF
GO
SET IDENTITY_INSERT [dbo].[Category] ON 

INSERT [dbo].[Category] ([idCategory], [name]) VALUES (1, N'Trà sữa')
INSERT [dbo].[Category] ([idCategory], [name]) VALUES (2, N'Coffee')
SET IDENTITY_INSERT [dbo].[Category] OFF
GO
SET IDENTITY_INSERT [dbo].[Drinks] ON 

INSERT [dbo].[Drinks] ([idDrinks], [name], [idCategory], [price]) VALUES (1, N'Cà phê nâu', 2, 20000)
INSERT [dbo].[Drinks] ([idDrinks], [name], [idCategory], [price]) VALUES (2, N'Cà phê đen ', 2, 20000)
INSERT [dbo].[Drinks] ([idDrinks], [name], [idCategory], [price]) VALUES (3, N'Trà sữa trân châu', 1, 30000)
INSERT [dbo].[Drinks] ([idDrinks], [name], [idCategory], [price]) VALUES (4, N'Trà sữa cầu vồng', 1, 30000)
SET IDENTITY_INSERT [dbo].[Drinks] OFF
GO
SET IDENTITY_INSERT [dbo].[TableCoffee] ON 

INSERT [dbo].[TableCoffee] ([idTable], [name], [status]) VALUES (1, N'Bàn 1', N'Trống')
INSERT [dbo].[TableCoffee] ([idTable], [name], [status]) VALUES (2, N'Bàn 2', N'Trống')
INSERT [dbo].[TableCoffee] ([idTable], [name], [status]) VALUES (3, N'Bàn 3', N'Trống')
INSERT [dbo].[TableCoffee] ([idTable], [name], [status]) VALUES (4, N'Bàn 4', N'Trống')
INSERT [dbo].[TableCoffee] ([idTable], [name], [status]) VALUES (5, N'Bàn 5', N'Trống')
SET IDENTITY_INSERT [dbo].[TableCoffee] OFF
GO
ALTER TABLE [dbo].[Account] ADD  DEFAULT (N'12345') FOR [passWord]
GO
ALTER TABLE [dbo].[Account] ADD  DEFAULT ((0)) FOR [type]
GO
ALTER TABLE [dbo].[Bill] ADD  DEFAULT (getdate()) FOR [timeCheckin]
GO
ALTER TABLE [dbo].[Bill] ADD  DEFAULT ((0)) FOR [status]
GO
ALTER TABLE [dbo].[BillInfo] ADD  DEFAULT ((0)) FOR [count]
GO
ALTER TABLE [dbo].[Category] ADD  DEFAULT (N'Enter name of Category!') FOR [name]
GO
ALTER TABLE [dbo].[Drinks] ADD  DEFAULT (N'Enter name of Drinks!') FOR [name]
GO
ALTER TABLE [dbo].[TableCoffee] ADD  DEFAULT (N'Enter name of table!') FOR [name]
GO
ALTER TABLE [dbo].[TableCoffee] ADD  DEFAULT ((0)) FOR [status]
GO
ALTER TABLE [dbo].[Bill]  WITH CHECK ADD FOREIGN KEY([idTable])
REFERENCES [dbo].[TableCoffee] ([idTable])
GO
ALTER TABLE [dbo].[BillInfo]  WITH CHECK ADD FOREIGN KEY([idBill])
REFERENCES [dbo].[Bill] ([idBill])
GO
ALTER TABLE [dbo].[BillInfo]  WITH CHECK ADD FOREIGN KEY([idDrinks])
REFERENCES [dbo].[Drinks] ([idDrinks])
GO
ALTER TABLE [dbo].[Drinks]  WITH CHECK ADD FOREIGN KEY([idCategory])
REFERENCES [dbo].[Category] ([idCategory])
GO
/****** Object:  StoredProcedure [dbo].[GetTableList]    Script Date: 7/5/2022 6:27:50 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[GetTableList]
AS SELECT * FROM dbo.TableCoffee
GO
/****** Object:  StoredProcedure [dbo].[InsertBill]    Script Date: 7/5/2022 6:27:50 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[InsertBill]
@idTable int
AS
BEGIN
    INSERT INTO dbo.Bill
    (
        timeCheckin,
        dateCheckout,
        idTable,
        status
    )
    VALUES
    (   GETDATE(), -- timeCheckin - date
        NULL, -- dateCheckout - date
        @idTable,         -- idTable - int
        0          -- status - int
        )
END
GO
/****** Object:  StoredProcedure [dbo].[InsertBillInfor]    Script Date: 7/5/2022 6:27:50 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[InsertBillInfor]
	@idBill int,
	@idDrink int,
	@count int
	AS 
	BEGIN
	    DECLARE @isExistsBillInfo INT 
		DECLARE @drinkCount INT =1
		SELECT @isExistsBillInfo = b.idBillInfo ,@drinkCount = b.count 
		FROM dbo.BillInfo AS b
		WHERE idBill = @idBill AND idDrinks = @idDrink
		IF (@isExistsBillInfo >0)
		BEGIN
		    DECLARE @newCount INT = @drinkCount + @count 
			IF (@newCount >0)
			UPDATE dbo.BillInfo SET count = @drinkCount + @count WHERE idDrinks = @idDrink
			ELSE
			DELETE dbo.BillInfo WHERE idBill = @idBill AND idDrinks = @idDrink
		END
		ELSE
BEGIN
	    INSERT dbo.BillInfo
(
    idBill,
    idDrinks,
    count
)
VALUES
(   @idBill, -- idBill - int
    @idDrink, -- idDrinks - int
    @count  -- count - int
    )
	END
	END
GO
/****** Object:  StoredProcedure [dbo].[User_login]    Script Date: 7/5/2022 6:27:50 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[User_login]
@userName NVARCHAR(50), @passWord NVARCHAR(50)
AS
BEGIN
    SELECT * FROM dbo.Account WHERE userName = @userName AND passWord = @passWord
END
GO
/****** Object:  Trigger [dbo].[UpdateBill]    Script Date: 7/5/2022 6:27:50 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE TRIGGER [dbo].[UpdateBill]
ON [dbo].[Bill]
FOR UPDATE
AS
BEGIN
    DECLARE @idBill INT;
    SELECT @idBill = idBill
    FROM inserted;

    DECLARE @idTable INT;
    SELECT @idTable = idTable
    FROM dbo.Bill
    WHERE idBill = @idBill;
	DECLARE @count INT = 0
	SELECT @count = COUNT(*) FROM dbo.Bill WHERE idTable = @idTable AND status = 0
	IF (@count = 0)
	UPDATE dbo.TableCoffee SET status = N'Trống' WHERE idTable = @idTable
END;
GO
ALTER TABLE [dbo].[Bill] ENABLE TRIGGER [UpdateBill]
GO
/****** Object:  Trigger [dbo].[UpdateBillInfo]    Script Date: 7/5/2022 6:27:51 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TRIGGER [dbo].[UpdateBillInfo]
ON [dbo].[BillInfo]
FOR INSERT, UPDATE
AS
BEGIN
    DECLARE @idBill INT;
    SELECT @idBill = Inserted.idBill
    FROM Inserted;
    DECLARE @idTable INT;
    SELECT @idTable = idTable
    FROM dbo.Bill
    WHERE idBill = @idBill
          AND status = 0;
    UPDATE dbo.TableCoffee
    SET status = N'Có người'
    WHERE @idTable = idTable;
END;
GO
ALTER TABLE [dbo].[BillInfo] ENABLE TRIGGER [UpdateBillInfo]
GO
USE [master]
GO
ALTER DATABASE [CoffeeShopManager] SET  READ_WRITE 
GO
