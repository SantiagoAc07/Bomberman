using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class motion : MonoBehaviour
{

    public new Rigidbody2D rigidbody { get; private set; }  //almacena una referencia al componente Rigidbody2D del objeto,
                                                            //que es necesario para moverlo f?sicamente en el juego. 


    private Vector2 direction = Vector2.down;  //Esta variable almacena la direcci?n en la que se mover? el personaje,
                                               //siendo inicialmente hacia abajo.

    public float speed = 5.0f;  //velocidad de movimiento del personaje.

    public KeyCode inputUp = KeyCode.W;
    public KeyCode inputDown = KeyCode.S;
    public KeyCode inputLeft = KeyCode.A;
    public KeyCode inputRight = KeyCode.D;


    public AnimatedSpriteRenderer spriteRendererUp;  //Esta variable almacena una referencia al script AnimatedSpriteRenderer que controla
                                                     //la animaci?n del personaje cuando se mueve hacia arriba.
    public AnimatedSpriteRenderer spriteRendererDown;
    public AnimatedSpriteRenderer spriteRendererLeft;

    public AnimatedSpriteRenderer spriteRendererRight;

    public AnimatedSpriteRenderer spriteRendererDeath; //muere

    private AnimatedSpriteRenderer activeSpriteRenderer;  //Muestra la animaci[on del personaje

    




    private void Awake()   //Este metodo se llama una vez cuando el objeto se crea por primera vez.
                           //Dentro del metodo, se obtiene una referencia al componente Rigidbody2D del objeto
                           //y se almacena en la variable rigidbody. Adem?s, se establece el activeSpriteRenderer
                           //al sprite de la direcci?n inicial (hacia abajo).
    {
        rigidbody = GetComponent<Rigidbody2D>();
        activeSpriteRenderer = spriteRendererDown;
    }

    private void Update()   //Este m?todo se llama una vez por frame del juego. Dentro del m?todo,
                            //se comprueba si alguna de las teclas de movimiento est? pulsada.
                            //Si es as?, se llama a la funci?n SetDirection con la direcci?n correspondiente
                            //y el AnimatedSpriteRenderer adecuado. Si ninguna tecla est? pulsada,
                            //se llama a SetDirection con una direcci?n nula y el activeSpriteRenderer actual.
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
    //Esta funci?n se encarga de cambiar la direcci?n de movimiento del personaje y activar la animaci?n correspondiente.
    {
        direction = newDirection;  //Esta l?nea establece la nueva direcci?n de movimiento.


        spriteRendererUp.enabled = spriteRenderer == spriteRendererUp; //Esta l?nea activa o desactiva el spriteRendererUp dependiendo de si es el
                                                                        //spriteRenderer que se est? pasando como argumento
        spriteRendererDown.enabled = spriteRenderer == spriteRendererDown;
        spriteRendererLeft.enabled = spriteRenderer == spriteRendererLeft;
        spriteRendererRight.enabled = spriteRenderer == spriteRendererRight;

        activeSpriteRenderer = spriteRenderer;  // Esta l?nea cambia la referencia del activeSpriteRenderer al que se pas? como argumento.
        activeSpriteRenderer.idle = direction == Vector2.zero; //Esta l?nea establece el estado idle del activeSpriteRenderer

    }

        private void FixedUpdate()
    //Este m?todo se llama una vez por cada actualizaci?n de f?sica del juego. Dentro del m?todo, se calcula la nueva posici?n del personaje
    //con base en la direcci?n y la velocidad, y se utiliza

    {
        Vector2 position = rigidbody.position;
        Vector2 translation = direction * speed * Time.fixedDeltaTime;

        rigidbody.MovePosition(position + translation);

    }

    private void OnColliderEnter(Collider2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Explosion"))
        {
            DeathSequence();
        
        }
    }
private void DeathSequence()
    {
        enabled = false;
        GetComponent<BombController>().enabled = false;



        spriteRendererUp.enabled = false;
        spriteRendererDown.enabled = false;
        spriteRendererLeft.enabled = false;
        spriteRendererRight.enabled = false;
        spriteRendererDeath.enabled = true;

        activeSpriteRenderer = spriteRendererDeath;

        Invoke(nameof(OnDeathSequenceEnded), 1.25f);
    }

private void OnDeathSequenceEnded()
    {
        gameObject.SetActive(false);
        FindObjectOfType<GameManager>().CheckWinState();

    }

  


 
}


