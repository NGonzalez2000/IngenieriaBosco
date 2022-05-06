CREATE PROCEDURE [dbo].[spBrand_Update]
	@Id INT,
	@Name NVARCHAR(50),
	@IsDolarValue BIT
AS

BEGIN

SET NOCOUNT ON

UPDATE [dbo].[Brands]
	SET [BrandName] = @Name, [IsDolarValue] = @IsDolarValue
		WHERE [Id] = @Id

END
