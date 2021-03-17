CREATE PROCEDURE [dbo].[uspCreateQuestion]
	@Question  nvarchar(MAX),
	@TypeId int,
	@SurveyId int,
	@QuestionNumber int,
	@QuestionId INT OUTPUT
AS
DECLARE @Count INT = 0
SET @QuestionId = 0
SELECT @Count = COUNT(*) FROM tblQuestionType WHERE Id = @TypeId

IF @Count > 0
BEGIN
	INSERT INTO tblQuestions (Question,QuestionType) VALUES (@Question,@TypeId)
	SET @QuestionId = SCOPE_IDENTITY()

	INSERT INTO tblSurveyQuestionMapping (QuestionId,SurveyId,QuestionNumber) VALUES (@QuestionId,@SurveyId,@QuestionNumber)
	RETURN 1
END
RETURN 0
