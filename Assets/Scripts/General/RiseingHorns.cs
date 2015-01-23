using UnityEngine;
using System.Collections;

public class RiseingHorns : MonoBehaviour 
{
    public float speed = 10.0f;
    public float hornHeight = 4.5f;
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
