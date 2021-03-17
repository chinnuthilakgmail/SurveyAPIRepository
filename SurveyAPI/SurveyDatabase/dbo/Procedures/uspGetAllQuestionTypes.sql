CREATE PROCEDURE [dbo].[uspGetAllQuestionTypes]
	 
AS
	SELECT Id,QuestionType,IsOptionsRequired FROM tblQuestionType
RETURN 1
