CREATE PROCEDURE [dbo].[GetProductById]
	@Id uniqueidentifier = ''
AS
BEGIN
	SELECT * FROM Products WHERE Id = @Id
END;
