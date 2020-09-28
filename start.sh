#!/bin/sh

# Build json-server image
docker build -t demo-templating-json-server -f DemoTemplating.JsonServer/Dockerfile ./DemoTemplating.JsonServer/
# Build api image
docker build -t demo-templating-api -f DemoTemplating.Api/Dockerfile .
# Build the benchmark image
docker build -t demo-templating-benchmark -f DemoTemplating.Benchmark/Dockerfile .


# Create docker network if needed
docker network create templating


# Remove existing containers if any
docker rm -f demo-templating-json-server
docker rm -f demo-templating-api


# Run json-server container
docker run -d -p 8080:80 --network=templating --name demo-templating-json-server demo-templating-json-server

# Run api container
docker run -d -p 80:80 --network=templating --name demo-templating-api demo-templating-api