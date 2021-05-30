using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoombaMovement : MonoBehaviour
{
    public Rigidbody2D rb;

    private bool movingRight;
    private bool isDead;

    public MarioScoreCounter score;

    void Start()
    {
        movingRight = true;
        isDead = false;
    }

    void Update()
    {
        if (movingRight && !isDead)
        {
            rb.velocity = new Vector2(3f, 0);
        }
        else if (!isDead)
        {
            rb.velocity = new Vector2(-3f, 0);
        }
        else
        {

            rb.velocity = new Vector2(0f, 0);
        }
    }

    public void Flip()
    {
        movingRight = !movingRight;
    }

    public void Death()
    {
        score.goombaScore++;
        isDead = true;
    }
}
