CREATE PROCEDURE [dbo].[GetHistoryNoteForUserLastMonthProcedure]
	@UserId nvarchar(MAX),
	@Date datetime2(7)
AS
BEGIN
	SELECT * FROM HistoryNotes WHERE UserId=@UserId AND Date<@Date
END;
