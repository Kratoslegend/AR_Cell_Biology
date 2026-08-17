================================================================================
                           AR CELL BIOLOGY
================================================================================

Autor: Javier Alonso Peñaloza León
Proyecto: EVA 1
Motor de desarrollo: Unity 6.5 (6000.5.0f1)
Plataforma de destino: Android
Tecnología de realidad aumentada: Vuforia Engine 11.4.4


1. DESCRIPCIÓN DEL PROYECTO
---------------------------

AR Cell Biology es una aplicación educativa para dispositivos Android que
permite observar y explorar dos tipos de células mediante realidad aumentada:
una célula animal y una célula vegetal.

La aplicación utiliza la cámara del teléfono para reconocer dos marcadores
impresos o mostrados en otra pantalla. Cuando se detecta uno de ellos, aparece
sobre el marcador una representación visual de la célula correspondiente. Al
mismo tiempo, la aplicación presenta información que ayuda a reconocer sus
principales estructuras.

2. OBJETIVO
------------

Desarrollar una aplicación móvil educativa en Unity que utilice realidad
aumentada para reconocer marcadores, mostrar representaciones de células y
entregar información sobre sus estructuras principales de una manera clara,
interactiva y fácil de utilizar.


3. FUNCIONALIDADES PRINCIPALES
------------------------------

- Reconocimiento de un marcador para la célula animal.
- Reconocimiento de un marcador para la célula vegetal.
- Visualización de una representación diferente para cada tipo de célula.
- Presentación automática de información según el marcador detectado.
- Identificación visual de las estructuras más importantes de cada célula.
- Botón ROTAR para cambiar la orientación de la célula.
- Botón ACERCAR para aumentar su tamaño.
- Botón ALEJAR para disminuir su tamaño.
- Botón REINICIAR para recuperar la escala y rotación originales.
- Interfaz adaptada a orientación vertical para teléfonos Android.
- Cambio correcto entre marcadores, evitando que quede visible la célula
  detectada anteriormente.


4. TECNOLOGÍAS Y HERRAMIENTAS UTILIZADAS
-----------------------------------------

- Unity Hub.
- Unity 6.5, versión 6000.5.0f1.
- Vuforia Engine 11.4.4.
- Visual Studio con soporte para desarrollo de videojuegos con Unity.
- Lenguaje C#.
- Android Build Support.
- Android SDK y NDK Tools.
- OpenJDK.
- TextMeshPro para los textos de la interfaz.
- Vuforia Target Manager para crear la base de datos de marcadores.


5. ORGANIZACIÓN DEL PROYECTO
----------------------------

La carpeta Assets fue organizada para mantener separados los distintos tipos
de recursos:

Assets/Scenes
    Contiene la escena principal ARCellBiology_Main.

Assets/Scripts
    Contiene el script ControlCelulasAR.cs, encargado de la interacción y de la
    información presentada en pantalla.

Assets/Images
    Contiene las representaciones de las células, marcadores e imágenes usadas
    por la interfaz y el icono de la aplicación.

Assets/Materials
    Carpeta destinada a los materiales del proyecto.

Assets/Models
    Carpeta destinada a recursos o modelos visuales.

Assets/Prefabs
    Contiene elementos reutilizables del proyecto cuando corresponde.

Assets/UI
    Carpeta destinada a los recursos de la interfaz de usuario.

Assets/StreamingAssets/Vuforia
    Contiene los archivos de la base de datos ARCellBiologyDB descargada desde
    Vuforia Target Manager.


6. ESTRUCTURA DE LA ESCENA PRINCIPAL
------------------------------------

La escena ARCellBiology_Main contiene los siguientes elementos principales:

- Directional Light:
  Proporciona iluminación a los objetos de la escena.

- ARCamera:
  Es la cámara entregada por Vuforia. Utiliza la cámara del teléfono para
  detectar los marcadores y calcular la posición de los objetos aumentados.

