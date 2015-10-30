using UnityEngine;
using System.Collections;

public class DoNotDie : MonoBehaviour 
{
	void Awake()
	{
		DontDestroyOnLoad(this);
	}
}
