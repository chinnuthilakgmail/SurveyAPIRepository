CREATE TABLE [dbo].[tblQuestions]
(
	[Id] INT IDENTITY(1,1) NOT NULL PRIMARY KEY, 
    [Question] NVARCHAR(MAX) NULL, 
    [QuestionType] INT NOT NULL REFERENCES tblQuestionType(Id)
)
