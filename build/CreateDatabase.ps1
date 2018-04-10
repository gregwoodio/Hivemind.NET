Import-Module AzureRM
Import-Module -Name SqlServer

Connect-AzureRmAccount

$rand = Get-Random
$resourceGroupName = "hivemindRG-$rand"
$location = "canadaeast"

$servername = "hiveminddb-$rand" # must be lowercase, numbers and hyphens
$creds = Get-Content .\content\database.json | ConvertFrom-Json
$adminLogin = $creds.adminLogin
$password = $creds.password

$ip = Invoke-RestMethod https://ipinfo.io/json

$startIP = $ip.ip
$endIP = $ip.ip
$databaseName = "HivemindDb"

New-AzureRmResourceGroup -Name $resourceGroupName -Location $location

$sqlServer = New-AzureRmSqlServer -ResourceGroupName $resourceGroupName -ServerName $servername -Location $location `
    -SqlAdministratorCredentials $(New-Object -TypeName System.Management.Automation.PSCredential -ArgumentList $adminLogin, $(ConvertTo-SecureString -String $password -AsPlainText -Force))

New-AzureRmSqlServerFirewallRule -ResourceGroupName $resourceGroupName `
    -ServerName $servername `
    -FirewallRuleName "AllowSome" -StartIpAddress $startIP -EndIpAddress $endIP

New-AzureRmSqlDatabase -ResourceGroupName $resourceGroupName `
    -ServerName $servername `
    -DatabaseName $databaseName `
    -RequestedServiceObjectiveName "Basic"

# Populate the database
$tables = 'D:\Jenkins\workspace\Hivemind\Database\dbo\Tables\Users.sql', `
    'D:\Jenkins\workspace\Hivemind\Database\dbo\Tables\Gangs.sql', `
    'D:\Jenkins\workspace\Hivemind\Database\dbo\Tables\Gangers.sql', `
    'D:\Jenkins\workspace\Hivemind\Database\dbo\Tables\Weapons.sql', `
    'D:\Jenkins\workspace\Hivemind\Database\dbo\Tables\Skills.sql', `
    'D:\Jenkins\workspace\Hivemind\Database\dbo\Tables\Injuries.sql', `
    'D:\Jenkins\workspace\Hivemind\Database\dbo\Tables\Territories.sql', `
    'D:\Jenkins\workspace\Hivemind\Database\dbo\Tables\UserGangs.sql', `
    'D:\Jenkins\workspace\Hivemind\Database\dbo\Tables\GangWeapons.sql', `
    'D:\Jenkins\workspace\Hivemind\Database\dbo\Tables\GangTerritories.sql', `
    'D:\Jenkins\workspace\Hivemind\Database\dbo\Tables\GangerAdvancements.sql', `
    'D:\Jenkins\workspace\Hivemind\Database\dbo\Tables\GangerInjuries.sql', `
    'D:\Jenkins\workspace\Hivemind\Database\dbo\Tables\GangerSkills.sql', `
    'D:\Jenkins\workspace\Hivemind\Database\dbo\Tables\GangerWeapons.sql'

$storedProcedureDirectory = 'D:\Jenkins\workspace\Hivemind\Database\dbo\Stored Procedures'
$sqlScripts = $tables + (Get-ChildItem $storedProcedureDirectory | Select-Object FullName)

$connectionString = $("Server=tcp:$($sqlServer.FullyQualifiedDomainName),1433;Initial Catalog=$databaseName;Persist Security Info=False;User ID=$adminLogin;Password=$password;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;")
$connection = New-Object -TypeName System.Data.SqlClient.SqlConnection($connectionString)
$connection.Open()

foreach ($script in $sqlScripts) 
{
    $query = [IO.File]::ReadAllText($script.FullName)
    $command = New-Object -TypeName System.Data.SqlClient.SqlCommand($query, $connection)
    $command.ExecuteNonQuery()
}
$connection.Close()