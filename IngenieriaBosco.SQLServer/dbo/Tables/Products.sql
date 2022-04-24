CREATE TABLE [dbo].[Products]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY,
	[Description] TEXT NOT NULL,
	[Code] NVARCHAR(40) NOT NULL UNIQUE,

	[CategoryId] INT FOREIGN KEY REFERENCES [dbo].[Categories](Id)NOT NULL,
	[BrandId] INT FOREIGN KEY REFERENCES [dbo].[Brands](Id) NOT NULL,

	[ListingPrice] MONEY NOT NULL,
	[RetailPrice] MONEY NOT NULL,
	[WholesalerPrice] MONEY NOT NULL,

	[Stock] INT NOT NULL,
	[WariningStock] INT NOT NULL
)
