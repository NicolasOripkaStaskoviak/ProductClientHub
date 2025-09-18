# Imagem base para build
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

# Copia os arquivos de solução e restaura dependências
COPY ["ProductClientHub.API/ProductClientHub.API.csproj", "ProductClientHub.API/"]
COPY ["ProductClientHub.Communication/ProductClientHub.Communication.csproj", "ProductClientHub.Communication/"]
COPY ["ProductClientHub.Exceptions/ProductClientHub.Exceptions.csproj", "ProductClientHub.Exceptions/"]
RUN dotnet restore "ProductClientHub.API/ProductClientHub.API.csproj"

# Copia o restante do código
COPY . .

WORKDIR "/src/ProductClientHub.API"
RUN dotnet publish "ProductClientHub.API.csproj" -c Release -o /app/publish

# Imagem base para runtime
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS runtime
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "ProductClientHub.API.dll"]
