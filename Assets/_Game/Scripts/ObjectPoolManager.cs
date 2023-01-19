using System.Collections.Generic;
using UnityEngine;

public static class ObjectPoolManager
{
    private static Dictionary<GameObject, Queue<GameObject>> gameObjectPoolDictionary = new Dictionary<GameObject, Queue<GameObject>>();
    private static Dictionary<object, Queue<Component>> poolDictionary = new Dictionary<object, Queue<Component>>();

    private static GameObject gameObjectObjectToSpawn;
    private static Component objectToSpawn;

    private const string POOLNAME = "Object Pool";
    private static Transform objectPool;

    public static void AddToPool(GameObject gameObject, int numberToAdd)
    {
        if (objectPool == null)
            objectPool = new GameObject(POOLNAME).GetComponent<Transform>();

        if (!gameObjectPoolDictionary.ContainsKey(gameObject))
        { 
            Queue<GameObject> objectPool = new Queue<GameObject>();
            gameObjectPoolDictionary.Add(gameObject, objectPool);
        }

        for (int i = 0; i < numberToAdd; i++)
        {
            gameObjectObjectToSpawn = Object.Instantiate(gameObject);
            objectToSpawn.transform.parent = objectPool;

            gameObjectPoolDictionary[gameObject].Enqueue(gameObjectObjectToSpawn);
            objectToSpawn.gameObject.SetActive(false);
        }
    }

    public static void AddToPool<T>(T gameObject, int numberToAdd) where T : Component
    {
        if (objectPool == null)
            objectPool = new GameObject(POOLNAME).GetComponent<Transform>();

        if (!poolDictionary.ContainsKey(gameObject))
        {
            Queue<Component> objectPool = new Queue<Component>();
            poolDictionary.Add(gameObject, objectPool);
        }

        for (int i = 0; i < numberToAdd; i++)
        {
            objectToSpawn = Object.Instantiate(gameObject);
            objectToSpawn.transform.parent = objectPool;

            poolDictionary[gameObject].Enqueue(objectToSpawn);
            objectToSpawn.gameObject.SetActive(false);
        }
    }

    public static GameObject SpawnFromPool(GameObject gameObject, Vector3 position, Quaternion rotation)
    {
        if (objectPool == null)
            objectPool = new GameObject(POOLNAME).GetComponent<Transform>();

        if (!gameObjectPoolDictionary.ContainsKey(gameObject))
        {
            Queue<GameObject> objectPool = new Queue<GameObject>();
            gameObjectPoolDictionary.Add(gameObject, objectPool);
        }

        if (gameObjectPoolDictionary[gameObject].Count > 0 && !gameObjectPoolDictionary[gameObject].Peek().activeInHierarchy)
        {
            gameObjectObjectToSpawn = gameObjectPoolDictionary[gameObject].Dequeue();
            objectToSpawn.transform.parent = objectPool;

            gameObjectObjectToSpawn.gameObject.SetActive(true);
            gameObjectObjectToSpawn.transform.SetPositionAndRotation(position, rotation);

            gameObjectPoolDictionary[gameObject].Enqueue(gameObjectObjectToSpawn);

            return gameObjectObjectToSpawn;
        }
        else
        {
            gameObjectObjectToSpawn = Object.Instantiate(gameObject, position, rotation);
            objectToSpawn.transform.parent = objectPool;

            gameObjectPoolDictionary[gameObject].Enqueue(gameObjectObjectToSpawn);

            return gameObjectObjectToSpawn;
        }
    }
    
    public static T SpawnFromPool<T>(T gameObject, Vector3 position, Quaternion rotation) where T : Component
    {
        if(objectPool == null)
            objectPool = new GameObject(POOLNAME).GetComponent<Transform>();

        if(!poolDictionary.ContainsKey(gameObject))
        {
            Queue<Component> objectPool = new Queue<Component>();
            poolDictionary.Add(gameObject, objectPool);
        }
        
        if (poolDictionary[gameObject].Count > 0 && !poolDictionary[gameObject].Peek().gameObject.activeInHierarchy)
        {
            objectToSpawn = poolDictionary[gameObject].Dequeue();
            objectToSpawn.transform.parent = objectPool;

            objectToSpawn.gameObject.SetActive(true);
            objectToSpawn.transform.SetPositionAndRotation(position, rotation);

            poolDictionary[gameObject].Enqueue(objectToSpawn);

            return objectToSpawn as T;
        }
        else
        {
            objectToSpawn = Object.Instantiate(gameObject, position, rotation);
            objectToSpawn.transform.parent = objectPool;

            poolDictionary[gameObject].Enqueue(objectToSpawn);

            return objectToSpawn as T;
        }
    }
}