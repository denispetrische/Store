CREATE PROCEDURE [dbo].[GetHistoryNotesForUserLastMonth]
	@UserId nvarchar(MAX),
	@Date datetime2(7)
AS
BEGIN
	SELECT * FROM HistoryNotes WHERE UserId=@UserId AND Date>@Date ORDER BY Date DESC
END;