- ImageTarget_Animal:
  Está asociado al marcador CelulaAnimal de la base de datos de Vuforia. Como
  objeto hijo contiene Contenido_Celula_Animal.

- ImageTarget_Vegetal:
  Está asociado al marcador CelulaVegetal. Como objeto hijo contiene
  Contenido_Celula_Vegetal.

- UI_ARCellBiology:
  Canvas principal de la interfaz. Contiene el encabezado, el panel de
  información y el panel inferior con los cuatro botones.

- EventSystem:
  Permite que Unity reciba las pulsaciones realizadas sobre los botones.

- Controlador_AR:
  Objeto que contiene el componente ControlCelulasAR y las referencias a ambas
  células y al texto informativo.


7. FUNCIONAMIENTO DE VUFORIA
----------------------------

Para el reconocimiento se creó en Vuforia Target Manager una base de datos de
tipo Device llamada ARCellBiologyDB. Dentro de ella se agregaron dos Image
Targets independientes:

- CelulaAnimal.
- CelulaVegetal.

Ambas imágenes obtuvieron una valoración alta de reconocimiento, lo que ayuda
a que Vuforia identifique suficientes puntos de contraste. La base de datos fue
descargada e importada en Unity, y luego se asignó el target correspondiente a
cada ImageTarget de la escena.

El modo de seguimiento se dejó en Tracked. Esta configuración fue importante,
porque evita que Vuforia mantenga visible una célula cuando el marcador ya no
está siendo observado. También se configuró la detección de hasta dos imágenes
simultáneas.

Los eventos On Target Found y On Target Lost de cada ImageTarget se conectaron
con métodos públicos del script. Cuando un marcador es encontrado, se muestra
la información de esa célula. Cuando se pierde el seguimiento, vuelve a
aparecer la instrucción inicial.


8. INTERFAZ DE USUARIO
----------------------

La aplicación utiliza un Canvas configurado para adaptarse a diferentes
resoluciones de pantalla. La interfaz se divide en tres partes:

- Encabezado:
  Muestra el nombre AR CELL BIOLOGY.

- Panel de información:
  Presenta una instrucción inicial y después muestra las estructuras de la
  célula detectada. Se utilizó un fondo azul oscuro con alta opacidad y texto
  blanco para mejorar la lectura sobre la imagen de la cámara.

- Panel de controles:
  Contiene los botones ROTAR, ACERCAR, ALEJAR y REINICIAR. Los botones utilizan
  un color turquesa para mantener una apariencia simple y coherente.

El texto se implementó con TextMeshPro. Durante el desarrollo se creó un nuevo
objeto de texto y se volvió a asignar al controlador, debido a que el componente
anterior dejó de renderizarse correctamente.


9. EXPLICACIÓN DEL SCRIPT ControlCelulasAR.cs
---------------------------------------------

El script ControlCelulasAR.cs concentra la lógica principal de interacción. Se
encuentra agregado al objeto Controlador_AR.

9.1 Referencias serializadas

El script utiliza variables privadas con el atributo SerializeField. Esto
permite mantener las variables protegidas dentro del código y, al mismo tiempo,
asignarlas desde el Inspector de Unity.

- celulaAnimal:
  Referencia al Transform de Contenido_Celula_Animal.

- celulaVegetal:
  Referencia al Transform de Contenido_Celula_Vegetal.

- textoInformacion:
  Referencia al componente TMP_Text que muestra las instrucciones y la
  información educativa.

También existen variables configurables para los grados de rotación, el paso de
escala y los límites mínimo y máximo. Estos valores pueden modificarse desde el
Inspector sin cambiar directamente el código.

9.2 Método Awake()

Awake se ejecuta al cargar el objeto. En este método se guardan la escala y la
rotación inicial de cada célula. Estos datos se almacenan para que el botón
REINICIAR pueda recuperar posteriormente el estado original.

