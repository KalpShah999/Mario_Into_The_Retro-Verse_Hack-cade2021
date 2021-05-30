using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubPunchCheck : MonoBehaviour
{
    private Vector3 startPosition;

    private void Start()
    {
        startPosition = transform.position;
    }

    public void Update()
    {
        if (transform.name.Contains("HeadCollider"))
        {
            transform.position = new Vector3(5f, 5.8f, 0f);
        }
        else
        {
            transform.position = startPosition;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag == "Player" && transform.name.Contains("Punch"))
        {
            transform.parent.transform.GetComponent<SubZero>().Punch();
        }
        else if (collision.transform.tag == "Player" && transform.name.Contains("Kick"))
        {
            transform.parent.transform.GetComponent<SubZero>().Kick();
        }
    }
}
