# Data server
Nginx server that serves static json files.

At this time it is a very basic server with no logic, no routes.

## Docker commands:

```
docker build -t html-server-image:v1 .
docker run -d -p 80:80 json-server-image:v1
```

