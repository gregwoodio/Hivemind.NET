CREATE TABLE [dbo].[Gangs] (
    [gangId]   NVARCHAR (100) NOT NULL,
    [gangName] NVARCHAR (100) NOT NULL,
    [house]    INT            NOT NULL,
    [credits]  INT            NULL,
    CONSTRAINT [PK_Gangs] PRIMARY KEY CLUSTERED ([gangId] ASC)
);

