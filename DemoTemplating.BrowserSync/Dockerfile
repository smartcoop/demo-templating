FROM node:current-alpine

RUN npm install -g browser-sync
RUN apk add --no-cache vim nano
CMD browser-sync start --proxy 'demo-templating-api' --files '/app' '/usr/share/nginx/html'