CREATE PROCEDURE [dbo].[spSell_Update]
	@Id INT,
	@Currency VARCHAR(3),
	@TotalPrice MONEY,
	@IsPayed BIT
AS

BEGIN

SET NOCOUNT ON

UPDATE [dbo].[Sells]
	SET [Currency] = @Currency,
		[TotalPrice] = @TotalPrice,
		[IsPayed] = @IsPayed
		WHERE [Id] = @Id

END
