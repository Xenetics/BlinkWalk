using UnityEngine;
using System;
using System.Collections.Generic;

public class PoolManager : MonoBehaviour
{
	//Member class for a prefab entered into the object pool
	[Serializable]
	public class ObjectPoolEntry 
	{
		//the object to pre-instantiate
		[SerializeField]
		public GameObject prefab;
		//quantity of object to pre-instantiate
		[SerializeField]
		public int count=3;
	}
	
	//The object prefabs which the pool can handle.
	public ObjectPoolEntry[] entries;
	//The pooled objects currently available. Indexed by the index of the objectPrefabs
	[HideInInspector]
	public List<GameObject>[] pool;
	//container object to store unused items in.
	protected GameObject containerObject;
	
	public static PoolManager Instance { get { return instance; } }
	private static PoolManager instance = null;
	
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
		DontDestroyOnLoad(this.gameObject);
		InitializePool();
	}
	
	// Use this for initialization
	void InitializePool()
	{
		containerObject = new GameObject("ObjectPool");
		containerObject.transform.parent = transform;

		//Loop through the object prefabs and make a new list for each one.
		//We do this because the pool can only support prefabs set to it in the editor,
		//so we can assume the lists of pooled objects are in the same order as object prefabs in the array
		pool = new List<GameObject>[entries.Length];    
		for (int i = 0; i < entries.Length; i++)
		{
			ObjectPoolEntry objectPrefab = entries[i];
			//create the repository
			pool[i] = new List<GameObject>();  
			//fill it
			for (int n = 0; n < objectPrefab.count; n++)
			{
				GameObject newObj = Instantiate(objectPrefab.prefab) as GameObject;
				newObj.name = objectPrefab.prefab.name;
				PoolObject(newObj);
			}
		}
	}
	

	//Gets a new object for the type provided. If no object of that type exists or if onlyPooled is true 
	//and there is no objects of that type in the pool then null will be returned.
	public GameObject GetObjectForType ( string objectType , bool onlyPooled )
	{
		for (int i = 0; i < entries.Length; i++)
		{
			GameObject prefab = entries[i].prefab;
			if (prefab.name == objectType)
			{
				if (pool[i].Count > 0)
				{
					GameObject pooledObject = pool[i][0];
					pool[i].RemoveAt(0);
					pooledObject.transform.parent = null;
					pooledObject.SetActive(true);
					return pooledObject;
				}
				if( !onlyPooled )
				{
					GameObject newObj = Instantiate( entries[ i ].prefab ) as GameObject;
					newObj.name = entries[ i ].prefab.name;
					return newObj;
				}
			}
		}
		//If we have gotten here either there was no object of the specified type
		//or none were left in the pool with onlyPooled set to true
		return null;
	}
	

	//Places the object back into a pool of the appropriate type if one exists.
	public void PoolObject ( GameObject obj )
	{
		for ( int i=0; i<entries.Length; i++)
		{
			if(entries[i].prefab.name == obj.name)
			{
				obj.SetActive(false);
				obj.transform.parent = containerObject.transform;
				pool[i].Add(obj);
				return;
			}
		}
	}
}

