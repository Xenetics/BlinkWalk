﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour 
{
    [SerializeField]
    private GameObject entity;

    [SerializeField]
    private float jumpForce = 1;
    private bool isAlive = true;
    private bool grounded = true;

    private bool crouching = false;
    [SerializeField]
    private float crouchtime = 1f;
    private float crouchTimer;

    private float maxVision = 10f;
    public float currentVision { get; set; }

    private static PlayerController instance = null;
    public static PlayerController Instance { get { return instance; } }

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
        currentVision = maxVision;
    }

	void Start () 
    {
	
	}
	
	void Update () 
    {
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

            if (Input.GetButtonDown("Crouch"))
            {
                crouching = true;
            }

            if (currentVision > 0.0f)
            {
                if (Input.GetButton("Vision"))
                {
                    currentVision -= Time.deltaTime;
                    Camera.main.cullingMask = LayerMask.NameToLayer("Everything");
                }
                else
                {
                    Camera.main.cullingMask = 1 << LayerMask.NameToLayer("Player") | 1 << LayerMask.NameToLayer("UI");
                }
            }
            else
            {
                Camera.main.cullingMask = 1 << LayerMask.NameToLayer("Player") | 1 << LayerMask.NameToLayer("UI");
            }

            if(crouching)
            {
                entity.transform.localScale = new Vector3(3, 2, 1);
                crouchTimer -= Time.deltaTime;
                if(crouchTimer <= 0f)
                {
                    crouching = false;
                    crouchTimer = crouchtime;
                }
            }
            else
            {
                entity.transform.localScale = new Vector3(2, 3, 1);
            }
        }
	}

    void OnCollisionEnter2D(Collision2D other)
    {
        grounded = true;
    }

    public void Reset()
    {

    }

    public float VisionScale()
    {
        float scale = currentVision / maxVision;

        return scale;
    }
}
