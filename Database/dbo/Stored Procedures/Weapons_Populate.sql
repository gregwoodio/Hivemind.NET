
CREATE PROCEDURE dbo.Weapons_Populate
AS
BEGIN
	SET NOCOUNT ON;

	-- ensure dbo.Weapons exists
	IF OBJECT_ID('dbo.Weapons', 'U') IS NULL
		BEGIN
		CREATE TABLE [dbo].[Weapons] (
			[weaponId]     INT            NOT NULL,
			[weaponName]   NVARCHAR (50)  NOT NULL,
			[shortRange]   NVARCHAR (50)  NULL,
			[longRange]    NVARCHAR (50)  NULL,
			[hitShort]     NVARCHAR (50)  NULL,
			[hitLong]      NVARCHAR (50)  NULL,
			[strength]     NVARCHAR (50)  NULL,
			[damage]       NVARCHAR (50)  NULL,
			[saveMod]      NVARCHAR (50)  NULL,
			[ammoRoll]     NVARCHAR (50)  NULL,
			[type]         INT            NOT NULL,
			[cost]         NVARCHAR (50)  NOT NULL,
			[availability] INT            NULL,
			[description]  NVARCHAR (MAX) NULL,
			CONSTRAINT [PK_Weapons] PRIMARY KEY CLUSTERED ([weaponId] ASC)
		);
		END;

	-- create working table
    IF OBJECT_ID('dbo.TempWeapons', 'U') IS NOT NULL
		 DROP TABLE dbo.TempWeapons;

	CREATE TABLE [dbo].[TempWeapons] (
		[weaponId]     INT            NOT NULL,
		[weaponName]   NVARCHAR (50)  NOT NULL,
		[shortRange]   NVARCHAR (50)  NULL,
		[longRange]    NVARCHAR (50)  NULL,
		[hitShort]     NVARCHAR (50)  NULL,
		[hitLong]      NVARCHAR (50)  NULL,
		[strength]     NVARCHAR (50)  NULL,
		[damage]       NVARCHAR (50)  NULL,
		[saveMod]      NVARCHAR (50)  NULL,
		[ammoRoll]     NVARCHAR (50)  NULL,
		[type]         INT            NOT NULL,
		[cost]         NVARCHAR (50)  NOT NULL,
		[availability] INT            NULL,
		[description]  NVARCHAR (MAX) NULL,
		CONSTRAINT [PK_TempWeapons] PRIMARY KEY CLUSTERED ([weaponId] ASC)
	);

	-- read from CSV into working table
	BULK INSERT [dbo].[TempWeapons] FROM '..\..\StaticValues\Weapons.csv' WITH (
		FIRSTROW = 1,
		FIELDTERMINATOR = ',',
		ROWTERMINATOR = '\n',
		ERRORFILE = '..\..\StaticValues\Weapons.Error.csv',
		DATAFILETYPE = 'widechar',
		TABLOCK
	);

	-- update or insert values from working table into permanent table
	MERGE dbo.Weapons w
	USING (SELECT * FROM dbo.TempWeapons) AS tw
		ON w.weaponId = tw.weaponId
	WHEN MATCHED THEN
		UPDATE SET 
			w.weaponName = tw.weaponName,
			w.shortRange = tw.shortRange,
			w.longRange = tw.longRange,
			w.hitShort = tw.hitShort,
			w.hitLong = tw.hitLong,
			w.strength = tw.strength,
			w.damage = tw.damage,
			w.saveMod = tw.saveMod,
			w.ammoRoll = tw.ammoRoll,
			w.type = tw.type,
			w.cost = tw.cost,
			w.availability = tw.availability,
			w.description = tw.description
	WHEN NOT MATCHED THEN
		INSERT VALUES (
			tw.weaponId,
			tw.weaponName,
			tw.shortRange,
			tw.longRange,
			tw.hitShort,
			tw.hitLong,
			tw.strength,
			tw.damage,
			tw.saveMod,
			tw.ammoRoll,
			tw.type,
			tw.cost,
			tw.availability,
			tw.description
		);

	-- drop working table
	DROP TABLE dbo.TempWeapons;
END