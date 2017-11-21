CREATE TABLE [dbo].[UserGangs] (
    [userGangId] NVARCHAR (100) DEFAULT (newid()) NOT NULL,
    [userId]     INT            NOT NULL,
    [gangId]     NVARCHAR (100) NOT NULL,
    PRIMARY KEY CLUSTERED ([userGangId] ASC),
    CONSTRAINT [FK_UserGangs_GangId] FOREIGN KEY ([gangId]) REFERENCES [dbo].[Gangs] ([gangId]),
    CONSTRAINT [FK_UserGangs_UserId] FOREIGN KEY ([userId]) REFERENCES [dbo].[Users] ([userId])
);

