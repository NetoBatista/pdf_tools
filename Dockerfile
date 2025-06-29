FROM mcr.microsoft.com/dotnet/sdk:8.0
WORKDIR /app

COPY . ./

WORKDIR /app/PdfTools

RUN dotnet restore

RUN dotnet publish -c Release -o out

ENV ASPNETCORE_ENVIRONMENT=Production

EXPOSE 8080

ENTRYPOINT ["dotnet", "./out/PdfTools.dll"]