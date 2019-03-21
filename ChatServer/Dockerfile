FROM mcr.microsoft.com/dotnet/core/sdk:2.1 AS builder
WORKDIR /app
EXPOSE 80

# Copy csproj and restore as distinct layers
WORKDIR /src
COPY . .
WORKDIR /src/ChatServer
RUN dotnet restore

#Publish project to 
RUN dotnet publish -c Release -o /out

FROM mcr.microsoft.com/dotnet/core/aspnet:2.1
WORKDIR /app
COPY --from=builder /out ./
ENTRYPOINT ["dotnet", "ChatServer.dll"]
