CREATE PROCEDURE [dbo].[spBrand_Insert]
	@Name NVARCHAR(50),
	@IsDolarValue bit,
	@CategoryId INT
AS

BEGIN

SET NOCOUNT ON

INSERT INTO [dbo].[Brands]
	VALUES(@Name,@IsDolarValue)

DECLARE @BrandId INT

SET @BrandId = (SELECT SCOPE_IDENTITY())

EXEC [dbo].[spCategoryBrands_Insert] @CategoryId, @BrandId

END