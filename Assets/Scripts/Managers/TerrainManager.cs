using UnityEngine;
using System.Collections;

public class TerrainManager : MonoBehaviour 
{
    [SerializeField]
    private GameObject currentChunk;
    [SerializeField]
    private float speed = 5f;
    private float chunkDonePoint = -16f;

	void Start () 
    {
	
	}
	
	void Update () 
    {
	    //put if game playing && not paused
        currentChunk.transform.Translate(-speed * Time.deltaTime, 0, 0);

        if(currentChunk.transform.position.x < chunkDonePoint)
        {
            //put chunk back in pool
            //add new chunk to end of train
        }
	}
}
