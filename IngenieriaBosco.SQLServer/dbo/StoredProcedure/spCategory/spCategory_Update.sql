CREATE PROCEDURE [dbo].[spCategory_Update]
	@Id INT,
	@CategoryName NVARCHAR(50)
AS

BEGIN

SET NOCOUNT ON

UPDATE [dbo].[Categories]
	SET [CategoryName] = @CategoryName
		WHERE [Id] = @Id

END
