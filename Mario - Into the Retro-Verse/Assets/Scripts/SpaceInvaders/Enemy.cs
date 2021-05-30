using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Rigidbody2D rb;
    public Transform enemyCheck;
    public LayerMask enemies;
    public GameObject bullet;

    public AudioSource audioSouce;
    public AudioClip deathAudio;

    int direction;
    bool canShoot;

    void Start()
    {
        direction = 0;
        StartCoroutine(Wait(1f));
        StartCoroutine(ShootWait());
    }

    void Update()
    {
        canShoot = !Physics2D.OverlapCircle(enemyCheck.position, 1f, enemies);

        switch (direction % 4)
        {
            case 0:
                rb.velocity = new Vector2(1f, 0f);
                break;
            case 1:
                rb.velocity = new Vector2(0f, -1f);
                break;
            case 2:
                rb.velocity = new Vector2(-1f, 0f);
                break;
            case 3:
                rb.velocity = new Vector2(0f, 1f);
                break;
        }
    }

    IEnumerator Wait(float time)
    {
        yield return new WaitForSeconds(time);
        direction++;

        switch (direction % 4)
        {
            case 1:
                StartCoroutine(Wait(1.1f));
                break;
            default:
                StartCoroutine(Wait(1f));
                break;
        }
    }

    IEnumerator ShootWait()
    {
        yield return new WaitForSeconds(2);
        StartCoroutine(Shoot());
    }

    IEnumerator Shoot()
    {
        yield return new WaitForSeconds(Random.Range(3f, 10f));
        if (canShoot)
        {
            var temp = Instantiate(bullet);
            temp.transform.position = new Vector3(transform.position.x, transform.position.y - 1f, 0f);
            temp.GetComponent<Rigidbody2D>().velocity = new Vector2(0f, -1f);
            temp.transform.parent = null;
        }
        StartCoroutine(Shoot());
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.transform.name.Contains("Fireball"))
        {
            audioSouce.transform.parent = null;
            audioSouce.clip = deathAudio;
            audioSouce.Play();
            Destroy(audioSouce.transform.gameObject, 1f);
            Destroy(gameObject);
        }
    }
}
