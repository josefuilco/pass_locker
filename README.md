# PassLocker: Gestor de Contraseñas
Un gestor de contraseñas personal para poder gestionar las contraseñas de cualquier aplicación de manera segura y con la privacidad de tenerlo en local.

# Tabla de Contenido
* [Tecnologias](#tecnologias)
* [Funcionalidades](#funcionalidades)

# Tecnologias
Las tecnologias usadas en este proyecto han sido tomadas por la rigidez y confiabilidad que estas brindan al momento de crear una aplicación de seguridad alta.

* .NET 6: Utilizada en el backend junto a **ASP.NET Core** para la creación de la API.
* Entity Framework: ORM confiable por parte de Microsoft y con la posibilidad de utilizar el LINQ.
* MySQL: Una base datos de uso libre y segura.
* React: Para una mayor agilidad al momento de desarrollar interfaces de usuario.
* React-Router: Para la navegación entre las interfaces de usuario SPA.
* Zustand: Libreria de manejo de estados globales.

# Funcionalidades
Las funcionalidades con las que cuenta la aplicación.

* [X] Cifrado de contraseñas.
* [X] Generación de contraseñas seguras.
* [X] Autenticación segura con contraseña maestra.
* [X] Categorización y busqueda rapida.
* [X] Protección por tiempo - 30 minutos como maximo la cookie.
* [] Restricción de acceso de datos por intentos fallidos por 24 horas.
