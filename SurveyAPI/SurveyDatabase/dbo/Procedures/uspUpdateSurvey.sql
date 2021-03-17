CREATE PROCEDURE [dbo].[uspUpdateSurvey]
	@Id int,
	@Title nvarchar(200),
	@Description nvarchar(MAX),
	@StartDate datetime,
	@EndDate datetime,
	@IsOpen bit
AS
	DECLARE @Count INT = 0
SELECT @Count = COUNT(*) FROM tblSurvey WHERE Id = @Id
IF @Count > 0
BEGIN
	UPDATE tblSurvey SET Title=@Title, Description=@Description, StartDate=@StartDate, EndDate=@EndDate, IsOpen=@IsOpen
	WHERE Id = @Id
	RETURN 1
END
ELSE	 
RETURN 0
