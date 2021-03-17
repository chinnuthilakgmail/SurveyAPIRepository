CREATE PROCEDURE [dbo].[uspGetSurveyDetails]
	@SurveyId int  
AS
	SELECT s.Id as SurveyId, s.Title as SurveyTitle,s.Description as SurveyDescription,
		--s.StartDate as SurveyStartDate,s.EndDate as SurveyEndDate,
		m.QuestionNumber as QuestionNumber,
		s.IsOpen as IsOpen,
		q.Id as QuestionId,q.Question as Question,
		t.Id as QuestionTypeId,t.QuestionType as QuestionType,t.IsOptionsRequired as IsOptionsRequired,
		o.Id as OptionId,o.OptionText as OptionText
		
	FROM tblSurvey s
	LEFT OUTER JOIN tblSurveyQuestionMapping m ON s.Id = m.SurveyId
	LEFT OUTER JOIN tblQuestions q ON q.Id = m.QuestionId
	LEFT OUTER JOIN tblQuestionType t ON t.Id = q.QuestionType
	LEFT OUTER JOIN tblOptions o ON o.QuestionId = m.QuestionId

	WHERE CONVERT(date, s.StartDate) <= CONVERT(date, GETDATE())
	AND CONVERT(date,s.EndDate) >= CONVERT(date,GETDATE())
	AND s.IsOpen = 1
	AND s.Id = @SurveyId
RETURN 1
