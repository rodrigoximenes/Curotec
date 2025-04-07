# Curotec.frontend/Dockerfile
FROM node:18-alpine AS build-stage
WORKDIR /app
COPY . .
RUN npm install && npm run build --prod

FROM nginx:alpine
COPY --from=build-stage /app/dist/curotec.frontend/browser /usr/share/nginx/html
EXPOSE 80
