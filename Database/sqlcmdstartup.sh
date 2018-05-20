# wait for sql server to start
sleep 20

# create database
/opt/mssql-tools/bin/sqlcmd -S localhost -U sa -P Welcome1234 -i ./sqlcmdscript.sql

sleep 5

# create tables
/opt/mssql-tools/bin/sqlcmd -S localhost -U sa -P Welcome1234 -d HivemindDb -i './dbo/Tables/Users.sql'
/opt/mssql-tools/bin/sqlcmd -S localhost -U sa -P Welcome1234 -d HivemindDb -i './dbo/Tables/Gangs.sql'
/opt/mssql-tools/bin/sqlcmd -S localhost -U sa -P Welcome1234 -d HivemindDb -i './dbo/Tables/Gangers.sql'
/opt/mssql-tools/bin/sqlcmd -S localhost -U sa -P Welcome1234 -d HivemindDb -i './dbo/Tables/Injuries.sql'
/opt/mssql-tools/bin/sqlcmd -S localhost -U sa -P Welcome1234 -d HivemindDb -i './dbo/Tables/Skills.sql'
/opt/mssql-tools/bin/sqlcmd -S localhost -U sa -P Welcome1234 -d HivemindDb -i './dbo/Tables/Territories.sql'
/opt/mssql-tools/bin/sqlcmd -S localhost -U sa -P Welcome1234 -d HivemindDb -i './dbo/Tables/Weapons.sql'
/opt/mssql-tools/bin/sqlcmd -S localhost -U sa -P Welcome1234 -d HivemindDb -i './dbo/Tables/UserGangs.sql'
/opt/mssql-tools/bin/sqlcmd -S localhost -U sa -P Welcome1234 -d HivemindDb -i './dbo/Tables/GangTerritories.sql'
/opt/mssql-tools/bin/sqlcmd -S localhost -U sa -P Welcome1234 -d HivemindDb -i './dbo/Tables/GangWeapons.sql'
/opt/mssql-tools/bin/sqlcmd -S localhost -U sa -P Welcome1234 -d HivemindDb -i './dbo/Tables/GangerAdvancements.sql'
/opt/mssql-tools/bin/sqlcmd -S localhost -U sa -P Welcome1234 -d HivemindDb -i './dbo/Tables/GangerInjuries.sql'
/opt/mssql-tools/bin/sqlcmd -S localhost -U sa -P Welcome1234 -d HivemindDb -i './dbo/Tables/GangerWeapons.sql'
/opt/mssql-tools/bin/sqlcmd -S localhost -U sa -P Welcome1234 -d HivemindDb -i './dbo/Tables/GangerSkills.sql'

