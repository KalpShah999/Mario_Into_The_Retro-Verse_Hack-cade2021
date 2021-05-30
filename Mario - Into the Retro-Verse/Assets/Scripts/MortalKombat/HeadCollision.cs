using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HeadCollision : MonoBehaviour
{
    public SubZero movementScript;
    Vector3 startPosition;

    public void Start()
    {
        startPosition = transform.position;
    }

    public void Update()
    {
        transform.position = startPosition;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (transform.name == "HeadCollider")
        {
            if (collision.collider.tag == "Player")
            {
                movementScript.Finished();
            }
        }
    }
}
