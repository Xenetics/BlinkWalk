﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlatformPlayerControl : MonoBehaviour 
{
    [SerializeField]
    private GameObject entity;
    private Rigidbody2D entityPhysics;
    private BoxCollider2D boxCollider;
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
    private Vector3 crouchDestination;
    private float crouchSpeed = 4f;
    [SerializeField]
    private float crouchtime = 1.5f;
    private float crouchTimer;

    private List<Vector3> colliding = new List<Vector3>();

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
        boxCollider = entity.GetComponent<BoxCollider2D>();
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
                crouchDestination = new Vector3(3, 1.5f, 1);
                if (entity.transform.localScale != crouchDestination)
                {
                    SmoothCrouch();
                }
                //entity.transform.localScale = new Vector3(3, 1.5f, 1);
            }
            else
            {
                crouching = false;
                crouchDestination = new Vector3(2, 3, 1);
                if (entity.transform.localScale != crouchDestination)
                {
                    SmoothCrouch();
                }
                //entity.transform.localScale = new Vector3(2, 3, 1);
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

    private void SmoothCrouch()
    {
        entity.transform.localScale = Vector3.Lerp(entity.transform.localScale, crouchDestination, Time.deltaTime * crouchSpeed);
        if(Vector3.Distance(entity.transform.localScale, crouchDestination) < 0.05f)
        {
            entity.transform.localScale = crouchDestination;
        }
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        walledRight = false;
        walledLeft = false;
        if (other.contacts.Length > 0)
        {
            foreach (ContactPoint2D contact in other.contacts)
            {
                if (Vector2.Dot(contact.normal, Vector2.up) > 0.5)
                {
                    grounded = true;
                }

                if (Vector2.Dot(contact.normal, -Vector2.right) > 0.5)
                {
                    walledRight = true;
                }

                if (Vector2.Dot(contact.normal, Vector2.right) > 0.5)
                {
                    walledLeft = true;
                }
            }
        }
        if (!colliding.Contains(other.transform.position))
        {
            colliding.Add(other.transform.position);
        }
    }

    void OnCollisionExit2D(Collision2D other)
    {
        if (other.contacts.Length > 0)
        {
            foreach (ContactPoint2D contact in other.contacts)
            {
                if (Vector2.Dot(contact.normal, -Vector2.right) > 0.5)
                {
                    walledRight = false;
                }

                if (Vector2.Dot(contact.normal, Vector2.right) > 0.5)
                {
                    walledLeft = false;
                }
            }
        }
        colliding.Remove(other.transform.position);
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

    void OnDrawGizmos()
    {
        foreach(Vector2 vec in colliding)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireCube(vec, new Vector3(1, 1, 1));
        }
    }
}
