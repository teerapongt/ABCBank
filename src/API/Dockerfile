FROM mcr.microsoft.com/dotnet/sdk:5.0 as build
WORKDIR /src
COPY ["src/API/API.csproj", "src/API/"]
RUN dotnet restore "src/API/API.csproj"
COPY . .
WORKDIR "/src/."
RUN dotnet publish src/API/API.csproj -c Release -o /publish \
    && ls \
    && mkdir /publish/.playwright \
    && cp -r src/API/bin/Release/net5.0/.playwright/unix /publish/.playwright 
    
FROM mcr.microsoft.com/playwright:v1.11.1-focal AS playwright
RUN apt-get update \
    && apt-get install -y wget \
    && wget https://packages.microsoft.com/config/ubuntu/20.04/packages-microsoft-prod.deb -O packages-microsoft-prod.deb \
    && dpkg -i packages-microsoft-prod.deb \
    && apt-get update \
    && apt-get install -y apt-transport-https \
    && apt-get install -y aspnetcore-runtime-5.0 \
    && apt-get remove -y wget
ENV ASPNETCORE_URLS=http://+:5000
ENV ASPNETCORE_ENVIRONMENT=Development
EXPOSE 5000
COPY --from=build /publish/ /app/
RUN chown -R pwuser:pwuser /app
ENTRYPOINT ["dotnet", "/app/API.dll"]