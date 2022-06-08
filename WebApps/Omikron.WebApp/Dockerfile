# STEP 1 build static website
FROM node:15.14.0-alpine3.11 as builder
RUN apk update && apk add --no-cache make git
# Create app directory
WORKDIR /app
# Install app dependencies
COPY ClientApp/package.json ClientApp/package-lock.json  /app/
RUN cd /app && npm set progress=false && npm install
# Copy project files into the docker image
COPY ./ClientApp  /app

ARG ENVIRONMENT
RUN cd /app && npm run build:$ENVIRONMENT

# STEP 2 build a small nginx image with static website
FROM nginx:1.19.9-alpine
## Remove default nginx website
RUN rm -rf /usr/share/nginx/html/*
## From 'builder' copy website to default nginx public folder
COPY ./nginx.conf /etc/nginx/conf.d/default.conf
COPY --from=builder /app/dist /usr/share/nginx/html

RUN chown -R 1001:1001 /usr/share/nginx/html
RUN chown -R 1001:1001 /var/cache/nginx
RUN chown -R 1001:1001 /etc/nginx/conf.d
RUN chown -R 1001:1001 /var/log/nginx
RUN touch /var/run/nginx.pid
RUN chown -R 1001:1001 /var/run/nginx.pid
EXPOSE 8080
CMD ["nginx", "-g", "daemon off;"]