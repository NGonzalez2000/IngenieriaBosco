CREATE PROCEDURE [dbo].[spBrandProviders_SelectByBrand]
	@BrandId INT
AS

BEGIN

SELECT [Id], [ProviderName] as [Name], [CUIT], [PhoneNumber], [Web]
	FROM [dbo].[Providers]
		WHERE [Id] = ANY(SELECT [ProviderId] FROM [dbo].[BrandProviders] WHERE [BrandId] = @BrandId)

END