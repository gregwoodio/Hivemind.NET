CREATE TABLE [dbo].[Users] (
    [email]    NVARCHAR (255) NOT NULL,
    [password] NVARCHAR (100) NOT NULL,
    [userGUID] NVARCHAR (100) DEFAULT (newid()) NOT NULL,
    PRIMARY KEY CLUSTERED ([userGUID] ASC),
    UNIQUE NONCLUSTERED ([email] ASC)
);







