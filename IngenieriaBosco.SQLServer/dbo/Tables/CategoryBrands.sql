CREATE TABLE [dbo].[CategoryBrands]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY,
	[CategoryId] INT FOREIGN KEY REFERENCES [dbo].[Categories](Id) NOT NULL,
	[BrandId] INT FOREIGN KEY REFERENCES [dbo].[Brands](Id) NOT NULL
)
