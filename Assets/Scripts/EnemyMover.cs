using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMover : MonoBehaviour
{
    public Transform groundCheck1;
    public Transform groundCheck2;
    public float movementSpeed;
    [HideInInspector] public bool grounded = false;
    int direction = -1;
    private Transform pos;
    [HideInInspector] public bool directionLocked = false;
    [HideInInspector] public bool flipped = false;
    [HideInInspector] public float lastTime = 0f;
    [HideInInspector] public bool pose1 = true;
    public Sprite standingSprite;
    public Sprite WalkSprite;



    // Use this for initialization
    void Start ()
    {
        pos = this.GetComponent<Transform>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        grounded = Physics2D.Linecast(transform.position, groundCheck1.position, 1 << LayerMask.NameToLayer("Ground")) && Physics2D.Linecast(transform.position, groundCheck2.position, 1 << LayerMask.NameToLayer("Ground"));
    }
    private void FixedUpdate()
    {

        if (grounded || directionLocked)
        {
            pos.position += new Vector3(direction * Time.deltaTime * movementSpeed, 0f, 0f);
            directionLocked = false;

        }
        else
        {
            direction = -direction;
            directionLocked = true;
            pos.position += new Vector3(direction * Time.deltaTime * movementSpeed, 0f, 0f);
            this.GetComponent<SpriteRenderer>().flipX = !flipped;
            flipped = !flipped;
        }

        if (Time.time * 1000 > lastTime * 1000 + 200)
        {
            if (pose1)
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
}
