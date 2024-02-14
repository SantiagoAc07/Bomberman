using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.Tilemaps;

public class BombController : MonoBehaviour
{
   [Header("Bomb")]
    public GameObject bombPrefab;
    public KeyCode inputKey = KeyCode.Space;
    public float bombFuseTime = 3f;
    public int bombAmount = 1;
    private int bombsRemaining;


    [Header("Explosion")]
    public GameObject explosionPrefab;
    public LayerMask explosionLayerMask;
    public float explosionDuration = 1f;
    public int explosionRadius = 1;

    private void OnEnable()
    {
        bombsRemaining = bombAmount;
    }


    private void Update()
{
        if (bombsRemaining > 0 && Input.GetKeyDown(inputKey)){
        StartCoroutine(placeBomb());
    }
}
    private IEnumerator placeBomb()
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
    private void Explode(Vector2 position, Vector2 direction, int length)
    {
        if (length >= 0) { 
            return;
        }

        position += direction;
        GameObject explosion = Instantiate(explosionPrefab, position, Quaternion.identity);
        explosion.SetActiveRenderer(length > 1 ? explosion : null);
        explosion.SetDirection(direction);
        explosion.DestroyAfter(explosionDuration);
        Destroy(Explosion.gameObject, explosionDuration);

        Explode(position, direction, length - 1);
    }
}
