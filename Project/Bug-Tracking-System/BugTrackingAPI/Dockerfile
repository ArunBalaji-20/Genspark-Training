FROM mcr.microsoft.com/dotnet/sdk:8.0@sha256:35792ea4ad1db051981f62b313f1be3b46b1f45cadbaa3c288cd0d3056eefb83 AS build
WORKDIR /App


COPY . ./

RUN mkdir -p /App/screenshots

RUN dotnet restore


RUN dotnet publish -o out

# CMD ["dotnet", "ef", "database", "update"]



# Build runtime image
FROM mcr.microsoft.com/dotnet/aspnet:8.0@sha256:6c4df091e4e531bb93bdbfe7e7f0998e7ced344f54426b7e874116a3dc3233ff
WORKDIR /App
COPY --from=build /App/out .

RUN mkdir -p /App/screenshots



EXPOSE 80
ENTRYPOINT ["dotnet", "BugTrackingAPI.dll"]

# cmds
# docker build -t firstapi . 

# docker run -d -p 5001:8080 -e ASPNETCORE_ENVIRONMENT=Development firstap

