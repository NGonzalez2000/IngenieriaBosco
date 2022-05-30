CREATE PROCEDURE [dbo].[spProductOrder_Payed]
	@Id INT
AS

BEGIN

SET NOCOUNT ON

UPDATE [dbo].[ProductOrders]
SET [IsPayed] = 1
WHERE [Id] = @Id

END