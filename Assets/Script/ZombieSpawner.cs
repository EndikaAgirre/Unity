using Meta.XR.MRUtilityKit;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieSpawner : MonoBehaviour
{
    [SerializeField] float spawnTimer = 1f;
    [SerializeField] GameObject prefabGhost;
    [SerializeField] float minEdgeDistance = 0.3f;
    [SerializeField] MRUKAnchor.SceneLabels spawnLabel;
    [SerializeField] float normalOffset;
    [SerializeField] int spawnTry = 1000;
    void Start()
    {
        Invoke("SpawnGhost", 1f);
    }

    // Update is called once per frame
    void Update()
    {
        if (!MRUK.Instance && !MRUK.Instance.IsInitialized)
            return;
    }
    private void SpawnGhost()
    {
        Invoke("SpawnGhost", 1f);
        MRUKRoom room = MRUK.Instance.GetCurrentRoom();

        int currentTry = 0;

        while (currentTry < spawnTry)
        {
            bool hasFoundPosition = room.GenerateRandomPositionOnSurface(MRUK.SurfaceType.VERTICAL, minEdgeDistance, LabelFilter.Included(spawnLabel), out Vector3 pos, out Vector3 norm);
            if (hasFoundPosition)
            {
                Vector3 randomPositionNormalOffset = pos + norm * normalOffset;
                randomPositionNormalOffset.y = 0f;

                Instantiate(prefabGhost, randomPositionNormalOffset, Quaternion.identity);

                return;
            }
            else
            {
                currentTry++;
            }
        }
    }
}
