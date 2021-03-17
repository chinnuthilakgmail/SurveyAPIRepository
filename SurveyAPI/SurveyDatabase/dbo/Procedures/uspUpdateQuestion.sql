CREATE PROCEDURE [dbo].[uspUpdateQuestion]
	@QuestionId int,
	@Question  nvarchar(MAX),
	@TypeId int,
	@SurveyId int,
	@QuestionNumber int
AS
DECLARE @Count INT = 0
 
SELECT @Count = COUNT(*) FROM tblQuestionType WHERE Id = @TypeId

IF @Count > 0
BEGIN
	UPDATE tblQuestions SET Question=@Question,QuestionType=@TypeId 
	WHERE Id=@QuestionId	 

	UPDATE tblSurveyQuestionMapping SET  SurveyId =@SurveyId ,QuestionNumber=@QuestionNumber 
	WHERE QuestionId = @QuestionId
	RETURN 1
END
RETURN 0
