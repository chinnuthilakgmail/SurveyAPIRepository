CREATE TABLE [dbo].[tblOptions]
(
	[Id] INT NOT NULL IDENTITY(1,1) PRIMARY KEY, 
    [QuestionId] INT NOT NULL REFERENCES tblQuestions(Id), 
    [OptionText] NVARCHAR(500) NOT NULL
)
