using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool
{
    private List<GameObject> pool = new List<GameObject>();
    GameObject template = null;
    GameObject parent = null;

    public void CreatePool(GameObject template, int amount)
    {
        this.template = template;
        GameObject _parent = new GameObject();
        parent = _parent;
        for (int i = 0; i < amount; i++)
        {
            AddOjbectToPool();
        }
    }

    public GameObject GetParent()
    {
        return parent;
    }

    public List<GameObject> GetPool()
    {
        return pool;
    }

    public GameObject Spawn()
    {
        if (pool.Count == 0)
        {
            Debug.LogWarning("Pool: No pool created.");
        }

        for (int i = 0; i < pool.Count; i++)
        {
            if (!pool[i].activeSelf)
            {
                pool[i].SetActive(true);
                return pool[i];
            }
        }

        GameObject addedObject = AddOjbectToPool();
        return addedObject;
    }

    public void Despawn(GameObject toDespawn)
    {
        pool.Find(x => x == toDespawn).SetActive(false);
    }

    private GameObject AddOjbectToPool()
    {
        GameObject toAdd = Object.Instantiate(template);
        toAdd.transform.parent = parent.transform;
        toAdd.SetActive(false);
        pool.Add(toAdd);
        return toAdd;
    }
}
