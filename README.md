# Back-EndTest
Hirings Double V Partners Test

#Requisitos

-Docker for Windows
-Sdk netcore 5 (No es necesario con docker-compose)

# Ejecutar desde docker-compose

Ejecutar la solución/proyecto en un editor de codigo ya sea visual studio code o visual studio 2019
En el terminal PowerShell/Cmd dirigirse al directorio del proyecto y ejecutar el comando "docker-compose up"

# Manualmente con docker

Ejecutar la solución/proyecto en un editor de codigo ya sea visual studio code o visual studio 2019

Ejecutar el siguiente comando en el terminal
docker run -e 'ACCEPT_EULA=Y' -e 'SA_PASSWORD=Pa55w0rd2019' -e 'MSSQL_PID=Express' -p 1433:1433 -d mcr.microsoft.com/mssql/server:2017-latest-ubuntu

Ejecutar la solución del cliente directamente con
dotnet run

# Sin docker

La solución también se puede ejecutar sin docker definiendo los parametros del servidor local de sqlserver en y esta automáticamente
genera sus respectivas migraciones y semillas.

Configuración genérica del appsettings.json directamente desde el servidor local:
  "ConnectionStrings": {
    "DefaultConnection": "Server=(localdb)\\mssqllocaldb;Database=testDB;Trusted_Connection=True;MultipleActiveResultSets=true"
  }


El api cuenta con su respectiva CRUD mediante metodos http
Y se pueden exponer a traves de Swagger
