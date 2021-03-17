CREATE PROCEDURE [dbo].[uspSaveOption]
	@OptionText nvarchar(500),
	@QuestionId int
AS
 DECLARE @IsRequired BIT = 0
 --Check if Options required
  SELECT @IsRequired =  IsOptionsRequired FROM tblQuestionType  WHERE Id = 
	(SELECT QuestionType FROM tblQuestions WHERE Id = @QuestionId)

IF @IsRequired = 1
BEGIN
	INSERT INTO tblOptions (OptionText,QuestionId) VALUES (@OptionText,@QuestionId)
	RETURN 1
END
RETURN 0
