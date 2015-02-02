using UnityEngine;
using System.Collections;

public class PlatformPlayerControl : MonoBehaviour 
{
    [SerializeField]
    private GameObject entity;
    [SerializeField]
    private GameObject trail;

    [SerializeField]
    private float moveForce = 5;

    [SerializeField]
    private float jumpForce = 1;
    private bool isAlive = true;
    [SerializeField]
    private bool grounded = true;
    [SerializeField]
    private bool walledLeft = false;
    [SerializeField]
    private bool walledRight = false;

    private bool crouching = false;
    [SerializeField]
    private float crouchtime = 1.5f;
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
                if (Input.GetButton("Jump"))
                {
                    entity.rigidbody2D.AddForce(new Vector2(0, jumpForce));
                    grounded = false;
                }
            }

            if (Input.GetButton("Left"))
            {
                if (!walledLeft)
                {
                    entity.rigidbody2D.velocity = new Vector2(-moveForce, entity.rigidbody2D.velocity.y);
                }
            }
            else if (Input.GetButton("Right"))
            {
                if (!walledRight)
                {
                    entity.rigidbody2D.velocity = new Vector2(moveForce, entity.rigidbody2D.velocity.y);
                }
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
                entity.transform.localScale = new Vector3(3, 1.5f, 1);
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
        HandleTrail();
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.contacts.Length > 0)
        {
            ContactPoint2D contact = other.contacts[0];
            if (Vector2.Dot(contact.normal, Vector2.up) > 0.5)
            {
                grounded = true;
            }

            if(Vector2.Dot(contact.normal, -Vector2.right) > 0.5)
            {
                walledRight = true;
            }
            else
            {
                walledRight = false;
            }

            if (Vector2.Dot(contact.normal, Vector2.right) > 0.5)
            {
                walledLeft = true;
            }
            else
            {
                walledLeft = false;
            }
        }
    }

    void OnCollisionStay2D(Collision2D other)
    {

    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Spike")
        {
            InGameUIManager.Instance.EndGame();
        }
    }

    public void Reset()
    {

    }

    private void HandleTrail()
    {
        if (grounded)
        {
            trail.SetActive(false);
        }
        else
        {
            trail.SetActive(true);
        }
    }
}
