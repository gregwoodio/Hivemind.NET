CREATE TABLE [dbo].[GangTerritories] (
    [gangTerritoryId] NVARCHAR (100) NOT NULL,
    [gangId]          NVARCHAR (100) NOT NULL,
    [territoryId]     INT            NOT NULL,
    PRIMARY KEY CLUSTERED ([gangTerritoryId] ASC),
    CONSTRAINT [FK_Gangs] FOREIGN KEY ([gangId]) REFERENCES [dbo].[Gangs] ([gangId]),
    CONSTRAINT [FK_Territories] FOREIGN KEY ([territoryId]) REFERENCES [dbo].[Territories] ([territoryId])
);

