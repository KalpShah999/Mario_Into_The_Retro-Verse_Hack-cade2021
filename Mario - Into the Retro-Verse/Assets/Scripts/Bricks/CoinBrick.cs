using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinBrick : MonoBehaviour
{
    public Sprite brickBroken;
    public GameObject brick;
    public GameObject coin;

    public MarioScoreCounter score;

    public AudioSource audioSouce;
    public AudioClip brickBreakAudio;
    public AudioClip bumpAudio;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Player" && brick.transform.name.Contains("Power"))
        {
            collision.collider.transform.GetComponent<Rigidbody2D>().AddForce(new Vector2(0f, -100f));
            brick.GetComponent<Animator>().enabled = false;
            brick.GetComponent<SpriteRenderer>().sprite = brickBroken;
            score.goombaScore += 1;
            audioSouce.transform.parent = null;
            audioSouce.clip = bumpAudio;
            audioSouce.Play();
            Destroy(audioSouce.transform.gameObject, 1f);
            Destroy(brick.transform.gameObject);

        }
        else if (collision.collider.tag == "Player" && brick.transform.name.Contains("Coin"))
        {
            collision.collider.transform.GetComponent<Rigidbody2D>().AddForce(new Vector2(0f, -100f));
            var temp = Instantiate(coin);
            temp.transform.position = new Vector3(transform.position.x, transform.position.y + 1.15f, 1f);
            temp.GetComponent<Rigidbody2D>().AddForce(new Vector2(0f, 200f));
            Destroy(temp, 0.5f);
            score.IncreaseCoin();
            audioSouce.transform.parent = null;
            audioSouce.clip = brickBreakAudio;
            audioSouce.Play();
            Destroy(audioSouce.transform.gameObject, 1f);
            Destroy(brick.transform.gameObject);
        }
    }
}
