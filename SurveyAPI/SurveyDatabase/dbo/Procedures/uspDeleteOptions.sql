CREATE PROCEDURE [dbo].[uspDeleteOptions]
	@QuestionId int  
AS
	DELETE tblOptions WHERE QuestionId = @QuestionId
RETURN 1
