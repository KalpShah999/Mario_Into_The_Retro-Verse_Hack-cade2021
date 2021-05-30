using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreezeProjectile : MonoBehaviour
{
    public Animator anim;
    public Rigidbody2D rb;

    void Start()
    {
        rb.velocity = new Vector2(-8f, 0f);
        //StartCoroutine(Move());
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        anim.SetTrigger("Explode");
        rb.velocity = new Vector2(0f, 0f);
        Destroy(gameObject, 0.2f);
    }

    IEnumerator Move()
    {
        rb.velocity = new Vector2(0f, 0f);
        yield return new WaitForSeconds(0.5f);
        rb.velocity = new Vector2(-10f, 0f);
    }
}
