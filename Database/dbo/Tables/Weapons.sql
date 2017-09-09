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

