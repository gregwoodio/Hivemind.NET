# Hivemind

## Autogenerating clients and entities

For Hivemind NG, clients and entities are automatically generated from the swagger.json provided by Swashbuckle. For instance, if you make a change a class in the Hivemind C# project, rebuild the project and then run Hivemind.Api. Go to ```http://localhost:<port>/swagger/docs/v1``` and copy and paste the swagger.json overtop of the one found in the /dev directory. Then navigate to the directory and run ```node .\generate.js```. This will parse through the swagger.json and, using mustache templates, create typescript classes for use in the Angular project. This way the classes stay up to date with the main project without the need for rewriting code.