CREATE PROCEDURE [dbo].[spCategory_SelectByProvider]
	@Id INT
AS

BEGIN

SET NOCOUNT ON

SELECT [Id], [CategoryName] AS [Name] FROM Categories
	where [Id] = ANY(SELECT [CategoryId] FROM [CategoryBrands]
		where [BrandId] = ANY(SELECT [BrandId] FROM [BrandProviders] WHERE [ProviderId] = @Id))

END
