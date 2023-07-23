using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooler : MonoBehaviour
{
	public static ObjectPooler Instance;

	public event Action OnInitialized;

	[System.Serializable]
	public class Pool
	{
		public PoolType tag;
		public int poolSize;
		public GameObject prefab;
		
		public Pool(PoolType tag, int poolSize, GameObject prefab)
		{
			this.tag = tag;
			this.poolSize = poolSize;
			this.prefab = prefab;
		}
	}

	public List<Pool> pools = new List<Pool>();

	private Dictionary<PoolType, Queue<GameObject>> poolDictionary = new Dictionary<PoolType, Queue<GameObject>>();

	private void Awake()
	{
		if(Instance == null)
		{
			Instance = this;
		}
		else
		{
			Destroy(gameObject);
		}

		InitializePooler();
	}

	private void InitializePooler()
	{
		// cycle through pools
		foreach(Pool item in pools)
		{
			// initialize container for pool objects
			Queue<GameObject> pool = new Queue<GameObject>();

			// create pool objects, set inactive and enqueue
			for (int i = 0; i < item.poolSize; i++)
			{
				GameObject objct = Instantiate(item.prefab);
				objct.SetActive(false);
				pool.Enqueue(objct);
			}

			// add pool to pool list
			poolDictionary.Add(item.tag, pool);
		}

		OnInitialized?.Invoke();
	}

	/// <summary>
	/// check if a pool with given tag exists
	/// </summary>
	/// <param name="tag"></param>
	/// <returns></returns>
	private bool PoolExists(PoolType tag)
	{
		return poolDictionary.ContainsKey(tag);
	}

	public GameObject TakeFromPool(PoolType tag, Vector3 position, Quaternion rotation)
	{
		// check if pool exists
		if (!PoolExists(tag))
		{
			Debug.LogError("NO POOL TO TAKE OBJECT FROM, CHECK IT.");
			return null;
		}

		// check if pool is empty
		if (poolDictionary[tag].Count <= 0)
		{
			// increase pool size
			IncreasePool(tag);
		}

		// remove object from pool list, activate it, set values and return it
		GameObject objct = poolDictionary[tag].Dequeue();

		objct.SetActive(true);
		objct.transform.SetPositionAndRotation(position, rotation);

		return objct;
	}

	public void ReturnToPool(PoolType tag, GameObject objct)
	{
		// check if pool exists
		if (!PoolExists(tag))
		{
			Debug.LogError("NO POOL TO RETURN OBJECT TO, CHECK IT.");
			return;
		}

		// set inactive and add to pool
		objct.SetActive(false);
		poolDictionary[tag].Enqueue(objct);
	}

	private void IncreasePool(PoolType tag, int amount = 2)
	{
		Pool pool = null;

		foreach (Pool item in pools)
		{
			if (item.tag == tag)
			{
				pool = item;
				break;
			}
		}

		for (int i = 0; i < amount; i++)
		{
			GameObject objct = Instantiate(pool.prefab);
			objct.SetActive(false);
			poolDictionary[tag].Enqueue(objct);
		}
	}

}

public enum PoolType
{
	LevelSectionA,
	LevelSectionB,
	LevelSectionC,
	LevelSectionD,
}