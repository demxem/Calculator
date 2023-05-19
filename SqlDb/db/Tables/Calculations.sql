CREATE TABLE [dbo].[Calculations]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY,
	[Type] NVARCHAR(50) NOT NULL, 
    [Expression] NVARCHAR(50) NOT NULL, 
    [CreationDate] DATETIME NOT NULL, 
    [Result] FLOAT NOT NULL,
);
