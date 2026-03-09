# ManageMedicalAppointments

Sistema de gestión de citas médicas basado en arquitectura de microservicios. Permite administrar pacientes, doctores, citas y recetas médicas a través de una API Gateway centralizada.

---

## Arquitectura

El sistema está compuesto por los siguientes microservicios:

```
     Cliente externo
           │
           │ 1) POST /auth  →  obtiene JWT
           │ 2) Peticiones con JWT
           ▼
┌──────────────────┐       ┌──────────────────────────────────────────────────┐
│  Authentication  │◀─ ─ ─ │                  API Gateway                     │
│   Api  :7216     │ JWT(*)│               (Ocelot  :7018)                    │
│                  │       │        valida JWT en cada ruta protegida         │
│  • Emite JWT     │       └──────┬──────────────┬──────────────┬─────────────┘
│  • Valida JWT(*) │              │              │              │
└──────────────────┘              │              │              │
                                  │              │              │
                       ┌──────────▼───┐  ┌───────▼───────┐  ┌───▼──────────────┐
                       │  Persons.Api │  │Appointments   │  │ Prescriptions    │
                       │    :44392    │  │  Api :44354   │  │   Api  :44395    │
                       │  /doctors    │  │  /schedule    │  │  /create         │
                       │  /patients   │  │  /start       │  │  /getById        │
                       └──────▲───────┘  │  /finish      │  │  /getAll         │
                              │          └──────┬────────┘  │  /deliver        │
                              │                 │           │  /markAsExpired  │
                              │ HTTP (sync)     │ RabbitMQ  └──────▲───────────┘
                              │                 │ (async)          │
                              │                 │ Appointment      │ HTTP (sync)
                              │                 │ FinishedEvent    │
                              └─────────────────┴──────────────────┘

(*) Validación JWT entre microservicios: planificada, no implementada aún.
```

### Rol de Authentication.Api

`Authentication.Api` cumple **dos responsabilidades**:

| Responsabilidad | Estado |
|----------------|--------|
| Emitir tokens JWT a clientes externos | ✅ Implementado |
| Validar tokens JWT en el API Gateway (clientes externos) | ✅ Implementado vía Ocelot `AuthenticationProviderKey: "Bearer"` |
| Validar tokens JWT en llamadas entre microservicios | 🔲 Planificado |

El flujo de autenticación es el siguiente:

1. El **cliente externo** obtiene un JWT llamando a `POST /auth`.
2. El **API Gateway** valida el token en cada ruta protegida antes de enrutar la petición.
3. Las rutas entre microservicios (p. ej. `Prescriptions.Api` → `Persons.Api`) también deberán validar JWT contra `Authentication.Api` *(pendiente de implementar)*.

### Comunicación entre servicios

| Tipo | Origen | Destino | Mecanismo | Ruta |
|------|--------|---------|-----------|------|
| Enrutamiento | `API Gateway` | `Persons.Api` | HTTP REST (Ocelot) | `/doctors/*`, `/patients/*` |
| Enrutamiento | `API Gateway` | `Appointments.Api` | HTTP REST (Ocelot) | `/appointments/*` |
| Enrutamiento | `API Gateway` | `Prescriptions.Api` | HTTP REST (Ocelot) | `/prescriptions/*` |
| Sincrónica interna | `Prescriptions.Api` | `Persons.Api` | HTTP REST | Valida paciente por identificación |
| Asincrónica interna | `Appointments.Api` | `Prescriptions.Api` | RabbitMQ | Evento `AppointmentFinishedEvent` en cola `prescriptions` |

---

## Microservicios

### Authentication.Api
Servicio de autenticación centralizado. Sus responsabilidades son:

- **Emitir tokens JWT** para clientes externos que accedan al sistema.
- **Punto de validación JWT** utilizado por el API Gateway para autorizar cada petición entrante a rutas protegidas.
- **Validación entre microservicios** *(planificado)*: las llamadas internas entre servicios deberán adjuntar y validar JWT contra este servicio.

