# Introduction 
Prueba previa Backend
[Euroval.com](Euroval.com)
En la siguiente prueba se pide realizar un pequeño panel de control (de ahora en adelante CMS) para
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

# Getting Started
TODO: Guide users through getting your code up and running on their own system. In this section you can talk about:
1.	Installation process
2.	Software dependencies
3.	Latest releases
4.	API references

# Build and Test
TODO: Describe and show how to build your code and run the tests. 

# Contribute
TODO: Explain how other users and developers can contribute to make your code better. 

If you want to learn more about creating good readme files then refer the following [guidelines](https://www.visualstudio.com/en-us/docs/git/create-a-readme). You can also seek inspiration from the below readme files:
- [ASP.NET Core](https://github.com/aspnet/Home)
- [Visual Studio Code](https://github.com/Microsoft/vscode)
- [Chakra Core](https://github.com/Microsoft/ChakraCore)