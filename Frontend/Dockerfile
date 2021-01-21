FROM node:14.15.4
WORKDIR /app
COPY ./package*.json /app/
RUN npm install
COPY . /app/
EXPOSE 3000
CMD ["npm","start"]