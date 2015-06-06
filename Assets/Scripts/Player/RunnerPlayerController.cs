using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class RunnerPlayerController : MonoBehaviour 
{
    [SerializeField]
    private GameObject entity;
    private Vector2 entityOrigin;
    private float correctionSpeed = 1;
    private Rigidbody2D entityPhysics;
    [SerializeField]
    private ParticleSystem trail;
    [SerializeField]
    private ParticleSystemRenderer particleTexture;
    [SerializeField]
    private Material upRightParticle;
    [SerializeField]
    private Material crouchParticle;

    [SerializeField]
    private ParticleSystem explosionParticle;

    [SerializeField]
    private float jumpForce = 800;
    private bool isAlive = true;
    private bool grounded = true;
    private bool crouching = false;
    [SerializeField]
    private float crouchtime = 1.5f;
    private float crouchTimer;

    private float maxVision = 10f;
    public float currentVision { get; set; }
    private float collectableWorth = 0.5f;

    private float botOutOfScreen = -12f;
    private float leftOutOfScreen = -19f;

    private static RunnerPlayerController instance = null;
    public static RunnerPlayerController Instance { get { return instance; } }

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
        entityPhysics = entity.GetComponentInChildren<Rigidbody2D>();
        entityOrigin = entity.transform.position;
    }

	void Start () 
    {
	
	}
	
	void Update () 
    {
        if (GameManager.WhatState() == "playing" && InGameUIManager.Instance.paused == false)
        {
            HandleTrail();
            if (isAlive)
            {
                if (grounded)
                {
                    if (Input.GetButtonDown("Jump") || Input.touchCount == 1)
                    {
                        entityPhysics.AddForce(new Vector2(0, jumpForce));
                        grounded = false;
                    }

                    if (entity.transform.position.x < entityOrigin.x)
                    {
                        entityPhysics.velocity = new Vector2(correctionSpeed, entityPhysics.velocity.y);
                    }
                }

                if (Input.GetButtonDown("Crouch") || Input.touchCount == 2)
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

                if (crouching)
                {
                    entity.transform.localScale = new Vector3(3, 1.5f, 1);
                    crouchTimer -= Time.deltaTime;
                    if (crouchTimer <= 0f)
                    {
                        crouching = false;
                        crouchTimer = crouchtime;
                    }
                }
                else
                {
                    entity.transform.localScale = new Vector3(2, 3, 1);
                }

                if (entity.transform.position.y < botOutOfScreen || entity.transform.position.x < leftOutOfScreen)
                {
                    isAlive = false;
                }
            }
            else
            {
                InGameUIManager.Instance.EndGame();
            }
        }
	}

    void OnCollisionEnter2D(Collision2D other)
    {
        grounded = true;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Collectable")
        {
            Destroy(other.gameObject);
            currentVision += collectableWorth;
        }

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

    public float VisionScale()
    {
        float scale = currentVision / maxVision;

        return scale;
    }

    private void HandleTrail()
    {
        if (isAlive)
        {
            trail.emissionRate = 10;
        }
        else
        {
            trail.emissionRate = 0;
        }

        if (!crouching)
        {
            particleTexture.material = upRightParticle;
        }
        else
        {
            particleTexture.material = crouchParticle;
        }
    }
}
