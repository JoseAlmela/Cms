# Introducción 
Ejemplo Backend
En la siguiente ejemplo de Web API con .Net Core 2.3 se pide realizar un Backend para un panel de control (de ahora en adelante CMS) para
poder gestionar un club deportivo.
La aplicación consta de dos partes claramente diferenciadas:
* Estructura y funcionalidad para la gestión de la web: control de acceso, usuarios, permisos, etc.
* Funcionalidad necesaria para la correcta gestión de un club polideportivo: socios, pistas, reservas, etc.

En la arquitectura del sistema estará́ claramente delimitada la funcionalidad del controlador, los modelos
que acceden a los datos y las vistas que los presentan.
A nivel de ejemplo, un flujo de acciones básico sería el siguiente. Cuando un usuario visita la web lo
primero que visualiza es un formulario de login junto con un enlace de “Regístrate”. El usuario
introducirá́ sus credenciales para acceder a la aplicación; si no esta registrado deberá́ hacerlo antes de
poder entrar a la aplicación.
Una vez que el usuario ha hecho login, entrará a la parte de la gestión del club polideportivo
propiamente dicha.
Funcionalidad Exigida:
1. Sólo se debe desarrollar el BackEnd necesario para este CMS.
2. CRUD de Usuarios
3. CRUD de Pistas
4. CRUD de Socios
5. CRUD de Reservas

Los usuarios las entidades que crearán el resto de entidades. Estos tienen rol "admin" que es requerido para
para el uso del resto del API. El controlador de Usuarios está abierto a su uso y no tiene ninguna restricción de uso.

# Empezando la instalación

1.	Requerimientos de instalación:
    Versión de DotNet Core 2.1, Ms localDb, Visual Studio 2017 (opcional).
2.	Software dependencies

| Dependencia                         | Versión | Proyecto |  
|-----------------------------------|:-----------:|-----------:|  
|MSTest.TestFramework               | {1.2.1} | CmsEurovalUnitTest |                                                   
|MSTest.TestAdapter                 | {1.2.1} | CmsEurovalUnitTest |                                                  
|Moq                                | {4.10.1}| CmsEurovalUnitTest |                                                  
|Microsoft.NETCore.App              | {2.1}   | CmsEurovalUnitTest |                                                 
|Microsoft.NET.Test.Sdk             | {15.7.0}| CmsEurovalUnitTest |                                                  
|Microsoft.AspNetCore.App           | {2.1.1} | EurovalDataAccess  |                                                  
|Microsoft.NETCore.App              | {2.1}   | EurovalDataAccess  |                                                  
|AutoMapper.Extensions.Microsoft    | {6.0.0} | EurovalBusinessLogic |                                                 
|Microsoft.NETCore.App              | {2.1}   | EurovalBusinessLogic |                                                
|AutoMapper                         | {8.0.0} | CmsEuroval |                                                          
|Microsoft.AspNetCore.App           | {2.1.1} | CmsEuroval |                                                          
|Microsoft.AspNetCore.Razor.Design  | {2.1.2} | CmsEuroval |                                                          
|Microsoft.VisualStudio.Web.CodeGen | {2.1.1} | CmsEuroval |                                                          
|Swashbuckle.AspNetCore             | {4.0.1} | CmsEuroval |                                                          
|AutoMapper.Extensions.Microsoft    | {6.0.0} | CmsEuroval |                                                          
|Microsoft.NETCore.App              | {2.1}   | CmsEuroval |                                                          
|Microsoft.AspNetCore.Authentication| {2.1.1} | CmsEuroval | 

3.	Última versión: V1
4.	Referencias API 
El API se lanza por defecto en la url https://localhost:44300/. También se puede consultar mas información del API
a través de la página de swagger configurada en:https://localhost:44300/swagger/index.html
Allí podrá ver información sobre los métodos y verbos del API que se exponen.

# Consideraciones

##Base de datos
La aplicación creará la base de datos con nombre *CmsEurovalBd* si no existe. También introducirá unos datos de prueba
para las entidades de API si no se encuentran datos.

##Seguridad
El usuario inicial (generado) con permisos para invocar los métodos del API expuestos es _jose@euroval.com_ con la contraseña _P@ssw0rd!_.
Con este usuario ya puede generar un token para autenticarse y poder hacer uso del API aunque el AccountController se ha dejado abierto para demostración.
Es requerido para invocar al resto de métodos añadir un _Header_ _Http_ llamado _Authorization_ con el contenido _Bearer_ *[Token]*. EL Api sigue el esquema de autentificación
JwtBearer.
La seguridad de los metodos se basa en estár autenticado y poseer el role _admin_ para la ejecución de los datos.

# Contruir y probar
 - Para lanzar la aplicación ha de situarse desde una consola en la carpeta de la solución y lanzar el comando: *dotnet run*
 - Se han desarrollado test unitarios de algunos métodos del API donde se hace un Mock de las dependencias de los controladores. Para lanzar los tests ha de situarse en la carpeta del proyecto de test y lanzar el siguiente comando: *CmsEurovalUnitTest> dotnet test*
 - Puede realizar las tareas anteriores abriendo la solución de la aplicación llamada _CmsEuroval.sln_.
