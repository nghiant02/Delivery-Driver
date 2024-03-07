using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{
    public GameObject packagePrefab;
    public GameObject speedUpPrefab;
    public GameObject slowDownPrefab;
    public string roadTag = "Road"; // The tag used for your road objects
    public int numberOfEachType = 4; // Number of each object type to spawn

    void Start()
    {
        SpawnObjectsOnRoad();
    }

    void SpawnObjectsOnRoad()
    {
        GameObject[] roads = GameObject.FindGameObjectsWithTag(roadTag);

        // Spawn Packages
        SpawnObjects(packagePrefab, roads, numberOfEachType);

        // Spawn SpeedUps
        SpawnObjects(speedUpPrefab, roads, numberOfEachType);

        // Spawn SlowDowns
        SpawnObjects(slowDownPrefab, roads, numberOfEachType);
    }

    void SpawnObjects(GameObject prefab, GameObject[] roads, int quantity)
    {
        for (int i = 0; i < quantity; i++)
        {
            GameObject road = roads[Random.Range(0, roads.Length)]; // Pick a random road
            Collider2D roadCollider = road.GetComponent<Collider2D>();
            if (roadCollider != null)
            {
                Vector2 spawnPosition = RandomPositionInBounds(roadCollider.bounds);
                Instantiate(prefab, spawnPosition, Quaternion.identity);
            }
        }
    }

    Vector2 RandomPositionInBounds(Bounds bounds)
    {
        float x = Random.Range(bounds.min.x, bounds.max.x);
        float y = Random.Range(bounds.min.y, bounds.max.y);
        return new Vector2(x, y);
    }
}
