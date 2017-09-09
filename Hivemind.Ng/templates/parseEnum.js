var swagger = require('./swagger.json');
var Mustache = require('Mustache');

for (let key in swagger.definitions) {
    let obj = swagger.definitions[key];
    if (obj.properties) {
        for (let prop in obj.properties) {
            let values = obj.properties[prop];
            if (values.enum) {
                let enumValues = {
                    "name": prop,
                    "values": values.enum.map(x => { return {'value': x}})
                };
                console.log(JSON.parse(enumValues));
                // let template = Mustache.parse('./enum-template.mustache');

                // console.log(template);
                // Mustache.render(template, enumValues);
            }
        }
    }
}




let obj = { "name": "House", "values": [   { "value": "CAWDOR" },   { "value": "ESCHER" } ] }