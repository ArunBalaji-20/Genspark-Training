# simple docker code without using nginx
# FROM node:alpine

# WORKDIR /usr/src/app

# COPY package*.json ./

# RUN npm install -g @angular/cli
# RUN npm install

# COPY . .



# CMD ["ng", "serve", "--host", "0.0.0.0"]

# -------------------------------------------

# using nginx

FROM node:alpine AS build

WORKDIR /app

COPY package.json package-lock.json ./

RUN npm install

COPY . .

RUN npm run build  --configuration=production




FROM nginx:alpine


COPY --from=build /app/dist/Bug-Tracking-Frontend/browser /usr/share/nginx/html

COPY nginx.conf /etc/nginx/conf.d/default.conf

EXPOSE 80

CMD ["nginx", "-g", "daemon off;"]