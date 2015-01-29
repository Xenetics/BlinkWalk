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
    private GameObject collectable;
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
        chunkTrain.Add(Instantiate(startChunk, new Vector3(0, 0, 0), Quaternion.identity) as GameObject); // add starting room
        StartTrain(); // WOO WOO
	}
	
	void Update () 
    {
	    //put if game playing && not paused
        for (int i = 0; i < chunkTrain.Count; ++i)
        {
            chunkTrain[i].transform.Translate(-speed * Time.deltaTime, 0, 0); // move chunks

            if (chunkTrain[i].transform.position.x < chunkDonePoint) // destroy chunks and make new ones if needed
            {
                Destroy(chunkTrain[i]);
                chunkTrain.RemoveAt(i);
                GrabNew();
            }
        }
	}

    private void StartTrain() // WOO WOO
    {
        for(int i = 1; i < trainLength; ++i)
        {
            GrabNew();
        }
    }

    private void GrabNew()
    {
        // Chooses what chunk at random
        int randomChunk = Random.RandomRange(1, prefabs.Length); 
        // Make and rename Chunk
        GameObject newChunk = Instantiate(prefabs[randomChunk], new Vector3((chunkTrain[chunkTrain.Count - 1].transform.position.x) + chunkSize, 0, 0), Quaternion.identity) as GameObject;
        newChunk.name = prefabs[randomChunk].name;
        // Check for spawnpoints and decided to make a Collectable at Spawnpoint returned
        Vector3 spawnPoint = SpawnCollectable(newChunk);
        if (spawnPoint != Vector3.zero)
        {
            GameObject newCollectable = Instantiate(collectable, spawnPoint, Quaternion.identity) as GameObject;
            newCollectable.name = collectable.name;
            newCollectable.transform.parent = newChunk.transform;
        }
        // Add chunk to ChunkTrain WOO WOO!!!!! 
        newChunk.transform.parent = this.transform;
        chunkTrain.Add(newChunk);
    }
    
    private Vector3 SpawnCollectable(GameObject chunk)
    {
        Transform[] PossibleSpawnpoints = chunk.GetComponentsInChildren<Transform>();

        Transform[] spawnpoints = new Transform[3];

        for(int i = 0; i < PossibleSpawnpoints.Length; ++i)
        {
            if(PossibleSpawnpoints[i].gameObject.name == "SpawnPoint")
            {
                for(int j = 0; j < spawnpoints.Length; ++j)
                {
                    if(spawnpoints[j] == null)
                    {
                        spawnpoints[j] = PossibleSpawnpoints[i];
                        break;
                    }
                }
            }
        }

        int toSpawn = Random.RandomRange(0, 10);

        switch(toSpawn)
        {
            case 0:
            case 1:
                int randomPoint = Random.RandomRange(0, spawnpoints.Length);
                return spawnpoints[randomPoint].transform.position;
            default:
                return Vector3.zero;
        }
    }
}
