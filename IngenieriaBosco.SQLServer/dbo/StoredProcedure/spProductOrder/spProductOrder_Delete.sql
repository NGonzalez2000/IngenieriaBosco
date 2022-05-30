CREATE PROCEDURE [dbo].[spProductOrder_Delete]
	@Id INT
AS

BEGIN

SET NOCOUNT ON

DELETE FROM [dbo].[ProductOrderList]
	WHERE [ProductOrderId] = @Id

DELETE FROM [dbo].[ProductOrders]
	WHERE [Id] = @Id

END