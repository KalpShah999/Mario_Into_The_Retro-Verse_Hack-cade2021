using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MarioSpace : MonoBehaviour
{
    public Rigidbody2D rb;
    public Transform groundCheck;
    public LayerMask whatIsGround;
    public Animator anim;
    public BoxCollider2D playerCollider;
    public CircleCollider2D playerCollider2;
    public GameObject transitionShip;

    public GameObject fireBall;
    public GameObject upShoot;
    public GameObject forwardShoot;

    private bool isGrounded;
    private float speed = 10f;
    private float jumpTime = 1f;
    private bool canMove;
    private bool invincible;

    public int health;

    public SubZero subEnemy;

    public static bool world2;
    public static bool world3;
    public static bool world4;
    public LevelChanger levelChanger;

    public AudioSource audioSource;
    public AudioClip jumpAudio;
    public AudioClip hitAudio;
    public AudioClip dieAudio;
    public AudioClip powerUpAudio;
    public AudioClip swooshAudio;
    public AudioClip flagAudio;
    public AudioClip endGameAudio;

    public GameObject gameOver;

    private bool jumpCanPlay;

    void Start()
    {
        playerCollider.enabled = true;
        playerCollider2.enabled = true;
        canMove = true;
        anim.SetBool("isDead", false);
        anim.SetBool("isFiredUp", false);
        if (SceneManager.GetActiveScene().name == "MortalKombat")
        {

            anim.SetBool("isFiredUp", true);
        }
        anim.SetBool("isShooting", false);
        jumpCanPlay = true;

        if (SceneManager.GetActiveScene().name == "EndGame")
        {
            canMove = false;
            StartCoroutine(CutScene());
        }
        else if (SceneManager.GetActiveScene().name == "MortalKombat")
        {
            if (world4 == false)
            {
                world4 = true;
                StartCoroutine(NewWorld());
            }
            health = 3;
        }
        else if (SceneManager.GetActiveScene().name == "SpaceInvader")
        {
            if (world3 == false)
            {
                world3 = true;
                StartCoroutine(NewWorld());
            }
            health = 3;
        }
        else if (SceneManager.GetActiveScene().name == "Pac-Man")
        {

            if (world2 == false)
            {
                world2 = true;
                StartCoroutine(NewWorld());
            }
            health = 4;
        }
        else 
        {
            health = 1;
        }
    }

    void Update()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        speed = Input.GetButton("Sprint") ? 10f : 6f;

        anim.SetFloat("velocityX", Mathf.Abs(horizontal));
        anim.SetBool("isGrounded", isGrounded);

        isGrounded = Physics2D.OverlapCircle(groundCheck.position, 0.15f, whatIsGround);

        if (isGrounded && canMove) 
        {
            jumpTime = 1;
        }

        if (vertical < -0.5f && anim.GetBool("isFiredUp"))
        {
            canMove = false;
            anim.SetBool("isCrouched", true);
            rb.velocity = new Vector2(0f, 0f);
            rb.constraints = RigidbodyConstraints2D.FreezeAll;
            anim.transform.localPosition = new Vector3(0f, 0f, 0f);
            playerCollider.offset = new Vector2(0f, 0.25f);
            playerCollider.size = new Vector2(1f, 0.5f);
        }
        else if (anim.GetBool("isFiredUp"))
        {
            canMove = true;
            anim.SetBool("isCrouched", false);
            rb.constraints = RigidbodyConstraints2D.None;
            rb.constraints = RigidbodyConstraints2D.FreezeRotation;
            anim.transform.localPosition = new Vector3(0f, 0.5f, 0f);
            playerCollider.offset = new Vector2(0f, 0.75f);
            playerCollider.size = new Vector2(1f, 1.75f);
        }

        if (horizontal > 0.1f && canMove)
        {
            rb.velocity = new Vector2(speed, rb.velocity.y);
            transform.localScale = new Vector3(1f, 1f, 1f);
        }
        else if (horizontal < -0.1f && canMove)
        {
            rb.velocity = new Vector2(-speed, rb.velocity.y);
            transform.localScale = new Vector3(-1f, 1f, 1f);
        }
        else if (canMove)
        {
            rb.velocity = new Vector2(0, rb.velocity.y);
        }

        if (Input.GetButton("Jump") && jumpTime > 0 && canMove && !SceneManager.GetActiveScene().name.Equals("SpaceInvader"))
        {
            isGrounded = false;
            rb.velocity = new Vector2(rb.velocity.x, 15f);
            jumpTime -= 5 * Time.deltaTime;
            if (jumpCanPlay)
            {
                jumpCanPlay = false;
                audioSource.clip = jumpAudio;
                audioSource.Play();
                StartCoroutine(ResetJumpAudio());
            }
        }

        if (Input.GetButtonUp("Jump") && canMove)
        {
            jumpTime = 0;
        }

        if (Input.GetButtonDown("Sprint") && !anim.GetBool("isShooting") && anim.GetBool("isFiredUp") && canMove)
        {
            StartCoroutine(Shoot());
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Hazard")
        {
            if (!invincible)
            {
                health--;
                audioSource.clip = hitAudio;
                audioSource.Play();
                if (health <= 0)
                {
                    Death();
                }
                else
                {
                    StartCoroutine(Invincibility());
                }
            }
        }
        else if (collision.collider.name == "HeadCollider")
        {
            rb.velocity = new Vector2(rb.velocity.x, 0f);
            rb.AddForce(new Vector2(0f, 700f));
            if (SceneManager.GetActiveScene().name == "MortalKombat")
            {
                StartFinalTransition();
            }
        }
        else if (collision.collider.name == "Transition")
        {
            StartTransition();
        }
        else if (collision.collider.tag == "Flower")
        {
            Destroy(collision.collider.transform.gameObject);
            audioSource.clip = powerUpAudio;
            audioSource.Play();
            anim.SetBool("isFiredUp", true);
            anim.transform.localPosition = new Vector3(0f, 0.5f, 0f);
            playerCollider.offset = new Vector2(0f, 0.75f);
            playerCollider.size = new Vector2(1f, 1.75f);
        }
    }

    IEnumerator Invincibility()
    {
        invincible = true;
        yield return new WaitForSeconds(2f);
        invincible = false;
    }

    public void Death()
    {
        GameObject.FindGameObjectWithTag("MainAudio").GetComponent<AudioSource>().clip = dieAudio;
        GameObject.FindGameObjectWithTag("MainAudio").GetComponent<AudioSource>().Play();
        transform.position = new Vector3(transform.position.x, transform.position.y, -3f);
        playerCollider.enabled = false;
        playerCollider2.enabled = false;
        anim.SetBool("isDead", true);
        canMove = false;
        rb.velocity = new Vector2(rb.velocity.x, 0f);
        rb.AddForce(new Vector2(0f, 500f));
        if (subEnemy != null)
        {
            subEnemy.isWin = true;
        }
        StartCoroutine(Restart());
    }

    public void StartTransition()
    {
        StartCoroutine(Transition());
    }

    public void StartFinalTransition()
    {
        StartCoroutine(FinalTransition());
    }

    IEnumerator Transition()
    {
        rb.velocity = new Vector2(0f, 0f);
        canMove = false;
        rb.constraints = RigidbodyConstraints2D.FreezeAll;
        transitionShip.SetActive(true);
        yield return new WaitForSeconds(0.75f);
        audioSource.clip = swooshAudio;
        audioSource.Play();
        transform.Find("Mario").transform.GetComponent<SpriteRenderer>().enabled = false;
        yield return new WaitForSeconds(0.5f);
        levelChanger.ToBlack();
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    IEnumerator FinalTransition()
    {
        rb.velocity = new Vector2(0f, 0f);
        canMove = false;
        yield return new WaitForSeconds(4f);
        rb.constraints = RigidbodyConstraints2D.FreezeAll;
        transitionShip.SetActive(true);
        yield return new WaitForSeconds(0.75f);
        audioSource.clip = swooshAudio;
        audioSource.Play();
        transform.Find("Mario").transform.GetComponent<SpriteRenderer>().enabled = false;
        yield return new WaitForSeconds(0.5f);
        levelChanger.ToBlack();
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    IEnumerator Restart()
    {
        yield return new WaitForSeconds(3f);
        levelChanger.ToBlack();
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    IEnumerator Shoot()
    {
        anim.SetBool("isShooting", true);
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        if (vertical > Mathf.Abs(horizontal) - 0.25f && vertical > 0.5f)
        {
            var temp = Instantiate(fireBall);
            temp.transform.position = upShoot.transform.position;
            temp.transform.parent = null;
            temp.GetComponent<Rigidbody2D>().velocity = new Vector2(0f, 10f);
        }
        else if (transform.localScale == new Vector3(1f, 1f, 1f))
        {
            var temp = Instantiate(fireBall);
            temp.transform.position = forwardShoot.transform.position;
            temp.transform.parent = null;
            temp.GetComponent<Rigidbody2D>().velocity = new Vector2(10f, 0f);
        }
        else if (transform.localScale == new Vector3(-1f, 1f, 1f))
        {
            var temp = Instantiate(fireBall);
            temp.transform.position = forwardShoot.transform.position;
            temp.transform.parent = null;
            temp.GetComponent<Rigidbody2D>().velocity = new Vector2(-10f, 0f);
        }
        yield return new WaitForSeconds(0.5f);
        anim.SetBool("isShooting", false);
    }

    IEnumerator NewWorld()
    {
        //Time.timeScale = 0;
        transform.Find("Mario").transform.GetComponent<SpriteRenderer>().enabled = false;
        levelChanger.ToWhite();
        yield return new WaitForSecondsRealtime(2.5f);
        //worldLogo.enabled = true;
        //yield return new WaitForSecondsRealtime(2f);
        //worldLogo.enabled = false;
        //Time.timeScale = 1;
        rb.velocity = new Vector2(0f, 0f);
        canMove = false;
        rb.constraints = RigidbodyConstraints2D.FreezeAll;
        transitionShip.SetActive(true);
        yield return new WaitForSeconds(0.75f);
        audioSource.clip = swooshAudio;
        audioSource.Play();
        transform.Find("Mario").transform.GetComponent<SpriteRenderer>().enabled = true;
        canMove = true;
        rb.constraints = RigidbodyConstraints2D.None;
        rb.constraints = RigidbodyConstraints2D.FreezeRotation;
        yield return new WaitForSeconds(0.75f);
        transitionShip.SetActive(false);
    }

    IEnumerator ResetJumpAudio()
    {
        yield return new WaitForSeconds(0.5f);
        jumpCanPlay = true;
    }

    IEnumerator CutScene()
    {
        transform.Find("Mario").transform.GetComponent<SpriteRenderer>().enabled = false;
        transform.Find("Mario").transform.GetComponent<Animator>().enabled = false;
        levelChanger.ToWhite();
        yield return new WaitForSecondsRealtime(2.5f);
        rb.velocity = new Vector2(0f, 0f);
        canMove = false;
        rb.constraints = RigidbodyConstraints2D.FreezeAll;
        transitionShip.SetActive(true);
        yield return new WaitForSecondsRealtime(0.75f);
        audioSource.clip = swooshAudio;
        audioSource.Play();
        transform.Find("Mario").transform.GetComponent<SpriteRenderer>().enabled = true;
        yield return new WaitForSecondsRealtime(2.5f);
        GameObject.FindGameObjectWithTag("MainAudio").GetComponent<AudioSource>().clip = flagAudio;
        GameObject.FindGameObjectWithTag("MainAudio").GetComponent<AudioSource>().Play();
        yield return new WaitForSecondsRealtime(3.75f);
        GameObject.FindGameObjectWithTag("MainAudio").GetComponent<AudioSource>().clip = endGameAudio;
        GameObject.FindGameObjectWithTag("MainAudio").GetComponent<AudioSource>().Play();
        yield return new WaitForSecondsRealtime(2f);
        gameOver.SetActive(true);
        yield return new WaitForSecondsRealtime(3f);
        levelChanger.ToBlack();
        yield return new WaitForSecondsRealtime(2f);
        SceneManager.LoadScene("MainMenu");
    }
}
