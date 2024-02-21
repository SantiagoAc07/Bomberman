using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatedSpriteRenderer : MonoBehaviour  //Esta variable puede usarse para controlar objetos dentro del motor de juego Unity.
{
   private SpriteRenderer spriteRenderer;  //se encarga de mostrar las im?genes o sprites en Unity.

    public Sprite idleSprite;  // sprite que se mostrar? cuando el personaje est? en estado de inactividad.
    public Sprite[] animationSprites;  //Esta variable almacenar? un arreglo de sprites que se utilizar?n para crear la animaci?n del personaje.


    public float animationTime = 0.25f; //Esta variable determinar? la velocidad de la animaci?n (en segundos).
    private int animationFrame;  //?ndice del sprite actual que se est? mostrando en la animaci?n.


    public bool loop = true;  //Esta variable determina si la animaci?n se repetir? desde el principio una vez que termine o si permanecer? en el ?ltimo sprite.
    public bool idle = true;  //determina si el personaje se encuentra en estado de inactividad o no.
    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>(); //Metodo que se almacena en la variable spriteRenderer.
    }

    private void OnEnable() //Este m?todo se llama cada vez que el objeto se vuelve visible en la escena del juego. Dentro del m?todo, se activa el componente
                            //SpriteRenderer para que se muestre el sprite.
    {
        spriteRenderer.enabled = true;
    }

    private void OnDisable() //Este m?todo se llama cada vez que el objeto se vuelve
                             //invisible en la escena del juego. Dentro del m?todo, se
                             //desactiva el componente SpriteRenderer para que no se
                             //muestre el sprite.
                             
    {
        spriteRenderer.enabled = false; 
    }

    private void Start()  //Este m?todo se llama una vez al inicio del juego. Dentro del m?todo, se invoca el m?todo NextFrame para que se inicie la animaci?n.
    {
        InvokeRepeating(nameof(NextFrame),animationTime, animationTime);
    }

    private void NextFrame() //Actualiza el sprite que se muestra en el componente SpriteRenderer.
    {
        animationFrame++;  //Esta l?nea incrementa el valor de la variable animationFrame en 1.
                           //Esto significa que el siguiente sprite en la animaci?n se mostrar?.



        if (loop && animationFrame >= animationSprites.Length)  //Esta condici?n comprueba si la animaci?n se debe repetir
                                                                //y si el ?ndice del sprite actual es mayor o igual al n?mero
                                                                //de sprites en la animaci?n. Si ambas condiciones se cumplen,
                                                                //entonces el valor de animationFrame se reinicia a 0 para comenzar
                                                                //la animaci?n desde el principio.
        {
            animationFrame = 0;
        }

        if (idle)  //Si est[a en inactividad muestra el Idle
        {
            spriteRenderer.sprite = idleSprite;
        }
        else if(animationFrame >= 0 && animationFrame < animationSprites.Length)  //Esta condici?n se ejecuta si el personaje no est? en estado de inactividad
                                                                                  //y el ?ndice del sprite actual est? dentro del rango del arreglo animationSprites.
                                                                                  //Si es as?, se establece el sprite que se muestra en el spriteRenderer al sprite
                                                                                  //correspondiente al ?ndice animationFrame
        {
            spriteRenderer.sprite = animationSprites[animationFrame];
            //Selecci?n del sprite: Si el personaje est? en estado de inactividad, se muestra el sprite idleSprite.
            //Si el personaje no est? en estado de inactividad y el ?ndice del sprite actual est? dentro
            //del rango del arreglo animationSprites, entonces se muestra el sprite correspondiente al ?ndice
            //animationFrame.
        }
    }
}

