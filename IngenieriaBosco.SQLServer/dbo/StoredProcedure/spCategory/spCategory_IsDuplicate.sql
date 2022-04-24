CREATE PROCEDURE [dbo].[spCategory_IsDuplicate]
	@CategoryName NVARCHAR(50)
AS

BEGIN 

SET NOCOUNT ON

IF EXISTS(SELECT * FROM [dbo].[Categories] WHERE [CategoryName] = @CategoryName)
	SELECT 1
ELSE 
	SELECT 0

END
