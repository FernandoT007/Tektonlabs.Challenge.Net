# Prueba .Net - C# 

Este proyecto aplica:

Principios SOLID y Clean Clean Architecture

 Patrones de diseño:
  * Mediator pattern
  * CQRS 
  * Repository pattern
  * Encapsulation
 
 Técnicas de desarrollo:
  * Domain-Driven Design
  * Test-Driven Development
 
 Conceptos de arquitectura de software:
  * Cross-Cutting Concerns
  * Middleware
  * Dependency inversion
  * Explicit depencies
  * Single responsibility
  * Don't Repeat Yourself - DRY
  * Persistence ignorance
  * Bounded contexts

## Database
Este proyecto usa un servicio de base de datos Postgres versión 16 gratuito de neon.tech

## Deploy
Para levantar el proyecto locamente se necesita tener en su equipo dotnet 8.0.200 y configurar el valor del FilePath la ruta en donde se va a grabar el tiempo de respuesta de cada request en el archivo appsettings del proyecto Tektonlabs.Challenge.Net.Api.

## Tecnologías
* [NET 8]
* [Entity Framework Core]
* [MediatR]
* [AutoMapper]
* [FluentValidation]
* [XUnit]
* [Dapper]

## Registros de prueba
Aqui algunos ID de prueba con descuentos registrados en el servicio externo de https://mockapi.io/:
 2fde5e81-ea62-47fe-ba69-51bbcd3ca77b
 7b137314-01cd-43f0-b2e0-437858097106
 faad811c-84ae-4b54-b227-c74e098b7ab2
 2a4df724-dd2c-4f1a-8897-0275b6cca922
 21870d02-67a5-4ac9-9f79-7f7cd78a0ac6
 15ffb903-8fb2-4fa1-b904-033c47ba4c0b
 c3ece2b8-5a98-4fcc-b025-74bee80e9522
 c90ef30e-b665-46fa-80af-6ef6829c7593
 4fa2a3b9-81b4-4733-85a8-0b6d6b7a6a2e
 8be02fa6-58ba-4837-8b42-a29782c9989b
 492df3a6-c20d-499b-9cdb-29fd9d43ca27
 2ff2aced-3fb9-4fe1-9bc4-d79fd61edf8b
 0a9dd73f-d118-4686-b35b-194193b1e248
 fcb450f0-c241-49cb-925d-03d4ca1549fe
 a19d0dd5-2591-4283-b9d5-96940fe93d21
 93d67e1d-1cf4-4a33-afb7-0cade843d88c
 bdc942cf-da46-4721-9438-4a41bd8524a6
 8e49a9f5-f75a-4c7e-80b1-b9a1aeafcad1

Para los nuevos registros que se cree con el endpoint Insert(POST) el descuento siempre será 0.  
