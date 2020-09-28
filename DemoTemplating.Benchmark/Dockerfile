FROM alpine

RUN apk update && apk add apache2-utils wrk

CMD ["wrk", "-t2", "-d10s", "-c100", "--latency", "http://demo-templating-api/templating/hello/name1"]