﻿/*
Deployment script for SurveyDatabase

This code was generated by a tool.
Changes to this file may cause incorrect behavior and will be lost if
the code is regenerated.
*/

GO
SET ANSI_NULLS, ANSI_PADDING, ANSI_WARNINGS, ARITHABORT, CONCAT_NULL_YIELDS_NULL, QUOTED_IDENTIFIER ON;

SET NUMERIC_ROUNDABORT OFF;


GO
:setvar DatabaseName "SurveyDatabase"
:setvar DefaultFilePrefix "SurveyDatabase"
:setvar DefaultDataPath "C:\Users\Swathi Arun\AppData\Local\Microsoft\Microsoft SQL Server Local DB\Instances\MSSQLLocalDB\"
:setvar DefaultLogPath "C:\Users\Swathi Arun\AppData\Local\Microsoft\Microsoft SQL Server Local DB\Instances\MSSQLLocalDB\"

GO
:on error exit
GO
/*
Detect SQLCMD mode and disable script execution if SQLCMD mode is not supported.
To re-enable the script after enabling SQLCMD mode, execute the following:
SET NOEXEC OFF; 
*/
:setvar __IsSqlCmdEnabled "True"
GO
IF N'$(__IsSqlCmdEnabled)' NOT LIKE N'True'
    BEGIN
        PRINT N'SQLCMD mode must be enabled to successfully execute this script.';
        SET NOEXEC ON;
    END


GO
USE [$(DatabaseName)];


GO
PRINT N'Altering [dbo].[uspGetSurveyDetails]...';


GO
ALTER PROCEDURE [dbo].[uspGetSurveyDetails]
	@SurveyId int  
AS
	SELECT s.Id as SurveyId, s.Title as SurveyTitle,s.Description as SurveyDescription,
		--s.StartDate as SurveyStartDate,s.EndDate as SurveyEndDate,
		m.QuestionNumber as QuestionNumber,
		s.IsOpen as IsOpen,
		q.Id as QuestionId,q.Question as Question,
		t.Id as QuestionTypeId,t.QuestionType as QuestionType,t.IsOptionsRequired as IsOptionsRequired,
		o.Id as OptionId,o.OptionText as OptionText
		
	FROM tblSurveyQuestionMapping m
	JOIN tblSurvey s ON s.Id = m.SurveyId
	JOIN tblQuestions q ON q.Id = m.QuestionId
	JOIN tblQuestionType t ON t.Id = q.QuestionType
	LEFT OUTER JOIN tblOptions o ON o.QuestionId = m.QuestionId

	WHERE CONVERT(date, s.StartDate) <= CONVERT(date, GETDATE())
	AND CONVERT(date,s.EndDate) >= CONVERT(date,GETDATE())
	AND s.IsOpen = 1
	AND s.Id = @SurveyId
RETURN 1
GO
PRINT N'Update complete.';


GO
