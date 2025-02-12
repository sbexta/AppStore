# Imagen base para ejecutar la aplicación
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

# Imagen para compilar la aplicación
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

# Copiar el archivo del proyecto y restaurar las dependencias
COPY AppStore.csproj /src/AppStore/
WORKDIR /src/AppStore
RUN dotnet restore "AppStore.csproj"

# Copiar el resto del código fuente
COPY . .

# Establecer el directorio de trabajo en la carpeta donde está el proyecto
WORKDIR "/src/AppStore"

# Compilar la aplicación
RUN dotnet build "AppStore.csproj" -c $BUILD_CONFIGURATION -o /app/build

# Publicar la aplicación
FROM build AS publish
RUN dotnet publish "AppStore.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# Imagen final para ejecutar la aplicación
FROM base AS final
WORKDIR /app

# Copiar los archivos publicados desde la etapa de publicación
COPY --from=publish /app/publish .

# Establecer el punto de entrada para ejecutar la aplicación
ENTRYPOINT ["dotnet", "AppStore.dll"]
