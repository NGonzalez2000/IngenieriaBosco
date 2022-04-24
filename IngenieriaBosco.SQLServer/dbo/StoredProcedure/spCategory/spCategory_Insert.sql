CREATE PROCEDURE [dbo].[spCategory_Insert]
	@CategoryName nvarchar(50)
AS

BEGIN

SET NOCOUNT ON

INSERT INTO [dbo].[Categories]
	VALUES(@CategoryName)

END
