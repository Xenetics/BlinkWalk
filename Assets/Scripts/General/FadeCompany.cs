using UnityEngine;
using System.Collections;

public class FadeCompany : MonoBehaviour 
{
    [SerializeField]
    private SpriteRenderer pic;
    private float fadeSpeed = 0.3f;

	void Start () 
    {
	
	}
	
	void Update () 
    {
        pic.color = Color.Lerp(pic.color, Color.white, fadeSpeed * Time.deltaTime);
	}
}
