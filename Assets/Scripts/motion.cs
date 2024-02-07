using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class motion : MonoBehaviour
{

    public float speed;
    public int x;
    public int y;
    public bool canMove;
    Rigidbody2D rb;

    private void Awake()
    {
        canMove = true;
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        int moveX = (int)Input.GetAxisRaw("Horizontal");
        int moveY = (int)Input.GetAxisRaw("Vertical");
        if (moveX != 0 && canMove)
        {
            x += moveX;
            canMove = false;
        }

        if (moveY != 0 && canMove)
        {
            y += moveY;
            canMove = false;
        }

        Vector2 currentPosition = new Vector2(
            transform.position.x,
            transform.position.y

            );

        if (currentPosition == CalcularDireccion(x, y))
        {
            canMove = true;
        }

    }

    private void FixedUpdate()
    {
        Vector2 finalPosition = CalcularDireccion(x, y);
        transform.position = Vector2.MoveTowards(transform.position, finalPosition, speed);
    }

    Vector2 CalcularDireccion(int x, int y)
    {
        return new Vector2(x + 0.5f, y + 0.5f);
    }
}
