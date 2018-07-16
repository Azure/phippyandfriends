# Build
FROM node:carbon AS base

WORKDIR /app

# Dependencies
FROM base AS dependencies  

# A wildcard is used to ensure both package.json AND package-lock.json are copied
COPY package*.json ./

# Install app dependencies including 'devDependencies'
RUN npm install

# Copy Files/Build ----
FROM dependencies AS build  
WORKDIR /app
COPY . /app

# Release with Alpine
FROM node:8.9-alpine AS release  

# Create app directory
WORKDIR /app

COPY --from=dependencies /app/package.json ./

# Install app dependencies
RUN npm install --only=production
COPY --from=build /app ./

CMD ["npm", "start"]