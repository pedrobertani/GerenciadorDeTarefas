# Etapa 1: Build do backend .NET
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build-backend
WORKDIR /app

# Copia os arquivos do backend e restaura as dependências
COPY ./*.sln .
COPY ./Domain/Domain.csproj ./Domain/
COPY ./Infrastructure.Data/Infrastructure.Data.csproj ./Infrastructure.Data/
COPY ./Infrastructure.IoC/Infrastructure.IoC.csproj ./Infrastructure.IoC/
COPY ./Application/Application.csproj ./Application/
COPY ./WebApi/WebApi.csproj ./WebApi/
RUN dotnet restore

# Copia o restante do código do backend e realiza o build
COPY . .
RUN dotnet publish WebApi/WebApi.csproj -c Release -o /app/publish

# Etapa 2: Build do frontend Angular
FROM node:18 AS build-frontend
WORKDIR /app

# Copia o código do Angular e instala as dependências
COPY ./UI.Angular/package*.json ./
RUN npm install

# Realiza o build do Angular
COPY ./UI.Angular/ .
RUN npm run build -- --output-path=dist

# Etapa 3: Imagem final com .NET Runtime e Angular embutido
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app

# Copia o build do backend
COPY --from=build-backend /app/publish ./

# Copia o build do frontend para a pasta wwwroot do backend
COPY --from=build-frontend /app/dist ./wwwroot

# Expõe a porta e define o comando de execução
EXPOSE 80
ENTRYPOINT ["dotnet", "WebApi.dll"]
