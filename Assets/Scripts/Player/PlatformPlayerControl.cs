using UnityEngine;
using System.Collections;

public class PlatformPlayerControl : MonoBehaviour 
{
    [SerializeField]
    private GameObject entity;
    private Rigidbody2D entityPhysics;
    [SerializeField]
    private ParticleSystem upRightTrail;
    [SerializeField]
    private ParticleSystem crouchTrail;

    [SerializeField]
    private ParticleSystem explosionParticle;

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

    [SerializeField]
    private bool crouching = false;
    [SerializeField]
    private float crouchtime = 1.5f;
    private float crouchTimer;

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
        entityPhysics = entity.GetComponentInChildren<Rigidbody2D>();
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
                    entityPhysics.AddForce(new Vector2(0, jumpForce));
                    grounded = false;
                }
            }

            if (Input.GetButton("Left"))
            {
                if (!walledLeft)
                {
                    entityPhysics.velocity = new Vector2(-moveForce, entityPhysics.velocity.y);
                }
            }
            else if (Input.GetButton("Right"))
            {
                if (!walledRight)
                {
                    entityPhysics.velocity = new Vector2(moveForce, entityPhysics.velocity.y);
                }
            }
            else
            {
                if ((entityPhysics.velocity.x < moveForce * 0.75f && entityPhysics.velocity.x > 0) || (entityPhysics.velocity.x > -moveForce * 0.75f && entityPhysics.velocity.x < 0))
                {
                    entityPhysics.velocity = new Vector2(0, entityPhysics.velocity.y);
                }
            }

            if (Input.GetButton("Crouch"))
            {
                crouching = true;
                entity.transform.localScale = new Vector3(3, 1.5f, 1);
            }
            else
            {
                crouching = false;
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

            if(Vector2.Dot(contact.normal, -Vector2.right) > 0.5 && !grounded)
            {
                walledRight = true;
            }
            else
            {
                walledRight = false;
            }

            if (Vector2.Dot(contact.normal, Vector2.right) > 0.5 && !grounded)
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
            entity.GetComponentInChildren<MeshRenderer>().enabled = false;
            explosionParticle.Play();
            isAlive = false;
        }
    }

    public void Reset()
    {

    }

    private void HandleTrail()
    {
        if (!crouching && entityPhysics.velocity.magnitude > 0)
        {
            upRightTrail.emissionRate = 10;
            crouchTrail.emissionRate = 0;
            
        }
        else if (crouching && entityPhysics.velocity.magnitude > 0)
        {
            crouchTrail.emissionRate = 10;
            upRightTrail.emissionRate = 0;
        }
        else
        {
            upRightTrail.emissionRate = 0;
            crouchTrail.emissionRate = 0;
        }
    }
}
