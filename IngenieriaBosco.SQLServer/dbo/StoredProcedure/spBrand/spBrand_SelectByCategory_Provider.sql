CREATE PROCEDURE [dbo].[spBrand_SelectByCategory_Provider]
	@CategoryId INT,
	@ProviderId INT
AS

BEGIN

SET NOCOUNT ON

SELECT [Id], [BrandName] as [Name], [IsDolarValue] FROM Brands
	where [Id] = ANY(Select [BrandId] From [CategoryBrands]
		where CategoryId = @CategoryId AND [BrandId] = ANY(SELECT [BrandId] FROM [BrandProviders] WHERE [ProviderId] = @ProviderId))

END
