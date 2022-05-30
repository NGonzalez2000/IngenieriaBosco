CREATE PROCEDURE [dbo].[spProductOrder_Recived]
	@Id INT
AS

BEGIN

SET NOCOUNT ON

UPDATE [dbo].[ProductOrders]
SET [IsRecived] = 1
WHERE [Id] = @Id

END
