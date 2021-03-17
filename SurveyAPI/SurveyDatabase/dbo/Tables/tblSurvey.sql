CREATE TABLE [dbo].[tblSurvey]
(
	[Id] INT IDENTITY(1,1) NOT NULL PRIMARY KEY, 
    [Title] NVARCHAR(200) NOT NULL, 
    [Description] NVARCHAR(MAX) NULL, 
    [StartDate] DATETIME NULL, 
    [EndDate] DATETIME NULL, 
    [IsOpen] BIT NOT NULL
	
)