CREATE PROCEDURE [dbo].[spProductOrder_SelectAll]
	
AS

BEGIN

SET NOCOUNT ON

SELECT [Id], CONVERT(VARCHAR(10),[Date],103)[Date], [USDPrice], [ARGPrice], [IsPayed], [IsRecived]
	FROM [dbo].[ProductOrders]

END