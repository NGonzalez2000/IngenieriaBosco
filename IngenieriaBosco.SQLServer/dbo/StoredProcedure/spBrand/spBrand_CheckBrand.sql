CREATE PROCEDURE [dbo].[spBrand_CheckBrand]
	@CategoryId INT,
	@Name NVARCHAR(50)
AS

BEGIN

SET NOCOUNT ON

SELECT [dbo].[Brands].[Id], [dbo].[Brands].[BrandName] as [Name], [IsDolarValue]        
	FROM dbo.Brands INNER JOIN
         dbo.CategoryBrands ON dbo.Brands.Id = dbo.CategoryBrands.BrandId
		 WHERE [dbo].[Brands].[Id] = ANY(SELECT [BrandId] FROM [dbo].[CategoryBrands] WHERE [CategoryId] = @CategoryId) and [BrandName] = @Name 
					
END
