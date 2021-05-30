using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBehaviour : MonoBehaviour
{
    public GameObject player;
    private bool follow;

    void Start()
    {
        follow = false;
    }

    void Update()
    {
        if (follow)
        {
            transform.position = new Vector3(player.transform.position.x, 2f, -10f);
        }

        if (!follow && player.transform.position.x >= transform.position.x)
        {
            follow = true;
        }
    }
}
