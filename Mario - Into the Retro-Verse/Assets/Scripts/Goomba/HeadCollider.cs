using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadCollider : MonoBehaviour
{
    public Animator anim;
    public GoombaMovement movementScript;

    public AudioSource audioSource;
    public AudioClip stompAudio;

    private void Start()
    {
        anim.SetBool("isDead", false);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (transform.name == "HeadCollider")
        {
            if (collision.collider.tag == "Player")
            {
                anim.SetBool("isDead", true);
                movementScript.Death();
                audioSource.transform.parent = null;
                audioSource.clip = stompAudio;
                audioSource.Play();
                Destroy(audioSource.transform.gameObject, 1f);
                Destroy(gameObject.transform.parent.gameObject, 0.5f);
            }
        }
    }
}
