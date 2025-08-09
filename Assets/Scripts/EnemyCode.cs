using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class EnemyCode : MonoBehaviour
{

    Rigidbody2D rb;
    SpriteRenderer sr;
    bool isRight = false;
    [SerializeField] float speed = 7;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponentInChildren<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Mathf.Abs(rb.velocity.x) <= 0.01f)
        {
            isRight = !isRight;
            sr.flipX = !sr.flipX;

        }

        if (isRight)
        {
            rb.velocity = new Vector2(speed, rb.velocity.y);
        }
        else
        {
            rb.velocity = new Vector2(-1 * speed, rb.velocity.y);

        }
    }
}
