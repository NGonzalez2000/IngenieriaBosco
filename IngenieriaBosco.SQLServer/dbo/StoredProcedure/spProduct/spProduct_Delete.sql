CREATE PROCEDURE [dbo].[spProduct_Delete]
	@Id INT
AS
BEGIN

SET NOCOUNT ON

DELETE FROM [dbo].[Products]
	WHERE [Id] = @Id

END
