CREATE PROCEDURE [dbo].[spCategory_SelectByName]
	@Name NVARCHAR(50)
AS

BEGIN

SET NOCOUNT ON

SELECT [Id], [CategoryName] as [Name]
	FROM [dbo].[Categories]
		WHERE [CategoryName] = @Name

END
