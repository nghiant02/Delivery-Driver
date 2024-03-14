using System.Collections.Generic;
using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{
    public GameObject packagePrefab;
    public GameObject speedUpPrefab;
    public GameObject slowDownPrefab;
    public string roadTag = "Road"; // The tag used for your road objects
    public int numberOfEachType = 4; // Number of each object type to spawn
    List<GameObject> package;
    List<GameObject> speedUp;
    List<GameObject> slowDown;

    void Start()
    {
        package = new List<GameObject>();
        speedUp = new List<GameObject>();
        slowDown = new List<GameObject>();
        Debug.Log("Package Prefab Assigned: " + (packagePrefab != null));
        Debug.Log("Speed Up Prefab Assigned: " + (speedUpPrefab != null));
        Debug.Log("Slow Down Prefab Assigned: " + (slowDownPrefab != null));
        

        SpawnObjectsOnRoad();
    }

    void SpawnObjectsOnRoad()
    {
        GameObject[] roads = GameObject.FindGameObjectsWithTag(roadTag);
        Debug.Log("Roads found: " + roads.Length);

        RandomObj(roads);

        // Spawn Packages
        SpawnObjects(packagePrefab, roads, numberOfEachType);

        // Spawn SpeedUps
        SpawnObjects(speedUpPrefab, roads, numberOfEachType);

        // Spawn SlowDowns
        SpawnObjects(slowDownPrefab, roads, numberOfEachType);
    }

    void RandomObj(GameObject[] roads)
    {
        GameObject road = roads[Random.Range(0, roads.Length)]; // Pick a random road

        packagePrefab.transform.position = RandomPositionInBounds(road);
        speedUpPrefab.transform.position = RandomPositionInBounds(road);
        slowDownPrefab.transform.position = RandomPositionInBounds(road);

    }

    void SpawnObjects(GameObject prefab, GameObject[] roads, int quantity)
    {
        for (int i = 0; i < quantity; i++)
        {
            GameObject road = roads[Random.Range(0, roads.Length)]; // Pick a random road
            Vector2 spawnPosition = RandomPositionInBounds(road);

            GameObject newObj = Instantiate(prefab, spawnPosition, Quaternion.identity); // Create the new object
            Debug.Log("Spawned Object: " + newObj.name);

            // Assign a name based on the prefab type and index
            List<GameObject> targetList = prefab == packagePrefab ? package : prefab == speedUpPrefab ? speedUp : slowDown;
            newObj.name = prefab.name + " #" + (targetList.Count + 1);
            targetList.Add(newObj); // Add the new object to the list
        }
    }

    Vector2 RandomPositionInBounds(GameObject road)
    {
        Collider2D roadCollider = road.GetComponent<Collider2D>();
        if (roadCollider != null)
        {
            Bounds bounds = roadCollider.bounds;
            float x = Random.Range(bounds.min.x, bounds.max.x);
            float y = Random.Range(bounds.min.y, bounds.max.y);
            return new Vector2(x, y);
        }
        return Vector2.zero; // Return a default value if no collider is found
    }
}
