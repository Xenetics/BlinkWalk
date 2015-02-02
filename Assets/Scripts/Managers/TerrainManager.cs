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
    private GameObject spike;
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
        if (GameManager.WhatState() == "playing" && InGameUIManager.Instance.paused == false)
        {
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
        // Check for spawnpoints and decide to make a Collectable at Spawnpoint returned
        Vector3 DotSpawnPoint = SpawnCollectable(newChunk);
        if (DotSpawnPoint != Vector3.zero)
        {
            GameObject newCollectable = Instantiate(collectable, DotSpawnPoint, Quaternion.identity) as GameObject;
            newCollectable.name = collectable.name;
            newCollectable.transform.parent = newChunk.transform;
        }
        // Check for spawnpoints and decide to make spikes
        Transform[] SpikeSpawnPoints = SpawnSpikes(newChunk);
        if(SpikeSpawnPoints != new Transform[0])
        {
            foreach(Transform spikePos in SpikeSpawnPoints)
            {
                GameObject newSpike = Instantiate(spike, spikePos.position, Quaternion.identity) as GameObject;
                newSpike.name = spike.name;
                newSpike.transform.parent = newChunk.transform;
            }
        }
        // Add chunk to ChunkTrain WOO WOO!!!!! 
        newChunk.transform.parent = this.transform;
        chunkTrain.Add(newChunk);
    }
    
    private Vector3 SpawnCollectable(GameObject chunk)
    {
        Transform[] possibleSpawnpoints = chunk.GetComponentsInChildren<Transform>();

        Transform[] spawnpoints = new Transform[3];

        for (int i = 0; i < possibleSpawnpoints.Length; ++i)
        {
            if (possibleSpawnpoints[i].gameObject.name == "DotSpawnPoint")
            {
                for(int j = 0; j < spawnpoints.Length; ++j)
                {
                    if(spawnpoints[j] == null)
                    {
                        spawnpoints[j] = possibleSpawnpoints[i];
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

    private Transform[] SpawnSpikes(GameObject chunk)
    {
        Transform[] possibleSpawnpoints = chunk.GetComponentsInChildren<Transform>();

        Transform[] spawnpoints = new Transform[2];

        for (int i = 0; i < possibleSpawnpoints.Length; ++i)
        {
            if (possibleSpawnpoints[i].gameObject.name == "SpikeStrip")
            {
                for (int j = 0; j < spawnpoints.Length; ++j)
                {
                    if (spawnpoints[j] == null)
                    {
                        spawnpoints[j] = possibleSpawnpoints[i];
                        break;
                    }
                }
            }
        }

        int toSpawn = Random.RandomRange(0, 10);

        switch (toSpawn)
        {
            case 0:
            case 1:
                int randomPoint = Random.RandomRange(0, spawnpoints.Length);
                
                Transform[] possibleSpikesToSpawn = spawnpoints[randomPoint].GetComponentsInChildren<Transform>();

                Transform[] spikesToSpawn = new Transform[possibleSpikesToSpawn.Length - 1];

                for (int i = 0; i < possibleSpikesToSpawn.Length; ++i)
                {
                    if (possibleSpikesToSpawn[i].gameObject.name == "SpikeSpawnPoint")
                    {
                        for (int j = 0; j < spikesToSpawn.Length; ++j)
                        {
                            if (spikesToSpawn[j] == null)
                            {
                                spikesToSpawn[j] = possibleSpikesToSpawn[i];
                                break;
                            }
                        }
                    }
                }
                return spikesToSpawn;
            default:
                return new Transform[0];
        }
    }
}
