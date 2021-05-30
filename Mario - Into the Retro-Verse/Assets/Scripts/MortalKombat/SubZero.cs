using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubZero : MonoBehaviour
{
    public GameObject player;
    public int health;
    private MarioSpace playerScript; //When lose, tell Mario he won
    public Animator anim;
    public GameObject punchBox;
    public GameObject kickBox;
    public GameObject freeze;
    public GameObject headCollider;
    public GameObject fightText;
    public GameObject finishHimText;

    private bool canMakeAction;
    public bool canMove;
    public bool isDead;
    public bool isWin;

    public AudioSource audioSource;
    public AudioSource audioSource2;
    public AudioSource audioSource3;

    public AudioClip finishHimAudio;
    public AudioClip fatalityAudio;
    public AudioClip fightAudio;

    public AudioClip deathAudio;
    public AudioClip dizzyAudio;
    public AudioClip gettingHitAudio;
    public AudioClip hitAudio;
    public AudioClip kickAudio;
    public AudioClip punchAudio;
    public AudioClip special1Audio;
    public AudioClip special2Audio;

    void Start()
    {
        health = 150;
        canMove = true;
        isWin = false;
        anim.SetBool("isWin", false);
        fightText.SetActive(false);
        finishHimText.SetActive(false);
        playerScript = player.GetComponent<MarioSpace>();
        StartCoroutine(WaitAtStart());
    }

    void Update()
    {
        anim.SetBool("isDead", isDead);

        if (canMakeAction && !isDead)
        {
            int temp = Random.Range(0, 2);

            if (temp == 1)
            {
                Special1();
            }
            else
            {
                Special2();
            }
        }

        if (health <= 0 && !isDead)
        {
            audioSource.clip = dizzyAudio;
            audioSource.Play();
            audioSource2.clip = finishHimAudio;
            audioSource2.Play();
            finishHimText.SetActive(true);
            isDead = true;
            canMove = false;
            headCollider.SetActive(true);
            transform.tag = "Untagged";
        }

        if (isWin)
        {
            StartCoroutine(Win());
        }
    }

    IEnumerator Win()
    {
        canMove = false;
        yield return new WaitForSeconds(0.25f);
        anim.SetBool("isWin", true);
    }

    public void Punch()
    {
        if (canMove)
        {
            StartCoroutine(PunchRoutine());
        }
    }

    public void Kick()
    {
        if (canMove)
        {
            StartCoroutine(KickRoutine());
        }
    }

    public void Special1()
    {
        if (canMove)
        {
            StartCoroutine(Special1Routine());
        }
    }

    public void Special2()
    {
        if (canMove)
        {
            StartCoroutine(Special2Routine());
        }
    }

    public void Finished()
    {
        audioSource.clip = deathAudio;
        audioSource.Play();
        audioSource2.clip = fatalityAudio;
        audioSource2.Play();
        canMove = false;
        isDead = true;
        anim.SetBool("Finished", true);
        transform.tag = "Untagged";
    }

    IEnumerator PunchRoutine()
    {
        canMakeAction = false;
        anim.SetTrigger("Punch");
        audioSource.clip = punchAudio;
        audioSource.Play();
        yield return new WaitForSeconds(0.1f);
        punchBox.SetActive(true);
        punchBox.SetActive(false);
        StartCoroutine(ActionCounter());
    }

    IEnumerator KickRoutine()
    {
        canMakeAction = false;
        anim.SetTrigger("LowKick");
        audioSource.clip = kickAudio;
        audioSource.Play();
        yield return new WaitForSeconds(0.2f);
        kickBox.SetActive(true);
        yield return new WaitForSeconds(0.3f);
        kickBox.SetActive(false);
        StartCoroutine(ActionCounter());
    }

    IEnumerator Special1Routine()
    {
        canMakeAction = false;
        anim.SetTrigger("Speical");
        yield return new WaitForSeconds(0.1f);
        audioSource.clip = special1Audio;
        audioSource.Play();
        var temp = Instantiate(freeze);
        temp.transform.position = new Vector3(transform.position.x - 2.5f, transform.position.y + 1f, 0f);
        temp.transform.parent = null;
        kickBox.SetActive(true);
        yield return new WaitForSeconds(0.4f);
        kickBox.SetActive(false);
        StartCoroutine(ActionCounter());
    }

    IEnumerator Special2Routine()
    {
        canMakeAction = false;
        anim.SetTrigger("Special2");
        kickBox.SetActive(true);
        audioSource.clip = special2Audio;
        audioSource.Play();
        yield return new WaitForSeconds(2f);
        kickBox.SetActive(false);
        StartCoroutine(ActionCounter());
    }

    IEnumerator WaitAtStart()
    {
        yield return new WaitForSeconds(3f);
        fightText.SetActive(true);
        audioSource2.clip = fightAudio;
        audioSource2.Play();
        StartCoroutine(ActionCounter());
    }

    IEnumerator ActionCounter()
    {
        canMakeAction = false;
        yield return new WaitForSeconds(3f);
        canMakeAction = true;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.transform.name.Contains("Fireball"))
        {
            health -= 10;
            anim.SetTrigger("Hit");
            audioSource.clip = gettingHitAudio;
            audioSource.Play();
            audioSource3.clip = hitAudio;
            audioSource3.Play();
        }
    }
}
