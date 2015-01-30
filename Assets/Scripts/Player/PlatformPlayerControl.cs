using UnityEngine;
using System.Collections;

public class PlatformPlayerControl : MonoBehaviour 
{
    [SerializeField]
    private GameObject entity;

    [SerializeField]
    private float moveForce = 1;

    [SerializeField]
    private float jumpForce = 1;
    private bool isAlive = true;
    private bool grounded = true;
    private bool walled = false;

    private bool crouching = false;
    [SerializeField]
    private float crouchtime = 1f;
    private float crouchTimer;

    private float botOutOfScreen = -12f;
    private float leftOutOfScreen = -19f;

    private static PlatformPlayerControl instance = null;
    public static PlatformPlayerControl Instance { get { return instance; } }

    void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
        else
        {
            instance = this;
        }
        crouchTimer = crouchtime;
    }

    void Start()
    {

    }

    void Update()
    {
        /*
        if (GameManager.WhatState() == "playing")
        {
        */
        if (isAlive)
        {
            if (grounded)
            {
                if (Input.GetButtonDown("Jump"))
                {
                    entity.rigidbody2D.AddForce(new Vector2(0, jumpForce));
                    grounded = false;
                }
            }

            if (Input.GetButton("Left") && ! walled)
            {
                entity.rigidbody2D.velocity = new Vector2(-moveForce, entity.rigidbody2D.velocity.y);
            }
            else if (Input.GetButton("Right") && !walled)
            {
                entity.rigidbody2D.velocity = new Vector2(moveForce, entity.rigidbody2D.velocity.y);
            }
            else
            {
                if ((entity.rigidbody2D.velocity.x < moveForce * 0.75f && entity.rigidbody2D.velocity.x > 0) || (entity.rigidbody2D.velocity.x > -moveForce * 0.75f && entity.rigidbody2D.velocity.x < 0))
                {
                    entity.rigidbody2D.velocity = new Vector2(0, entity.rigidbody2D.velocity.y);
                }
            }

            if (Input.GetButton("Crouch"))
            {
                entity.transform.localScale = new Vector3(3, 2, 1);
            }
            else
            {
                entity.transform.localScale = new Vector3(2, 3, 1);
            }
            /*
            if (entity.transform.position.y < botOutOfScreen || entity.transform.position.x < leftOutOfScreen)
            {
                isAlive = false;
            }
            */
        }
        else
        {
            //InChallengeUIManager.Instance.EndGame();
        }
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Floor" || other.gameObject.tag == "Platform")
        {
            grounded = true;
            walled = false;
        }
    }

    void OnCollisionStay2D(Collision2D other)
    {
        if (other.gameObject.tag == "Wall" && !grounded)
        {
            walled = true;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {

    }

    public void Reset()
    {

    }
}
