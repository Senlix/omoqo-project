#Build
FROM node:20.9.0-alpine3.18 AS build

WORKDIR /app

COPY package*.json ./
RUN npm install --silent
COPY . .
RUN npm run build

# Serve
FROM nginx:1.9.15-alpine
VOLUME /var/cache/nginx
COPY --from=build /app/dist/client /usr/share/nginx/html
EXPOSE 80