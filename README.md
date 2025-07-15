# Pr�ctica: Integraci�n de GitHub y Azure

## Objetivo
Aprender a utilizar GitHub para el control de versiones y desplegar una aplicaci�n en Azure utilizando GitHub Actions.

## Requisitos
- Cuenta en [GitHub](https://github.com/)
- Cuenta en [Azure](https://portal.azure.com/)
- Visual Studio 2022 instalado

## Pasos

### 1. Crear un repositorio en GitHub
1. Accede a GitHub y crea un nuevo repositorio.
2. Clona el repositorio en tu equipo.

### 2. Crear una aplicaci�n sencilla
1. Abre Visual Studio 2022 y crea un proyecto (por ejemplo, una aplicaci�n web ASP.NET Core).
2. Realiza un commit inicial y s�belo a GitHub.

### 3. Configurar Azure App Service
1. Entra al portal de Azure y crea un recurso de tipo **App Service**.
2. Configura el nombre, sistema operativo y plan de hosting.

### 4. Desplegar con GitHub Actions
1. En tu repositorio de GitHub, ve a la pesta�a **Actions** y selecciona el flujo de trabajo para desplegar en Azure.
2. Modifica el archivo `.github/workflows/azure.yml` si es necesario.
3. Agrega los secretos de Azure (`AZURE_WEBAPP_PUBLISH_PROFILE`) en la configuraci�n del repositorio.

### 5. Verificar el despliegue
1. Realiza un cambio en tu aplicaci�n y haz push a GitHub.
2. Verifica que el flujo de trabajo de GitHub Actions se ejecute y despliegue la aplicaci�n en Azure.
3. Accede a la URL de tu App Service para comprobar que los cambios se reflejan.

## Recursos �tiles
- [Documentaci�n de GitHub Actions](https://docs.github.com/es/actions)
- [Documentaci�n de Azure App Service](https://learn.microsoft.com/es-es/azure/app-service/)
- [Tutorial de despliegue continuo con GitHub y Azure](https://learn.microsoft.com/es-es/azure/app-service/deploy-github-actions)

---

�Con esta pr�ctica aprender�s a integrar el flujo de trabajo de desarrollo con GitHub y el despliegue autom�tico en Azure! :)))