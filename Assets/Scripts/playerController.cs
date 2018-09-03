using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerController : MonoBehaviour
{

    public float movementSpeed;
    public float jumpSpeed;
    public float maxSpeed = 5f;
    public Transform groundCheck;

    public Transform groundCheck2;
    public Rigidbody2D rb2d;

    public Sprite standingSprite;
    public Sprite jumpSprite;
    public Sprite WalkSprite;

    [HideInInspector] public bool facingRight = true;
    private bool grounded = false;
    [HideInInspector] public bool jump = false;
    [HideInInspector] public float lastTime = 0f;
    [HideInInspector] public bool pose1 = true;


    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        grounded = Physics2D.Linecast(transform.position, groundCheck.position, 1 << LayerMask.NameToLayer("Ground")) || Physics2D.Linecast(transform.position, groundCheck2.position, 1 << LayerMask.NameToLayer("Ground"));
        if (Input.GetButtonDown("Jump") && grounded)
        {
            jump = true;
        }
    }

    private void FixedUpdate()
    {
        float h = Input.GetAxis("Horizontal");

        // anim.SetFloat("Speed", Mathf.Abs(h));
        if (Mathf.Abs(h) > 0 && grounded)
        {
            if(Time.time*1000 > lastTime*1000 + 200)
            {
                if(pose1)
                {
                    this.GetComponent<SpriteRenderer>().sprite = standingSprite;
                    pose1 = false;

                }
                else
                {
                    this.GetComponent<SpriteRenderer>().sprite = WalkSprite;
                    pose1 = true;


                }
                lastTime = Time.time;
            }
        }

        if (h == 0 && grounded)
        {
            this.GetComponent<SpriteRenderer>().sprite = standingSprite;
        }

        if (!grounded)
        {
            this.GetComponent<SpriteRenderer>().sprite = jumpSprite;
        }

        if (h * rb2d.velocity.x < maxSpeed)
        {
            rb2d.AddForce(Vector2.right * h * movementSpeed);
        }

        if (Mathf.Abs(rb2d.velocity.x) > maxSpeed)
        {
            rb2d.velocity = new Vector2(Mathf.Sign(rb2d.velocity.x) * maxSpeed, rb2d.velocity.y);
        }

        if (jump)
        {
            this.GetComponent<SpriteRenderer>().sprite = jumpSprite;
            // anim.SetTrigger("Jump");
            rb2d.AddForce(new Vector2(0f, jumpSpeed));
            jump = false;
        }

        if (h > 0 && !facingRight)
            Flip();
        else if (h < 0 && facingRight)
            Flip();
    }

    void Flip()
    {
        facingRight = !facingRight;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "ground")
        {
            this.GetComponent<SpriteRenderer>().sprite = standingSprite;
        }
    }
}
