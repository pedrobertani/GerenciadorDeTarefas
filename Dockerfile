
# Etapa 1: Imagem base do SDK para build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

# Copiar o arquivo de configura��o do NuGet
# Isso garante que o NuGet use a configura��o correta antes do restore
COPY WebApi/nuget.config ./

# Copiar o arquivo de solu��o e todos os arquivos de projeto
# Certifique-se de copiar os arquivos de projeto para realizar o restore corretamente
COPY *.sln . 
COPY WebApi/WebApi.csproj WebApi/
COPY Domain/Domain.csproj Domain/
COPY Application/Application.csproj Application/
COPY Infrastructure.Data/Infrastructure.Data.csproj Infrastructure.Data/
COPY Infrastructure.IoC/Infrastructure.IoC.csproj Infrastructure.IoC/

# Limpar o cache local do NuGet para evitar problemas com pacotes antigos
RUN dotnet nuget locals all --clear

# Restaurar depend�ncias
RUN dotnet restore

# Copiar todo o restante do c�digo para o container
COPY . .

# Build do projeto
RUN dotnet build -c Release --no-restore

# Publicar a aplica��o para uma pasta tempor�ria
RUN dotnet publish -c Release -o /app/publish --no-restore

# Etapa 2: Imagem de runtime
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app

# Copiar os arquivos publicados da etapa de build
COPY --from=build /app/publish .

# Definir a porta exposta pela aplica��o
EXPOSE 80
ENV ASPNETCORE_URLS=http://+:80

# Comando de entrada para rodar a WebAPI
ENTRYPOINT ["dotnet", "WebApi.dll"]