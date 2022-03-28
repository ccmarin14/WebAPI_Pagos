## WebAPI_Pagos

WebAPI para gestionar los datos de una pasarela de pagos de pruebas.

![Demo_App](https://github.com/ccmarin14/WebAPI_Pagos/blob/main/Documentos/test.gif)

### Información de desarrollo

- IDE: VS Code 2019
- Lenguaje: C#
- Framework:
	* .NET Core 5.0
	* Entity (ORM)
- Database: SQL Developer/Server
- Nuget:
	* Microsoft.AspNetCore.Mvc.NewtonsoftJson v3.0.0 (Ayuda a gestionar los JSON de estructura compleja)
	* Microsoft.EntityFrameworkCore.SqlServer v5.0.12 (ORM para manipular la información de la base de datos desde el código como objetos)
	* Microsoft.EntityFrameworkCore.Tools v5.0.12 (Herramientas para el manejo del ORM Entity [Scaffold])
	* Microsoft.VisualStudio.Web.CodeGeneration.Design v5.0.2 (Generador de Vistas y Controladores ASP.Net)
	* Swashbuckle.AspNetCore v5.0.0	(Interfaz para explorar y probar operaciones de una WebAPI)
- Scaffold-DbContext: Referenciado en appsettings.json como `PagosDB`

### Acceso

https://localhost:44315/swagger o https://localhost:44315/ desde Postman.

### Descripción

El WebAPI permite gestionar sin restricciones toda la información de las tablas relacionadas en la base de datos, la cual cuenta con la siguiente estructura:
	
![Image text](https://github.com/ccmarin14/WebAPI_Pagos/blob/main/Documentos/DesingDB.png)

Cada entidad puede ser gestionada con métodos put, post, get y delete, algunas de ellas cuentan con un método get que recibe un `id` y devuelve información detallada, como lo son facturas, pedidos y envíos.

Con el fin de generar una factura y el envío de un pedido, se añadieron registros en la base de datos en las tablas de pedidos, estados, asignaciones, contactos, tipoContactos y productos. Al cambiar el estado de un pedido a `Facturado y enviando`, el API se encarga de generar una factura y el envío del pedido.
