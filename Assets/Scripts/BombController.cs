using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Tilemaps;
using static TMPro.SpriteAssetUtilities.TexturePacker_JsonArray;

[DebuggerDisplay("{" + nameof(GetDebuggerDisplay) + "(),nq}")]
public class BombController : MonoBehaviour
{
    #region Variables

    [Header("Bomb")]
    public GameObject bombPrefab;
    public KeyCode inputKey = KeyCode.Space;
    public int bombFuseTime = 3;
    public int bombAmount = 1;
    private int bombsRemaining;

    [Header("Explosion")]
    public GameObject explosionPrefab;
    public float explosionDuration = 1f;
    public int explosionRadius = 1;
    public LayerMask explosionLayerMask;

    [Header("Destructible")]
    public Tilemap destructibleTiles;
    public GameObject destructiblePrefab;




    #endregion

    #region Unity Methods

    private void OnEnable()
    {
        bombsRemaining = bombAmount;
    }

    private void Update()
    {
        if (bombsRemaining > 0 && Input.GetKeyDown(inputKey))   //Comprueba si hay bombas disponibles y si se presiona la tecla de colocar bomba
        {
            StartCoroutine(PlaceBomb());
        }
    }

    #endregion

    #region Coroutines

    private IEnumerator PlaceBomb()  //Nombre corrutina
    {
        Vector2 position = transform.position; //Obtiene la posición actual del objeto
        position.x = (position.x);  //Redondea las coordenadas de la posición a valores enteros
        position.y = (position.y);

        GameObject bomb = Instantiate(bombPrefab, position, Quaternion.identity);  //Crea una instancia del prefab de la bomba en la posición calculada
        bombsRemaining--;                                                          //Disminuye la cantidad de bombas disponibles

        yield return new WaitForSeconds(bombFuseTime);               //Suspende la ejecución de la corrutina durante bombFuseTime segundos.
                                                                     //Simula el tiempo de mecha de la bomba antes de la explosión.

        position = bomb.transform.position;                         //Obtiene la posición de la bomba después de la espera.
        position.x = (position.x);
        position.y = (position.y);

        Explode(position, Vector2.up, explosionRadius);            //Repite la lógica de explosión en las 4 direcciones cardinales (arriba, abajo, izquierda y derecha).
        Explode(position, Vector2.down, explosionRadius);
        Explode(position, Vector2.left, explosionRadius);
        Explode(position, Vector2.right, explosionRadius);

        Destroy(bomb);                                            
        bombsRemaining++;
    }

    #endregion

    #region Explosion

    private void Explode(Vector2 position, Vector2 direction, int length)  //Posición actual de la explosión, Dirección en la que se propaga la explosión., Longitud restante de la explosión.
    {
        if (length < 0)
            return;                                  //Si la longitud es menor a 0, la explosión se detiene en esa dirección.

        position += direction;

        Explode(position, direction, length - 1);

        if (Physics2D.OverlapBox(position, Vector2.one / 2f, 0f, explosionLayerMask))
        {

            return;
        }

        if (length >= 1)
        {
            Instantiate(explosionPrefab, position, Quaternion.identity);
        }

        // Handle explosion effects
        Collider2D[] detecteds = Physics2D.OverlapCircleAll(position, explosionRadius, explosionLayerMask);
        foreach (var item in detecteds)
        {
            Destroy(item.gameObject);
        }

        if (detecteds.Length >= 1)
        //si detecta una caja con el overlaopCircle entonces destruye la caja que detecta
        {

             Vector3Int cell = destructibleTiles.WorldToCell(position);
            TileBase tile = destructibleTiles.GetTile(cell);
            if (tile != null)
            {
              Instantiate(destructiblePrefab, position, Quaternion.identity);
              destructibleTiles.SetTile(cell, tile);
            }
        }

     
    }

    public void AddBomb()
    {
        bombAmount++;
        bombsRemaining++;
    }

    private string GetDebuggerDisplay()
    {
        return ToString();
    }
}
#endregion