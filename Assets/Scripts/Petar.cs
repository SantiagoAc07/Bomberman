using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Petar : MonoBehaviour
{
    public AnimatedSpriteRenderer Explosion;


    public void SetActiveRenderer(AnimatedSpriteRenderer renderer)
    {
        Explosion.enabled = renderer == Explosion;
    }

    public void SetDirection(Vector2 direction)
    {
        float angle = Mathf.Atan2(direction.y, direction.x);
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

    }

    public void DestroyAfter (float seconds)
    {
        Destroy(gameObject, seconds);
        
    }
    
        
  

}