Antes de utilizar cada referencia se comprueba que no sea null. Esta validación
evita errores si accidentalmente un objeto no ha sido asignado en el Inspector.

9.3 Método Start()

Start se ejecuta al iniciar la escena y llama a MostrarInstruccionInicial(). De
esta manera, antes de detectar un marcador, la aplicación muestra el siguiente
mensaje:

"Apunta la cámara a uno de los marcadores para explorar la célula."

9.4 Método Rotar()

Este método se conecta con el evento On Click del botón ROTAR. Llama al método
auxiliar RotarCelula para cada representación y aplica una rotación local de 30
grados por pulsación. Si una célula no está visible, su transformación puede
actualizarse igualmente sin provocar errores.

9.5 Métodos Acercar() y Alejar()

Ambos métodos llaman a CambiarEscala(). ACERCAR entrega un valor positivo y
ALEJAR entrega el mismo valor en negativo. La escala se mantiene uniforme en
los ejes X, Y y Z.

Mathf.Clamp limita el resultado entre escalaMinima y escalaMaxima. Esto evita
que el usuario reduzca la célula hasta hacerla desaparecer o que la aumente de
forma excesiva.

9.6 Método Reiniciar()

El método Reiniciar recupera la escala y la rotación guardadas durante Awake.
También actualiza el estado visual necesario y vuelve a presentar la
instrucción inicial. Este método se conecta con el botón REINICIAR.

9.7 MostrarInformacionAnimal()

Este método reemplaza el contenido de textoInformacion por una descripción de
la célula animal. La información ayuda a reconocer las siguientes estructuras:

- Núcleo y nucléolo.
- Mitocondrias.
- Retículo endoplasmático.
- Ribosomas.
- Aparato de Golgi.
- Lisosomas y vesículas.
- Centríolos.

Se utilizaron etiquetas de texto enriquecido, como <b> y </b>, para destacar el
nombre de cada estructura dentro de TextMeshPro.

9.8 MostrarInformacionVegetal()

Cumple una función similar, pero presenta las estructuras características de la
célula vegetal:

- Pared celular.
- Membrana celular.
- Vacuola central.
- Núcleo.
- Cloroplastos.
- Mitocondrias.
- Retículo endoplasmático.
- Aparato de Golgi.

9.9 Métodos auxiliares

- RotarCelula():
  Recibe un Transform y aplica la rotación configurada.

- CambiarEscala():
  Calcula la nueva escala y respeta los límites establecidos.

- OcultarSprite():
  Busca el componente SpriteRenderer asociado al contenido y permite ocultarlo
  cuando se necesita reiniciar el estado visual.

- MostrarInstruccionInicial():
  Activa el objeto TextMeshPro, asegura que el texto sea visible, asigna color
  blanco y muestra el mensaje inicial. También fuerza la actualización de la
  malla del texto para evitar problemas de renderizado.

Los mensajes Debug.Log agregados a las funciones fueron útiles para comprobar
en la Console que cada botón ejecutaba el método correspondiente.


10. DIFICULTADES ENCONTRADAS Y CÓMO SE RESOLVIERON
--------------------------------------------------

Creación e importación de los targets

Fue necesario crear la base de datos, agregar cada marcador individualmente,
descargarla e importarla en Unity. En un momento aparecieron ImageTargets
duplicados en la jerarquía. Se revisó cuál correspondía a cada célula, se
eliminaron los elementos innecesarios y se dejaron nombres claros.

Cámara no disponible en el computador

Al ejecutar el proyecto en el editor apareció el mensaje "Couldn't config the
stream!" porque el computador utilizado no tenía una cámara disponible. Esto no
era un error del script. Las funciones se probaron inicialmente mediante los
mensajes de la Console y la experiencia completa se validó instalando el APK en
un teléfono Android con cámara.

Una célula permanecía visible al cambiar de marcador

