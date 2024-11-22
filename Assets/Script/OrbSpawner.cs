using Meta.XR.MRUtilityKit;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbSpawner : MonoBehaviour
{
    [SerializeField] int numberOfOrbsToSpawn = 5;
    [SerializeField] GameObject orbPrefab;
    [SerializeField] float height;
    [SerializeField] List<GameObject> spawnedOrbs;
    [SerializeField] int maxNumberTry = 100;
    int currentTry = 0;
    private void Start()
    {
        MRUK.Instance.RegisterSceneLoadedCallback(SpawnOrbs);
    }
    private void SpawnOrbs()
    {
        for (int i = 0; i < numberOfOrbsToSpawn; i++)
        {
            Vector3 randomPosition = Vector3.zero;
            MRUKRoom room = MRUK.Instance.GetCurrentRoom();
            
            while(currentTry < maxNumberTry)
            {
                bool hasFound = room.GenerateRandomPositionOnSurface(MRUK.SurfaceType.FACING_UP, 1, LabelFilter.Included(MRUKAnchor.SceneLabels.FLOOR), out randomPosition, out Vector3 n);
                if (hasFound)
                    break;

                currentTry++;
            }

            randomPosition.y = height;
            GameObject spawned = Instantiate(orbPrefab, randomPosition, Quaternion.identity);
            spawnedOrbs.Add(spawned);
        }
    }
}
