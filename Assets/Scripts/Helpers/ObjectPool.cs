using MyBox;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ObjectPool : Singleton<ObjectPool>
{
    public List<GameObject> PrefabsForPool;

    private List<GameObject> pooledObjects = new List<GameObject>();

    public GameObject GetObjectFromPool(string objectName)
    {
        var instance = pooledObjects.FirstOrDefault(obj => obj.name == objectName);
        if(instance != null)
        {
            Debug.Log("pooledObject "+instance);
            if(!instance.activeSelf)
            {
                pooledObjects.Remove(instance);
                instance.SetActive(true);
            }
            return instance;
        }

        var prefab = PrefabsForPool.FirstOrDefault(obj => obj.name == objectName);
        if(prefab != null)
        {
            var newInstance = Instantiate(prefab, transform);
            newInstance.name = objectName;
            pooledObjects.Add(prefab);
            return newInstance;
        }

        Debug.LogWarning("Object pool doesn't have a prefab for the object with name " + objectName);
        return null;
    }

    public void PoolObject(GameObject obj)
    {
        obj.SetActive(false);
        pooledObjects.Add(obj);
    }
}
