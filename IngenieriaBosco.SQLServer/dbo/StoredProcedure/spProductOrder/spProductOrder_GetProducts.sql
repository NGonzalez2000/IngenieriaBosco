CREATE PROCEDURE [dbo].[spProductOrder_GetProducts]
	@Id INT
AS

BEGIN

SET NOCOUNT ON

SELECT [Id], [Code], [Description], [ListingPrice], [Multiplier]
	FROM [dbo].[ProductOrderList]
		WHERE [ProductOrderId] = @Id

END
