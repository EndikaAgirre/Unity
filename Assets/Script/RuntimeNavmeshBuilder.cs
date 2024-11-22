using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Unity.AI.Navigation;
using Meta.XR.MRUtilityKit;

public class RuntimeNavmeshBuilder : MonoBehaviour
{
    private NavMeshSurface navmeshsurface;
    private void Start()
    {
        navmeshsurface = GetComponent<NavMeshSurface>();
        MRUK.Instance.RegisterSceneLoadedCallback(BuildNavMesh);
    }
    private void BuildNavMesh()
    {
        StartCoroutine(BuildNavMeshRoutine());
    }
    private IEnumerator BuildNavMeshRoutine()
    {
        yield return new WaitForEndOfFrame();
        navmeshsurface.BuildNavMesh();
    }
}
