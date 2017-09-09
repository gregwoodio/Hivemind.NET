CREATE TABLE [dbo].[GangerSkills] (
    [gangerSkillId] NVARCHAR (100) NOT NULL,
    [gangerId]      NVARCHAR (100) NULL,
    [skillId]       INT            NULL,
    PRIMARY KEY CLUSTERED ([gangerSkillId] ASC),
    CONSTRAINT [FK_GangerSkills_GangerId] FOREIGN KEY ([gangerId]) REFERENCES [dbo].[Gangers] ([gangerId]),
    CONSTRAINT [FK_GangerSkills_SkillId] FOREIGN KEY ([skillId]) REFERENCES [dbo].[Skills] ([skillId])
);

