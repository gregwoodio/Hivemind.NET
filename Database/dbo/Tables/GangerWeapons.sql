CREATE TABLE [dbo].[GangerWeapons] (
    [gangerWeaponId] NVARCHAR (100) NOT NULL,
    [gangerId]       NVARCHAR (100) NULL,
    [weaponId]       INT            NULL,
    [cost]           INT            NOT NULL,
    PRIMARY KEY CLUSTERED ([gangerWeaponId] ASC),
    CONSTRAINT [FK_GangerWeapons_GangerId] FOREIGN KEY ([gangerId]) REFERENCES [dbo].[Gangers] ([gangerId]),
    CONSTRAINT [FK_GangerWeapons_WeaponId] FOREIGN KEY ([weaponId]) REFERENCES [dbo].[Weapons] ([weaponId])
);



