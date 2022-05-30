CREATE TABLE [dbo].[ProductOrderList]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY,
	[ProductOrderId] INT FOREIGN KEY REFERENCES [dbo].[ProductOrders](Id) NOT NULL,
	[Code] NVARCHAR(40) NOT NULL,
	[Description] TEXT NOT NULL,
	[ListingPrice] MONEY NOT NULL,
	[Multiplier] INT NOT NULL
)
