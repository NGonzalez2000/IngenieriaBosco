CREATE PROCEDURE [dbo].[spSell_Delete]
	@Id INT

AS 
BEGIN

SET NOCOUNT ON

DELETE FROM [dbo].[SellItems]
	WHERE [SellId] = @Id

DELETE FROM [dbo].[Sells]
	WHERE [Id] = @Id

END
