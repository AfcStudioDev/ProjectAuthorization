FROM node:18.19-alpine AS builder
WORKDIR /usr/src/app

COPY package.json package-lock.json ./
RUN npm install
COPY . .

RUN npm run build --prod

FROM nginx:1.15.8-alpine

RUN rm /etc/nginx/conf.d/default.conf
#COPY nginx/debug/nginx.conf /etc/nginx/conf.d

COPY --from=builder /usr/src/app/dist/hello-world/browser /usr/share/nginx/html
