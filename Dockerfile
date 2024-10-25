# Etapa 1: Imagem base do SDK para build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

# Copiar o arquivo de solução e todos os arquivos de projeto
COPY *.sln . 
COPY WebApi/WebApi.csproj WebApi/
COPY Domain/Domain.csproj Domain/
COPY Application/Application.csproj Application/
COPY Infrastructure.Data/Infrastructure.Data.csproj Infrastructure.Data/
COPY Infrastructure.IoC/Infrastructure.IoC.csproj Infrastructure.IoC/
COPY UI.Angular/UI.Angular.esproj UI.Angular/

# Limpar o cache local do NuGet para evitar problemas com pacotes antigos
RUN dotnet nuget locals all --clear

# Restaurar dependências
RUN dotnet restore

# Copiar todo o restante do código para o container
COPY . .

# Build do projeto
RUN dotnet build -c Release --no-restore

# Publicar a aplicação para uma pasta temporária
RUN dotnet publish -c Release -o /app/publish --no-restore

# Etapa 2: Imagem de runtime
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app

# Copiar os arquivos publicados da etapa de build
COPY --from=build /app/publish .

# Definir a porta exposta pela aplicação
EXPOSE 80
ENV ASPNETCORE_URLS=http://+:80

# Comando de entrada para rodar a WebAPI
ENTRYPOINT ["dotnet", "WebApi.dll"]