# create stored procedures
/opt/mssql-tools/bin/sqlcmd -S localhost -U sa -P Welcome1234 -d HivemindDb -i './dbo/Stored Procedures/GangerAdvancements_Add.sql'
/opt/mssql-tools/bin/sqlcmd -S localhost -U sa -P Welcome1234 -d HivemindDb -i './dbo/Stored Procedures/GangerAdvancements_IsValid.sql'
/opt/mssql-tools/bin/sqlcmd -S localhost -U sa -P Welcome1234 -d HivemindDb -i './dbo/Stored Procedures/GangerAdvancements_Remove.sql'
/opt/mssql-tools/bin/sqlcmd -S localhost -U sa -P Welcome1234 -d HivemindDb -i './dbo/Stored Procedures/GangerInjuries_Add.sql'
/opt/mssql-tools/bin/sqlcmd -S localhost -U sa -P Welcome1234 -d HivemindDb -i './dbo/Stored Procedures/GangerSkills_Add.sql'
/opt/mssql-tools/bin/sqlcmd -S localhost -U sa -P Welcome1234 -d HivemindDb -i './dbo/Stored Procedures/GangerSkills_GetByGangId.sql'
/opt/mssql-tools/bin/sqlcmd -S localhost -U sa -P Welcome1234 -d HivemindDb -i './dbo/Stored Procedures/Gangers_AddGanger.sql'
/opt/mssql-tools/bin/sqlcmd -S localhost -U sa -P Welcome1234 -d HivemindDb -i './dbo/Stored Procedures/Gangers_GetByGangId.sql'
/opt/mssql-tools/bin/sqlcmd -S localhost -U sa -P Welcome1234 -d HivemindDb -i './dbo/Stored Procedures/Gangers_GetById.sql'
/opt/mssql-tools/bin/sqlcmd -S localhost -U sa -P Welcome1234 -d HivemindDb -i './dbo/Stored Procedures/Gangers_UpdateGanger.sql'
/opt/mssql-tools/bin/sqlcmd -S localhost -U sa -P Welcome1234 -d HivemindDb -i './dbo/Stored Procedures/GangerWeapons_AddGangerWeapon.sql'
/opt/mssql-tools/bin/sqlcmd -S localhost -U sa -P Welcome1234 -d HivemindDb -i './dbo/Stored Procedures/GangerWeapons_GetByGangId.sql'
/opt/mssql-tools/bin/sqlcmd -S localhost -U sa -P Welcome1234 -d HivemindDb -i './dbo/Stored Procedures/GangerWeapons_RemoveGangerWeapon.sql'
/opt/mssql-tools/bin/sqlcmd -S localhost -U sa -P Welcome1234 -d HivemindDb -i './dbo/Stored Procedures/Gangs_AddGang.sql'
/opt/mssql-tools/bin/sqlcmd -S localhost -U sa -P Welcome1234 -d HivemindDb -i './dbo/Stored Procedures/Gangs_GetById.sql'
/opt/mssql-tools/bin/sqlcmd -S localhost -U sa -P Welcome1234 -d HivemindDb -i './dbo/Stored Procedures/Gangs_UpdateGang.sql'
/opt/mssql-tools/bin/sqlcmd -S localhost -U sa -P Welcome1234 -d HivemindDb -i './dbo/Stored Procedures/GangTerritories_Add.sql'
/opt/mssql-tools/bin/sqlcmd -S localhost -U sa -P Welcome1234 -d HivemindDb -i './dbo/Stored Procedures/GangTerritories_Remove.sql'
/opt/mssql-tools/bin/sqlcmd -S localhost -U sa -P Welcome1234 -d HivemindDb -i './dbo/Stored Procedures/GangWeapons_AddGangWeapon.sql'
/opt/mssql-tools/bin/sqlcmd -S localhost -U sa -P Welcome1234 -d HivemindDb -i './dbo/Stored Procedures/GangWeapons_RemoveGangWeapon.sql'
/opt/mssql-tools/bin/sqlcmd -S localhost -U sa -P Welcome1234 -d HivemindDb -i './dbo/Stored Procedures/Injuries_GetAll.sql'
/opt/mssql-tools/bin/sqlcmd -S localhost -U sa -P Welcome1234 -d HivemindDb -i './dbo/Stored Procedures/Injuries_GetByGangerId.sql'
/opt/mssql-tools/bin/sqlcmd -S localhost -U sa -P Welcome1234 -d HivemindDb -i './dbo/Stored Procedures/Injuries_GetByGangId.sql'
/opt/mssql-tools/bin/sqlcmd -S localhost -U sa -P Welcome1234 -d HivemindDb -i './dbo/Stored Procedures/Injuries_GetById.sql'
/opt/mssql-tools/bin/sqlcmd -S localhost -U sa -P Welcome1234 -d HivemindDb -i './dbo/Stored Procedures/Injuries_Populate.sql'
/opt/mssql-tools/bin/sqlcmd -S localhost -U sa -P Welcome1234 -d HivemindDb -i './dbo/Stored Procedures/Skills_GetAll.sql'
/opt/mssql-tools/bin/sqlcmd -S localhost -U sa -P Welcome1234 -d HivemindDb -i './dbo/Stored Procedures/Territories_GetAll.sql'
/opt/mssql-tools/bin/sqlcmd -S localhost -U sa -P Welcome1234 -d HivemindDb -i './dbo/Stored Procedures/Territories_GetByGangId.sql'
/opt/mssql-tools/bin/sqlcmd -S localhost -U sa -P Welcome1234 -d HivemindDb -i './dbo/Stored Procedures/Territories_GetById.sql'
/opt/mssql-tools/bin/sqlcmd -S localhost -U sa -P Welcome1234 -d HivemindDb -i './dbo/Stored Procedures/Territories_Populate.sql'
/opt/mssql-tools/bin/sqlcmd -S localhost -U sa -P Welcome1234 -d HivemindDb -i './dbo/Stored Procedures/UserGangs_AssociateGangToUser.sql'
/opt/mssql-tools/bin/sqlcmd -S localhost -U sa -P Welcome1234 -d HivemindDb -i './dbo/Stored Procedures/UserGangs_GetByUserGuid.sql'
/opt/mssql-tools/bin/sqlcmd -S localhost -U sa -P Welcome1234 -d HivemindDb -i './dbo/Stored Procedures/Users_Add.sql'
/opt/mssql-tools/bin/sqlcmd -S localhost -U sa -P Welcome1234 -d HivemindDb -i './dbo/Stored Procedures/Users_GetByEmail.sql'
/opt/mssql-tools/bin/sqlcmd -S localhost -U sa -P Welcome1234 -d HivemindDb -i './dbo/Stored Procedures/Users_GetUserByGuid.sql'
/opt/mssql-tools/bin/sqlcmd -S localhost -U sa -P Welcome1234 -d HivemindDb -i './dbo/Stored Procedures/Weapons_GetAll.sql'
/opt/mssql-tools/bin/sqlcmd -S localhost -U sa -P Welcome1234 -d HivemindDb -i './dbo/Stored Procedures/Weapons_GetByGangerId.sql'
/opt/mssql-tools/bin/sqlcmd -S localhost -U sa -P Welcome1234 -d HivemindDb -i './dbo/Stored Procedures/Weapons_GetById.sql'
/opt/mssql-tools/bin/sqlcmd -S localhost -U sa -P Welcome1234 -d HivemindDb -i './dbo/Stored Procedures/Weapons_GetGangerWeaponsByGangId.sql'
/opt/mssql-tools/bin/sqlcmd -S localhost -U sa -P Welcome1234 -d HivemindDb -i './dbo/Stored Procedures/Weapons_GetGangStash.sql'
/opt/mssql-tools/bin/sqlcmd -S localhost -U sa -P Welcome1234 -d HivemindDb -i './dbo/Stored Procedures/Weapons_Populate.sql'

# populate tables that need static data
/opt/mssql-tools/bin/sqlcmd -S localhost -U sa -P Welcome1234 -d HivemindDb -i './populate.sql'
