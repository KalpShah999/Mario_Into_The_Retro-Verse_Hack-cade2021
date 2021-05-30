using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ghost : MonoBehaviour
{
    public int startDirection = 0;
    public Animator anim;

    int directionX;
    int directionY;

    private Vector3 positionOld;
    private Vector3 positionNew;

    void Start()
    {
        directionX = 0;
        directionY = 0;
        PickDirection(startDirection);
    }

    void Update()
    {
        transform.Translate(new Vector3(directionX, directionY, 0f) * Time.deltaTime);
    }

    public void PickDirection(int temp)
    {
        switch (temp)
        {
            case 3:
                directionX = 3;
                directionY = 0;
                break;
            case 2:
                directionX = -3;
                directionY = 0;
                break;
            case 1:
                directionX = 0;
                directionY = 3;
                break;
            default:
                directionX = 0;
                directionY = -3;
                break;
        }

        anim.SetInteger("Direction", temp);

        StartCoroutine(CheckPosition(2f));
    }

    IEnumerator CheckPosition(float time)
    {
        positionOld = transform.position;
        yield return new WaitForSeconds(time);
        positionNew = transform.position;
        if (Mathf.Abs(positionNew.x - positionOld.x) < 0.5f && Mathf.Abs(positionNew.y - positionOld.y) < 0.5f)
        {
            PickDirection(Random.Range(0, 4));
        }
        else
        {
            StartCoroutine(CheckPosition(2f));
        }
    }
}
