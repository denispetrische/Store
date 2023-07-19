CREATE PROCEDURE [dbo].[CreateHistoryNote]
	@Id uniqueidentifier = '',
	@Message nvarchar(Max) = '',
	@Date nvarchar(Max) = '',
	@UserGuid uniqueidentifier = ''
AS
BEGIN
	INSERT INTO HistoryNotes([Id],[Message],[Date],[UserGuid])
	                  VALUES(@Id, @Message, @Date, @UserGuid)
END;
