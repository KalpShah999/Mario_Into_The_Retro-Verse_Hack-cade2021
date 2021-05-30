using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DotsChecker : MonoBehaviour
{
    public MarioMovement movementScript;
    public MarioSpace movementSpace;
    
    void Update()
    {
        if (transform.childCount <= 0)
        {
            if (movementScript != null)
            {
                movementScript.StartTransition();
            } else if (movementSpace != null)
            {
                movementSpace.StartTransition();
            }
        }
    }
}
