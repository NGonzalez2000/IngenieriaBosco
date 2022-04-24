CREATE PROCEDURE [dbo].[spProduct_SelectByCategory]
	@CategoryId INT
AS
BEGIN

SET NOCOUNT ON

SELECT * FROM [dbo].[Products]
	WHERE [CategoryId] = @CategoryId

END
