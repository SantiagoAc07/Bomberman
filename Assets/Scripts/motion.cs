using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class motion : MonoBehaviour
{

    public new Rigidbody2D rigidbody { get; private set; }
    private Vector2 direction = Vector2.down;
    public float speed = 5.0f;

    public KeyCode inputUp = KeyCode.W;
    public KeyCode inputDown = KeyCode.S;
    public KeyCode inputLeft = KeyCode.A;
    public KeyCode inputRight = KeyCode.D;


    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody2D>();
    }


    private void Update()
    {
        if (Input.GetKey(inputUp))
        {
            direction = Vector2.up;
        }
        else if (Input.GetKey(inputDown))
        {
            direction = Vector2.down;
        }
        else if (Input.GetKey(inputLeft))
        {
            direction = Vector2.left;
        }
        else if (Input.GetKey(inputRight))
        {
            direction = Vector2.right;
        }

        else
        {
            SetDirection(Vector2.zero);

        }
    }

    private void FixedUpdate()
    {
        Vector2 position = rigidbody.position;
        Vector2 translation = direction * speed * Time.fixedDeltaTime;

        rigidbody.MovePosition(position + translation);

    }
    private void SetDirection(Vector2 newDirection)
    {
        {
            direction = newDirection;
        }
    }
}

