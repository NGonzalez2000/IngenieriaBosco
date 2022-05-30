CREATE PROCEDURE [dbo].[spCurrency_Select]
	@Type VARCHAR(3)
AS

BEGIN

SET NOCOUNT ON

SELECT [Amount]
	FROM [dbo].[Currency]
		WHERE [Type] = @Type

END