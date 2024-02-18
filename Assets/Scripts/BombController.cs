using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Tilemaps;

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
        if (bombsRemaining > 0 && Input.GetKeyDown(inputKey))
        {
            StartCoroutine(PlaceBomb());
        }
    }

    #endregion

    #region Coroutines

    private IEnumerator PlaceBomb()
    {
        Vector2 position = transform.position;
        position.x = Mathf.Round(position.x);
        position.y = Mathf.Round(position.y);

        GameObject bomb = Instantiate(bombPrefab, position, Quaternion.identity);
        bombsRemaining--;

        yield return new WaitForSeconds(bombFuseTime);

        position = bomb.transform.position;
        position.x = Mathf.Round(position.x);
        position.y = Mathf.Round(position.y);

        Explode(position, Vector2.up, explosionRadius);
        Explode(position, Vector2.down, explosionRadius);
        Explode(position, Vector2.left, explosionRadius);
        Explode(position, Vector2.right, explosionRadius);

        Destroy(bomb);
        bombsRemaining++;
    }

    #endregion

    #region Explosion

    private void Explode(Vector2 position, Vector2 direction, int length)
    {
        if (length < 0)
            return;

        position += direction;

        Explode(position, direction, length - 1);

        if (Physics2D.OverlapBox(position, Vector2.one / 2f, 0f, explosionLayerMask))
        {

            return;
        }

        if (length > 1)
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

        // private void ClearDestructible(Vector2 position)
        // {
        //  Vector3Int cell = destructibleTiles.WorldToCell(position);
        //  TileBase tile = destructibleTiles.GetTile(cell);
        //  if (tile != null)
        // {
        //   Destroy(tile);
        // }
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