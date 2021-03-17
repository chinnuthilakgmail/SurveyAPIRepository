CREATE TABLE [dbo].[tblSurveyQuestionMapping]
(
	[SurveyId] INT NOT NULL , 
    [QuestionId] INT NOT NULL, 
    [QuestionNumber] INT NOT NULL, 
    CONSTRAINT [PK_tblSurveyQuestionMapping] PRIMARY KEY ([QuestionId], [SurveyId]),  
)
