CREATE PROCEDURE [dbo].[spCashOperation_SelectTop]
	@Quantity int
AS

BEGIN

SET NOCOUNT ON

SELECT TOP (@Quantity)
	[Id],[Amount],[Operation],[Currency],CONVERT(VARCHAR(10),[Date],103)[Date]
		FROM CashOperations ORDER BY [Date] DESC

END
