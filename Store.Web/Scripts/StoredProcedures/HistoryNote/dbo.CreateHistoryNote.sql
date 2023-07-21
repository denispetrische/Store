CREATE PROCEDURE [dbo].[CreateHistoryNote]
	@Id uniqueidentifier,
	@Message nvarchar(MAX),
	@Date datetime2(7),
	@UserId nvarchar(MAX)
AS
BEGIN
	INSERT INTO HistoryNotes([Id],[Message],[Date],[UserId])
	            VALUES(@Id, @Message, @Date, @UserId)
END;