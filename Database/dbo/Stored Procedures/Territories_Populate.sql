-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[Territories_Populate]
AS
BEGIN
	SET NOCOUNT ON;

	-- ensure dbo.Territories exists
	IF OBJECT_ID('dbo.Territories', 'U') IS NULL
		BEGIN
		CREATE TABLE [dbo].[Territories] (
			[territoryId] INT            NOT NULL,
			[name]        NVARCHAR (100) NOT NULL,
			[description] NVARCHAR (MAX) NOT NULL,
			[income]      NVARCHAR (50)  NOT NULL,
			CONSTRAINT [PK_Territories] PRIMARY KEY CLUSTERED ([territoryId] ASC)
		);
		END;

	-- create working table
    IF OBJECT_ID('dbo.TempTerritories', 'U') IS NOT NULL
		 DROP TABLE dbo.TempTerritories;

	CREATE TABLE [dbo].[TempTerritories] (
		[territoryId] INT            NOT NULL,
		[name]        NVARCHAR (100) NOT NULL,
		[description] NVARCHAR (MAX) NOT NULL,
		[income]      NVARCHAR (50)  NOT NULL,
		CONSTRAINT [PK_TempTerritories] PRIMARY KEY CLUSTERED ([territoryId] ASC)
	);

	-- read from CSV into working table
	BULK INSERT [dbo].[TempTerritories] FROM 'D:\Source\Hivemind\Hivemind\Database\StaticValues\Territories.csv' WITH (
		FIRSTROW = 1,
		FIELDTERMINATOR = ',',
		ROWTERMINATOR = '\n',
		ERRORFILE = 'D:\Source\Hivemind\Hivemind\Database\StaticValues\Territories.Error.csv',
		DATAFILETYPE = 'widechar',
		TABLOCK
	);

	-- update or insert values from working table into permanent table
	MERGE dbo.Territories t
	USING (SELECT * FROM dbo.TempTerritories) AS tt
		ON t.territoryId = tt.territoryId
	WHEN MATCHED THEN
		UPDATE SET 
			t.name = tt.name,
			t.description = tt.description,
			t.income = tt.income
	WHEN NOT MATCHED THEN
		INSERT VALUES (
			tt.territoryId,
			tt.name,
			tt.description,
			tt.income
		);

	-- drop working table
	DROP TABLE dbo.TempTerritories;
END