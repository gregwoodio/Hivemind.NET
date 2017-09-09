-- Batch submitted through debugger: SQLQuery28.sql|4|0|C:\Users\Greg\AppData\Local\Temp\~vs7AFE.sql

CREATE PROCEDURE [dbo].[Injuries_Populate] 
AS
BEGIN
	SET NOCOUNT ON;

    IF OBJECT_ID('dbo.TempInjuries', 'U') IS NOT NULL
		 DROP TABLE dbo.TempInjuries;

	CREATE TABLE [dbo].[TempInjuries] (
	    [injuryId]    INT            NOT NULL,
		[injuryName]  NVARCHAR (100) NOT NULL,
		[description] NVARCHAR (MAX) NOT NULL,
		CONSTRAINT [PK_TempInjuries] PRIMARY KEY CLUSTERED ([injuryId] ASC)
	);

	BULK INSERT [dbo].[TempInjuries] FROM 'D:\Source\Hivemind\Hivemind\Database\StaticValues\Injuries.csv' WITH (
		FIRSTROW = 1,
		FIELDTERMINATOR = ',',
		ROWTERMINATOR = '\n',
		ERRORFILE = 'D:\Source\Hivemind\Hivemind\Database\StaticValues\Injuries.Error.csv',
		TABLOCK
	);

	UPDATE i
	SET 
		i.description = ti.description,
		i.injuryName = ti.injuryName
	FROM dbo.Injuries i
	INNER JOIN dbo.TempInjuries ti
	ON i.injuryId = ti.injuryId;

	DROP TABLE dbo.TempInjuries;
END