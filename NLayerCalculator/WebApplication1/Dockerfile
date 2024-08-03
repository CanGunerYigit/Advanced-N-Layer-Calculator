# See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

# This stage is used when running from VS in fast mode (Default for Debug configuration)


#BASE STAGE
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base #asp.net core uygulamasýný çalýþtýracak ortamý saðlar 
USER app #kullanýcý ayarlarý 
WORKDIR /app #çalýþma dizini 
EXPOSE 8080   #portlarýmýz
EXPOSE 8081


# This stage is used to build the service project
#BUILD STAGE
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build # uygulamayý derlemek için kullanýlan sdk bizimki dotnet sdk 8
ARG BUILD_CONFIGURATION=Release #derleme yapýlandýrmasý basic olarak release gelir isteðe göre build olarak deeðiþtirilebilir
WORKDIR /src # çalýþma dizini 
COPY ["WebApplication1/WebApplication1.csproj", "WebApplication1/"]  #proje dosyalarýný containerlara kopyalar
COPY ["BusinessLayer/BusinessLayer.csproj", "BusinessLayer/"]
COPY ["CommonLayer/CommonLayer.csproj", "CommonLayer/"]
COPY ["DataAccessLayer/DataAccessLayer.csproj", "DataAccessLayer/"]
RUN dotnet restore "./WebApplication1/WebApplication1.csproj" #proje baðýmlýlýklarýný yükler
COPY . .
WORKDIR "/src/WebApplication1" #çalýþma dizini 
RUN dotnet build "./WebApplication1.csproj" -c $BUILD_CONFIGURATION -o /app/build #uygulamayý derler ve dizini belirler

# This stage is used to publish the service project to be copied to the final stage
#Publish Stage 
FROM build AS publish #yukarýdaki build aþamasýný kullanýr
ARG BUILD_CONFIGURATION=Release #derleme yapýlandýrmasý basic olarak release gelir isteðe göre build olarak deeðiþtirilebilir
RUN dotnet publish "./WebApplication1.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false # uygulamayý yayýnlayýp çýktýyý dizine koyar

# This stage is used in production or when running from VS in regular mode (Default when not using the Debug configuration)
#Final Stage
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "WebApplication1.dll"]