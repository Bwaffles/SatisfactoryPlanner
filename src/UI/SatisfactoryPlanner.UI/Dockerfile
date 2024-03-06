# pull official base image
FROM node:21.6.1-alpine
WORKDIR /app

# add `/app/node_modules/.bin` to $PATH
ENV PATH /app/node_modules/.bin:$PATH

# install app dependencies
COPY package.json ./
COPY package-lock.json ./
RUN npm install
RUN npm install react-scripts@5.0.1 -g

COPY . ./

EXPOSE 3000

CMD ["npm", "start"]