### Persons.Api
Gestiona doctores y pacientes.

| Método | Ruta | Descripción |
|--------|------|-------------|
| POST | `/doctors/create` | Crear doctor |
| GET | `/doctors/getAll` | Listar todos los doctores |
| GET | `/doctors/getById?id={id}` | Obtener doctor por ID |
| PUT | `/doctors/update` | Actualizar doctor |
| DELETE | `/doctors/delete?id={id}` | Eliminar doctor |
| POST | `/patients/create` | Crear paciente |
| GET | `/patients/getAll` | Listar todos los pacientes |
| GET | `/patients/getByIdentification?identification={id}` | Obtener paciente por identificación |
| PUT | `/patients/update` | Actualizar paciente |
| DELETE | `/patients/delete?id={id}` | Eliminar paciente |

### Appointments.Api
Gestiona el ciclo de vida de las citas médicas.

| Método | Ruta | Descripción |
|--------|------|-------------|
| POST | `/appointments/schedule` | Agendar cita |
| PUT | `/appointments/start` | Iniciar cita |
| PUT | `/appointments/finish` | Finalizar cita (publica evento a RabbitMQ) |

**Estados de una cita:** `Pending` → `InProgress` → `Completed` / `Cancelled`

### Prescriptions.Api
Gestiona las recetas médicas generadas al finalizar una cita.

| Método | Ruta | Descripción |
|--------|------|-------------|
| POST | `/prescriptions/create` | Crear receta |
| GET | `/prescriptions/getById?prescriptionId={id}` | Obtener receta por ID |
| GET | `/prescriptions/getByPatientIdentification?patientIdentification={id}` | Obtener recetas por identificación de paciente |
| GET | `/prescriptions/getAll` | Listar todas las recetas |
| PUT | `/prescriptions/deliver` | Marcar receta como entregada |
| PUT | `/prescriptions/markAsExpired` | Marcar receta como expirada |

**Estados de una receta:** `Active` → `Delivered` / `Expired`

### ApiGateway
Punto de entrada único al sistema, implementado con **Ocelot**. Enruta las solicitudes hacia cada microservicio y valida el token JWT (excepto en `/auth`).

---

## Stack Tecnológico

| Categoría | Tecnología |
|-----------|-----------|
| Framework | .NET Framework 4.8.1 / ASP.NET Web API |
| Lenguaje | C# 7.3 |
| Patrón CQRS | MediatR |
| Validaciones | FluentValidation |
| ORM | Entity Framework Core |
| Mensajería | RabbitMQ |
| API Gateway | Ocelot |
| Autenticación | JWT (OWIN Middleware) |
| Base de datos | SQL Server |
| Testing | NUnit + Moq + FluentAssertions |

---

## Patrones de Diseño

- **Clean Architecture:** Cada servicio se divide en capas `Domain`, `Application`, `Infrastructure` y `Api`.
- **CQRS:** Separación de comandos (escritura) y consultas (lectura) mediante MediatR.
- **Repository Pattern:** Abstracción del acceso a datos a través de interfaces de repositorio.
- **Specification Pattern:** Encapsulamiento de criterios de consulta reutilizables.
- **Domain Events:** Comunicación entre servicios mediante eventos de dominio publicados en RabbitMQ.

---

## Estructura del Proyecto

