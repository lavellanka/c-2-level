1. Создать БД с именем Lesson7

2. Создать таблицу Departments

CREATE TABLE [dbo].[Departments]
(
	[Id] INT IDENTITY(1, 1) NOT NULL PRIMARY KEY, 
	[Name] NVARCHAR(100) NULL
)


3. Создать таблицу Employees

CREATE TABLE [dbo].[Employees] (
    [Id]         INT            IDENTITY (1, 1) NOT NULL,
    [FirstName]  NVARCHAR (50)  NOT NULL,
    [SecondName] NVARCHAR (50)  NOT NULL,
    [Position]   NVARCHAR (100) NOT NULL,
    [Salary]     INT            NOT NULL,
    [Department] INT            NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC), 
    FOREIGN KEY ([Department]) REFERENCES [Departments]([Id])
);

4. Для заполнения текстовыми данными выполнить:

SET IDENTITY_INSERT [dbo].[Departments] ON
INSERT INTO [dbo].[Departments] ([Id], [Name]) VALUES (1, N'Администрация')
INSERT INTO [dbo].[Departments] ([Id], [Name]) VALUES (2, N'Бухгалтерия')
INSERT INTO [dbo].[Departments] ([Id], [Name]) VALUES (3, N'Технический отдел')
SET IDENTITY_INSERT [dbo].[Departments] OFF

SET IDENTITY_INSERT [dbo].[Employees] ON
INSERT INTO [dbo].[Employees] ([Id], [FirstName], [SecondName], [Position], [Salary], [Department]) VALUES (1, N'Константин', N'Иванов', N'Директор', 100000, 1)
INSERT INTO [dbo].[Employees] ([Id], [FirstName], [SecondName], [Position], [Salary], [Department]) VALUES (2, N'Оксана', N'Петрова', N'Секретарь', 35000, 1)
INSERT INTO [dbo].[Employees] ([Id], [FirstName], [SecondName], [Position], [Salary], [Department]) VALUES (3, N'Анна', N'Сидорова', N'Старший бухгалтер', 75000, 2)
INSERT INTO [dbo].[Employees] ([Id], [FirstName], [SecondName], [Position], [Salary], [Department]) VALUES (4, N'Наталья', N'Ленина', N'Бухгалтер', 50000, 2)
INSERT INTO [dbo].[Employees] ([Id], [FirstName], [SecondName], [Position], [Salary], [Department]) VALUES (5, N'Денис', N'Павлов', N'Администратор сети', 75000, 3)
INSERT INTO [dbo].[Employees] ([Id], [FirstName], [SecondName], [Position], [Salary], [Department]) VALUES (6, N'Виталий', N'Игорев', N'Техник', 50000, 3)
SET IDENTITY_INSERT [dbo].[Employees] OFF
