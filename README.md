# Prueba Tecnica A6 - By Daniel Hoyos Correa

## Enfoque
El proyecto tuvo un enfoque diverso. En un principio, para el manejo de persistencia de sistemas y datos, se hizo uso del modelo "Escena Master" o "Master Scene". Este modelo permitio la omision de patrones como el Singleton que podrian generar dependencias innecesarias, incumplir con el "Single Responsability" y aumentar la dificultad a la hora de acceder a sistemas frecuentados. En este caso los sistemas que se involucran en esta escena, son el "GameManager" (Encargado de orquestar la experiencia), el "AudioManager" (Encargado de manejar el sistema de sonido dentro de la experiencia), el "SceneController" (En cargado de ayudar al "GameManager" a fluir entre las etapas de la experiencia) y finalmente, el "UIManager" (Encargado de organizar el flujo visual de las interfaces). Ademas de sistemas "Core" de la experiencia, dentro de la escena tambien se encuentran presentes algunas interfaces que podrian ser repetitivas, tales como: el apartado de settings y la pantalla de carga.

El modelo de "Master Scene" no fue lo unico que se implemento. Dentro del proyecto, tambien podemos encontrar una presencia fuerte del conocido patron "MVC" o "Model-View-Controller". En esta ocasion, y como comunmente es usado, lo empleamos para la UI de la experiencia. Se fabricaron interfaces donde se maneja una conexion dentro de la matriz modelo, entre el controlador y la vista. El controlador nos va ofrecer la funcionalidad logica del aspecto visual, mientras que la vista simplemente aloja las referencias a los objetos visuales implementados.

Adicional al modelo "MVC", el proyecto se caracteriza por una programacion orientada a eventos y con un uso correspondiente del "Single Responsability". Se puede apreciar que la mayoria de procesos esperan "Callbacks" de diferentes funcionalidades. Funcionalidades como: Cargar la imagen, la apertura de la caja, la presion de un boton, entre otras... Esto nos permite no adelantar logica que podria fallar por falta de algun dato, ademas de facilitar la modularidad del flujo.

Finalmente, se empleo una herramienta muy util para el manejo de datos dentro del motor, los "ScriptableObjects". En este caso, se encuentran presentes para alojar la informacion del cubo sorpresa y para almacenar la libreria de audios implementados en la experiencia.

## Consideraciones

- Dentro del "ScriptableObject" del cubo hay una propiedad de velocidad, que si bien es funcional, no fue trabajada para actualizarse en Runtime.
- Se puede ampliar la cantidad de colores dominantes de la imagen en el objeto "ExcelExporter", alojado en la escena "Game" (Esta dentro de un objeto vacio llamado "Utils").
- El explorador de archivos permite mostrar todos los archivos si asi se desea, pero al cargar algun archivo en el formato no admitido, la experiencia simplemente no continua ya que se asume un "Fail" en la carga de textura.
- El volumen de las diferentes secciones del "Mixer" estan siendo guardadas con "PlayerPrefs", asi que al retornar a la experiencia cualquier cambio efectuado seguira presente.
- El boton para abrir mi e-mail, solo funcionara si se tiene activado el servicio de Windows Mail.
- El boton en la parte inferior derecha, dentro de la escena de "Game", posterior a elegir la imagen, permite elegir una nueva imagen.
- No es necesario elegir un folder para el archivo XML. En caso de no elegir folder, el destino es el predeterminado por Application.dataPath.

## Recursos Utilizados

- [AnotherFileBrowser](https://github.com/SrejonKhan/AnotherFileBrowser)
- [NuGet](https://github.com/GlitchEnzo/NuGetForUnity/releases/download/v4.1.1/NuGetForUnity.4.1.1.unitypackage) *Se uso para instalar la extension EPPlus, que permite el formateo de archivos .xlsx
