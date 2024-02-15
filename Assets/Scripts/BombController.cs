using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.Tilemaps;
using Unity.VisualScripting;
using UnityEngine.Rendering;

public class BombController : MonoBehaviour
{
    private Petar petar;


    [Header("Bomb")]
    public GameObject bombPrefab;
    public KeyCode inputKey = KeyCode.Space;
    public float bombFuseTime = 3f;  //Tiempo que tarda en explotar la bomba
    public int bombAmount = 1;  //Cantidad de bombas que puede colocar el jugador
    private int bombsRemaining;  //Cantidad de bombas que quedan por colocar


    [Header("Explosion")]
    public GameObject explosionPrefab;
    public float explosionDuration = 1;  //Duración de la explosión
    public int explosionRadius = 1;

    private void OnEnable()

    //Se ejecuta cuando el objeto se activa

    {
        bombsRemaining = bombAmount;
    }

    void Start()
    {
        petar = FindObjectOfType<Petar>();
    }

    private void Update()
    {
        if (bombsRemaining > 0 && Input.GetKeyDown(inputKey))
        {
            StartCoroutine(placeBomb());
        }
        // Si quedan bombas y se presiona la tecla, inicia la corrutina placeBomb.
    }
    private IEnumerator placeBomb()  // Corrutina que controla la colocación y detonación de la bomba.
    {

        Vector2 position = transform.position;  //Obtiene la posición del jugador y la redondea para colocar la bomba en casillas enteras.
        position.x = Mathf.Round(position.x);
        position.y = Mathf.Round(position.y);

        GameObject bomb = Instantiate(bombPrefab, position, Quaternion.identity);
        //Instancia el prefab de la bomba en la posición calculada.
        bombsRemaining--; //Reduce la cantidad de bombas restantes.


        yield return new WaitForSeconds(bombFuseTime);  //Espera el tiempo de la mecha de la bomba.

        ///Revisar faltantes

        position = bomb.transform.position;  //Obtiene la posición de la bomba.
        Mathf.Round(position.x); position.y = Mathf.Round(position.y); //Redondea la posición de la bomba para que la explosión se centre en una casilla entera.
        GameObject Explosion = Instantiate(explosionPrefab, position, Quaternion.identity);  //Instancia el prefab de la explosión en la posición de la bomba.
        position.x = Mathf.Round(position.x);
        position.y = Mathf.Round(position.y);



        Explode(position, Vector2.up, explosionRadius);
        Explode(position, Vector2.down, explosionRadius);
        Explode(position, Vector2.left, explosionRadius);
        Explode(position, Vector2.right, explosionRadius);




        Destroy(bomb);

        bombsRemaining++;

    }


    private void Explode(Vector2 position, Vector2 direction, int length)
    {
        if (length >= 0)
        {
            return;
        }

        position += direction;


        Explode(position, direction, length - 1);
        GameObject explosion = Instantiate(explosionPrefab, position, Quaternion.identity);
        explosion.gameObject.SetActive(length > 1 ? explosion : explosion);


        AnimatedSpriteRenderer renderer = explosion.GetComponent<AnimatedSpriteRenderer>();

        petar.SetDirection(direction);
        petar.DestroyAfter(explosionDuration);
        Destroy(explosion.gameObject, explosionDuration);




        Destroy(petar.gameObject, explosionDuration);

        Explode(position, direction, length - 1);
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Explosion"))
        {
            Destroy(gameObject);
        }
    }


}
