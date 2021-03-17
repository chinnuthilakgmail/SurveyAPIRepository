CREATE PROCEDURE [dbo].[uspSaveAnswer]
	@SurveyId int,
	@QuestionId int,
	@OptionId int =0,
	@Answer nvarchar(MAX) = NULL,
	@PersonName nvarchar(200),
	@PersonEmail nvarchar(300)
AS
DECLARE @Count INT = 0
DECLARE @PersonId INT = 0
DECLARE @IsOptionRequired BIT =0
--Get person Id
SELECT @PersonId = Id FROM tblPerson WHERE EmailId = @PersonEmail
IF @PersonId IS NULL OR @PersonId = 0
BEGIN
	INSERT INTO tblPerson (EmailId,Name) VALUES (@PersonEmail,@PersonName)
	SET @PersonId = SCOPE_IDENTITY()
END

--Check If Question, survey exists
SELECT @Count = COUNT(*) FROM tblSurveyQuestionMapping WHERE QuestionId = @QuestionId AND SurveyId = @SurveyId
IF @Count > 0
BEGIN
	SELECT @IsOptionRequired = IsOptionsRequired FROM tblQuestionType  WHERE Id = 
	(SELECT QuestionType FROM tblQuestions WHERE Id = @QuestionId)
	IF @IsOptionRequired = 1
	BEGIN
	--Check if option exists
		SELECT @Count = COUNT(*) FROM tblOptions WHERE Id = @OptionId AND QuestionId = @QuestionId
		IF @Count > 0
		BEGIN
			--Save Option
			INSERT INTO tblAnswers (AnswerText,OptionId,PersonId,QuestionId,SurveyId) VALUES (NULL,@OptionId,@PersonId,@QuestionId,@SurveyId)
			RETURN 1 --Success
		END
		ELSE
		RETURN 2 --No Option Available
	END
	ELSE
	BEGIN
		--Save Answer
		INSERT INTO tblAnswers (AnswerText,OptionId,PersonId,QuestionId,SurveyId) VALUES (@Answer,0,@PersonId,@QuestionId,@SurveyId)
		RETURN 1 --Success
	END
END
RETURN 0 -- qstn or survey does not exist
