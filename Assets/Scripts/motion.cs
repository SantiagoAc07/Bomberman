using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class motion : MonoBehaviour
{

    public new Rigidbody2D rigidbody { get; private set; }  //almacena una referencia al componente Rigidbody2D del objeto,
                                                            //que es necesario para moverlo físicamente en el juego. 


    private Vector2 direction = Vector2.down;  //Esta variable almacena la dirección en la que se moverá el personaje,
                                               //siendo inicialmente hacia abajo.

    public float speed = 5.0f;  //velocidad de movimiento del personaje.

    public KeyCode inputUp = KeyCode.W;
    public KeyCode inputDown = KeyCode.S;
    public KeyCode inputLeft = KeyCode.A;
    public KeyCode inputRight = KeyCode.D;


    public AnimatedSpriteRenderer spriteRendererUp;  //Esta variable almacena una referencia al script AnimatedSpriteRenderer que controla
                                                     //la animación del personaje cuando se mueve hacia arriba.
    public AnimatedSpriteRenderer spriteRendererDown;
    public AnimatedSpriteRenderer spriteRendererLeft;

    public AnimatedSpriteRenderer spriteRendererRight;
    private AnimatedSpriteRenderer activeSpriteRenderer;  //Muestra la animaci[on del personaje

    




    private void Awake()   //Este método se llama una vez cuando el objeto se crea por primera vez.
                           //Dentro del método, se obtiene una referencia al componente Rigidbody2D del objeto
                           //y se almacena en la variable rigidbody. Además, se establece el activeSpriteRenderer
                           //al sprite de la dirección inicial (hacia abajo).
    {
        rigidbody = GetComponent<Rigidbody2D>();
        activeSpriteRenderer = spriteRendererDown;
    }

    private void Update()   //Este método se llama una vez por frame del juego. Dentro del método,
                            //se comprueba si alguna de las teclas de movimiento está pulsada.
                            //Si es así, se llama a la función SetDirection con la dirección correspondiente
                            //y el AnimatedSpriteRenderer adecuado. Si ninguna tecla está pulsada,
                            //se llama a SetDirection con una dirección nula y el activeSpriteRenderer actual.
    {
        if (Input.GetKey(inputUp)){
            SetDirection(Vector2.up, spriteRendererUp);
        } else if (Input.GetKey(inputDown)){
            SetDirection(Vector2.down, spriteRendererDown);
        } else if (Input.GetKey(inputLeft)){
            SetDirection(Vector2.left, spriteRendererLeft);
        } else if (Input.GetKey(inputRight)){
            SetDirection(Vector2.right, spriteRendererRight);            
        } else {
            SetDirection(Vector2.zero, activeSpriteRenderer);
        }
    }

    private void SetDirection(Vector2 newDirection, AnimatedSpriteRenderer spriteRenderer)
    //Esta función se encarga de cambiar la dirección de movimiento del personaje y activar la animación correspondiente.
    {
        direction = newDirection;  //Esta línea establece la nueva dirección de movimiento.


            spriteRendererUp.enabled = spriteRenderer == spriteRendererUp; //Esta línea activa o desactiva el spriteRendererUp dependiendo de si es el
                                                                           //spriteRenderer que se está pasando como argumento
            spriteRendererDown.enabled = spriteRenderer == spriteRendererDown;
            spriteRendererLeft.enabled = spriteRenderer == spriteRendererLeft;
            spriteRendererRight.enabled = spriteRenderer == spriteRendererRight;

            activeSpriteRenderer = spriteRenderer;  // Esta línea cambia la referencia del activeSpriteRenderer al que se pasó como argumento.
            activeSpriteRenderer.idle = direction == Vector2.zero; //Esta línea establece el estado idle del activeSpriteRenderer

    }

        private void FixedUpdate()
    //Este método se llama una vez por cada actualización de física del juego. Dentro del método, se calcula la nueva posición del personaje
    //con base en la dirección y la velocidad, y se utiliza

    {
        Vector2 position = rigidbody.position;
        Vector2 translation = direction * speed * Time.fixedDeltaTime;

        rigidbody.MovePosition(position + translation);

    }
    }


