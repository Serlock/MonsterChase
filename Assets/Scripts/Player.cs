using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public float moveForce = 10f;
    public float jumpForce = 11f;

    private float movementX;
    public Rigidbody2D myBody;
    public Animator anim;
    private SpriteRenderer sr;
    private string WALK_ANIMATION = "Walk";
    private string JUMP_ANIMATION = "Jump";
    private string GROUND_TAG = "Ground";
    private string ENEMY_TAG = "Enemy";

    private bool isGrounded;

    private void Awake()
    {
        myBody = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        PlayerMoveKeyboard();
        AnimatePlayer();
        PlayerJump();
    }

    private void PlayerMoveKeyboard()
    {
        movementX = Input.GetAxisRaw("Horizontal");
        transform.position += new Vector3(movementX, 0f, 0f) * Time.deltaTime * moveForce;
    }

    void AnimatePlayer()
    {
        if (movementX > 0)
        {
            anim.SetBool(WALK_ANIMATION, true);
            sr.flipX = false;
        }
        else if (movementX < 0)
        {
            anim.SetBool(WALK_ANIMATION, true);
            sr.flipX = true;
        }
        else
        {
            anim.SetBool(WALK_ANIMATION, false);
        }

    }

    void PlayerJump()
    {
        if (Input.GetButtonDown("Jump"))
        {
            if (isGrounded == true)
            {
                myBody.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);
                anim.SetBool(JUMP_ANIMATION, true);
                isGrounded = false;
            }
            else
            {
                myBody.AddForce(new Vector2(0f, -jumpForce * 2), ForceMode2D.Impulse);
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Vector2 direction = new Vector2();

        Debug.Log(direction.y);
        if (collision.gameObject.CompareTag(GROUND_TAG))
        {
            isGrounded = true;
            anim.SetBool(JUMP_ANIMATION, false);
        }

        if (collision.gameObject.CompareTag(ENEMY_TAG))
        {
            direction = collision.GetContact(0).normal;
            if (direction.y > 0.8 && direction.y < 1)
            {
                Destroy(collision.gameObject);
            }
            else
            {
                Destroy(gameObject);
                SceneManager.LoadScene("Credits");
            }
        }
    }

    private void OnTriggerEnter2d(Collider2D collider)
    {
        if (collider.CompareTag(ENEMY_TAG))
        {
            Destroy(gameObject);
            SceneManager.LoadScene("Credits");
        }
    }
}
