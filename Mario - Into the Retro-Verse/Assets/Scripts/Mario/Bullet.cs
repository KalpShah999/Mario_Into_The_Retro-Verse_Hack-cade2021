using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    float rotationX;

    public AudioSource audioSouce;
    public AudioClip fireballAudio;

    private void Start()
    {
        if (audioSouce != null)
        {
            audioSouce.clip = fireballAudio;
            audioSouce.Play();
        }
    }

    void Update()
    {
        if (transform.name.Contains("Fireball"))
        {
            transform.localRotation = Quaternion.Euler(new Vector3(0f, 0f, rotationX));
            rotationX -= 720 * Time.deltaTime;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(gameObject);
    }
}
