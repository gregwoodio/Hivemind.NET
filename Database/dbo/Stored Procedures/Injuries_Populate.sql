-- Batch submitted through debugger: SQLQuery28.sql|4|0|C:\Users\Greg\AppData\Local\Temp\~vs7AFE.sql

CREATE PROCEDURE [dbo].[Injuries_Populate] 
AS
BEGIN
	SET NOCOUNT ON;

	-- ensure dbo.Injuries exists
	IF OBJECT_ID('dbo.Injuries', 'U') IS NULL
		BEGIN
		
		CREATE TABLE [dbo].[Injuries] (
			[injuryId]    INT            NOT NULL,
			[injuryName]  NVARCHAR (100) NOT NULL,
			[description] NVARCHAR (MAX) NOT NULL,
			CONSTRAINT [PK_TempInjuries] PRIMARY KEY CLUSTERED ([injuryId] ASC)
		);

		END;

	-- create working table
    IF OBJECT_ID('dbo.TempInjuries', 'U') IS NOT NULL
		 DROP TABLE dbo.TempInjuries;

	CREATE TABLE [dbo].[TempInjuries] (
	    [injuryId]    INT            NOT NULL,
		[injuryName]  NVARCHAR (100) NOT NULL,
		[description] NVARCHAR (MAX) NOT NULL,
		CONSTRAINT [PK_TempInjuries] PRIMARY KEY CLUSTERED ([injuryId] ASC)
	);

	-- read from CSV into working table
	BULK INSERT [dbo].[TempInjuries] FROM '..\..\StaticValues\Injuries.csv' WITH (
		FIRSTROW = 1,
		FIELDTERMINATOR = ',',
		ROWTERMINATOR = '\n',
		ERRORFILE = '..\..\StaticValues\Injuries.Error.csv',
		DATAFILETYPE = 'widechar',
		TABLOCK
	);

	-- update or insert values from working table into permanent table
	MERGE dbo.Injuries i
	USING (SELECT * FROM dbo.TempInjuries) AS ti
		ON i.injuryId = ti.injuryId
	WHEN MATCHED THEN
		UPDATE SET i.injuryName = ti.injuryName, i.description = ti.description
	WHEN NOT MATCHED THEN
		INSERT VALUES (ti.injuryId, ti.injuryName, ti.description);

	-- drop working table
	DROP TABLE dbo.TempInjuries;
END