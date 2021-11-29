using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class FruitFactory : MonoBehaviour
{
    private struct Zone
    {
        public Vector3 min;
        public Vector3 max;
    }

    private Zone[] zones = new Zone[]{
        new Zone()
        {
            min = new Vector3(-7, 1, 8),
            max = new Vector3(6, 1, 55)
        },
        new Zone()
        {
            min = new Vector3(73, 1, 3),
            max = new Vector3(108, 1, 35)
        }
    };

    [SerializeField]
    private GameObject[] fruits;
    [SerializeField]
    private GameManager gameManager;
    [SerializeField]
    private HumanBehaviour humanBehaviour;


    private List<GameObject> createdFruits = new List<GameObject>();

    public void CreateFruits(int count)
    {
        for (int i = 0; i < count; i++)
        {
            var zoneIndex = Random.Range(0, zones.Length);

            var x = Random.Range(zones[zoneIndex].min.x, zones[zoneIndex].max.x);
            var z = Random.Range(zones[zoneIndex].min.z, zones[zoneIndex].max.z);

            var position = new Vector3(x, 1, z);

            var randomFruitIndex = Random.Range(0, fruits.Length);

            var fruit = Instantiate(fruits[randomFruitIndex], position, Quaternion.identity, transform);
            var behaviour = fruit.AddComponent<FruitBehaviour>();
            behaviour.gameManager = gameManager;
            behaviour.humanBehaviour = humanBehaviour;

            createdFruits.Add(fruit);
        }
    }

    public void RemoveAllFruits()
    {
        var fruitsForRemove = createdFruits.Where(f => f != null).ToArray();
        Debug.LogError(fruitsForRemove.Length);
        foreach (var fruit in fruitsForRemove)
        {
            Destroy(fruit);
        }
    }
}
