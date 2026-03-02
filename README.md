# Luna y las Estrellas Perdidas

<div align="center">

![Unity](https://img.shields.io/badge/Unity-2022.3+-black?style=for-the-badge&logo=unity)
![C#](https://img.shields.io/badge/C%23-10.0-blue?style=for-the-badge&logo=c-sharp)
![Corgi Engine](https://img.shields.io/badge/Corgi_Engine-9.0-green?style=for-the-badge)

**Un juego de plataformas 2D espacial donde Luna, una valiente astronauta, debe recuperar las estrellas guardianas para salvar su estación espacial.**

</div>

## Historia

Luna es una pequeña astronauta que vive en la **Estación Espacial Esperanza**. Una noche, un cometa travieso llamado **Caos** pasa demasiado cerca de la estación. Su poderosa cola magnética arranca las **tres Estrellas Guardianas** que mantienen la estación en órbita estable.

Sin estas estrellas, la estación comenzará a caer hacia la Tierra en cuestión de horas. Luna debe ser valiente y aventurarse por **tres zonas peligrosas del espacio** para recuperar cada estrella antes de que sea demasiado tarde y su hogar se pierda para siempre.

Cada zona presenta sus propios desafíos: asteroides flotantes, nubes de gas que ocultan plataformas, y el gélido interior del cometa mismo. ¿Podrá Luna recuperar las tres estrellas a tiempo?


## Los Tres Niveles

### Nivel 1: Cinturón de Asteroides
Un campo caótico de rocas flotantes donde Luna debe saltar entre asteroides para alcanzar la primera Estrella Guardiana. Algunas plataformas se mueven , otras son estables pero están infestadas de meteoritos vivientes y drones espaciales.

**Easter egg:** El gato de Luna

### Nivel 2: Nebulosa Púrpura
Una zona de escalada y movimiento vertical.

### Nivel 3: Núcleo del Cometa
En proceso.

## Configuración e Instalación

### Requisitos del Sistema

| Componente | Versión Mínima |
|------------|----------------|
| **Unity** | 6000.3.6f1 |
| **Corgi Engine** | 9.0+ |
| **Sistema Operativo** | Windows 10/11, macOS 10.15+ |
| **RAM** | 8 GB |
| **Almacenamiento** | 2 GB libres |

### Pasos de Instalación

#### 1. Clonar el Repositorio
```bash
git clone https://github.com/Valengp2006/Scripting_Proyecto1.git
cd Scripting_Proyecto1
```

#### 2. Instalar Unity Hub

- Descarga Unity Hub desde [unity.com](https://unity.com/download)
- Instala Unity **6000.3.6f1** (recomendado)
- Asegúrate de incluir el módulo **"Windows Build Support"** o **"Mac Build Support"**

#### 3. Importar Corgi Engine

**IMPORTANTE:** Corgi Engine no está incluido en el repositorio por licencia.

1. Compra **Corgi Engine** desde [Unity Asset Store](https://assetstore.unity.com/packages/templates/systems/corgi-engine-2d-2-5d-platformer-26617)
2. Abre Unity Hub
3. Agrega el proyecto: **"Add" → Selecciona la carpeta `Scripting_Proyecto1`**
4. Abre el proyecto en Unity
5. Ve a **Window → Package Manager → My Assets**
6. Busca **Corgi Engine** y haz click en **"Import"**
7. Importa **todos los archivos**

#### 4. Configurar el Proyecto

Una vez abierto el proyecto en Unity:

1. Ve a **Edit → Project Settings → Player**
2. Verifica que **Company Name** esté configurado
3. Ve a **Edit → Project Settings → Quality**
4. Selecciona un preset de calidad apropiado

#### 5. Ejecutar el Juego

1. Abre la escena del menú principal:  
   `Assets/Scenes/MenuPrincipal.unity`
2. Presiona el botón **Play ▶️** en el editor
3. Usa las **flechas del teclado** para moverte y **Espacio** para saltar

### Verificar que Todo Funciona

**Checklist de instalación exitosa:**

- [ ] Unity abre el proyecto sin errores
- [ ] Corgi Engine está importado (carpeta `CorgiEngine` visible en Assets)
- [ ] La escena `MenuPrincipal.unity` se carga correctamente
- [ ] Al dar Play, el personaje se mueve con las flechas
- [ ] No hay errores en la consola de Unity
- [ ] Las monedas rotan y flotan
- [ ] Los portales giran continuamente

## Solución de Problemas Comunes

### "NullReferenceException: Object reference not set..."
**Solución:** Asegúrate de que Corgi Engine está completamente importado.

### "The type or namespace name 'MoreMountains' could not be found"
**Solución:** Importa Corgi Engine desde el Package Manager.

### "Scene 'MenuPrincipal' couldn't be loaded"
**Solución:** Ve a `File → Build Settings` y agrega las escenas manualmente.

### El personaje no se mueve
**Solución:** Verifica que el Input Manager esté configurado correctamente en Project Settings.

### Las monedas no aparecen
**Solución:** Asegúrate de estar en la rama correcta del repositorio:
```bash
git checkout sprites-plataformas
```

## Licencia

Proyecto educativo para el curso de Scripting.  
**Uso no comercial únicamente.**

<div align="center">

**¡Gracias por jugar Luna y las Estrellas Perdidas!**

</div>
