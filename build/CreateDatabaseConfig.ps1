# CreateDatabaseConfig

#<?xml version="1.0"?>
#<connectionStrings>
#  <add name="default" connectionString="Data Source=localhost\SQLEXPRESS;Initial Catalog=HivemindDb;Integrated Security=True"/>
#</connectionStrings>

function CreateDatabaseConfig
{
[cmdletbinding()]
Param (
    [string] $connectionString,
    [string] $filePath
)
Process 
    {
        $xml = New-Object System.Xml.XmlDocument
        $declaration = $xml.CreateXmlDeclaration("1.0", "UTF-8", $null)
        $root = $xml.DocumentElement

        $xml.InsertBefore($declaration, $root)
        $connectionStrings = $xml.CreateElement("connectionStrings")
        $xml.AppendChild($connectionStrings)

        $addConnectionStringElement = $xml.CreateElement("add")
        $nameAttribute = $xml.CreateAttribute("name")
        $nameAttribute.Value = "default"
        $addConnectionStringElement.SetAttributeNode($nameAttribute)

        $connectionStringAttribute = $xml.CreateAttribute("connectionString")
        $connectionStringAttribute.Value = $connectionString
        $addConnectionStringElement.SetAttributeNode($connectionStringAttribute)

        $connectionStrings.AppendChild($addConnectionStringElement)
        $xml.Save($filePath)
    }
}