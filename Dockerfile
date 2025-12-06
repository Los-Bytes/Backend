FROM mcr.microsoft.com/dotnet/sdk:9.0 AS builder
WORKDIR /app
COPY Backend.API/*.csproj Backend.API/
RUN dotnet restore ./Backend.API
COPY . .
RUN dotnet publish ./Backend.API -c Release -o out

FROM mcr.microsoft.com/dotnet/aspnet:9.0
WORKDIR /app
COPY --from=builder /app/out .
EXPOSE 80
ENTRYPOINT ["dotnet", "Backend.API.dll"]
