CREATE PROCEDURE [dbo].[GetAllHistoryNotesLastDay]
	@Date datetime2(7)
AS
BEGIN
	SELECT * From HistoryNotes WHERE Date>@Date
END;