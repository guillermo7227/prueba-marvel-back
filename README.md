# Back End Prueba Marvel

Este proyecto requiere datos secretos para correr localmente. Es necesario crear variables de entorno con la utilidad de `dotnet user-secrets` para todas entradas que están vacías en el `appsettings.json`.

A continuación están los comandos para crear estas variables desde la carpeta raíz de la solución.

```
dotnet user-secrets init --project Marvel.Api                                                                                    
dotnet user-secrets set "Jwt:Key" "super-secret-key-super-secret-key" --project Marvel.API                    
dotnet user-secrets set "MarvelAPI:PrivateKey" "Mi-Key-Privada-Marvel" --project Marvel.API          
dotnet user-secrets set "MarvelAPI:PublicKey" "Mi-Key-Publica-Marvel" --project Marvel.API
dotnet user-secrets set "DbInfo:DbHost" "Server=tcp:apps-db-server-gjaa.database.windows.net,1433;" --project Marvel.API
dotnet user-secrets set "DbInfo:DbDatabase" "Initial Catalog=marveldb;" --project Marvel.API
dotnet user-secrets set "DbInfo:DbUsername" "User ID={{usuario-enviado-en-email}};" --project Marvel.API
dotnet user-secrets set "DbInfo:DbPassword" "Password={{contrase-enviada-en-email}};" --project marvel.api    
```

Para correr el proyecto:

`dotnet run --project Marvel.Api` 


Guillermo Agudelo - 2025

