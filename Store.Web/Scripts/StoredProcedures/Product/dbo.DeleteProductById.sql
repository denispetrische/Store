CREATE PROCEDURE [dbo].[DeleteProductById]
	@Id uniqueidentifier = ''
AS
BEGIN
	DELETE FROM Products WHERE Id = @Id
END;