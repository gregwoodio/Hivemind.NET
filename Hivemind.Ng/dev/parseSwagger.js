var swagger = require('./swagger.json');
var Mustache = require('Mustache');
var fs = require('fs');
var _ = require('lodash');

// After making changes to enums, classes or WebAPI project, run the project
// and go to http://localhost:61774/swagger/docs/v1 . Copy that file to
// swagger.json and run this function to update the typescript files.
function generateFromSwagger() {
    let templates = [
        { 
            func: parseEnums, 
            template: './templates/enum-template.mustache', 
            outputDir: '../src/app/entities/' 
        },
        { 
            func: parseClasses, 
            template: './templates/class-template.mustache',
            outputDir: '../src/app/entities/'
        },
        {
            func: parseClients,
            template: './templates/client-template.mustache',
            outputDir: '../src/app/clients/'
        }
    ];

    templates.forEach(type => {
        writeToFile(type.func(), type.template, type.outputDir);
    });

    console.log('Done!');
}

// parse swagger.json for template-friendly enum values.
function parseEnums() {
    let enums = [];

    for (let key in swagger.definitions) {
        let obj = swagger.definitions[key];
        if (obj.properties === undefined) {
            continue;
        }

        for (let prop in obj.properties) {
        let values = obj.properties[prop];
            if (values.enum) {
                enums.push({
                    "name": prop,
                    "first": values.enum[0],
                    "values": values.enum.map(x => { return {'value': x}}).splice(1, values.enum.length)
                });
            }
        }
    }
    return enums;
}

function parseClasses() {
    let classes = [];

    for (let key in swagger.definitions) {
        var definition = swagger.definitions[key];
        let obj = {
            name: key,
            properties: [],
            imports: {}
        };
        if (definition.properties === undefined) {
            continue;
        }
    
        for (let prop in definition.properties) {
            let type;
            let property = definition.properties[prop]

            if (property.type === undefined) {
                if (property.$ref === undefined) {
                    continue;
                }

                // references another class
                let ref = _.last(property.$ref.split('/'));
                obj.imports[ref] = ref;
                obj.properties.push({
                    name:  _.camelCase(prop),
                    type: ref
                });

                continue;
            }
            switch (property.type.toLowerCase()) {
                case 'integer':
                case 'float':
                case 'double':
                    type = 'number';
                    break;
                case 'boolean':
                    type = 'boolean';
                    break;
                case 'string':
                    type = 'string';
                    break;
                case 'array':
                    if (property.items.$ref) {
                        let referencedObjectName = _.last(property.items.$ref.split('/'));
                        type = referencedObjectName + '[]';
                        obj.imports[referencedObjectName] = referencedObjectName;
                    } else if (property.items.enum) {
                        type = 'any';
                    }
                    break;
                case 'enum':
                    type = prop;
                    break;
                case 'object':
                    type = 'any';
                    break;
            }
            obj.properties.push({
                name:  _.camelCase(prop),
                type: type
            });
        }

        // map imports from object to array
        obj.imports = _.values(obj.imports).map(value => {
            return { import: value }
        });

        if (obj.properties) {
            classes.push(obj);
        }
    }

    return classes;
}

function parseClients() {
    let clients = [];
    let paths = swagger.paths;

    for (let key in paths) {
        for (let httpMethod in paths[key]) {
            let client = {
                methods: [],
                imports: []
            };
            client.name = paths[key][httpMethod].tags[0] + 'Client';
            
            let alreadyFound = _.find(clients, {'name': client.name});
            if (alreadyFound) {
                client = alreadyFound;
            }

            var method = {
                path: prepareForPathParams(key),
                httpMethod: httpMethod,
                methodName: paths[key][httpMethod].operationId.split('_')[1],
                bodyParams: [],
                pathParams: []
            };

            if (paths[key][httpMethod].responses['200'] &&
                paths[key][httpMethod].responses['200'].schema &&
                paths[key][httpMethod].responses['200'].schema.$ref
            ) {
                let reference = _.last(paths[key][httpMethod].responses['200'].schema.$ref.split('/'))
                method.returns = reference;
                if (!_.find(client.imports, { import: reference })) {
                    client.imports.push({ import: reference });
                }
            } else if (paths[key][httpMethod].responses['200'] &&
                paths[key][httpMethod].responses['200'].schema &&
                paths[key][httpMethod].responses['200'].schema.items &&
                paths[key][httpMethod].responses['200'].schema.items.$ref
            ) {
                let reference = _.last(paths[key][httpMethod].responses['200'].schema.items.$ref.split('/'))
                method.returns = reference + '[]';
                if (!_.find(client.imports, { import: reference })) {
                    client.imports.push({ import: reference });
                }
            } else if (paths[key][httpMethod].responses['204']) {
                method.returns = 'string';
            } else {
                method.returns = 'void';
            }
            
            if (paths[key][httpMethod].parameters) {
                paths[key][httpMethod].parameters.forEach(param => {
                    let type;

                    if (!param.type) {
                        if (param.schema && param.schema.$ref) {
                            let reference = _.last(param.schema.$ref.split('/'));
                            if (!_.find(client.imports, { import: reference })) {
                                client.imports.push({ import: reference });
                            }
                            type = reference;
                        }
                    } else {
                        type = toTypeScriptType(param.type);
                    }

                    if (param.in == 'body') {
                        method.bodyParams.push({
                            name: param.name,
                            required: param.required ? '' : '?',
                            type: type
                        });
                    } else if (param.in == 'path') {
                        method.pathParams.push({
                            name: param.name,
                            required: param.required ? '' : '?',
                            type: type
                        });
                    }
                });
            }
            
            client.methods.push(method);

            if (!alreadyFound) {
                clients.push(client);                
            }
        }
    }

    return clients;
}

// write object to file using template
function writeToFile(values, templateLocation, outputDir) {
    let file = fs.readFileSync(templateLocation);
    let template = file.toString();
    values.forEach(value => {
        console.log('Writing ' + value.name + '.ts...');
        let rendered = Mustache.render(template, value);
        fs.writeFileSync(outputDir + value.name + '.ts', rendered);
    });
}

function prepareForPathParams(path) {
    return path.split('{').join('\' + ').split('}').join(' + \'');
}

function toTypeScriptType(cSharpType) {
    let type;
    switch (cSharpType)  {
        case 'integer':
        case 'float':
        case 'double':
            type = 'number';
            break;
        case 'boolean':
            type = 'boolean';
            break;
        case 'string':
            type = 'string';
            break;
        case 'array':
            // ?
            break;
        case 'enum':
            type = prop;
            break;
        case 'object':
            type = 'any';
            break;
    }
    return type;
}

module.exports = generateFromSwagger;