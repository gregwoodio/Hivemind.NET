CREATE TABLE [dbo].[Users] (
    [userId]   INT            IDENTITY (1, 1) NOT NULL,
    [username] NVARCHAR (255) NOT NULL,
    [password] NVARCHAR (100) NOT NULL,
    [userGUID] NVARCHAR (100) DEFAULT (newid()) NULL,
    PRIMARY KEY CLUSTERED ([userId] ASC),
    UNIQUE NONCLUSTERED ([username] ASC)
);



