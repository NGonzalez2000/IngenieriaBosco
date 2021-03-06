CREATE TABLE [dbo].[ProductOrders]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY,
	[ProviderId] INT FOREIGN KEY REFERENCES [dbo].[Providers](Id) NOT NULL,
	[Date] DATETIME NOT NULL,
	[ARGPrice] MONEY NOT NULL,
	[USDPrice] MONEY NOT NULL,
	[IsRecived] BIT NOT NULL,
	[IsPayed] BIT NOT NULL

)
