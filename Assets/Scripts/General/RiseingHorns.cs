using UnityEngine;
using System.Collections;

public class RiseingHorns : MonoBehaviour 
{
    public float speed = 2.0f;
    public float hornHeight = -0.5f;
	void Start () 
    {
	
	}

	void Update () 
    {
        Vector3 temp = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        if (temp.y < hornHeight)
        {
            temp.y += Time.deltaTime * speed;
            transform.position = temp;
        }
	}
}
