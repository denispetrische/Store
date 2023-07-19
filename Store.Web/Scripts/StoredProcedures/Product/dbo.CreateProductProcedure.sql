CREATE PROCEDURE [dbo].[CreateProduct]
	@Id uniqueidentifier = '',
	@Name nvarchar(Max) = '',
	@Description nvarchar(Max) = '',
	@IsOnTrade bit = 0,
	@ReceiptDate datetime2(7),
	@ExpireDate datetime2(7),
	@Amount bigint = 0,
	@Price bigint = 0,
	@Currency nvarchar(Max) = ''
AS
BEGIN
	INSERT INTO Products([Id],[Name],[Description],[IsOnTrade],[ReceiptDate],[ExpireDate],[Amount],[Price],[Currency])
	            VALUES(@Id, @Name, @Description, @IsOnTrade, @ReceiptDate, @ExpireDate, @Amount, @Price, @Currency)
END;
