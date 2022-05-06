CREATE PROCEDURE [dbo].[spCategory_Insert]
	@CategoryName nvarchar(50)
AS

BEGIN

SET NOCOUNT ON

INSERT INTO [dbo].[Categories]
	VALUES(@CategoryName)

DECLARE @CategoryId INT

SET @CategoryId = SCOPE_IDENTITY()

INSERT INTO [dbo].[CategoryBrands]
	VALUES(@CategoryId,1)

END
