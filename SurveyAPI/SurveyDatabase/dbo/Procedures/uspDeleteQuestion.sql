CREATE PROCEDURE [dbo].[uspDeleteQuestion]
	@QuestionId int  
AS
	DELETE tblOptions WHERE QuestionId = @QuestionId
	DELETE tblSurveyQuestionMapping WHERE QuestionId = @QuestionId
	DELETE tblQuestions WHERE Id = @QuestionId
RETURN 1

