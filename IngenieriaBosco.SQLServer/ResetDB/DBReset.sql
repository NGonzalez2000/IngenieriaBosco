DELETE FROM [dbo].[ClientEmails]
DELETE FROM [dbo].[ProviderEmails]
DELETE FROM [dbo].[CategoryBrands]
DELETE FROM [dbo].[BrandProviders]
DELETE FROM [dbo].[Brands]
DELETE FROM [dbo].[Categories]
DELETE FROM [dbo].[Products]
DELETE FROM [dbo].[Providers]
DELETE FROM [dbo].[Clients]
DELETE FROM [dbo].[CashOperations]
DELETE FROM [dbo].[Currency]


DBCC CHECKIDENT ('[BrandProviders]', RESEED, 0);
DBCC CHECKIDENT ('[Brands]', RESEED, 0);
DBCC CHECKIDENT ('[Categories]', RESEED, 0);
DBCC CHECKIDENT ('[CategoryBrands]', RESEED, 0);
DBCC CHECKIDENT ('[ClientEmails]', RESEED, 0);
DBCC CHECKIDENT ('[Clients]', RESEED, 0);
DBCC CHECKIDENT ('[Products]', RESEED, 0);
DBCC CHECKIDENT ('[ProviderEmails]', RESEED, 0);
DBCC CHECKIDENT ('[Providers]', RESEED, 0);
DBCC CHECKIDENT ('[CashOperations]', RESEED, 0);

INSERT INTO [dbo].[Brands]
	VALUES('',0)

INSERT INTO [dbo].[Currency]
	VALUES('USD',0)
INSERT INTO [dbo].[Currency]
	VALUES('ARG',0)

SELECT * FROM [dbo].[ClientEmails]
SELECT * FROM [dbo].[ProviderEmails]
SELECT * FROM [dbo].[CategoryBrands]
SELECT * FROM [dbo].[BrandProviders]
SELECT * FROM [dbo].[Brands]
SELECT * FROM [dbo].[Categories]
SELECT * FROM [dbo].[Products]
SELECT * FROM [dbo].[Providers]
SELECT * FROM [dbo].[Clients]