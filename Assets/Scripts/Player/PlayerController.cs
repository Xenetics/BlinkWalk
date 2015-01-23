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
            else
            {

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
