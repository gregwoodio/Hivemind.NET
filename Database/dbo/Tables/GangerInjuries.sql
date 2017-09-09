CREATE TABLE [dbo].[GangerInjuries] (
    [gangerInjuryId] NVARCHAR (100) NOT NULL,
    [gangerId]       NVARCHAR (100) NULL,
    [injuryId]       INT            NULL,
    PRIMARY KEY CLUSTERED ([gangerInjuryId] ASC),
    CONSTRAINT [FK_GangerInjury_GangerId] FOREIGN KEY ([gangerId]) REFERENCES [dbo].[Gangers] ([gangerId]),
    CONSTRAINT [FK_GangerInjury_InjuryId] FOREIGN KEY ([injuryId]) REFERENCES [dbo].[Injuries] ([injuryId])
);

