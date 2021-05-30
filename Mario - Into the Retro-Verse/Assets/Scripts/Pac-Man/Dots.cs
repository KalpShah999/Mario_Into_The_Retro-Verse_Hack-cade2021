using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dots : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip eatenAudio;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag == "Player")
        {
            audioSource.transform.parent = null;
            audioSource.clip = eatenAudio;
            audioSource.Play();
            Destroy(audioSource.transform.gameObject, 1f);
            Destroy(gameObject);
        }
    }
}
