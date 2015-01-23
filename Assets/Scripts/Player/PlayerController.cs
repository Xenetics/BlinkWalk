using UnityEngine;
using System.Collections;

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

    private int eyeOpened = 1;
    private int eyeClosed = 8;

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

            if (Input.GetButton("Vision"))
            {
                Camera.main.cullingMask = LayerMask.NameToLayer("Everything");
            }
            else
            {
                Camera.main.cullingMask = 1 << LayerMask.NameToLayer("Player") | 1 << LayerMask.NameToLayer("UI");
            }

            if(crouching)
            {
                entity.transform.localScale = new Vector3(2, 2, 1);
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
}
