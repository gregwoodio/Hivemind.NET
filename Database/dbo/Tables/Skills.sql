CREATE TABLE [dbo].[Skills] (
    [skillId]     INT            NOT NULL,
    [skillName]   NVARCHAR (100) NOT NULL,
    [description] NVARCHAR (MAX) NOT NULL,
    [category]    INT            NOT NULL,
    CONSTRAINT [PK_Skills] PRIMARY KEY CLUSTERED ([skillId] ASC)
);