Durante las primeras pruebas, al pasar desde una célula a la otra podía quedar
visible la representación anterior. La causa estaba relacionada con el modo de
seguimiento extendido. Se cambió la condición del Default Observer Event
Handler desde Tracked or Extended Tracked a Tracked. Así, la representación se
oculta cuando el marcador realmente deja de observarse.

12. PRUEBAS REALIZADAS
----------------------

La aplicación se probó directamente en un dispositivo Android. Se verificó lo
siguiente:

- La aplicación solicita y utiliza la cámara correctamente.
- El marcador animal muestra la célula animal.
- El marcador vegetal muestra la célula vegetal.
- El texto cambia según la célula detectada.
- La información se puede leer sobre el fondo de la cámara.
- Al retirar un marcador, su representación deja de mostrarse.
- Es posible pasar de una célula a la otra sin reiniciar la aplicación.
- El botón ROTAR modifica la orientación.
- ACERCAR aumenta el tamaño dentro del límite permitido.
- ALEJAR reduce el tamaño sin hacerlo desaparecer.
- REINICIAR recupera el estado configurado.
- La interfaz se mantiene ordenada en orientación vertical.
- El APK se instala y ejecuta correctamente.


13. INSTRUCCIONES DE USO
------------------------

1. Instalar AR_Cell_Biology.apk en un teléfono Android.
2. Autorizar el uso de la cámara cuando el sistema lo solicite.
3. Abrir la aplicación en orientación vertical.
4. Apuntar la cámara hacia uno de los marcadores.
5. Esperar a que aparezca la célula sobre la imagen.
6. Leer la información mostrada en el panel superior.
7. Utilizar ROTAR, ACERCAR y ALEJAR para explorarla.
8. Utilizar REINICIAR para volver al estado inicial.
9. Retirar el primer marcador antes de enfocar el segundo para realizar una
   comparación clara.


14. CONTENIDO RECOMENDADO PARA LA ENTREGA
-----------------------------------------

Para que el proyecto pueda abrirse correctamente en otro computador, la carpeta
comprimida debe incluir como mínimo:

- Assets.
- Packages.
- ProjectSettings.
- README_AR_Cell_Biology.txt.
- AR_Cell_Biology.apk.

No es necesario incluir las carpetas Library, Temp, Logs u obj, debido a que
Unity puede volver a generarlas. Excluirlas reduce considerablemente el tamaño
del archivo comprimido.


15. DECLARACIÓN SOBRE EL USO DE CHATGPT
---------------------------------------

Este proyecto fue desarrollado, configurado, integrado y probado por mí. La
organización de la escena, la creación y configuración del proyecto en Unity,
la integración de Vuforia, la preparación de los marcadores, la conexión de los
eventos, la construcción de la interfaz, la implementación del código C# y las
pruebas realizadas en el teléfono fueron ejecutadas por mí durante el proceso de
desarrollo.

El código ControlCelulasAR.cs fue escrito, implementado, revisado y probado por
mí dentro del proyecto. Debido a que era mi primera experiencia desarrollando
una aplicación completa con Unity Hub, utilicé ChatGPT como herramienta de
apoyo para resolver dudas, ordenar, mejorar el código, interpretar mensajes de error, 
revisar problemas de configuración y proponer formas de mejorar la legibilidad 
y organización de la aplicación. Cada cambio fue aplicado y comprobado por mí antes 
de incorporarlo a la versión final.

Las imágenes de las células, los marcadores y el icono de la aplicación fueron
generados con apoyo de ChatGPT para obtener ilustraciones claras, diferenciadas
y adecuadas al objetivo educativo del proyecto.

ChatGPT fue utilizado como una herramienta de orientación y aprendizaje, no
como reemplazo del trabajo práctico. Las decisiones finales, la implementación,
la configuración y la validación del funcionamiento de la aplicación fueron
realizadas por mí.

================================================================================
                              FIN DEL README
================================================================================
