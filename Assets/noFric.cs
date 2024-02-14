using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class noFric : MonoBehaviour
{
    private Rigidbody2D boom;
    bool boomB = true;
    private float positions = 1;
    // Start is called before the first frame update
    void Start()
    {
        boom = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            StartCoroutine(position());
        }
    }
    

   
    public IEnumerator position() 
    {
        if(boomB)
        {
            boomB = false;
            boom.velocity = new Vector2(boom.position.x * positions, 0);
            yield return new WaitForSeconds(5f);
            boomB = true;
        }
    
    }
}
