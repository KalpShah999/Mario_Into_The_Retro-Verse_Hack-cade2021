using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnPoint : MonoBehaviour
{
    public int[] directions;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.name.Contains("Ghost"))
        {
            StartCoroutine(Wait(collision.transform));
        }
    }

    IEnumerator Wait(Transform temp)
    {
        yield return new WaitForSeconds(0.1f);
        temp.GetComponent<Ghost>().PickDirection(directions[Random.Range(0, directions.Length)]);
    }
}
