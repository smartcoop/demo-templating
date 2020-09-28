#!/bin/sh

# Remove existing containers if any
docker rm -f demo-templating-benchmark

# Run json-server container
docker run --rm --network=templating --name demo-templating-benchmark demo-templating-benchmark
