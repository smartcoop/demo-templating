# Build json-server image
docker build -t demo-templating-json-server -f DemoTemplating.JsonServer/Dockerfile ./DemoTemplating.JsonServer/
# Build api image
docker build -t demo-templating-api -f DemoTemplating.Api/Dockerfile .

# Create docker network if needed
docker network create templating

# Remove existing json-server container if any
docker rm -f demo-templating-json-server
# Run json-server container
docker run -d -p 8080:80 --network=templating --name demo-templating-json-server demo-templating-json-server

# Remove existing api container if any
docker rm -f demo-templating-api
# Run api container
docker run -d -p 80:80 --network=templating --name demo-templating-api demo-templating-api