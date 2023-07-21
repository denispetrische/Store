CREATE PROCEDURE [dbo].[GetProductsForMarket]
AS
BEGIN
	SELECT * FROM Products Where IsOnTrade=1 AND ExpireDate>ReceiptDate
END;