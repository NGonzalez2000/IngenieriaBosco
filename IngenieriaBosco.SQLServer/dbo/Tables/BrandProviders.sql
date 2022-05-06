CREATE TABLE [dbo].[BrandProviders]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY,
	[BrandId] INT NOT NULL FOREIGN KEY REFERENCES [dbo].[Brands](Id),
	[ProviderId] INT NOT NULL FOREIGN KEY REFERENCES [dbo].[Providers](Id)
)
