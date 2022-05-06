CREATE PROCEDURE [dbo].[spBrand_SelectByCategoryId]
    @CategoryId INT
AS

BEGIN

SET NOCOUNT ON

SELECT [Brands].[Id], [BrandName] as [Name], [IsDolarValue]
    FROM [dbo].[Brands] INNER JOIN
         [dbo].[CategoryBrands] ON [dbo].[Brands].Id = [dbo].[CategoryBrands].[BrandId] INNER JOIN
         [dbo].[Categories] ON [dbo].[CategoryBrands].[CategoryId] = [dbo].[Categories].[Id]
         WHERE [dbo].[CategoryBrands].[CategoryId] = @CategoryId and [dbo].[CategoryBrands].[BrandId] <> 1
END
