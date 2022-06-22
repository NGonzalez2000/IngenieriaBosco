CREATE PROCEDURE [dbo].[spSell_SelectAll]
AS

BEGIN

SET NOCOUNT ON

SELECT [Id],[FactN],[PtoVta],[FirstName],[LastName],[CUIL],[ResponsabilidadIVA],[Currency],[TotalPrice],[IsPayed],CONVERT(VARCHAR(10),[Date],103)[Date]
	FROM [dbo].[Sells]

END
