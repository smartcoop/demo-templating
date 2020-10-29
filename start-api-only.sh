#!/bin/sh

# Build api image
docker build -t demo-templating-api -f DemoTemplating.Api/Dockerfile .

# Remove existing containers if any
docker rm -f demo-templating-api

#run the container
docker run -d -p 80:80 --network=templating --name demo-templating-api demo-templating-api