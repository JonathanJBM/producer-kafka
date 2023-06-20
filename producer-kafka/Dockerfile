#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /app
COPY ["producer-kafka.csproj", "producer-kafka/"]
RUN dotnet restore "producer-kafka/producer-kafka.csproj"
COPY . .
WORKDIR "/app/producer-kafka"
RUN dotnet build "producer-kafka.csproj" -c Release -o /app/build
RUN dotnet publish "producer-kafka.csproj" -c Release -o /app/publish /p:UseAppHost=false


FROM mcr.microsoft.com/dotnet/runtime:7.0 AS runtime
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "producer-kafka.dll"]