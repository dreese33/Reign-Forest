using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooler : MonoBehaviour
{
    [System.Serializable]
    public class Pool {
        public string tag;
        public GameObject prefab;
        public int maxSize;
    }

    #region Singleton

    public static ObjectPooler Instance;
    private void Awake() {
        Instance = this;
    }

    #endregion

    public List<Pool> pools;
    public Dictionary<string, Queue<GameObject>> poolDictionary;

    void Start() {
        poolDictionary = new Dictionary<string, Queue<GameObject>>();

        //Initial objects in pool - based on wave number
        foreach (Pool pool in pools) {
            Queue<GameObject> objectPool = new Queue<GameObject>();
            for (int i = 0; i < pool.maxSize; i++) {
                GameObject obj = Instantiate(pool.prefab);
                obj.SetActive(false);
                objectPool.Enqueue(obj);
            }

            poolDictionary.Add(pool.tag, objectPool);
        }
    }

    private void PrintMissingTagWarning(string tag) {
        Debug.LogWarning("Pool with tag " + tag + " doesn't exist");
    }

    public GameObject SpawnFromPool(string tag) {
        if (!poolDictionary.ContainsKey(tag)) {
            PrintMissingTagWarning(tag);
            return null;
        }

        GameObject objectToSpawn = poolDictionary[tag].Dequeue();

        objectToSpawn.SetActive(true);

        IPooledObject pooledObj = objectToSpawn.GetComponent<IPooledObject>();
        if (pooledObj != null) {
            pooledObj.OnObjectSpawn();
        }

        //Returns object to pool
        //poolDictionary[tag].Enqueue(objectToSpawn);

        return objectToSpawn;
    }

    public void ReleaseToPool(GameObject obj, string tag) {
        if (!poolDictionary.ContainsKey(tag)) {
            PrintMissingTagWarning(tag);
            return;
        }

        obj.SetActive(false);
        poolDictionary[tag].Enqueue(obj);
    }
}
