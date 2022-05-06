CREATE PROCEDURE [dbo].[spCategoryBrands_Insert]
	@CategoryId INT,
	@BrandId INT
AS

BEGIN

INSERT INTO [dbo].[CategoryBrands]
	VALUES(@CategoryId,@BrandId)

END