```
ManageMedicalAppointments/
├── ApiGateway/                         # Ocelot API Gateway
├── Authentication.Api/                 # Servicio de autenticación JWT
├── Appointments.Api/                   # API de citas
├── Appointments.Application/           # Casos de uso de citas (CQRS)
├── Appointments.Domain/                # Entidades y contratos de citas
├── Appointments.Infrastructure/        # Repositorios, EF Core, RabbitMQ
├── Appointments.Test/                  # Tests de citas
├── Persons.Api/                        # API de personas
├── Persons.Application/                # Casos de uso de personas (CQRS)
├── Persons.Domain/                     # Entidades y contratos de personas
├── Persons.Infrasctructure/            # Repositorios y EF Core de personas
├── Persons.Tests/                      # Tests de personas
├── Prescriptions.Api/                  # API de recetas
├── Prescriptions.Application/          # Casos de uso de recetas (CQRS)
├── Prescriptions.Domain/               # Entidades y contratos de recetas
├── Prescriptions.Infrastructure/       # Repositorios, EF Core, HttpService
└── Prescriptions.Tests/                # Tests de recetas
```

---

## Requisitos Previos

- [.NET Framework 4.8.1](https://dotnet.microsoft.com/download/dotnet-framework/net481)
- [SQL Server](https://www.microsoft.com/sql-server)
- [RabbitMQ](https://www.rabbitmq.com/download.html) corriendo en `localhost:5672` (usuario/contraseña: `guest/guest`)
- Visual Studio 2022 (recomendado)

---

## Configuración y Ejecución

### 1. Base de datos

Cada microservicio con persistencia tiene sus propias migraciones de Entity Framework Core. Ejecuta las migraciones para `Appointments`, `Persons` y `Prescriptions`:

```powershell
# Desde el directorio raíz de la solución
Update-Database -Project Appointments.Infrastructure
Update-Database -Project Persons.Infrastructure
Update-Database -Project Prescriptions.Infrastructure
```

### 2. RabbitMQ

Asegúrate de tener RabbitMQ en ejecución con la configuración por defecto:

```
Host:     localhost
Port:     5672
Usuario:  guest
Password: guest
```

### 3. Configuración de variables en Web.config

Cada API que requiere autenticación necesita las claves JWT en su `Web.config`:

**`Appointments.Api`, `Persons.Api`, `Prescriptions.Api`**
```xml
<appSettings>
  <add key="Jwt:Key"      value="tu_clave_secreta" />
  <add key="Jwt:Issuer"   value="tu_issuer" />
  <add key="Jwt:Audience" value="tu_audience" />
</appSettings>
```

**`Prescriptions.Api`** — requiere adicionalmente la URL base de `Persons.Api` para las consultas HTTP internas:
```xml
<appSettings>
  <!-- ... claves JWT ... -->
  <add key="UrlPersons" value="https://localhost:44392" />
</appSettings>
```

### 4. Iniciar los servicios

Inicia los proyectos en el siguiente orden:

1. `Authentication.Api` (`:7216`)
2. `Persons.Api` (`:44392`)
3. `Appointments.Api` (`:44354`)
4. `Prescriptions.Api` (`:44395`)
5. `ApiGateway` (`:7018`)

> En Visual Studio puedes configurar múltiples proyectos de inicio en las propiedades de la solución.

---

## Flujo Principal

```
1. El cliente obtiene un token JWT desde /auth
2. Agenda una cita  →  POST /appointments/schedule
3. Inicia la cita   →  PUT  /appointments/start
4. Finaliza la cita →  PUT  /appointments/finish
   └── Appointments.Api publica AppointmentFinishedEvent en RabbitMQ (cola: "prescriptions")
   └── Prescriptions.Api consume el evento y crea la receta automáticamente
5. Consulta recetas →  GET  /prescriptions/getByPatientIdentification
6. Entrega receta   →  PUT  /prescriptions/deliver
```

---

## Tests

El proyecto cuenta con tests unitarios para los tres microservicios principales, cubriendo los handlers de comandos y consultas.

```powershell
# Ejecutar todos los tests desde la solución
dotnet test
```

| Proyecto de tests | Cobertura |
|-------------------|-----------|
| `Appointments.Test` | ScheduleAppointment, StartAppointment, FinishAppointment |
| `Persons.Tests` | CRUD Doctores, CRUD Pacientes |
| `Prescriptions.Tests` | Create, GetAll, GetById, GetByPatientIdentification, Deliver |
