CREATE PROCEDURE [dbo].[spCashOperation_Insert]
	@Amount MONEY,
	@Operation NVARCHAR(30),
	@Currency VARCHAR(3),
	@Date VARCHAR(10)
AS

BEGIN

SET NOCOUNT ON

SET DATEFORMAT DMY

INSERT INTO [dbo].[CashOperations]
	VALUES(@Amount,@Operation,@Currency,@Date)

DECLARE @tmpAmount MONEY

SET @tmpAmount = (SELECT [Amount] FROM [dbo].[Currency] WHERE [Type] = @Currency)

UPDATE [dbo].[Currency]
	SET [Amount] = (@tmpAmount + @Amount)
	WHERE [Type] = @Currency

END
