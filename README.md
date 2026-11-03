# Cardboard — Recolectar con la mirada

Asignatura: Interfaces Inteligentes — Curso 2026/27

Érika Crespo Molero

alu0101639185@ull.edu.es

## Hitos

En esta práctica he creado una escena de realidad virtual para **Google Cardboard** con Unity (Built-In Render Pipeline), pensada para ejecutarse en un iPhone. He aprendido a instalar el **Cardboard XR Plugin** y a configurar el proyecto (XR Plug-in Management, orientación Landscape Left, permiso de cámara para leer el QR del visor y `Active Input Handling: Both` para poder usar un mando). He montado un **avatar** con `CharacterController` y una jerarquía `Avatar → Cabeza → Main Camera`, de modo que el avatar se desplaza y Cardboard solo se encarga de girar la cámara.

He trabajado la interacción **sin controles táctiles**: un `Physics.Raycast` lanzado desde la cámara detecta el objeto que se mira y un temporizador recoge el objeto tras mirarlo un tiempo. He usado un `Transform` como retícula que crece según el progreso, y un `TextMeshPro` 3D como marcador, porque los `Canvas` normales no se ven en modo Cardboard. También he usado el nuevo **Input System** (`Gamepad`, `Keyboard`, `Mouse`) para mover al avatar con el stick de un mando o con WASD, y he añadido el movimiento **mirando hacia abajo** calculando la inclinación de la cámara. Por último, he implementado un objeto **recuperador** con una lista estática de recolectables y un enum de estados (`Libre`, `Guardado`, `Volando`, `Entregado`), de forma que los objetos recogidos no se destruyen sino que se ocultan y vuelan hacia el jugador cuando este mira el recuperador. El terreno y los objetos proceden de paquetes gratuitos de la Asset Store.

## Ejercicios

### Escena y terreno
La escena contiene un lugar montado con baldosas y paredes de los Backrooms de un paquete gratuito de la Asset Store, Las dimensiones son pequeñas para que vaya fluido en el iPhone.

### Movimiento del avatar
El avatar se mueve con el stick izquierdo de un mando (o con WASD en el editor) y también caminando hacia delante cuando se mira hacia abajo, entre un ángulo mínimo y uno máximo configurables. 

### Recolección con la mirada
Los objetos recolectables giran sobre sí mismos. Al apuntarlos con la retícula, esta crece y, pasado un segundo, el objeto desaparece. Además, suena una música diferente para cada objeto que se va recolectando.

### Objeto recuperador
Un objeto de la escena (Kanye West) sirve para recuperar lo recogido: al mirarlo, los recolectables guardados aparecen en él uno tras otro y vuelan hacia el jugador. Suena una música distinta también para esto.

### Prueba en el iPhone con Cardboard
El proyecto se compila para iOS, se abre en Xcode y se ejecuta en el iPhone dentro del visor, escaneando el código QR la primera vez.

![gameplay](./gameplay.gif)
