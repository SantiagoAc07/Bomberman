using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatedSpriteRenderer : MonoBehaviour  //Esta variable puede usarse para controlar objetos dentro del motor de juego Unity.
{
   private SpriteRenderer spriteRenderer;  //se encarga de mostrar las imágenes o sprites en Unity.

    public Sprite idleSprite;  // sprite que se mostrará cuando el personaje esté en estado de inactividad.
    public Sprite[] animationSprites;  //Esta variable almacenará un arreglo de sprites que se utilizarán para crear la animación del personaje.


    public float animationTime = 0.25f; //Esta variable determinará la velocidad de la animación (en segundos).
    private int animationFrame;  //índice del sprite actual que se está mostrando en la animación.


    public bool loop = true;  //Esta variable determina si la animación se repetirá desde el principio una vez que termine o si permanecerá en el último sprite.
    public bool idle = true;  //determina si el personaje se encuentra en estado de inactividad o no.
    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>(); //Metodo que se almacena en la variable spriteRenderer.
    }

    private void OnEnable() //Este método se llama cada vez que el objeto se vuelve visible en la escena del juego. Dentro del método, se activa el componente
                            //SpriteRenderer para que se muestre el sprite.
    {
        spriteRenderer.enabled = true;
    }

    private void OnDisable() //Este método se llama cada vez que el objeto se vuelve
                             //invisible en la escena del juego. Dentro del método, se
                             //desactiva el componente SpriteRenderer para que no se
                             //muestre el sprite.
                             
    {
        spriteRenderer.enabled = false; 
    }

    private void Start()  //Este método se llama una vez al inicio del juego. Dentro del método, se invoca el método NextFrame para que se inicie la animación.
    {
        InvokeRepeating(nameof(NextFrame),animationTime, animationTime);
    }

    private void NextFrame() //Actualiza el sprite que se muestra en el componente SpriteRenderer.
    {
        animationFrame++;  //Esta línea incrementa el valor de la variable animationFrame en 1.
                           //Esto significa que el siguiente sprite en la animación se mostrará.



        if (loop && animationFrame >= animationSprites.Length)  //Esta condición comprueba si la animación se debe repetir
                                                                //y si el índice del sprite actual es mayor o igual al número
                                                                //de sprites en la animación. Si ambas condiciones se cumplen,
                                                                //entonces el valor de animationFrame se reinicia a 0 para comenzar
                                                                //la animación desde el principio.
        {
            animationFrame = 0;
        }

        if (idle)  //Si est[a en inactividad muestra el Idle
        {
            spriteRenderer.sprite = idleSprite;
        }
        else if(animationFrame >= 0 && animationFrame < animationSprites.Length)  //Esta condición se ejecuta si el personaje no está en estado de inactividad
                                                                                  //y el índice del sprite actual está dentro del rango del arreglo animationSprites.
                                                                                  //Si es así, se establece el sprite que se muestra en el spriteRenderer al sprite
                                                                                  //correspondiente al índice animationFrame
        {
            spriteRenderer.sprite = animationSprites[animationFrame];
        }
    }
}

