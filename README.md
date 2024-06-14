# ACME School Management

Este proyecto proporciona un sistema básico de gestión de cursos y estudiantes para la escuela ACME. 

## Características

- Registro de estudiantes especificando su nombre y edad (solo adultos pueden registrarse).
- Registro de cursos con nombre, tarifa de inscripción, fecha de inicio y fecha de finalización.
- Permitir que un estudiante se inscriba en un curso después del pago de la tarifa de inscripción (si aplica).
- Listar cursos que han ocurrido entre un rango de fechas y sus estudiantes.

## Estructura del Proyecto

- **Core Layer**: Contiene las entidades y la lógica de negocio. Contiene los casos de uso y servicios.
- **Api Layer**: Contiene las configuraciones de logs, middlewares y carga de servicios por DI.
- **Infrastructure Layer**: Contiene las implementaciones de repositorios en memoria y el patrón Unit of Work.
- **Tests**: Contiene las pruebas unitarias usando xUnit.net.

## Cómo Ejecutar

1. Clonar el repositorio.
2. Abrir el proyecto en Visual Studio.
3. Ejecutar las pruebas unitarias para verificar la funcionalidad.

## Mejoras Futuras

- Persistencia de datos en una base de datos.
- Integración con una pasarela de pago real.
- Publicación de la aplicación como una API REST.

## Dependencias

- Entity Framework Core (para futuros trabajos con bases de datos).
- xUnit.net (para pruebas unitarias).
- FluentValidation para validaciones de datos de entrada.
- Mapper para mapeo de objetos entre capas.

## Consideraciones

- Actualmente, los datos se almacenan en memoria para simplificar el desarrollo.
- Se han creado abstracciones para facilitar futuras extensiones y cambios.
- Me hubiera gustado ahondar en los unit test y automation test pero no hubo tiempo.
- Demore unas 8 horas y tuve que armar la arquitectura casi de cero.
- Me hubiera gustado tener mas tiempo para entregar algo de mejor calidad pero es lo que pude hacer con el tiempo que tenia.

