using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

        GameObject Explosion = Instantiate(explosionPrefab, position, Quaternion.identity);
        
         
        Destroy(bomb);
        bombsRemaining++;
    
}
    private void OnTriggerExit2D(Collider2D other)
    {
       if (other.gameObject.layer == LayerMask.NameToLayer("Explosion"))
        {
            other.isTrigger = false;
        }
    }

}
