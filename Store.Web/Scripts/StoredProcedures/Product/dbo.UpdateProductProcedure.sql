CREATE PROCEDURE [dbo].[UpdateProduct]
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
	BEGIN TRAN
		IF(@Id != '')
		BEGIN
			UPDATE Products SET [Name]=@Name, [Description]=@Description, [IsOnTrade]=@IsOnTrade, 
			                    [ReceiptDate]=@ReceiptDate, [ExpireDate]=@ExpireDate, [Amount]=@Amount,
								[Price]=@Price, [Currency]=@Currency
							WHERE Id = @Id
		END
	COMMIT TRAN
END;
