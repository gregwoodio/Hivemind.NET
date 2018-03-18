CREATE TABLE [dbo].[GangerAdvancements] (
    [advancementId] NVARCHAR (100) NOT NULL,
    [gangerId]      NVARCHAR (100) NULL,
    PRIMARY KEY CLUSTERED ([advancementId] ASC),
    CONSTRAINT [FK_GangerAdvancements_GangerId] FOREIGN KEY ([gangerId]) REFERENCES [dbo].[Gangers] ([gangerId])
);

