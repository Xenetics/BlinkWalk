using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TerrainManager : MonoBehaviour 
{
    public static TerrainManager Instance { get { return instance; } }
    private static TerrainManager instance = null;

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

    [SerializeField]
    private GameObject startChunk;
    [SerializeField]
    private GameObject[] prefabs;

    private List<GameObject> chunkTrain;
    [SerializeField]
    private float speed = 5f;
    private int trainLength = 5;
    private float chunkSize = 35f;
    private float chunkDonePoint = -35f;

	void Start () 
    {
        chunkTrain = new List<GameObject>();
        chunkTrain.Add(Instantiate(startChunk, new Vector3(0, 0, 0), Quaternion.identity) as GameObject);
        StartTrain();
	}
	
	void Update () 
    {
	    //put if game playing && not paused
        for (int i = 0; i < chunkTrain.Count; ++i)
        {
            chunkTrain[i].transform.Translate(-speed * Time.deltaTime, 0, 0); // move chunks

            if (chunkTrain[i].transform.position.x < chunkDonePoint)
            {
                Destroy(chunkTrain[i]);
                chunkTrain.RemoveAt(i);
                GrabNew();
            }
        }
	}

    private void StartTrain()
    {
        for(int i = 1; i < trainLength; ++i)
        {
            GrabNew();
        }
    }

    private void GrabNew()
    {
        int randomChunk = Random.RandomRange(1, prefabs.Length); // chooses what chunk at random
        GameObject newChunk = Instantiate(prefabs[randomChunk], new Vector3((chunkTrain[chunkTrain.Count - 1].transform.position.x) + chunkSize, 0, 0), Quaternion.identity) as GameObject;
        newChunk.name = prefabs[randomChunk].name;
        newChunk.transform.parent = this.transform;
        chunkTrain.Add(newChunk);
    }
}
