# See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

# This stage is used when running from VS in fast mode (Default for Debug configuration)


#BASE STAGE
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base #asp.net core uygulamas�n� �al��t�racak ortam� sa�lar 
USER app #kullan�c� ayarlar� 
WORKDIR /app #�al��ma dizini 
EXPOSE 8080   #portlar�m�z
EXPOSE 8081


# This stage is used to build the service project
#BUILD STAGE
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build # uygulamay� derlemek i�in kullan�lan sdk bizimki dotnet sdk 8
ARG BUILD_CONFIGURATION=Release #derleme yap�land�rmas� basic olarak release gelir iste�e g�re build olarak dee�i�tirilebilir
WORKDIR /src # �al��ma dizini 
COPY ["WebApplication1/WebApplication1.csproj", "WebApplication1/"]  #proje dosyalar�n� containerlara kopyalar
COPY ["BusinessLayer/BusinessLayer.csproj", "BusinessLayer/"]
COPY ["CommonLayer/CommonLayer.csproj", "CommonLayer/"]
COPY ["DataAccessLayer/DataAccessLayer.csproj", "DataAccessLayer/"]
RUN dotnet restore "./WebApplication1/WebApplication1.csproj" #proje ba��ml�l�klar�n� y�kler
COPY . .
WORKDIR "/src/WebApplication1" #�al��ma dizini 
RUN dotnet build "./WebApplication1.csproj" -c $BUILD_CONFIGURATION -o /app/build #uygulamay� derler ve dizini belirler

# This stage is used to publish the service project to be copied to the final stage
#Publish Stage 
FROM build AS publish #yukar�daki build a�amas�n� kullan�r
ARG BUILD_CONFIGURATION=Release #derleme yap�land�rmas� basic olarak release gelir iste�e g�re build olarak dee�i�tirilebilir
RUN dotnet publish "./WebApplication1.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false # uygulamay� yay�nlay�p ��kt�y� dizine koyar

# This stage is used in production or when running from VS in regular mode (Default when not using the Debug configuration)
#Final Stage
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "WebApplication1.dll"]