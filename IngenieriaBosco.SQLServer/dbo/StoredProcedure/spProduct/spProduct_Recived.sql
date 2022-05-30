CREATE PROCEDURE [dbo].[spProduct_Recived]
	@Id int
AS

BEGIN

SET NOCOUNT ON

DECLARE @temp INT

SET @temp = (select [Multiplier] from [dbo].[ProductOrderList] where [Id] = @Id)

update [dbo].[Products]
set Stock = (Stock + @temp)
where [Code] = (select [Code] from [dbo].[ProductOrderList] where [Id] = @Id)

END