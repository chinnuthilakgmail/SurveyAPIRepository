CREATE TABLE [dbo].[tblAnswers]
(
	[Id] INT NOT NULL IDENTITY(1,1) PRIMARY KEY, 
    [SurveyId] INT NOT NULL REFERENCES tblSurvey(Id), 
    [QuestionId] INT NOT NULL REFERENCES tblQuestions(Id), 
    [OptionId] INT NULL REFERENCES tblOptions(Id), 
    [AnswerText] NVARCHAR(MAX) NULL, 
    [PersonId] INT NOT NULL REFERENCES tblPerson(Id)
)
