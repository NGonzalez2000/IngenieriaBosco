CREATE PROCEDURE [dbo].[spBrand_Delete]
	@Id INT
AS

BEGIN

SET NOCOUNT ON

DELETE FROM [dbo].[BrandProviders]
	WHERE [BrandId] = @Id

DELETE FROM [dbo].[CategoryBrands]
	WHERE [BrandId] = @Id

DELETE FROM [dbo].[Brands]
	WHERE [Id] = @Id

EnD