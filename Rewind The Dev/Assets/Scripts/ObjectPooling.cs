using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooling : MonoBehaviour
{
    [System.Serializable]
    public class Pool
    {
        public string key;
        public GameObject prefab;
        public int size;
    }

    public static ObjectPooling instance;
    private void Awake() => instance = this;
    

    public Pool[] pools;
    public Dictionary<string, Queue<GameObject>> poolDictionary = new Dictionary<string, Queue<GameObject>>();


    void Start()
    {
        for (int i = 0; i < pools.Length; i++)
        {
            Queue<GameObject> pool = new Queue<GameObject>();
            for (int a = 0; a < pools[i].size; a++)
            {
                GameObject aux = Instantiate(pools[i].prefab);
                aux.SetActive(false);
                pool.Enqueue(aux);               
            }
            poolDictionary.Add(pools[i].key, pool);
        }       
    }

    public GameObject SpawnFromPool(string pool,Vector3 pos,Quaternion rot)
    {
        if (poolDictionary.ContainsKey(pool))
        {
            GameObject aux = poolDictionary[pool].Dequeue();
            aux.SetActive(true);
            aux.transform.position = pos;
            aux.transform.rotation = rot;
            poolDictionary[pool].Enqueue(aux);
            return aux;
        }
        else 
        {
            Debug.Log("Pool not found.");
            return null;
        }
    }
}
