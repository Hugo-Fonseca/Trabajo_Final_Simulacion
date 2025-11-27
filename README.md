##  Autor

**Nombre del autor:** Hugo Alexander Fonseca Chaparro  
**Carrera:** Ingeniería en Multimedia  
**Año:** 2025  
**Curso:** Electiva de Profundización 1 / Simulacion

---

# Simulaciones Interactivas en Unity
Proyecto desarrollado en Unity en el cual se implementan diferentes simulaciones y minijuegos educativos e interactivos orientados a la comprensión de conceptos físicos y mecánicos mediante experiencias prácticas.

# Escenarios Disponibles

El proyecto cuenta con los siguientes escenarios:

## Escenario Principal:
La simulacion comienza en un terno cerrado donde se encuentra un gato, este se puede desplazar libremente por la zona.
Hay unos objetos en el escenario que se ven como casas, el personaje puede interactuar con cada una y entrar a una simualacion, correspondiente a cada casa.
Si se cansa de una simulacion puede presionas Esc. para volver al escenario principal, ver sus puntajes e ir a otra simulación.

### 1. Shooter
Juego de tipo arcade donde el jugador controla un personaje que dispara proyectiles para eliminar enemigos.
- El puntaje aumenta al destruir enemigos.
- Al perder, se muestra pantalla de Game Over.
- Manejo de récord de puntuación (High Score).

---

### 2. Cats Life
Simulación donde se muestra a una IA simple de un gato el cual realiza una rutina diaria.
Ademas cuenta con algunos valores espesificos, como:
- Estado: (Asustado, quieto, jugando, dormir, comer, explorar)
- Energia: 100%
El gato cambia sus estados de forma aleatoria la mayoria pero con ciertas exepciones.
Cuando la energia llega a 0 el gato automaticamente pasa al estado de dormir y se dirige al punto esxacto para recuperar energia.
Cuando la nergia esta menos del 70% se genera un objeto comida el cual.
- Dura 4 segundos, si el gato no llego se destruye y cambia a otro estado.
- El objeto permanece hasta que el gato termine de comer.
Ademas de que para el susto se genera un sonido y un objeto parecido al agua, en una zona aleatoria del escenario
- Contador de sustos.
Este se puede encontrar en el escenario principal cuantas veces a sido asustado el gato actualmente.

---

### 3. Colisiones
Escenario enfocado en esquivar proyectiles.
- El jugador puede moverse hacia arriba y abajo.
- El objetivo es evitar ser impactado.
- En el momento en que un proyectil impacte al jugador aparecera un panerl de Game Over, el cual le dara la puntuacion incluyendo un boton para reintentar.
El puntaje maximo se puede observar en el escenario principal.

---

### 4. Plano Inclinado
Simulación física donde se observa el comportamiento de objetos sobre superficies inclinadas.
- Control de la inclinación mediante Slider.
- El objetivo es evitar que el objeto llegue al suelo.
- Presionando la tecla `Q` se reinicia la simulación.
- Hay un elemento NPC gato el cual siempre persigue a al objeto sobre el plano.

---

### 5. Movimiento Parabólico
Escenario de lanzamiento de proyectil.
- Control de ángulo y fuerza mediante sliders.
- Botón de lanzamiento.
- Visualización de distancia recorrida.
- Velocidad mostrada en tiempo real.
- NPC gato recoge la pelota después del impacto.

---

##  Controles del Juego

###  Menú Principal
- `W A S D` o left Stic -> Moverse por el menú principal  
- `E` o Boton Wast -> Interactuar / Entrar a una simulación  
- `ESC` o botón Select -> Volver al menú principal desde cualquier escenario  

---

###  Pausa (en todos los escenarios)
- `P` o `Start` -> Pausar juego / simulación  
- Presionar nuevamente `P` o `Start`-> Reanudar  
- Botón **Exit** -> Cierra el juego completamente (en versión build)

---

###  Shooter
- Click izquierdo -> Disparar  
- Movimiento con input configurado por el jugador  
- Al morir aparece pantalla de Game Over

---

###  Colisiones
- `W`, `S` o left Stic -> Mover arriba / abajo  
- Objetivo: esquivar proyectiles

---

###  Plano Inclinado
- Slider -> Ajusta inclinación
- `Q` o Button North -> Reiniciar simulación

---

###  Movimiento Parabólico
- Slider fuerza -> Ajusta potencia
- Slider ángulo -> Ajusta dirección
- Botón Lanzar -> Ejecuta disparo
- Distancia y velocidad se muestran en pantalla

---

##  Instrucciones de Ejecución

### Opción 1: Desde Unity
1. Abrir el proyecto en Unity Hub.
2. Abrir la escena principal (MainMenu).
3. Presionar Play.

### Opción 2: Build Ejecutable
1. Ir a carpeta `/Build`.
2. Ejecutar el archivo `.exe`.
3. Jugar directamente sin Unity.

---

##  Contenido del Repositorio

- Carpeta `Assets` -> Código fuente y recursos
- Carpeta `Build` -> Ejecutable del proyecto
- README.md -> Documento de instrucciones
- Scripts en C#
- Escenas Unity

---
