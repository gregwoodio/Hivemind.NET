CREATE TABLE [dbo].[Territories] (
    [territoryId] INT            NOT NULL,
    [name]        NVARCHAR (100) NOT NULL,
    [description] NVARCHAR (MAX) NOT NULL,
    [income]      NVARCHAR (50)  NOT NULL,
    CONSTRAINT [PK_Territories] PRIMARY KEY CLUSTERED ([territoryId] ASC)
);

