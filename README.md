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

## Using the templating with BrowserSync
By running the templating server with BrowserSync, you can easily modify the templates and json files on the fly. The browser will automatically reload when one file is changed.
In order to run the demo with BrowserSync, you have to :
- Edit the `start-with-browser-sync.sh` file located at the root of the repository. Change the value of the 2 variables `localJsonFolder` and `localTemplateFolder` to make them point at your local folder containing the json files and the templates files respectively. This step is needed because absolute paths are required in order to mount the correct directory in the docker container.
- Start the script `start-with-browser-sync.sh`
- This will create another web server on port 3000 which will watch any file modification and refresh the browser if needed. To see how it works, you can go to `http://localhost:3000/templating/hello/name1` and try to edit the `hello.html` template.

## Benchmarking the templating server
In order to benchmark the templating server, you can use the `demo-templating-benchmark` docker image that is built when running the `start.sh` script. This image contains 2 benchmarking tools :
- [wrk](https://github.com/wg/wrk)
- [ab](https://httpd.apache.org/docs/2.4/en/programs/ab.html)

You can use the benchmark image in 3 different ways :
- Simply by running the `start-benchmark.sh` script that will take care of running the image for you. It will start a benchmark test for 10 seconds on the url `http://demo-templating-api/templating/hello/name1` by using 2 threads and 100 simultaneous connections.
- You can specify the command to run as the image parameter. For example, to run the same simulation as the default one, use `docker run --rm --network=templating --name demo-templating-benchmark demo-templating-benchmark wrk -t2 -d10s -c100 --latency http://demo-templating-api/templating/hello/name1`
- You can also start the image as an interactive container to type your commands inside it directly: `docker run --rm -it --network=templating --name demo-templating-benchmark demo-templating-benchmark /bin/sh`. Then you can use `wrk` and `ab` with any parameter you want.