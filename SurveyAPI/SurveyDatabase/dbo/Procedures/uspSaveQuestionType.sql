CREATE PROCEDURE [dbo].[uspSaveQuestionType]
	@QuestionType nvarchar(200) ,
	@IsOptionsRequired bit
AS
DECLARE @Count INT = 0
SELECT @Count = COUNT(*) FROM tblQuestionType WHERE QuestionType = @QuestionType
IF @Count = 0
BEGIN
	INSERT INTO tblQuestionType (QuestionType,IsOptionsRequired) VALUES (@QuestionType,@IsOptionsRequired)
	RETURN 1
END
RETURN 0
