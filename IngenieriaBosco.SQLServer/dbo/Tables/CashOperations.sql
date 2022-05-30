CREATE TABLE [dbo].[CashOperations]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY,
	[Amount] MONEY NOT NULL,
	[Operation] NVARCHAR(30) NOT NULL,
	[Currency] VARCHAR(3) NOT NULL,
	[Date] datetime NOT NULL
)
