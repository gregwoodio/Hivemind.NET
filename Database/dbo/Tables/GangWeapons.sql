CREATE TABLE [dbo].[GangWeapons] (
    [gangWeaponId] NVARCHAR (100) NOT NULL,
    [gangId]       NVARCHAR (100) NULL,
    [weaponId]     INT            NULL,
    PRIMARY KEY CLUSTERED ([gangWeaponId] ASC),
    CONSTRAINT [FK_GangWeapons_GangId] FOREIGN KEY ([gangId]) REFERENCES [dbo].[Gangs] ([gangId]),
    CONSTRAINT [FK_GangWeapons_WeaponId] FOREIGN KEY ([weaponId]) REFERENCES [dbo].[Weapons] ([weaponId])
);

