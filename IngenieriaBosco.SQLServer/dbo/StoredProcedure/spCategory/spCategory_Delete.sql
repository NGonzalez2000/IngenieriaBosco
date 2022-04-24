CREATE PROCEDURE [dbo].[spCategory_Delete]
	@Id INT
AS

BEGIN

SET NOCOUNT ON

DELETE FROM [dbo].[Categories]
	WHERE [Id] = @Id

END
