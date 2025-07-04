FROM node:22

WORKDIR /app

COPY package.json .

RUN  npm install

RUN npm install -g  nodemon

COPY . .

EXPOSE 3000

CMD ["npm", "start"]


# --------------------------
# docker build -t nodemon-docker-example .
# ---------------------------------
# docker run  -it  -v ${PWD}:/app nodemon-docker-example