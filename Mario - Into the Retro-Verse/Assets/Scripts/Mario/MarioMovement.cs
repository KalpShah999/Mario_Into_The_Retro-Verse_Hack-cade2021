using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MarioMovement : MonoBehaviour
{
    public Rigidbody2D rb;
    public Transform groundCheck;
    public LayerMask whatIsGround;
    public Animator anim;
    public BoxCollider2D playerCollider;
    public CircleCollider2D playerCollider2;
    public GameObject transitionShip;

    private bool isGrounded;
    private float speed = 10f;
    private float jumpTime = 1f;
    private bool canMove;

    void Start()
    {
        playerCollider.enabled = true;
        playerCollider2.enabled = true;
        canMove = true;
        anim.SetBool("isDead", false);
    }

    void Update()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        speed = Input.GetButton("Sprint") ? 10f : 6f;

        anim.SetFloat("velocityX", Mathf.Abs(horizontal));
        anim.SetBool("isGrounded", isGrounded);

        isGrounded = Physics2D.OverlapCircle(groundCheck.position, 0.15f, whatIsGround);

        if (isGrounded && canMove) 
        {
            jumpTime = 1;
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

        if (Input.GetButton("Jump") && jumpTime > 0 && canMove)
        {
            isGrounded = false;
            rb.velocity = new Vector2(rb.velocity.x, 15f);
            jumpTime -= 5 * Time.deltaTime;
        }

        if (Input.GetButtonUp("Jump") && canMove)
        {
            jumpTime = 0;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Hazard")
        {
            //Die
            transform.position = new Vector3(transform.position.x, transform.position.y, -3f);
            playerCollider.enabled = false;
            playerCollider2.enabled = false;
            anim.SetBool("isDead", true);
            canMove = false;
            rb.velocity = new Vector2(rb.velocity.x, 0f);
            rb.AddForce(new Vector2(0f, 500f));
            StartCoroutine(Restart());
        }
        else if (collision.collider.name == "HeadCollider")
        {
            rb.velocity = new Vector2(rb.velocity.x, 0f);
            rb.AddForce(new Vector2(0f, 700f));
        }
        else if (collision.collider.name == "Transition")
        {
            StartTransition();
        }
    }

    public void StartTransition()
    {
        StartCoroutine(Transition());
    }

    IEnumerator Transition()
    {
        rb.velocity = new Vector2(0f, 0f);
        canMove = false;
        rb.constraints = RigidbodyConstraints2D.FreezeAll;
        transitionShip.SetActive(true);
        yield return new WaitForSeconds(0.75f);
        gameObject.transform.GetComponent<SpriteRenderer>().enabled = false;
    }

    IEnumerator Restart()
    {
        yield return new WaitForSeconds(3f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
