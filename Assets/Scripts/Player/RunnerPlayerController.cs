using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class RunnerPlayerController : MonoBehaviour 
{
    [SerializeField]
    private GameObject entity;

    [SerializeField]
    private float jumpForce = 800;
    private bool isAlive = true;
    private bool grounded = true;

    private bool crouching = false;
    [SerializeField]
    private float crouchtime = 1f;
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
    }

	void Start () 
    {
	
	}
	
	void Update () 
    {
        if (GameManager.WhatState() == "playing")
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

                if (crouching)
                {
                    entity.transform.localScale = new Vector3(3, 2, 1);
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
