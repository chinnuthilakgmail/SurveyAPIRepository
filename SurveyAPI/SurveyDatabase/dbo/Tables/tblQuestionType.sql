CREATE TABLE [dbo].[tblQuestionType]
(
	[Id] INT IDENTITY(1,1) NOT NULL PRIMARY KEY, 
    [QuestionType] NVARCHAR(200) NULL, 
    [IsOptionsRequired] BIT NOT NULL
)
