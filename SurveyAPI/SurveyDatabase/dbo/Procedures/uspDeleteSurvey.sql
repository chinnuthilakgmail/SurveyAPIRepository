CREATE PROCEDURE [dbo].[uspDeleteSurvey]
	@Id int
AS
		DECLARE @Count INT = 0
	SELECT @Count = COUNT(*) FROM tblSurvey WHERE Id = @Id
IF @Count > 0
	BEGIN
		DELETE tblSurvey WHERE Id = @Id
		RETURN 1
	END
ELSE	
RETURN 0
