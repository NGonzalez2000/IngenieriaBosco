CREATE PROCEDURE [dbo].[spCategory_Delete]
	@Id INT
AS

BEGIN

SET NOCOUNT ON

DELETE FROM [dbo].[BrandProviders] WHERE
	[BrandId] = ANY(SELECT [BrandId] FROM [CategoryBrands] WHERE [CategoryId] = @Id)

DELETE FROM [dbo].[CategoryBrands]
	WHERE [CategoryId] = @Id

DELETE FROM [dbo].[Categories]
	WHERE [Id] = @Id

END
