FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build

# build application 
WORKDIR /server
COPY . .
RUN dotnet restore server/aera/aera-core.csproj
RUN dotnet publish server/aera/aera-core.csproj -c release -o /app --no-self-contained --no-restore

FROM node:16-alpine AS frontend

WORKDIR /app
COPY /AeraWebApp/package*.json /app/
RUN npm ci
COPY ./AeraWebApp/ /app/
RUN npm run build

# final stage/image
FROM mcr.microsoft.com/dotnet/aspnet:5.0-focal

# RUN apk add --no-cache bash
WORKDIR /app
COPY --from=build /app .
COPY --from=frontend /app/dist .
ENTRYPOINT ["./aera-core"]
