CREATE PROCEDURE [dbo].[uspCreateSurvey]
	@Title nvarchar(200),
	@Description nvarchar(MAX),
	@StartDate datetime,
	@EndDate datetime,
	@IsOpen bit,
	@SurveyId int output
AS

DECLARE @Count INT = 0
SET @SurveyId = 0
SELECT @Count = COUNT(*) FROM tblSurvey WHERE Title = @Title
IF @Count = 0
BEGIN
	INSERT INTO tblSurvey(Title,Description,StartDate,EndDate,IsOpen) VALUES (@Title,@Description,@StartDate,@EndDate,@IsOpen)
	SET @SurveyId = SCOPE_IDENTITY()	
	RETURN 1
END
ELSE	 
RETURN 0
