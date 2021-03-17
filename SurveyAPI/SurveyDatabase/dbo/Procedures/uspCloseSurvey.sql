CREATE PROCEDURE [dbo].[uspCloseSurvey]
	@Id int
AS
	DECLARE @Count INT = 0
	SELECT @Count = COUNT(*) FROM tblSurvey WHERE Id = @Id
IF @Count > 0
	BEGIN
		UPDATE tblSurvey SET IsOpen = 0 WHERE Id = @Id
		RETURN 1
	END
ELSE	
RETURN 0
