CREATE TABLE [dbo].[Injuries] (
    [injuryId]    INT            NOT NULL,
    [injuryName]  NVARCHAR (100) NOT NULL,
    [description] NVARCHAR (MAX) NOT NULL,
    CONSTRAINT [PK_Injuries] PRIMARY KEY CLUSTERED ([injuryId] ASC)
);

