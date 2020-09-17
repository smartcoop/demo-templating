#!/bin/sh

localJsonFolder='//d/Dev/Git/demo-templating/DemoTemplating.JsonServer'
localTemplateFolder='//d/Dev/Git/demo-templating/DemoTemplating.Api/Templates'

# Build json-server image
docker build -t demo-templating-json-server -f DemoTemplating.JsonServer/Dockerfile ./DemoTemplating.JsonServer/
# Build api image
docker build -t demo-templating-api -f DemoTemplating.Api/Dockerfile .
# Build browsersync container
docker build -t demo-templating-browser-sync -f DemoTemplating.BrowserSync/Dockerfile .


# Create docker network if needed
docker network create templating


# Remove existing containers if any
docker rm -f demo-templating-json-server
docker rm -f demo-templating-api
docker rm -f demo-templating-browser-sync

# Run json-server container
docker run -d -p 8080:80 --network=templating --name demo-templating-json-server -v $localJsonFolder:/usr/share/nginx/html demo-templating-json-server

# Run api container
docker run -d -p 80:80 --network=templating --name demo-templating-api -v $localTemplateFolder:/app/Templates demo-templating-api

# Run api container
docker run -d -p 3000:3000 --network=templating --name demo-templating-browser-sync -v $localJsonFolder:/usr/share/nginx/html -v $localTemplateFolder:/app/Templates demo-templating-browser-sync