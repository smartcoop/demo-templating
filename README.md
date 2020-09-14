# Little HTML templating server
## Description
This is a demo of a small HTMl templating server. Its purpose is to fill html templates with data from a JSON file.
It has 4 components :
- `DemoTemplating.JsonServer` : A nginx web server that serves static JSON files
- `DemoTemplating.Api` : An ASP.NET Core api that contains the HTML templates, and a single route for filling them
- `DemoTemplating.RendererLib` : A rendering library that uses RazorEngine to render the HTML files with the JSON data
- `DemoTemplating.Cli` : A Command Line Interface that can be used to render the HTML without calling the api
![Templating schema](schema.jpg?raw=true "Schema")
## Building and running the server
A single script `start.sh` takes care of building the docker images and running them. As long as you have Docker installed, all you need to do is run the script and everything will be ready.
## Rendering templates with the api
HTML templates must be placed in the `DemoTemplating.Api/Templates` folder and have the `.html` extension.
JSON files must be placed in the `DemoTemplating.JsonServer` folder and have the `.json` extension.
To render HTML templates and fill them with JSON data, simply call the `/templating/{htmlFileName}/{jsonFileName}` route.
By default, the api will run on port 80 and a few examples of HTML and JSON files are already present. If you want to test it, you can go to `http://localhost/templating/hello/name1` and see the result. This call will :
- get the HTML file `hello.html` from the template folder
- get the `name1.json` file from the nginx server
- fill the HTML placeholders with the data from the JSON file
- send the resulting HTML back
## Rendering templates with the cli

Console app (.NET Core).
Templating Cli render the html with the json passed as arguments of the command line and display it in browser.

This command line takes 3 arguments, an html file, a json file and a result html file

```
./DemoTemplating.Cli.exe hello.html name1.json outputfile.html
```

