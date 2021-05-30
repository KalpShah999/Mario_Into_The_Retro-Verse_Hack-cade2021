using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Field : MonoBehaviour
{
    public MarioSpace spaceScript;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.name.Contains("SpaceEnemy"))
        {
            spaceScript.Death();
        }
    }
}
