# Sistema de Ventas - Angular y .NET

![Logo del Sistema](url_de_la_imagen_del_logo.png)

Este proyecto es un sistema de ventas desarrollado utilizando Angular para el frontend y .NET para el backend. Está diseñado con una arquitectura de múltiples capas para garantizar la escalabilidad, mantenibilidad y modularidad del sistema.

## Características

- **Frontend (Angular):**
  - Interfaz de usuario intuitiva y amigable.
  - Utilización de componentes reutilizables para una estructura modular.
  - Integración con APIs RESTful para el intercambio de datos.

- **Backend (.NET):**
  - Arquitectura en capas para separar la lógica de negocio, acceso a datos y presentación.
  - Uso de Entity Framework para la gestión de la base de datos.
  - Implementación de patrones de diseño para la extensibilidad y mantenibilidad.

## Estructura del Proyecto

El proyecto está dividido en las siguientes capas:

1. **Capa de Presentación (Frontend):** Contiene la interfaz de usuario desarrollada con Angular.
2. **Capa de Aplicación (Backend):** Maneja la lógica de negocio y la comunicación con la capa de presentación.
3. **Capa de Acceso a Datos (Backend):** Se encarga de interactuar con la base de datos utilizando Entity Framework.
4. **Capa de Base de Datos:** Almacena los datos del sistema.

## Instalación y Uso

### Requisitos Previos
- Node.js y npm (para el frontend con Angular)
- Visual Studio o Visual Studio Code (para el backend con .NET)
- SQL Server o motor de base de datos compatible

### Pasos de Instalación

1. **Clonar el Repositorio:**
   ```bash
   
cd sistema-ventas/frontend
npm install

ng serve

